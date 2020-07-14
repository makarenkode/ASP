using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Staffs
{
    public class Brigade
    {
        [Required]
        [DisplayName("Brigade Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Master")]
        public int MasterId { get; set; }
        public virtual Master Master { get; set; }

        //[Required]
        //[DisplayName("Brigadier Id")]
        //public int BrigadierId { get; set; }
        //public virtual Brigadier Brigadier { get; set; }

        public virtual ICollection<ServiceWorker> ServiceWorkers { get; set; }
    }
}