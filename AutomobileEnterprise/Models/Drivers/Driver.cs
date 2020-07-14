using AutomobileEnterprise.Models.Cars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Drivers
{
    public class Driver : Person
    {
        [Required]
        [DisplayName("Car Id")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}