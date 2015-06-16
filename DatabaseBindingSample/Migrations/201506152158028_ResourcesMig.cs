namespace DatabaseBindingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourcesMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceName = c.String(),
                        ResourceTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResourceTypes", t => t.ResourceTypeId, cascadeDelete: true)
                .Index(t => t.ResourceTypeId);
            
            CreateTable(
                "dbo.ResourceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AllowMultipleSelection = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResourceModelAppointmentModels",
                c => new
                    {
                        ResourceModel_Id = c.Int(nullable: false),
                        AppointmentModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResourceModel_Id, t.AppointmentModel_Id })
                .ForeignKey("dbo.Resources", t => t.ResourceModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.AppointmentModel_Id, cascadeDelete: true)
                .Index(t => t.ResourceModel_Id)
                .Index(t => t.AppointmentModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resources", "ResourceTypeId", "dbo.ResourceTypes");
            DropForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments");
            DropForeignKey("dbo.ResourceModelAppointmentModels", "ResourceModel_Id", "dbo.Resources");
            DropIndex("dbo.ResourceModelAppointmentModels", new[] { "AppointmentModel_Id" });
            DropIndex("dbo.ResourceModelAppointmentModels", new[] { "ResourceModel_Id" });
            DropIndex("dbo.Resources", new[] { "ResourceTypeId" });
            DropTable("dbo.ResourceModelAppointmentModels");
            DropTable("dbo.ResourceTypes");
            DropTable("dbo.Resources");
        }
    }
}
