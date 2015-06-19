using DatabaseBindingSample.Helpers;
using DatabaseBindingSample.Models;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;

namespace DatabaseBindingSample.ViewModels
{
    public class MainViewModel
    {
        #region ctor; fields

        public MainViewModel()
        {
            this.context = new SchedulingDbContext();
            this.dialogHostService = new Mvvm.DialogHostService();

            this.appointments = new RadObservableCollection<AppointmentModel>();
            this.loadAppointmentsCommand = new DelegateCommand<IDateSpan>(LoadAppointments);
            this.saveCommand = new DelegateCommand(Save);
            this.editCategoriesCommand = new DelegateCommand(EditCategories);

            this.appointments.CollectionChanged += appointments_CollectionChanged;
            this.context.Categories.Load();
            this.context.ResourceTypes.Load();
        }


        private readonly SchedulingDbContext context;
        private readonly Mvvm.IDialogHostService dialogHostService;
        private readonly RadObservableCollection<AppointmentModel> appointments;
        private readonly DelegateCommand<IDateSpan> loadAppointmentsCommand;
        private readonly DelegateCommand saveCommand;
        private readonly DelegateCommand editCategoriesCommand;

        private IDateSpan dateSpan;

        #endregion // ctor; fields

        #region properties

        public ObservableCollection<CategoryModel> Categories
        {
            get { return context.Categories.Local; }
        }

        public ObservableCollection<ResourceTypeModel> ResourceTypes
        {
            get { return context.ResourceTypes.Local; }
        }

        public RadObservableCollection<AppointmentModel> Appointments
        {
            get { return appointments; }
        }

        #endregion // properties

        #region commands

        public DelegateCommand<IDateSpan> LoadAppointmentsCommand
        {
            get { return loadAppointmentsCommand; }
        }

        public DelegateCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public DelegateCommand EditCategoriesCommand
        {
            get { return editCategoriesCommand; }
        }
        #endregion // commands

        #region methods

        private void LoadAppointments(IDateSpan dateSpan)
        {
            this.dateSpan = dateSpan;

            this.appointments.Clear();
            List<AppointmentModel> appointments = this.GetAppointmentsByRange(dateSpan);
            this.appointments.AddRange(appointments);
        }

        private List<AppointmentModel> GetAppointmentsByRange(IDateSpan dateSpan)
        {

            List<AppointmentModel> result = new List<AppointmentModel>();

            /* keep in mind that we need query the database and the local.
             * for example: if we open some Appointment and change the start from 01/01/2015 to 15/03/2015,
             * my changes are not in the database, and it stores temporary in local.
             */

            // load the MasterAppointments
            context.Appointments.Where(app =>
                (app.Start >= dateSpan.Start && app.Start <= dateSpan.End)
                ||
                (app.End >= dateSpan.Start && app.End <= dateSpan.End))
                .Load();


            // load the old appointments that may Occurrence in this dateSpan;
            context.Appointments.Where(app =>
                    app.Start <= dateSpan.Start && app.RecurrenceRule != null)
                    .Load();


            // add the appointments that may Occurrence in this dateSpan
            foreach (AppointmentModel item in context.Appointments.Local)
            {
                if ((item.Start >= dateSpan.Start && item.Start <= dateSpan.End)
                    ||
                    (item.End >= dateSpan.Start && item.End <= dateSpan.End))
                {
                    result.Add(item);
                }
                else if (RecurrenceHelper.IsAnyOccurrenceInRange(item.RecurrenceRule.PatternString, dateSpan.Start, dateSpan.End) && !result.Contains(item))
                {
                    result.Add(item);
                }
            }

            // query and load the exceptions
            IQueryable<AppointmentModel> exceptioAppointments = from app in context.ExceptionAppointments
                                                                where (app.Start >= dateSpan.Start && app.Start <= dateSpan.End)
                                                                ||
                                                                (app.End >= dateSpan.Start && app.End <= dateSpan.End)
                                                                select app.ExceptionOccurrence.RecurrenceRule.MasterAppointment;

            foreach (AppointmentModel item in exceptioAppointments)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        private void Save()
        {
            context.SaveChanges();
        }

        void appointments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            {
                context.Appointments.AddRange(e.NewItems.Cast<AppointmentModel>());
            }

            else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
            {
                context.Appointments.RemoveRange(e.OldItems.Cast<AppointmentModel>());
            }
        }

        private void EditCategories()
        {
            CategoriesEditorViewModel viewModel = new CategoriesEditorViewModel(this.context);
            bool? changed = dialogHostService.RaiseDialog(viewModel);

            if (changed ?? false)
            {
                this.LoadAppointments(this.dateSpan);
            }
        }

        #endregion // methods
    }
}
