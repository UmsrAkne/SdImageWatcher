using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SdImageWatcher.Models;

namespace SdImageWatcher.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DirectoryRegistrationPageViewModel : BindableBase, IDialogAware
    {
        private DatabaseContext databaseContext;
        private string directoryPath = string.Empty;

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public string DirectoryPath { get => directoryPath; set => SetProperty(ref directoryPath, value); }

        public DelegateCommand CloseCommand => new (() =>
        {
            RequestClose?.Invoke(new DialogResult());
        });

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            databaseContext = parameters.GetValue<DatabaseContext>(nameof(DatabaseContext));
        }
    }
}