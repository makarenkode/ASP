using AutomobileEnterprise.Models.Cars;
using AutomobileEnterprise.Models.Parts;
using AutomobileEnterprise.Models.Staffs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Works
{
    public class Work
    {
        [DisplayName("Id")]
        public int WorkId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("ServiceWorkerId")]
        public int ServiceWorkerId { get; set; }
        public virtual ServiceWorker ServiceWorker { get; set; }

        [Required]
        [DisplayName("Car Id")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
   
        [Required]
        [DisplayName("Cost")]
        public int Cost { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}