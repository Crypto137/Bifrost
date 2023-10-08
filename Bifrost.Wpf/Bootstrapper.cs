using System.Windows;
using Caliburn.Micro;
using Bifrost.Wpf.ViewModels;

namespace Bifrost.Wpf
{
    public class Bootstrapper :BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
