﻿﻿using DatabaseBindingSample.Models;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace DatabaseBindingSample.ViewModels
{
    public class CategoriesEditorViewModel : Mvvm.DialogViewModelBase
    {
        public CategoriesEditorViewModel(SchedulingDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.Categories.Load();
            this.saveCommand = new DelegateCommand(Save);
            base.SetTitle("Edit Categories");
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
            this.DialogResult = true;
            base.OnRequestClose();
        }
    }
}