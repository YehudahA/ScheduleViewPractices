using System.Data.Entity;

namespace DatabaseBindingSample.Models
{
    public class SchedulingDbContext : DbContext
    {
        public SchedulingDbContext()
            : base("ScheduleConnectionString")
        {
        }

        public DbSet<AppointmentModel> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentModel>()
                .Ignore(appointment => appointment.TimeZone)
                .Ignore(appointment => appointment.Location)
                .Ignore(appointment => appointment.Url)
                .Ignore(appointment => appointment.UniqueId);
        }
    }
}
