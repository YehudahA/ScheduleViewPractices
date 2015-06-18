using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace DatabaseBindingSample.Models
{
    [Table("Appointments")]
    public class AppointmentModel : AppointmentModelBase, IAppointment, IObjectGenerator<IRecurrenceRule>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual RecurrenceRuleModel RecurrenceRule { get; set; }

        IRecurrenceRule IAppointment.RecurrenceRule
        {
            get { return this.RecurrenceRule; }
            set { this.RecurrenceRule = value as RecurrenceRuleModel; }
        }

        #region IObjectGenerator<IRecurrenceRule>

        /////////////////////////////////////////
        // When the user click on "Edit recurrence" button on AppointmentDialog, and a new IRecureenceRule is required, then:
        // if the appointment implements IObjectGenerator<IRecurrenceRule>, the new IRecurrenceRule will be created using the CreateNew() method.
        // else, a new instance of Telerik.Windows.Controls.ScheduleView.RecurrenceRule will be generated
        ////////////////////////////////////////

        IRecurrenceRule IObjectGenerator<IRecurrenceRule>.CreateNew(IRecurrenceRule item)
        {
            IRecurrenceRule rule = (this as IObjectGenerator<IRecurrenceRule>).CreateNew();
            rule.CopyFrom(item);
            return rule;
        }

        IRecurrenceRule IObjectGenerator<IRecurrenceRule>.CreateNew()
        {
            if (this.RecurrenceRule == null)
                this.RecurrenceRule = new RecurrenceRuleModel();

            return this.RecurrenceRule; ;
        }

        #endregion // IObjectGenerator<IRecurrenceRule>

        #region override

        protected override AppointmentModelBase CreateNewAppointment()
        {
            return new AppointmentModel();
        }

        public override bool Equals(Telerik.Windows.Controls.ScheduleView.IAppointment other)
        {
            AppointmentModel otherApp = other as AppointmentModel;

            return otherApp != null
                && otherApp.Id == this.Id;
        }

        public override void CopyFrom(IAppointment other)
        {
            base.CopyFrom(other);

            AppointmentModel otherAppointment = other as AppointmentModel;

            if (otherAppointment.RecurrenceRule != null)
                this.RecurrenceRule = otherAppointment.RecurrenceRule.Copy() as RecurrenceRuleModel;
        }

        #endregion // override
    }
}