using System.Windows;
using Microsoft.Practices.ServiceLocation;
using ViewModel;
using ViewModel.Interfaces;
using Microsoft.Practices.Unity;
using View.Services;
using System;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            base.OnStartup(e);
            var container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);
            container.RegisterInstance<IWindowService>(new WindowService());
            container.RegisterType<ITimerService, TimerService>();
            container.RegisterInstance<ITimeService>(new TimeService());
            container.RegisterInstance<IMessageBoxService>(new MessageBoxService());
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);


            var main = new MainWindow();
            var vm = new MainWindowViewModel();
            vm.ApplicationExit += MainWindowViewModel_ApplicationExit;
            main.DataContext = vm;
            main.Show();
        }

        private void MainWindowViewModel_ApplicationExit()
        {
            Application.Current.Shutdown();
        }
    }
}
