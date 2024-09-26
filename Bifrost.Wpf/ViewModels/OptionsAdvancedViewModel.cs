using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Launcher;
using Bifrost.Wpf.Models;

namespace Bifrost.Wpf.ViewModels
{
    public class OptionsAdvancedViewModel : OptionsCategoryBaseViewModel
    {
        private bool _force32Bit;
        private DownloaderModel _selectedDownloader;
        private bool _noSound;
        private bool _noAccount;
        private bool _noOptions;
        private bool _noStore;
        private bool _noCatalog;
        private bool _noNews;
        private bool _noLogout;

        public List<DownloaderModel> Downloaders { get; }

        public OptionsAdvancedViewModel(LaunchManager launchManager) : base(launchManager)
        {
            Downloaders = Enum.GetNames(typeof(Downloader)).Select(name => new DownloaderModel(name)).ToList();

            _force32Bit = _launchManager.LaunchConfig.Force32Bit;
            _selectedDownloader = Downloaders[(int)_launchManager.LaunchConfig.Downloader];
            _noSound = _launchManager.LaunchConfig.NoSound;
            _noAccount = _launchManager.LaunchConfig.NoAccount;
            _noOptions = _launchManager.LaunchConfig.NoOptions;
            _noStore = _launchManager.LaunchConfig.NoStore;
            _noCatalog = _launchManager.LaunchConfig.NoCatalog;
            _noNews = _launchManager.LaunchConfig.NoNews;
            _noLogout = _launchManager.LaunchConfig.NoLogout;
        }

        public override void UpdateLaunchManager()
        {
            _launchManager.LaunchConfig.Force32Bit = _force32Bit;
            _launchManager.LaunchConfig.Downloader = (Downloader)Downloaders.IndexOf(SelectedDownloader);
            _launchManager.LaunchConfig.NoSound = _noSound;
            _launchManager.LaunchConfig.NoAccount = _noAccount;
            _launchManager.LaunchConfig.NoOptions = _noOptions;
            _launchManager.LaunchConfig.NoStore = _noStore;
            _launchManager.LaunchConfig.NoCatalog = _noCatalog;
            _launchManager.LaunchConfig.NoNews = _noNews;
            _launchManager.LaunchConfig.NoLogout = _noLogout;
        }

        #region Properties

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

        public bool NoSound
        {
            get { return _noSound; }
            set { _noSound = value; NotifyOfPropertyChange(() => NoSound); }
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
