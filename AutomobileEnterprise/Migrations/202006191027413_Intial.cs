namespace AutomobileEnterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bosses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Garages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BossId = c.Int(nullable: false),
                        Location = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bosses", t => t.BossId, cascadeDelete: true)
                .Index(t => t.BossId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false),
                        Mileage = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        WriteOffDate = c.DateTime(nullable: false),
                        Cat = c.Int(nullable: false),
                        Discriminator = c.String(maxLength: 128),
                        Garage_Id = c.Int(),
                        Workshop_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Garages", t => t.Garage_Id)
                .ForeignKey("dbo.Workshops", t => t.Workshop_Id)
                .Index(t => t.Garage_Id)
                .Index(t => t.Workshop_Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Distance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkshopManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkshopId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        Boss_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workshops", t => t.WorkshopId, cascadeDelete: true)
                .ForeignKey("dbo.Bosses", t => t.Boss_Id)
                .Index(t => t.WorkshopId)
                .Index(t => t.Boss_Id);
            
            CreateTable(
                "dbo.Masters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkshopManagerId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkshopManagers", t => t.WorkshopManagerId, cascadeDelete: true)
                .Index(t => t.WorkshopManagerId);
            
            CreateTable(
                "dbo.Brigades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Masters", t => t.MasterId, cascadeDelete: true)
                .Index(t => t.MasterId);
            
            CreateTable(
                "dbo.ServiceWorkers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Specialization = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        Brigade_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brigades", t => t.Brigade_Id)
                .Index(t => t.Brigade_Id);
            
            CreateTable(
                "dbo.Workshops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brigadiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrigadeId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brigades", t => t.BrigadeId, cascadeDelete: true)
                .Index(t => t.BrigadeId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Busses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Seats = c.Int(nullable: false),
                        RouteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId)
                .Index(t => t.Id)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.FireEngines",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TankVolume = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.RouteTaxis",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Seats = c.Int(nullable: false),
                        RouteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId)
                .Index(t => t.Id)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Taxies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Seats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Tractors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CarringCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Trucks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CarringCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trucks", "Id", "dbo.Cars");
            DropForeignKey("dbo.Tractors", "Id", "dbo.Cars");
            DropForeignKey("dbo.Taxies", "Id", "dbo.Cars");
            DropForeignKey("dbo.RouteTaxis", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.RouteTaxis", "Id", "dbo.Cars");
            DropForeignKey("dbo.FireEngines", "Id", "dbo.Cars");
            DropForeignKey("dbo.Busses", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Busses", "Id", "dbo.Cars");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Drivers", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Brigadiers", "BrigadeId", "dbo.Brigades");
            DropForeignKey("dbo.WorkshopManagers", "Boss_Id", "dbo.Bosses");
            DropForeignKey("dbo.WorkshopManagers", "WorkshopId", "dbo.Workshops");
            DropForeignKey("dbo.Cars", "Workshop_Id", "dbo.Workshops");
            DropForeignKey("dbo.Masters", "WorkshopManagerId", "dbo.WorkshopManagers");
            DropForeignKey("dbo.ServiceWorkers", "Brigade_Id", "dbo.Brigades");
            DropForeignKey("dbo.Brigades", "MasterId", "dbo.Masters");
            DropForeignKey("dbo.Cars", "Garage_Id", "dbo.Garages");
            DropForeignKey("dbo.Garages", "BossId", "dbo.Bosses");
            DropIndex("dbo.Trucks", new[] { "Id" });
            DropIndex("dbo.Tractors", new[] { "Id" });
            DropIndex("dbo.Taxies", new[] { "Id" });
            DropIndex("dbo.RouteTaxis", new[] { "RouteId" });
            DropIndex("dbo.RouteTaxis", new[] { "Id" });
            DropIndex("dbo.FireEngines", new[] { "Id" });
            DropIndex("dbo.Busses", new[] { "RouteId" });
            DropIndex("dbo.Busses", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Drivers", new[] { "CarId" });
            DropIndex("dbo.Brigadiers", new[] { "BrigadeId" });
            DropIndex("dbo.ServiceWorkers", new[] { "Brigade_Id" });
            DropIndex("dbo.Brigades", new[] { "MasterId" });
            DropIndex("dbo.Masters", new[] { "WorkshopManagerId" });
            DropIndex("dbo.WorkshopManagers", new[] { "Boss_Id" });
            DropIndex("dbo.WorkshopManagers", new[] { "WorkshopId" });
            DropIndex("dbo.Cars", new[] { "Workshop_Id" });
            DropIndex("dbo.Cars", new[] { "Garage_Id" });
            DropIndex("dbo.Garages", new[] { "BossId" });
            DropTable("dbo.Trucks");
            DropTable("dbo.Tractors");
            DropTable("dbo.Taxies");
            DropTable("dbo.RouteTaxis");
            DropTable("dbo.FireEngines");
            DropTable("dbo.Busses");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Drivers");
            DropTable("dbo.Brigadiers");
            DropTable("dbo.Workshops");
            DropTable("dbo.ServiceWorkers");
            DropTable("dbo.Brigades");
            DropTable("dbo.Masters");
            DropTable("dbo.WorkshopManagers");
            DropTable("dbo.Routes");
            DropTable("dbo.Cars");
            DropTable("dbo.Garages");
            DropTable("dbo.Bosses");
        }
    }
}
