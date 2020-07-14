using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileEnterprise.Models.Cars
{
    public class Passanger : Car
    {
        public Passanger()
        {
            Cat = Category.Passanger;
        }
    }
}