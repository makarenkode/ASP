using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Cars
{
    public enum Category
    {
        Passanger =1,
        Freight = 2,
        Auxiliary = 3,
    }
    public class Car
    {
        [DisplayName("Car Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Model")]
        public string Model { get; set; }

        [Required]
        [DisplayName("Mileage")]
        public int Mileage { get; set; }

        [Required]
        [DisplayName("Purchase Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [DisplayName("Write off Date")]
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WriteOffDate { get; set; }

        [Required]
        [DisplayName("Category")]
        public Category Cat { get; set; }



    }
}