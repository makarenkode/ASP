using System;
using AutomobileEnterprise.Models.Route;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomobileEnterprise.Models.Cars
{
    [Table("Busses")]
    public class Bus : Car
    {
        [Required]
        [DisplayName("Seats")]
        public int Seats { get; set; }

        [DisplayName("Route Id")]
        public int? RouteId { get; set; }
        public virtual Routes Route { get; set; }


        public Bus()
        {
            Cat = Category.Passanger;
        }


    }
}