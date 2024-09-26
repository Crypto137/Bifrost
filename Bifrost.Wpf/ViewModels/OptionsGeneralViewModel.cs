using Bifrost.Launcher;
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

        public OptionsGeneralViewModel(LaunchManager launchManager) : base(launchManager)
        {
            SkipStartupMovies = _launchManager.LaunchConfig.NoStartupMovies;
            NoSplash = _launchManager.LaunchConfig.NoSplash;
            ForceCustomResolution = _launchManager.LaunchConfig.ForceCustomResolution;
            CustomResolutionX = _launchManager.LaunchConfig.CustomResolutionX.ToString();
            CustomResolutionY = _launchManager.LaunchConfig.CustomResolutionY.ToString();
        }

        public override void UpdateLaunchManager()
        {
            _launchManager.LaunchConfig.NoStartupMovies = SkipStartupMovies;
            _launchManager.LaunchConfig.NoSplash = NoSplash;
            _launchManager.LaunchConfig.ForceCustomResolution = ForceCustomResolution;
            _launchManager.LaunchConfig.CustomResolutionX = int.Parse(CustomResolutionX);
            _launchManager.LaunchConfig.CustomResolutionY = int.Parse(CustomResolutionY);
        }

        public override bool ValidateInput()
        {
            // Custom resolution
            if (string.IsNullOrWhiteSpace(_customResolutionX) || string.IsNullOrWhiteSpace(_customResolutionY))
            {
                MessageBox.Show("Please enter a valid custom resolution.", "Error");
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
    }
}
