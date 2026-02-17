using Caliburn.Micro;
using Bifrost.Core.ClientManagement;

namespace Bifrost.Wpf.ViewModels
{
    /// <summary>
    /// Base class for options category (tab) viewmodels used in <see cref="OptionsViewModel"/>. Inherits from <see cref="Screen"/>.
    /// </summary>
    public abstract class OptionsCategoryBaseViewModel : Screen
    {
        protected readonly ClientLauncher _clientLauncher;

        public OptionsCategoryBaseViewModel(ClientLauncher clientLauncher)
        {
            _clientLauncher = clientLauncher;
        }

        public virtual void UpdateClientLauncher()
        {
        }

        public virtual bool ValidateInput()
        {
            return true;
        }
    }
}
