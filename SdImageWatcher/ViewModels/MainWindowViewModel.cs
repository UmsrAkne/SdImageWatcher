using Prism.Mvvm;
using Prism.Services.Dialogs;
using SdImageWatcher.Models;

namespace SdImageWatcher.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private string title = "Prism Application";

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            using var db = new DatabaseContext();
            db.Database.EnsureCreated();
        }

        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}