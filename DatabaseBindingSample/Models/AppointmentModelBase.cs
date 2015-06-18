using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace DatabaseBindingSample.Models
{
    public abstract class AppointmentModelBase : BindableBase, IAppointment, IExtendedAppointment
    {
        public AppointmentModelBase()
        {
            this.Resources = new List<ResourceModel>();
        }

        #region properties

        private string subject, body;
        private DateTime start, end;
        private bool isAllDayEvent;
        private Importance importance;

        public string Subject
        {
            get { return subject; }
            set { SetProperty(ref subject, value); }
        }

        public string Body
        {
            get { return body; }
            set { SetProperty(ref body, value); }
        }

        public DateTime Start
        {
            get { return start; }
            set { SetProperty(ref start, value); }
        }

        public DateTime End
        {
            get { return end; }
            set { SetProperty(ref end, value); }
        }

        public bool IsAllDayEvent
        {
            get { return isAllDayEvent; }
            set { SetProperty(ref isAllDayEvent, value); }
        }

        public Importance Importance
        {
            get { return importance; }
            set { SetProperty(ref importance, value); }
        }

        #endregion // properties

        #region categorize

        public int? CategoryId { get; set; }

        private CategoryModel category;

        [ForeignKey("CategoryId")]
        public virtual CategoryModel Category
        {
            get { return this.category; }
            set { SetProperty(ref category, value); }
        }

        ICategory IExtendedAppointment.Category
        {
            get { return this.Category; }
            set { this.Category = value as CategoryModel; }
        }

        #endregion // categorize

        #region resources

        public virtual List<ResourceModel> Resources { get; set; }

        IList IAppointment.Resources
        {
            get { return this.Resources; }
        }

        #endregion // resources

        #region timemarker

        public string TimeMarkerString { get; set; }

        public ITimeMarker TimeMarker
        {
            get { return Helpers.TimeMarkerHelper.ResolveTimeMarkerByName(this.TimeMarkerString); }
            set { this.TimeMarkerString = value.TimeMarkerName; }
        }

        #endregion // timemarker

        #region not used properties

        [NotMapped]
        public TimeZoneInfo TimeZone
        {
            get;
            set;
        }

        IRecurrenceRule IAppointment.RecurrenceRule
        {
            get;
            set;
        }

        #endregion // not used properties

        #region event handlers

        public event EventHandler RecurrenceRuleChanged;

        public void OnRecurrenceRuleChanged(EventArgs args)
        {
            EventHandler recurrenceRuleChanged = this.RecurrenceRuleChanged;

            if (recurrenceRuleChanged != null)
            {
                recurrenceRuleChanged(this, args);
            }
        }

        #endregion // event handlers

        #region ICopyable<IAppointment>

        public IAppointment Copy()
        {
            AppointmentModelBase appointment = this.CreateNewAppointment();
            appointment.CopyFrom(this);
            return appointment;
        }

        protected abstract AppointmentModelBase CreateNewAppointment();

        public virtual void CopyFrom(IAppointment other)
        {
            AppointmentModelBase sourceAppointment = other as AppointmentModelBase;

            if (sourceAppointment == null)
                return;

            this.Subject = sourceAppointment.Subject;
            this.Body = sourceAppointment.Body;
            this.Start = sourceAppointment.Start;
            this.End = sourceAppointment.End;
            this.IsAllDayEvent = this.IsAllDayEvent;
            this.Importance = sourceAppointment.Importance;
            this.Category = sourceAppointment.Category;
            this.Resources.Clear();
            this.Resources.AddRange(sourceAppointment.Resources);
            this.TimeMarker = sourceAppointment.TimeMarker;
        }

        #endregion // ICopyable<IAppointment>

        #region IEquatable<IAppointment>

        public abstract bool Equals(IAppointment other);

        #endregion // IEquatable<IAppointment>

        #region IEditableObject

        void IEditableObject.BeginEdit()
        {
        }

        void IEditableObject.CancelEdit()
        {
        }

        void IEditableObject.EndEdit()
        {
        }

        #endregion // IEditableObject
    }
}