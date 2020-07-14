using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Buildings
{
    public class Building
    {
        [DisplayName("Building Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Location")]
        public string Location { get; set; }
    }
}