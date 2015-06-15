using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls.ScheduleView;

namespace DatabaseBindingSample.Models
{
    [Table("Appointments")]
    public class AppointmentModel : Appointment
    {
        [Key]
        public int Id { get; set; }
    }
}
