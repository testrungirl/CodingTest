using CodingTest.Models;
using CodingTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Services
{
    public interface IExpensivePaymentGateway
    {
        public States HandleTransaction(PaymentDto model);

    }
}
