using AutomobileEnterprise.Models.Cars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Route
{
    public class Departure
    {
        [DisplayName("Id")]
        public int DepartureId { get; set; }
        
        [Required]
        [DisplayName("Car Id")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime Date { get; set; }
   
    }
}