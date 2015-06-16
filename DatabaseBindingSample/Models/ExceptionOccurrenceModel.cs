using System;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls.ScheduleView;

namespace DatabaseBindingSample.Models
{
    [Table("ExceptionOccurrences")]
    public class ExceptionOccurrenceModel : IExceptionOccurrence
    {
        public int Id { get; set; }
        public DateTime ExceptionDate { get; set; }

        [ForeignKey("RecurrenceRule")]
        public int ReccurrenceRuleId { get; set; }

        public virtual RecurrenceRuleModel RecurrenceRule { get; set; }
        public virtual ExceptionAppointmentModel Appointment { get; set; }

        IAppointment IExceptionOccurrence.Appointment
        {
            get { return this.Appointment; }
            set { this.Appointment = value as ExceptionAppointmentModel; }
        }

        #region ICopyable<IExceptionOccurrence>

        public IExceptionOccurrence Copy()
        {
            IExceptionOccurrence newOcc = new ExceptionOccurrenceModel();
            newOcc.CopyFrom(this);
            return newOcc;
        }

        public void CopyFrom(IExceptionOccurrence other)
        {
            this.ExceptionDate = other.ExceptionDate;
            (this as IExceptionOccurrence).Appointment = other.Appointment;
        }

        #endregion // ICopyable<IExceptionOccurrence>
    }
}
