using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.ScheduleView.ICalendar;

namespace DatabaseBindingSample.Models
{
    [Table("RecurrenceRules")]
    public class RecurrenceRuleModel : IRecurrenceRule
    {
        #region ctor; fields

        public RecurrenceRuleModel()
        {
            this.Exceptions = new HashSet<ExceptionOccurrenceModel>();
        }

        #endregion // ctor; fields

        #region properties

        [Key]
        public int AppointmentId { get; set; }
        public string PatternString { get; set; }

        public virtual AppointmentModel MasterAppointment { get; set; }
        public virtual ICollection<ExceptionOccurrenceModel> Exceptions{get;set;}

        ICollection<IExceptionOccurrence> IRecurrenceRule.Exceptions
        {
            get { return new DatabaseBindingSample.Collections.Generic.CollectionWrapper<IExceptionOccurrence,ExceptionOccurrenceModel>(this.Exceptions); }
        }

        [NotMapped]
        public RecurrencePattern Pattern
        {
            get
            {
                RecurrencePattern pattern;
                RecurrencePatternHelper.TryParseRecurrencePattern(this.PatternString, out pattern);
                return pattern;
            }
            set
            {
                this.PatternString = RecurrencePatternHelper.RecurrencePatternToString(value);
            }
        }

        #endregion // properties

        #region methods

        public IAppointment CreateExceptionAppointment(IAppointment master)
        {
            ExceptionAppointmentModel newApp = new ExceptionAppointmentModel();
            newApp.CopyFrom(master);
            return newApp;
        }

        #endregion // methods

        #region ICopyable<IRecurrenceRule>

        public IRecurrenceRule Copy()
        {
            RecurrenceRuleModel newRule = new RecurrenceRuleModel();
            newRule.CopyFrom(this);

            return newRule;
        }

        public void CopyFrom(IRecurrenceRule other)
        {
            RecurrenceRuleModel otherModel = other as RecurrenceRuleModel;

            this.Pattern = other.Pattern;
            this.Exceptions.Clear();
            this.MasterAppointment = otherModel.MasterAppointment;
            
            foreach (ExceptionOccurrenceModel occ in otherModel.Exceptions)
            {
                this.Exceptions.Add(occ);
            }
        }

        #endregion // ICopyable<IRecurrenceRule>

        #region IObjectGenerator<IExceptionOccurrence>

        public IExceptionOccurrence CreateNew(IExceptionOccurrence item)
        {
            IExceptionOccurrence newExcepOcc = this.CreateNew();
            newExcepOcc.CopyFrom(item);
            return newExcepOcc;
        }

        public IExceptionOccurrence CreateNew()
        {
            ExceptionOccurrenceModel newExc = new ExceptionOccurrenceModel();
            //this.Exceptions.Add(newExc);
            return newExc;
        }

        #endregion // IObjectGenerator<IExceptionOccurrence>
    }
}
