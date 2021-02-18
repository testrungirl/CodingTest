using CodingTest.Services;
using CodingTest.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodingTest.Test
{
    [TestClass]
    public class TestPayment
    {

        public ITransactionService _transactionservice { get; }
        public IExpensivePaymentGateway _expensivePaymentGateway { get; }
        public ICheapPaymentGateway _cheapPaymentGateway { get; }

        public TestPayment( ITransactionService transactionService, IExpensivePaymentGateway expensivePaymentGateway, ICheapPaymentGateway cheapPaymentGateway)
        {
            _transactionservice = transactionService;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
        }
        
        [TestMethod]
        public void IsPaymentProcessing()
        {
            var paymentdto = new PaymentDto
            {
                Amount = 500,
                CardHolder = "Test",
                CreditCardNumber = "5500 0000 0000 0004",
                ExpirationDate = DateTime.Now.AddYears(1),
                SecurityCode = "454"

            };
            var result = _transactionservice.ProcessPayment(paymentdto);

            Assert.IsNotNull(result, $"{result} should not be null");
        }

        [TestMethod]
        public void TestCheapGateway()
        {
            var paymentdto = new PaymentDto
            {
                Amount = 500,
                CardHolder = "Test",
                CreditCardNumber = "5500 0000 0000 0004",
                ExpirationDate = DateTime.Now.AddYears(1),
                SecurityCode = "454"

            };
            var result = 
                _cheapPaymentGateway.HandleTransaction(paymentdto);

            Assert.IsNotNull(result, $"{result} should not be null");
        } 
        [TestMethod]
        public void TestExpensiveGateway()
        {
            var paymentdto = new PaymentDto
            {
                Amount = 500,
                CardHolder = "Test",
                CreditCardNumber = "5500 0000 0000 0004",
                ExpirationDate = DateTime.Now.AddYears(1),
                SecurityCode = "454"

            };
            var result = 
                _expensivePaymentGateway.HandleTransaction(paymentdto);

            Assert.IsNotNull(result, $"{result} should not be null");
        }
    }
}
