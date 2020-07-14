using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models
{
    public class Person
    {
        [DisplayName("Person ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Second name")]
        public string SecondName { get; set; }

        public string FullName => FirstName + " " + SecondName;
    }
}