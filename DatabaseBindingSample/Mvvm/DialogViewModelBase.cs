using Microsoft.Practices.Prism.Mvvm;
using System;

namespace DatabaseBindingSample.Mvvm
{
    public abstract class DialogViewModelBase : BindableBase, IDialogViewModel
    {
        string title = "Dialog";

        public string Title
        {
            get { return title; }
        }

        public virtual bool? DialogResult
        {
            get;
            protected set;
        }

        protected void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be empty");

            this.title = title;
        }

        protected void OnRequestClose()
        {
            EventHandler requestClose = this.RequestClose;

            if (requestClose != null)
            {
                requestClose(this, EventArgs.Empty);
            }
        }

        public event EventHandler RequestClose;
    }
}
