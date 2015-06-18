using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseBindingSample.Models
{
    [Table("ExceptionAppointments")]
    public class ExceptionAppointmentModel : AppointmentModelBase
    {
        [Key]
        public int ExceptionOccurrenceId { get; set; }
        public virtual ExceptionOccurrenceModel ExceptionOccurrence { get; set; }

        #region override

        protected override AppointmentModelBase CreateNewAppointment()
        {
            return new ExceptionAppointmentModel();
        }

        public override bool Equals(Telerik.Windows.Controls.ScheduleView.IAppointment other)
        {
            ExceptionAppointmentModel otherApp = other as ExceptionAppointmentModel;

            return otherApp != null
                && otherApp.ExceptionOccurrenceId == this.ExceptionOccurrenceId;
        }

        #endregion // override
    }
}
