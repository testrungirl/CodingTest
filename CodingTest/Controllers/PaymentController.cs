using CodingTest.Helpers;
using CodingTest.Services;
using CodingTest.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public ITransactionService _transactionService { get; }

     
        public PaymentController(ITransactionService transactionService)
        {
            _transactionService = transactionService;

        }

        [HttpPost]
        public ActionResult<GenericResponse<PaymentResponse>> ProcessPayment([FromBody] PaymentDto payment)
        {
            // Model is Automatically validated using ActionValidationFilter Class in Helpers
            try
            {

                if (ModelState.IsValid)
                {
                    var res = _transactionService.ProcessPayment(payment);
                    if (res.Success == true)
                    {
                        return Ok(res);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, res);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new GenericResponse<PaymentResponse> {Data = null, Message = "The Request is Invalid", Success = false });

                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new GenericResponse<PaymentResponse> { Data = null, Message = e.Message, Success = false });

            }
        }

    }
}
