using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Parts
{
    public class Part
    {
        [DisplayName("Id")]
        public int PartId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Cost")]
        public int Cost { get; set; }
    }
}