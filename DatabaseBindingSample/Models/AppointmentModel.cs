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
        public int Id { get; set; }

        public virtual RecurrenceRuleModel RecurrenceRule { get; set; }

        IRecurrenceRule IAppointment.RecurrenceRule
        {
            get { return this.RecurrenceRule; }
            set { this.RecurrenceRule = value as RecurrenceRuleModel; }
        }

        #region IObjectGenerator<IRecurrenceRule>

        /////////////////////////////////////////
        // if IAppointment implaments the IObjectGenerator<IRecurrenceRule>, the Schedule use the CreateNew() method to create IRecurrenceRule
        // else, Telerik.Windows.Controls.ScheduleView.RecurrenceRule instance generated
        ////////////////////////////////////////

        IRecurrenceRule IObjectGenerator<IRecurrenceRule>.CreateNew(IRecurrenceRule item)
        {
            IRecurrenceRule rule = (this as IObjectGenerator<IRecurrenceRule>).CreateNew();
            rule.CopyFrom(item);
            return rule;
        }

        IRecurrenceRule IObjectGenerator<IRecurrenceRule>.CreateNew()
        {
            RecurrenceRuleModel rule = new RecurrenceRuleModel();
            this.RecurrenceRule = rule;
            return rule;
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
        
        #endregion // override
    }
}