using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Bifrost.Launcher;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public class OptionsViewModel : Screen
    {
        private readonly LaunchManager _launchManager;

        private bool _skipStartupMovies;
        private bool _forceCustomResolution;
        private string _customResolutionX;
        private string _customResolutionY;
        private bool _noSound;
        private bool _force32Bit;
        private DownloaderModel _selectedDownloader;
        private bool _noAccount;
        private bool _noOptions;
        private bool _noStore;
        private bool _noCatalog;
        private bool _noNews;
        private bool _noLogout;

        public List<DownloaderModel> Downloaders { get; }

        public OptionsLoggingViewModel OptionsLogging { get; }

        // Properties are in a separate file because they are too verbose

        public OptionsViewModel(LaunchManager launchManager)
        {
            Downloaders = Enum.GetNames(typeof(Downloader)).Select(name => new DownloaderModel(name)).ToList();

            // Initialize data from the launch manager
            _launchManager = launchManager;

            // Logging
            OptionsLogging = new(_launchManager);

            // Engine
            SkipStartupMovies = _launchManager.LaunchConfig.NoStartupMovies;
            ForceCustomResolution = _launchManager.LaunchConfig.ForceCustomResolution;
            CustomResolutionX = _launchManager.LaunchConfig.CustomResolutionX.ToString();
            CustomResolutionY = _launchManager.LaunchConfig.CustomResolutionY.ToString();
            NoSound = _launchManager.LaunchConfig.NoSound;

            // Advanced
            Force32Bit = _launchManager.LaunchConfig.Force32Bit;
            SelectedDownloader = Downloaders[(int)_launchManager.LaunchConfig.Downloader];
            NoAccount = _launchManager.LaunchConfig.NoAccount;
            NoOptions = _launchManager.LaunchConfig.NoOptions;
            NoStore = _launchManager.LaunchConfig.NoStore;
            NoCatalog = _launchManager.LaunchConfig.NoCatalog;
            NoNews = _launchManager.LaunchConfig.NoNews;
            NoLogout = _launchManager.LaunchConfig.NoLogout;
        }

        public void Apply()
        {
            if (string.IsNullOrWhiteSpace(_customResolutionX) || string.IsNullOrWhiteSpace(_customResolutionY))   // Validate custom resolution
            {
                MessageBox.Show("Please enter a valid custom resolution.", "Error");
                return;
            }

            UpdateLaunchManager();
            TryCloseAsync();
        }

        private void UpdateLaunchManager()
        {
            // Logging
            OptionsLogging.UpdateLaunchManager();

            // Engine
            _launchManager.LaunchConfig.ForceCustomResolution = ForceCustomResolution;
            _launchManager.LaunchConfig.CustomResolutionX = int.Parse(CustomResolutionX);
            _launchManager.LaunchConfig.CustomResolutionY = int.Parse(CustomResolutionY);
            _launchManager.LaunchConfig.NoSound = NoSound;

            // Advanced
            _launchManager.LaunchConfig.Force32Bit = Force32Bit;
            _launchManager.LaunchConfig.Downloader = (Downloader)Downloaders.IndexOf(SelectedDownloader);
            _launchManager.LaunchConfig.NoAccount = NoAccount;
            _launchManager.LaunchConfig.NoOptions = NoOptions;
            _launchManager.LaunchConfig.NoStore = NoStore;
            _launchManager.LaunchConfig.NoCatalog = NoCatalog;
            _launchManager.LaunchConfig.NoNews = NoNews;
            _launchManager.LaunchConfig.NoLogout = NoLogout;
        }

        #region Properties

        public bool SkipStartupMovies
        {
            get { return _skipStartupMovies; }
            set { _skipStartupMovies = value; NotifyOfPropertyChange(() => SkipStartupMovies); }
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

        public bool NoSound
        {
            get { return _noSound; }
            set { _noSound = value; NotifyOfPropertyChange(() => NoSound); }
        }

        public bool Force32Bit
        {
            get { return _force32Bit; }
            set { _force32Bit = value; NotifyOfPropertyChange(() => Force32Bit); }
        }

        public DownloaderModel SelectedDownloader
        {
            get { return _selectedDownloader; }
            set { _selectedDownloader = value; NotifyOfPropertyChange(() => SelectedDownloader); }
        }

        public bool NoAccount
        {
            get { return _noAccount; }
            set { _noAccount = value; NotifyOfPropertyChange(() => NoAccount); }
        }

        public bool NoOptions
        {
            get { return _noOptions; }
            set { _noOptions = value; NotifyOfPropertyChange(() => NoOptions); }
        }

        public bool NoStore
        {
            get { return _noStore; }
            set { _noStore = value; NotifyOfPropertyChange(() => NoStore); }
        }

        public bool NoCatalog
        {
            get { return _noCatalog; }
            set { _noCatalog = value; NotifyOfPropertyChange(() => NoCatalog); }
        }

        public bool NoNews
        {
            get { return _noNews; }
            set { _noNews = value; NotifyOfPropertyChange(() => NoNews); }
        }

        public bool NoLogout
        {
            get { return _noLogout; }
            set { _noLogout = value; NotifyOfPropertyChange(() => NoLogout); }
        }

        #endregion
    }
}
