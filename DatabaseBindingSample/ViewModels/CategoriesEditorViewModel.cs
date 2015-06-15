using DatabaseBindingSample.Models;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace DatabaseBindingSample.ViewModels
{
    public class CategoriesEditorViewModel : Mvvm.IDialogViewModel
    {
        public CategoriesEditorViewModel(SchedulingDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.Categories.Load();
            this.saveCommand = new DelegateCommand(Save);
        }

        private readonly SchedulingDbContext dbContext;
        private readonly DelegateCommand saveCommand;

        public ObservableCollection<CategoryModel> Categories
        {
            get { return dbContext.Categories.Local; }
        }

        public DelegateCommand SaveCommand
        {
            get { return saveCommand; }
        }

        private void Save()
        {
            dbContext.SaveChanges();
            this.FinishInteraction();
        }

        #region IDialogViewModel

        public string Title
        {
            get { return "Edit categories"; }
        }

        public bool? DialogResult
        {
            get;
            private set;
        }

        public System.Action FinishInteraction
        {
            get;
            set;
        }

        #endregion // IDialogViewModel
    }
}
