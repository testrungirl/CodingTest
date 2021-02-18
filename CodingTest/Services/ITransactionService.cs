using CodingTest.Helpers;
using CodingTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Services
{
   public interface ITransactionService
    {
        public GenericResponse<PaymentResponse> ProcessPayment(PaymentDto model);
    }
}
