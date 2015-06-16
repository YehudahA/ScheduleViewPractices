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
        public DbSet<RecurrenceRuleModel> RecurrenceRules { get; set; }
        public DbSet<ExceptionOccurrenceModel> ExceptionOccurrences { get; set; }
        public DbSet<ExceptionAppointmentModel> ExceptionAppointments { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ResourceTypeModel> ResourceTypes { get; set; }
        public DbSet<ResourceModel> Resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppointmentModel>().Map(map => map.MapInheritedProperties());
            modelBuilder.Entity<ExceptionAppointmentModel>().Map(map => map.MapInheritedProperties());

            modelBuilder.Entity<RecurrenceRuleModel>()
                .HasRequired(appointment => appointment.MasterAppointment)
                .WithOptional(a => a.RecurrenceRule)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ExceptionAppointmentModel>()
                .HasRequired(appointment => appointment.ExceptionOccurrence)
                .WithOptional(occurrence => occurrence.Appointment)
                .WillCascadeOnDelete(true);
        }
    }
}
