using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Cars
{
    [Table("Taxies")]
    public class Taxi : Car
    {
        [Required]
        [DisplayName("Seats")]
        public int Seats { get; set; }

        public Taxi()
        {
            Cat = Category.Passanger;
        }
    }
}