namespace AutomobileEnterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorksAndDepartures2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departures",
                c => new
                    {
                        DepartureId = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DepartureId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        PartId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Cost = c.Int(nullable: false),
                        Work_WorkId = c.Int(),
                    })
                .PrimaryKey(t => t.PartId)
                .ForeignKey("dbo.Works", t => t.Work_WorkId)
                .Index(t => t.Work_WorkId);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        WorkId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ServiceWorkerId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceWorkers", t => t.ServiceWorkerId, cascadeDelete: true)
                .Index(t => t.ServiceWorkerId)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "ServiceWorkerId", "dbo.ServiceWorkers");
            DropForeignKey("dbo.Parts", "Work_WorkId", "dbo.Works");
            DropForeignKey("dbo.Works", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Departures", "CarId", "dbo.Cars");
            DropIndex("dbo.Works", new[] { "CarId" });
            DropIndex("dbo.Works", new[] { "ServiceWorkerId" });
            DropIndex("dbo.Parts", new[] { "Work_WorkId" });
            DropIndex("dbo.Departures", new[] { "CarId" });
            DropTable("dbo.Works");
            DropTable("dbo.Parts");
            DropTable("dbo.Departures");
        }
    }
}
