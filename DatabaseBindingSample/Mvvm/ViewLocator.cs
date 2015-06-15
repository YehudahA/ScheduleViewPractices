﻿using System;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace DatabaseBindingSample.Mvvm
{
    public static class ViewLocator
    {
        private static Type defaultViewModelTypeToViewTypeResolver(Type viewType)
        {
            string viewName = viewType.FullName;
            viewName = viewName.Replace(".ViewModels.", ".Views.");
            viewName = viewName.Replace("ViewModel", "View");

            string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            string viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewAssemblyName);

            Type result = Type.GetType(viewModelName);
            return result;
        }

        public static FrameworkElement GetViewByViewModel(object viewModel)
        {
            Type viewType = defaultViewModelTypeToViewTypeResolver(viewModel.GetType());
            FrameworkElement result = Activator.CreateInstance(viewType) as FrameworkElement;
            result.DataContext = viewModel;

            return result;
        }
    }
}