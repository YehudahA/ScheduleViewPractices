namespace DatabaseBindingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments");
            DropForeignKey("dbo.RecurrenceRules", "AppointmentId", "dbo.Appointments");
            DropPrimaryKey("dbo.Appointments");
            AlterColumn("dbo.Appointments", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Appointments", "Id");
            AddForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecurrenceRules", "AppointmentId", "dbo.Appointments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecurrenceRules", "AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments");
            DropPrimaryKey("dbo.Appointments");
            AlterColumn("dbo.Appointments", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Appointments", "Id");
            AddForeignKey("dbo.RecurrenceRules", "AppointmentId", "dbo.Appointments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ResourceModelAppointmentModels", "AppointmentModel_Id", "dbo.Appointments", "Id", cascadeDelete: true);
        }
    }
}
