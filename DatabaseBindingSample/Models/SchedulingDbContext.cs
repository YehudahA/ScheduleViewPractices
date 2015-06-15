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
    }
}
