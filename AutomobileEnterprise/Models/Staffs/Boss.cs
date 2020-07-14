using AutomobileEnterprise.Models.Buildings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Staffs
{
    public class Boss : Person
    {
        //[Required]
        //[DisplayName("")]
        public virtual ICollection<WorkshopManager> WorkshopManagers { get; set; }
        public virtual ICollection<Garage> Garages { get; set; }

    }
}