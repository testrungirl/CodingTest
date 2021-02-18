using CodingTest.Data;
using CodingTest.Helpers;
using CodingTest.Models;
using CodingTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DatabaseContext _context;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;

        public TransactionService(DatabaseContext context, ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway)
        {
            _context = context;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
        }
        public GenericResponse<PaymentResponse> ProcessPayment(PaymentDto model) {

            try
            {

                States states = States.NotStarted;
                if (model.Amount < 20)
                {
                    states = _cheapPaymentGateway.HandleTransaction(model);



                }
                else if (model.Amount > 20 && model.Amount <= 500)
                {
                    if (_expensivePaymentGateway.HandleTransaction(model) == Models.States.Failed)
                    {
                        states = RetryHandler.Retry(() => _cheapPaymentGateway.HandleTransaction(model), 0);

                    }


                }
                else if (model.Amount > 500)
                {
                    states = RetryHandler.Retry(() => _expensivePaymentGateway.HandleTransaction(model), 3);

                }

                var transaction = new Payment
                {
                    Amount = model.Amount,
                    CardHolder = model.CardHolder,
                    CreditCardNumber = model.CreditCardNumber,
                    ExpirationDate = model.ExpirationDate,
                    SecurityCode = model.SecurityCode,

                };
                _context.Add(transaction);
                _context.SaveChanges();

                var transactionState = new PaymentState
                {
                    Payment = transaction,
                    State = states,
                };
                _context.Add(transactionState);
                _context.SaveChanges();

                if (states == States.Failed)
                    return new GenericResponse<PaymentResponse> { Data = new PaymentResponse { Id = transaction.Id, States = states.ToString() }, Message = "Payment failed", Success = false };

                return new GenericResponse<PaymentResponse> { Data = new PaymentResponse { Id = transaction.Id, States = states.ToString() }, Message = "Payment Processed", Success = true };
            }
            catch(Exception e)
            {
                return new GenericResponse<PaymentResponse> { Data = null, Message = e.Message, Success = false };

            }

        }
    }
}
