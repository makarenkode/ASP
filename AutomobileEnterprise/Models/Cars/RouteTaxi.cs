using AutomobileEnterprise.Models.Route;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Cars
{
    [Table("RouteTaxies")]
    public class RouteTaxi : Car
    {
        [Required]
        [DisplayName("Seats")]
        public int Seats { get; set; }

        [DisplayName("Route Id")]
        public int? RouteId { get; set; }
        public virtual Routes Route { get; set; }

        public RouteTaxi()
        {
            Cat = Category.Passanger;
        }

    }
}