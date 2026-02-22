using Bifrost.Core.ClientManagement;
using System.Windows;

namespace Bifrost.Wpf.ViewModels
{
    public class OptionsGeneralViewModel : OptionsCategoryBaseViewModel
    {
        private bool _skipStartupMovies;
        private bool _noSplash;
        private bool _forceCustomResolution;
        private string _customResolutionX;
        private string _customResolutionY;

        private bool _enableAutoLogin;
        private string _autoLoginEmailAddress;
        private string _autoLoginPassword;

        private string _customArguments;

        public OptionsGeneralViewModel(ClientLauncher clientLauncher) : base(clientLauncher)
        {
            _skipStartupMovies = _clientLauncher.LaunchConfig.NoStartupMovies;
            _noSplash = _clientLauncher.LaunchConfig.NoSplash;
            _forceCustomResolution = _clientLauncher.LaunchConfig.ForceCustomResolution;
            _customResolutionX = _clientLauncher.LaunchConfig.CustomResolutionX.ToString();
            _customResolutionY = _clientLauncher.LaunchConfig.CustomResolutionY.ToString();

            _enableAutoLogin = _clientLauncher.LaunchConfig.EnableAutoLogin;
            _autoLoginEmailAddress = _clientLauncher.LaunchConfig.AutoLoginEmailAddress;
            _autoLoginPassword = _clientLauncher.LaunchConfig.AutoLoginPassword;

            _customArguments = _clientLauncher.LaunchConfig.CustomArguments;
        }

        public override void UpdateClientLauncher()
        {
            _clientLauncher.LaunchConfig.NoStartupMovies = _skipStartupMovies;
            _clientLauncher.LaunchConfig.NoSplash = _noSplash;
            _clientLauncher.LaunchConfig.ForceCustomResolution = _forceCustomResolution;
            _clientLauncher.LaunchConfig.CustomResolutionX = int.Parse(_customResolutionX);
            _clientLauncher.LaunchConfig.CustomResolutionY = int.Parse(_customResolutionY);

            _clientLauncher.LaunchConfig.EnableAutoLogin = _enableAutoLogin;
            _clientLauncher.LaunchConfig.AutoLoginEmailAddress = _autoLoginEmailAddress;
            _clientLauncher.LaunchConfig.AutoLoginPassword = _autoLoginPassword;

            _clientLauncher.LaunchConfig.CustomArguments = _customArguments;
        }

        public override bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(_customResolutionX) || string.IsNullOrWhiteSpace(_customResolutionY))
            {
                MessageBox.Show("Please enter a valid custom resolution.", "Error");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_autoLoginEmailAddress) || string.IsNullOrWhiteSpace(_autoLoginPassword))
            {
                MessageBox.Show("Please enter valid auto-login credentials.", "Error");
                return false;
            }

            return true;
        }

        public bool SkipStartupMovies
        {
            get { return _skipStartupMovies; }
            set { _skipStartupMovies = value; NotifyOfPropertyChange(() => SkipStartupMovies); }
        }

        public bool NoSplash
        {
            get { return _noSplash; }
            set { _noSplash = value; NotifyOfPropertyChange(() => NoSplash); }
        }

        public bool ForceCustomResolution
        {
            get { return _forceCustomResolution; }
            set { _forceCustomResolution = value; NotifyOfPropertyChange(() => ForceCustomResolution); }
        }

        public string CustomResolutionX
        {
            get { return _customResolutionX; }
            set { _customResolutionX = value; NotifyOfPropertyChange(() => CustomResolutionX); }
        }

        public string CustomResolutionY
        {
            get { return _customResolutionY; }
            set { _customResolutionY = value; NotifyOfPropertyChange(() => CustomResolutionY); }
        }

        public bool EnableAutoLogin
        {
            get { return _enableAutoLogin; }
            set
            {
                if (value)
                {
                    MessageBoxResult result = MessageBox.Show("Auto-login does not store your credentials in a secure way. " +
                        "You should use this option only with throwaway credentials on local servers. " +
                        "Are you sure you want to enable auto-login?",
                        "Confirm Enable Auto-Login", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                    if (result != MessageBoxResult.Yes)
                        return;
                }

                _enableAutoLogin = value;
                NotifyOfPropertyChange(() => EnableAutoLogin);
            }
        }

        public string AutoLoginEmailAddress
        {
            get { return _autoLoginEmailAddress; }
            set { _autoLoginEmailAddress = value; NotifyOfPropertyChange(() => AutoLoginEmailAddress); }
        }

        public string AutoLoginPassword
        {
            get { return _autoLoginPassword; }
            set { _autoLoginPassword = value; NotifyOfPropertyChange(() => AutoLoginPassword); }
        }

        public string CustomArguments
        {
            get { return _customArguments; }
            set { _customArguments = value; NotifyOfPropertyChange(() => CustomArguments); }
        }
    }
}
