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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(entity.ClrType.Name.Replace("Model", "")));
        }
    }
}
