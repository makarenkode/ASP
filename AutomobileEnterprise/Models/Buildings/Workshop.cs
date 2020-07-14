using AutomobileEnterprise.Models.Cars;
using AutomobileEnterprise.Models.Staffs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Buildings
{
    public class Workshop : Building
    { 
    //{   [Required]
    //    [DisplayName("Workshop Manager Id")]
    //    public int WorkshopManagerId { get; set; }
    //    public virtual WorkshopManager WorkshopManager { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}