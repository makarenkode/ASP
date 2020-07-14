using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Staffs
{
    public class Brigadier : Person
    {
        [Required]
        [DisplayName("Brigade Id ")]
        public int BrigadeId { get; set; }
        public virtual Brigade Brigade { get; set; }
    }
}