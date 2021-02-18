using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Models
{
    public class PaymentState
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public virtual Payment Payment { get; set; }
        public States  State { get; set; }
    }

    public enum States
    {
        NotStarted,
        Pending,
        Processed,
        Failed
    }
}
