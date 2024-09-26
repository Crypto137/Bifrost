using Caliburn.Micro;
using Bifrost.Launcher;

namespace Bifrost.Wpf.ViewModels
{
    public class OptionsViewModel : Screen
    {
        private readonly OptionsCategoryBaseViewModel[] _categories;

        public OptionsGeneralViewModel OptionsGeneral { get; }
        public OptionsLoggingViewModel OptionsLogging { get; }
        public OptionsAdvancedViewModel OptionsAdvanced { get; }

        public OptionsViewModel(LaunchManager launchManager)
        {
            // Initialize categories (Tabs) from the launch manager
            OptionsGeneral = new(launchManager);
            OptionsLogging = new(launchManager);
            OptionsAdvanced = new(launchManager);

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
                category.UpdateLaunchManager();

            TryCloseAsync();
        }
    }
}
