using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Cars
{   [Table("Tractors")]
    public class Tractor : Car
    {
        [Required]
        [DisplayName("Carring Capasity")]
        public int CarringCapacity { get; set; }

        public Tractor()
        {
            Cat = Category.Freight;
        }
    }
}