using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutomobileEnterprise.Models.Buildings;
using AutomobileEnterprise.Models.Cars;
using AutomobileEnterprise.Models.Drivers;
using AutomobileEnterprise.Models.Parts;
using AutomobileEnterprise.Models.Route;
using AutomobileEnterprise.Models.Staffs;
using AutomobileEnterprise.Models.Works;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AutomobileEnterprise.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public string GetRoles
        {
            get
            {
                var userRoles = db.Roles
                    .Where(r => r.Users
                        .Any(ur => ur.UserId == Id))
                    .Select(r => r.Name)
                    .ToList();
                return string.Join(" | ", userRoles.ToArray());
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }


        public DbSet<Routes> Routes { get; set; }
        public DbSet<ServiceWorker> ServiceWorkers { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<WorkshopManager> WorkshopManagers { get; set; }
        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<Brigadier> Brigadiers { get; set; }
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Car> Cars { get; set; }
        //public DbSet<Passanger> Passangers { get; set; }
        //public DbSet<Bus> Buses { get; set; }
        //public DbSet<FireEngine> FireEngines { get; set; }
        //public DbSet<RouteTaxi> RouteTaxis { get; set; }
        //public DbSet<Taxi> Taxis { get; set; }
        //public DbSet<Tractor> Tractors { get; set; }
        //public DbSet<Truck> Trucks { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        //public DbSet<Routes> Routes { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Work> Works { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bus>().ToTable("Busses");
            modelBuilder.Entity<FireEngine>().ToTable("FireEngines");
            modelBuilder.Entity<RouteTaxi>().ToTable("RouteTaxis");
            modelBuilder.Entity<Taxi>().ToTable("Taxies");
            modelBuilder.Entity<Tractor>().ToTable("Tractors");
            modelBuilder.Entity<Truck>().ToTable("Trucks");
        }

        public System.Data.Entity.DbSet<AutomobileEnterprise.Models.Cars.Bus> Buses { get; set; }

        public System.Data.Entity.DbSet<AutomobileEnterprise.Models.Cars.Truck> Trucks { get; set; }

        public System.Data.Entity.DbSet<AutomobileEnterprise.Models.Cars.FireEngine> FireEngines { get; set; }

        public System.Data.Entity.DbSet<AutomobileEnterprise.Models.Cars.RouteTaxi> RouteTaxis { get; set; }

        public System.Data.Entity.DbSet<AutomobileEnterprise.Models.Cars.Taxi> Taxis { get; set; }

        public System.Data.Entity.DbSet<AutomobileEnterprise.Models.Cars.Tractor> Tractors { get; set; }
    }
}