using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace DatabaseBindingSample.Models
{
    [Table("Appointments")]
    public class AppointmentModel : BindableBase, IAppointment, IExtendedAppointment
    {
        [Key]
        public int Id { get; set; }

        #region IAppointment properties

        private string subject, body;
        private bool isAllDayEvent;
        private DateTime start, end;

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

        #endregion // IAppointment

        #region Unused properties

        IRecurrenceRule IAppointment.RecurrenceRule
        {
            get;
            set;
        }

        IList IAppointment.Resources
        {
            get { return new List<Resource>(); }
        }

        [NotMapped]
        public TimeZoneInfo TimeZone
        {
            get;
            set;
        }

        #endregion // Unused properties

        #region IExtendedAppointment

        private Importance importance;

        public Importance Importance
        {
            get { return importance; }
            set { SetProperty(ref importance, value); }
        }

        ICategory IExtendedAppointment.Category
        {
            get;
            set;
        }

        public string TimeMarkerName { get; set; }

        public TimeMarker TimeMarker
        {
            get { return Helpers.TimeMarkerHelper.GetTimeMarkerByName(TimeMarkerName); }
            set { TimeMarkerName = value.TimeMarkerName; }
        }

        ITimeMarker IExtendedAppointment.TimeMarker
        {
            get { return this.TimeMarker; }
            set { this.TimeMarker = value as TimeMarker; }
        }

        #endregion // IExtendedAppointment

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
            AppointmentModel appointment = new AppointmentModel();
            appointment.CopyFrom(this);
            return appointment;
        }

        public void CopyFrom(IAppointment other)
        {
            AppointmentModel appointment = other as AppointmentModel;

            if (appointment == null)
                return;

            this.Id = appointment.Id;
            this.Subject = appointment.Subject;
            this.Body = appointment.Body;
            this.Start = appointment.Start;
            this.End = appointment.End;
            this.IsAllDayEvent = this.IsAllDayEvent;
            this.Importance = appointment.Importance;
        }

        #endregion // ICopyable<IAppointment>

        #region IEquatable<IAppointment>

        public bool Equals(IAppointment other)
        {
            AppointmentModel otherApp = other as AppointmentModel;

            return otherApp != null
                && otherApp.Id == this.Id;
        }

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