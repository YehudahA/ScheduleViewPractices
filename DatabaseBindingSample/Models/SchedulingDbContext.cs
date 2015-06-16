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
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ResourceTypeModel> ResourceTypes { get; set; }
        public DbSet<ResourceModel> Resources { get; set; }
    }
}
