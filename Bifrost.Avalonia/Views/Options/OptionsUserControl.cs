using Avalonia.Controls;
using Bifrost.Core.ClientManagement;

namespace Bifrost.Avalonia.Views.Options
{
    public abstract class OptionsUserControl : UserControl
    {
        protected Window _owner;
        protected ClientLauncher _clientLauncher;

        public virtual void Initialize(Window owner, ClientLauncher clientLauncher)
        {
            _owner = owner;
            _clientLauncher = clientLauncher;
        }

        public virtual bool ValidateInput(out string message)
        {
            message = string.Empty;
            return true;
        }

        public virtual void UpdateClientLauncher()
        {
        }
    }
}
