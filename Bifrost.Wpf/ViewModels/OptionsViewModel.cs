using Caliburn.Micro;
using Bifrost.Core.ClientManagement;

namespace Bifrost.Wpf.ViewModels
{
    public class OptionsViewModel : Screen
    {
        private readonly OptionsCategoryBaseViewModel[] _categories;

        public OptionsGeneralViewModel OptionsGeneral { get; }
        public OptionsLoggingViewModel OptionsLogging { get; }
        public OptionsAdvancedViewModel OptionsAdvanced { get; }

        public OptionsViewModel(ClientLauncher clientLauncher)
        {
            // Initialize categories (Tabs) from the client launcher
            OptionsGeneral = new(clientLauncher);
            OptionsLogging = new(clientLauncher);
            OptionsAdvanced = new(clientLauncher);

            _categories = new OptionsCategoryBaseViewModel[] { OptionsGeneral, OptionsLogging, OptionsAdvanced };
        }

        public void Apply()
        {
            foreach (OptionsCategoryBaseViewModel category in _categories)
            {
                if (category.ValidateInput() == false)
                    return;
            }

            foreach (OptionsCategoryBaseViewModel category in _categories)
                category.UpdateClientLauncher();

            TryCloseAsync();
        }
    }
}
