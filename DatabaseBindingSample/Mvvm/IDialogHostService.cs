﻿using System;

namespace DatabaseBindingSample.Mvvm
{
    interface IDialogHostService
    {
        bool? RaiseDialog<T>(T viewModel) where T : IDialogViewModel;
    }
}