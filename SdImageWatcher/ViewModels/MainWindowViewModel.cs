using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SdImageWatcher.Models;
using SdImageWatcher.Views;

namespace SdImageWatcher.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        private readonly IDialogService dialogService;
        private readonly DatabaseContext databaseContext;
        private string title = "Prism Application";

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            databaseContext = new DatabaseContext();
            databaseContext.Database.EnsureCreated();
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

        public DelegateCommand ShowDirectoryRegistrationDialogCommand => new (() =>
        {
            var param = new DialogParameters { { nameof(DatabaseContext), databaseContext }, };
            dialogService.ShowDialog(nameof(DirectoryRegistrationPage), param, _ =>
            {
            });
        });

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            databaseContext.Dispose();
        }
    }
}