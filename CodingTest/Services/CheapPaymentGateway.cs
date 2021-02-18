using CodingTest.Helpers;
using CodingTest.Models;
using CodingTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Services
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        // Simulated Response using random behaviour

        public States HandleTransaction(PaymentDto model)
        {
            var state = RetryHandler.GetRandomEnum<States>();
            return state;
        }
    }
}
