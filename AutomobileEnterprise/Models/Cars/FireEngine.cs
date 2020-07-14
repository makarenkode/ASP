using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Cars
{
    [Table("FireEngines")]
    public class FireEngine : Car
    {
        [Required]
        [DisplayName("Tank Volume")]
        public int TankVolume { get; set; }

        public FireEngine()
        {
            Cat = Category.Auxiliary;
        }
    }
}