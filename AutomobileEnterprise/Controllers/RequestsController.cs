using AutomobileEnterprise.Models;
using AutomobileEnterprise.Models.Cars;
using AutomobileEnterprise.Models.Drivers;
using AutomobileEnterprise.Models.Route;
using AutomobileEnterprise.Models.Staffs;
using AutomobileEnterprise.Models.Works;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileEnterprise.Controllers
{
  
    public class RequestsController : Controller
    {
        private int PageSize = 4;
        private ApplicationDbContext db = new ApplicationDbContext();

        public class Req2vm {
            public IPagedList<Driver> Drivers { get; set; }
            public SelectList Cars { get; set; }
            public int? CarId { get; set; }

        }

        public ActionResult Request2([Bind(Include = "CarId")] Req2vm vm, int? pageNum, int? carId)
        {
            IQueryable<Driver> drivers = db.Drivers;

            if (vm == null) {
                vm = new Req2vm();
            }

            vm.CarId = carId;
            Console.WriteLine(carId);

            if(vm.CarId != null && vm.CarId != 0)
            {
                drivers = drivers.Where(d => d.CarId == vm.CarId);
            }

            List<Car> cars = db.Cars.ToList();
            cars.Insert(0, new Car() { Id = 0, Model = "All", PurchaseDate = new DateTime(2000, 11, 12), WriteOffDate = new DateTime(2000, 12,12), Mileage = 0, Cat = Category.Passanger });
            int pageQuantity = pageNum ?? 1;
            vm.Drivers = drivers.OrderBy(d => d.SecondName).ToPagedList(pageQuantity, PageSize);
            vm.Cars = new SelectList(cars, "Id", "Model");
            return View(vm);


        }
        public class Req4vm
        {
            public IPagedList<Bus> Buses { get; set; }
            public SelectList Routes { get; set; }
            public int? RouteId { get; set; }
        }

        public ActionResult Request4([Bind(Include = "RouteId")] Req4vm vm, int? pageNum, int? routeId)
        {
            IQueryable<Bus> buses = db.Buses;
            if (vm == null)
            {
                vm = new Req4vm();

            }

            vm.RouteId = routeId;
            List<Routes> routes  = db.Routes.ToList();
            routes.Insert(0, new Routes() { Id = 0, Name = "All" , Distance = 0 });
            int pageQuantity = pageNum ?? 1;
            vm.Buses = buses.OrderBy(d => d.Model).ToPagedList(pageQuantity, PageSize);
            vm.Routes = new SelectList(routes, "Id", "Name");

            return View(vm); 
        }
        public class Req10vm
        {
            public IPagedList<Departure> Departures { get; set; }
            public SelectList Cars { get; set; }
            public int? CarId { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? Begin { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? End { get; set; }

        }

        public ActionResult Request10([Bind(Include = "CarId")] Req10vm vm, int? pageNum, int? carId, DateTime? begin, DateTime? end)
        {
            IQueryable<Departure> departures = db.Departures;
            if (vm == null)
            {
                vm = new Req10vm();

            }
            if (begin == null)
            {
                vm.Begin = new DateTime(1999, 1, 1);
            }
            else
            {
                vm.Begin = begin;
            }
            if (end == null)
            {
                vm.End = new DateTime(2020, 6, 20);
            }
            else
            {
                vm.End = end;
            }

            vm.CarId = carId;
            if (vm.CarId != null && vm.CarId != 0)
            {
                departures = departures.Where(d => d.CarId == vm.CarId);
            }
            departures = departures.Where(d => DateTime.Compare(d.Date, (DateTime)vm.End) < 0 && DateTime.Compare(d.Date, (DateTime)vm.Begin) > 0);

            List<Car> cars = db.Cars.Where(c => c.Cat == Category.Freight).ToList();
            cars.Insert(0, new Car() { Id = 0, Model = "All", PurchaseDate = new DateTime(2000, 11, 12), WriteOffDate = new DateTime(2000, 12, 12), Mileage = 0, Cat = Category.Passanger });
            int pageQuantity = pageNum ?? 1;
            vm.Departures = departures.OrderBy(d => d.CarId).ToPagedList(pageQuantity, PageSize);
            vm.Cars = new SelectList(cars, "Id", "Model");

            return View(vm);

        }
        public class Req12vm
        {
            public IPagedList<Car> Cars { get; set; }
            public bool check { get; set; }
            public DateTime? Begin { get; set; }
            public DateTime? End { get; set; }
        }

        public ActionResult Request12([Bind(Include = "CarId")] Req12vm vm, int? pageNum, bool? check, DateTime? begin, DateTime? end)
        {
            IQueryable<Car> cars = db.Cars;

            if (vm == null)
            {
                vm = new Req12vm();

    
            }
            if(check == null)
            {
                check = false;
            }
            vm.check = (bool)check;
            if (begin == null)
            {
                vm.Begin = new DateTime(1999, 1, 1);
            }
            else {
                vm.Begin = begin;
            }
            if (end == null)
            {
                vm.End = new DateTime(2020, 6, 20);
            }
            else
            {
                vm.End= end;
            }

            if (check == false)
            {
                cars = cars.Where(c => DateTime.Compare(c.PurchaseDate, (DateTime)vm.End)< 0 && DateTime.Compare(c.PurchaseDate, (DateTime)vm.Begin) > 0);
            }
            else
            {
                cars = cars.Where(c => DateTime.Compare(c.WriteOffDate, (DateTime)vm.End) < 0 && DateTime.Compare(c.WriteOffDate, (DateTime)vm.Begin) > 0);
            }
           
            int pageQuantity = pageNum ?? 1;
            vm.Cars = cars.OrderBy(c => c.Id).ToPagedList(pageQuantity, PageSize);
            return View(vm);
        }

        public class Req13vm
        {
            public IPagedList<Master> masters { get; set; }
            public SelectList heads { get; set; }
            public int? HeadId { get; set; }
        }

        public ActionResult Request13([Bind(Include = "WorkerId")] Req13vm vm, int? pageNum, int? headId) { 
      
            IQueryable<Master> masters = db.Masters;
            if (vm == null)
            {
                vm = new Req13vm();
            }

            vm.HeadId = headId;
            if (vm.HeadId != null && vm.HeadId != 0)
            {
                masters = masters.Where(d => d.WorkshopManagerId == vm.HeadId);
            }

            List<WorkshopManager> workshopManagers = db.WorkshopManagers.ToList();
            int pageQuantity = pageNum ?? 1;
            vm.masters = masters.OrderBy(d => d.SecondName).ToPagedList(pageQuantity, PageSize);
            vm.heads = new SelectList(workshopManagers, "Id", "FirstName");

            return View(vm);
        }

        public class Req14vm
        {
            public IPagedList<Work> works { get; set; }
            public SelectList Workers { get; set; }
            public int? WorkerId { get; set; }

        }

        public ActionResult Request14([Bind(Include = "WorkerId")] Req14vm vm, int? pageNum, int?workerId) {
            
            IQueryable<Work> works = db.Works;
            if (vm == null)
            {
                vm = new Req14vm();
            }

            vm.WorkerId = workerId;
            //Console.WriteLine(carId);

            if (vm.WorkerId != null && vm.WorkerId != 0)
            {
                works = works.Where(d => d.ServiceWorkerId == vm.WorkerId);
            }

            List<ServiceWorker> serviceWorkers = db.ServiceWorkers.ToList();
            int pageQuantity = pageNum ?? 1;
            vm.works = works.OrderBy(d => d.Name).ToPagedList(pageQuantity, PageSize);
            vm.Workers = new SelectList(serviceWorkers, "Id", "FirstName");
            return View(vm);
        }



    }
}