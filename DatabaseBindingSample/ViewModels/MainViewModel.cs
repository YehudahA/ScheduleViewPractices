using DatabaseBindingSample.Models;
using Microsoft.Practices.Prism.Commands;
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
            this.appointments = new RadObservableCollection<AppointmentModel>();
            this.loadAppointmentsCommand = new DelegateCommand<IDateSpan>(LoadAppointments);
            this.saveCommand = new DelegateCommand(Save);

            this.appointments.CollectionChanged += appointments_CollectionChanged;
            this.context.Categories.Load();
        }

        private readonly SchedulingDbContext context;
        private readonly RadObservableCollection<AppointmentModel> appointments;
        private readonly DelegateCommand<IDateSpan> loadAppointmentsCommand;
        private readonly DelegateCommand saveCommand;

        #endregion // ctor; fields

        #region properties

        public ObservableCollection<CategoryModel> Categories
        {
            get { return context.Categories.Local; }
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

        #endregion // commands

        #region methods

        private void LoadAppointments(IDateSpan dateSpan)
        {
            appointments.Clear();
            IQueryable<AppointmentModel> query = context.Appointments.Where(app => app.Start >= dateSpan.Start && app.End <= dateSpan.End);
            appointments.AddRange(query);
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

        #endregion // methods
    }
}
