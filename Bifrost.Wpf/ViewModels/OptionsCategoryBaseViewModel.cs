using Caliburn.Micro;
using Bifrost.Launcher;

namespace Bifrost.Wpf.ViewModels
{
    /// <summary>
    /// Base class for options category (tab) viewmodels used in <see cref="OptionsViewModel"/>. Inherits from <see cref="Screen"/>.
    /// </summary>
    public abstract class OptionsCategoryBaseViewModel : Screen
    {
        protected readonly LaunchManager _launchManager;

        public OptionsCategoryBaseViewModel(LaunchManager launchManager)
        {
            _launchManager = launchManager;
        }

        public virtual void UpdateLaunchManager()
        {
        }

        public virtual bool ValidateInput()
        {
            return true;
        }
    }
}
