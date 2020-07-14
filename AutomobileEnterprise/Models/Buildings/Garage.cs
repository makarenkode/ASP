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
    public class Garage : Building
    {
        [Required]
        [DisplayName("Boss Id")]
        public int BossId { get; set; }
        public virtual Boss Boss { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}