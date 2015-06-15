using System;

namespace DatabaseBindingSample.Mvvm
{
    public interface IDialogViewModel
    {
        string Title { get; }
        bool? DialogResult { get; }
        Action FinishInteraction { get; set; }
    }
}
