using Microsoft.Practices.Prism.Mvvm;
using System;

namespace DatabaseBindingSample.Mvvm
{
    public abstract class DialogViewModelBase : BindableBase, IDialogViewModel
    {
        public virtual string Title { get; protected set; }
        public virtual bool? DialogResult { get; protected set; }

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
