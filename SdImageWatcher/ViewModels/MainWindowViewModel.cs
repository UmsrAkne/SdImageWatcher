﻿using Prism.Mvvm;

namespace SdImageWatcher.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}