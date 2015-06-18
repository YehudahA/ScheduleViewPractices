namespace DatabaseBindingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeMarkerMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "TimeMarkerString", c => c.String());
            AddColumn("dbo.ExceptionAppointments", "TimeMarkerString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExceptionAppointments", "TimeMarkerString");
            DropColumn("dbo.Appointments", "TimeMarkerString");
        }
    }
}
