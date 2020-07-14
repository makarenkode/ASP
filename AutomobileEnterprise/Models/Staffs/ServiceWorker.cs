using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Staffs
{   public enum Specializations
    {
        Technician = 1,
        Welder = 2,
        Locksmith = 3,
        Collector = 4
    }
    public class ServiceWorker : Person
    {
        [Required]
        [DisplayName("Specialization")]
        public Specializations Specialization { get; set; }
    }
}