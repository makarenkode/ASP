using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Staffs
{
    public class Master : Person 
    {
        [Required]
        [DisplayName("Workshop Manager Id")]
        public int WorkshopManagerId { get; set; }
        public virtual WorkshopManager WorkshopManager { get; set; }

        public virtual ICollection<Brigade> Brigades { get; set; }
    }
}