namespace testingDriverAppWebapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        EntityId = c.Guid(nullable: false),
                        Name = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.EntityId);
            
            CreateTable(
                "dbo.JobEntities",
                c => new
                    {
                        JobEntityId = c.Guid(nullable: false),
                        JobId = c.Guid(nullable: false),
                        EntityId = c.Guid(nullable: false),
                        MethodToNotify = c.String(),
                        NotifyTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobEntityId)
                .ForeignKey("dbo.Entities", t => t.EntityId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Guid(nullable: false),
                        DriverId = c.Guid(nullable: false),
                        VehicleId = c.String(),
                        PickUpTime = c.DateTime(nullable: false),
                        PickUpLocation = c.String(),
                        DropOffTime = c.DateTime(nullable: false),
                        DropOffLocation = c.String(),
                        Comments = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobId);
            
            CreateTable(
                "dbo.JobHorses",
                c => new
                    {
                        JobHorseId = c.Guid(nullable: false),
                        JobId = c.Guid(nullable: false),
                        HorseId = c.Guid(nullable: false),
                        Space = c.Int(nullable: false),
                        Notes = c.String(),
                        HasCompanionHorse = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JobHorseId)
                .ForeignKey("dbo.Horses", t => t.HorseId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.HorseId);
            
            CreateTable(
                "dbo.Horses",
                c => new
                    {
                        HorseId = c.Guid(nullable: false),
                        Name = c.String(),
                        Brand = c.String(),
                        Microchip = c.String(),
                        Colour = c.String(),
                        Sex = c.String(),
                        Hand = c.String(),
                        Another = c.String(),
                    })
                .PrimaryKey(t => t.HorseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobHorses", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.JobHorses", "HorseId", "dbo.Horses");
            DropForeignKey("dbo.JobEntities", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.JobEntities", "EntityId", "dbo.Entities");
            DropIndex("dbo.JobHorses", new[] { "HorseId" });
            DropIndex("dbo.JobHorses", new[] { "JobId" });
            DropIndex("dbo.JobEntities", new[] { "EntityId" });
            DropIndex("dbo.JobEntities", new[] { "JobId" });
            DropTable("dbo.Horses");
            DropTable("dbo.JobHorses");
            DropTable("dbo.Jobs");
            DropTable("dbo.JobEntities");
            DropTable("dbo.Entities");
        }
    }
}
