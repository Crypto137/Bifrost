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

        public OptionsGeneralViewModel(LaunchManager launchManager) : base(launchManager)
        {
            _skipStartupMovies = _launchManager.LaunchConfig.NoStartupMovies;
            _noSplash = _launchManager.LaunchConfig.NoSplash;
            _forceCustomResolution = _launchManager.LaunchConfig.ForceCustomResolution;
            _customResolutionX = _launchManager.LaunchConfig.CustomResolutionX.ToString();
            _customResolutionY = _launchManager.LaunchConfig.CustomResolutionY.ToString();

            _enableAutoLogin = _launchManager.LaunchConfig.EnableAutoLogin;
            _autoLoginEmailAddress = _launchManager.LaunchConfig.AutoLoginEmailAddress;
            _autoLoginPassword = _launchManager.LaunchConfig.AutoLoginPassword;

            _customArguments = _launchManager.LaunchConfig.CustomArguments;
        }

        public override void UpdateLaunchManager()
        {
            _launchManager.LaunchConfig.NoStartupMovies = _skipStartupMovies;
            _launchManager.LaunchConfig.NoSplash = _noSplash;
            _launchManager.LaunchConfig.ForceCustomResolution = _forceCustomResolution;
            _launchManager.LaunchConfig.CustomResolutionX = int.Parse(_customResolutionX);
            _launchManager.LaunchConfig.CustomResolutionY = int.Parse(_customResolutionY);

            _launchManager.LaunchConfig.EnableAutoLogin = _enableAutoLogin;
            _launchManager.LaunchConfig.AutoLoginEmailAddress = _autoLoginEmailAddress;
            _launchManager.LaunchConfig.AutoLoginPassword = _autoLoginPassword;

            _launchManager.LaunchConfig.CustomArguments = _customArguments;
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
