using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Cars
{
    [Table("Trucks")]
    public class Truck : Car
    {
        [Required]
        [DisplayName("Carrying Capacity")]
        public int CarringCapacity { get; set; }

        public Truck()
        {
            Cat = Category.Freight;
        }
    }
}