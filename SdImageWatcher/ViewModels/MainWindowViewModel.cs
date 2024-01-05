using Prism.Mvvm;
using SdImageWatcher.Models;

namespace SdImageWatcher.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public MainWindowViewModel()
        {
            using var db = new DatabaseContext();
            db.Database.EnsureCreated();
        }

        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}