using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
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
        private ObservableCollection<ExFileInfo> directories;

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public string DirectoryPath { get => directoryPath; set => SetProperty(ref directoryPath, value); }

        public ObservableCollection<ExFileInfo> Directories
        {
            get => directories;
            private set => SetProperty(ref directories, value);
        }

        public DelegateCommand<TextBox> DirectoryRegistrationCommand => new ((textBox) =>
        {
            var d = new ExFileInfo(new DirectoryInfo(DirectoryPath));
            if (!d.IsDirectory)
            {
                return;
            }

            databaseContext.WatchingDirectoryPaths.Add(d);
            databaseContext.SaveChanges();
            Directories.Add(d);
            textBox.Text = string.Empty;
        });

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
            Directories = new ObservableCollection<ExFileInfo>(databaseContext.WatchingDirectoryPaths.ToList());
        }
    }
}