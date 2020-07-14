using AutomobileEnterprise.Models.Buildings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Staffs
{
    public class WorkshopManager : Person
    {
        [Required]
        [DisplayName("Workshop Id")]
        public int WorkshopId { get; set; }
        public virtual Workshop Workshop { get; set; }

        public virtual ICollection <Master> Masters { get; set; }
    }
}