namespace DatabaseBindingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recurrence : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments");
            DropPrimaryKey("dbo.Appointments");
            CreateTable(
                "dbo.RecurrenceRules",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false),
                        PatternString = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId, cascadeDelete: true)
                .Index(t => t.AppointmentId);
            
            CreateTable(
                "dbo.ExceptionOccurrences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionDate = c.DateTime(nullable: false),
                        ReccurrenceRuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecurrenceRules", t => t.ReccurrenceRuleId, cascadeDelete: true)
                .Index(t => t.ReccurrenceRuleId);
            
            CreateTable(
                "dbo.ExceptionAppointments",
                c => new
                    {
                        ExceptionOccurrenceId = c.Int(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        IsAllDayEvent = c.Boolean(nullable: false),
                        Importance = c.Int(nullable: false),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ExceptionOccurrenceId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.ExceptionOccurrences", t => t.ExceptionOccurrenceId, cascadeDelete: true)
                .Index(t => t.ExceptionOccurrenceId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ResourceModelExceptionAppointmentModels",
                c => new
                    {
                        ResourceModel_Id = c.Int(nullable: false),
                        ExceptionAppointmentModel_ExceptionOccurrenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResourceModel_Id, t.ExceptionAppointmentModel_ExceptionOccurrenceId })
                .ForeignKey("dbo.Resources", t => t.ResourceModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.ExceptionAppointments", t => t.ExceptionAppointmentModel_ExceptionOccurrenceId, cascadeDelete: true)
                .Index(t => t.ResourceModel_Id)
                .Index(t => t.ExceptionAppointmentModel_ExceptionOccurrenceId);
            
            AlterColumn("dbo.Appointments", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Appointments", "Id");
            AddForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments");
            DropForeignKey("dbo.RecurrenceRules", "AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.ExceptionOccurrences", "ReccurrenceRuleId", "dbo.RecurrenceRules");
            DropForeignKey("dbo.ResourceModelExceptionAppointmentModels", "ExceptionAppointmentModel_ExceptionOccurrenceId", "dbo.ExceptionAppointments");
            DropForeignKey("dbo.ResourceModelExceptionAppointmentModels", "ResourceModel_Id", "dbo.Resources");
            DropForeignKey("dbo.ExceptionAppointments", "ExceptionOccurrenceId", "dbo.ExceptionOccurrences");
            DropForeignKey("dbo.ExceptionAppointments", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ResourceModelExceptionAppointmentModels", new[] { "ExceptionAppointmentModel_ExceptionOccurrenceId" });
            DropIndex("dbo.ResourceModelExceptionAppointmentModels", new[] { "ResourceModel_Id" });
            DropIndex("dbo.ExceptionAppointments", new[] { "CategoryId" });
            DropIndex("dbo.ExceptionAppointments", new[] { "ExceptionOccurrenceId" });
            DropIndex("dbo.ExceptionOccurrences", new[] { "ReccurrenceRuleId" });
            DropIndex("dbo.RecurrenceRules", new[] { "AppointmentId" });
            DropPrimaryKey("dbo.Appointments");
            AlterColumn("dbo.Appointments", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.ResourceModelExceptionAppointmentModels");
            DropTable("dbo.ExceptionAppointments");
            DropTable("dbo.ExceptionOccurrences");
            DropTable("dbo.RecurrenceRules");
            AddPrimaryKey("dbo.Appointments", "Id");
            AddForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments", "Id", cascadeDelete: true);
        }
    }
}
