﻿using System;

namespace DatabaseBindingSample.Mvvm
{
    interface IDialogService
    {
        bool? RaiseDialog<T>(T viewModel) where T : IDialogViewModel;
    }
}