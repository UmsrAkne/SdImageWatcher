using System.Windows;
using Prism.Ioc;
using SdImageWatcher.ViewModels;
using SdImageWatcher.Views;

namespace SdImageWatcher
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<DirectoryRegistrationPage, DirectoryRegistrationPageViewModel>();
        }
    }
}