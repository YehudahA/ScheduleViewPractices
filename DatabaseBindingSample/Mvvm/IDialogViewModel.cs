﻿using System;

namespace DatabaseBindingSample.Mvvm
{
    public interface IDialogViewModel
    {
        string Title { get; }
        bool? DialogResult { get; }
        event EventHandler RequestClose;
    }
}