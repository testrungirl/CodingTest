using CodingTest.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Models
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [PresentOrFutureDate]
        public DateTime ExpirationDate { get; set; }

        //[RegularExpression("/^(.{0}|.{3,})$/")]
        public string SecurityCode { get; set; }
        [Required]
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "Amount must be a positive value")]

        public decimal Amount { get; set; }
      
    }
}
