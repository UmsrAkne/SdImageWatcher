﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
            ReloadFileList();
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<ExFileInfo> Files { get; set; }

        public DelegateCommand ShowDirectoryRegistrationDialogCommand => new (() =>
        {
            var param = new DialogParameters { { nameof(DatabaseContext), databaseContext }, };
            dialogService.ShowDialog(nameof(DirectoryRegistrationPage), param, _ =>
            {
                ReloadFileList();
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

        private void ReloadFileList()
        {
            var watchingDirectories = databaseContext.WatchingDirectories.ToList();
            var infos = new List<ExFileInfo>();
            foreach (var exFileInfo in watchingDirectories)
            {
                infos.AddRange(FileUtils.GetAllFilePaths(exFileInfo.FullName)
                    .Select(p => new ExFileInfo(new FileInfo(p))));
            }

            foreach (var info in infos)
            {
                databaseContext.Add(info);
            }

            databaseContext.SaveChanges();

            var list = databaseContext.Files.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                list[i].Index = i + 1;
            }

            Files = new ObservableCollection<ExFileInfo>(list);
            RaisePropertyChanged(nameof(Files));
        }
    }
}