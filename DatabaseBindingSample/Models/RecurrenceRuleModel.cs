using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.ScheduleView.ICalendar;

namespace DatabaseBindingSample.Models
{
    [Table("RecurrenceRules")]
    public class RecurrenceRuleModel : IRecurrenceRule
    {
        public RecurrenceRuleModel()
        {
            this.Exceptions = new HashSet<ExceptionOccurrenceModel>();
        }

        [Key]
        public int AppointmentId { get; set; }
        public string PatternString { get; set; }

        public virtual AppointmentModel MasterAppointment { get; set; }
        public virtual ICollection<ExceptionOccurrenceModel> Exceptions { get; set; }

        #region IRecurrenceRule

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

        /// <summary>
        /// This method called when the user create new ExceptionAppointment.
        /// the method receive the master appointment, and copy his properties [Subject, Body etc] to new ExceptionAppointment.
        /// </summary>
        /// <param name="master">The master appointment</param>
        /// <returns>New <see cref="ExceptionAppointmentModel"/></returns>
        public IAppointment CreateExceptionAppointment(IAppointment master)
        {
            ExceptionAppointmentModel newApp = new ExceptionAppointmentModel();
            newApp.CopyFrom(master);
            return newApp;
        }

        /// <summary>
        /// 
        /// </summary>
        ICollection<IExceptionOccurrence> IRecurrenceRule.Exceptions
        {
            get { return new ObjectModel.CollectionWrapper<IExceptionOccurrence, ExceptionOccurrenceModel>(this.Exceptions); }
        }

        #endregion // IRecurrenceRule

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
            this.MasterAppointment = otherModel.MasterAppointment;
        }

        #endregion // ICopyable<IRecurrenceRule>

        #region IObjectGenerator<IExceptionOccurrence>

        public IExceptionOccurrence CreateNew(IExceptionOccurrence item)
        {
            IExceptionOccurrence newExcepOcc = this.CreateNew();
            newExcepOcc.CopyFrom(item);
            return newExcepOcc;
        }

        /// <summary>
        /// Create a new ExceptionOccurrence object. this method called when the user DELETE an Occurrence of the RecurrenceRule, or when the user want CHANGE some property of some Occourrence.
        /// </summary>
        /// <returns>ExceptionOccurrenceModel object</returns>
        public IExceptionOccurrence CreateNew()
        {
            ExceptionOccurrenceModel newExc = new ExceptionOccurrenceModel();
            return newExc;
        }

        #endregion // IObjectGenerator<IExceptionOccurrence>
    }
}