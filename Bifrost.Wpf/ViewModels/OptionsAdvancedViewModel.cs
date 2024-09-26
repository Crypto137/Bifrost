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

            Force32Bit = _launchManager.LaunchConfig.Force32Bit;
            SelectedDownloader = Downloaders[(int)_launchManager.LaunchConfig.Downloader];
            NoSound = _launchManager.LaunchConfig.NoSound;
            NoAccount = _launchManager.LaunchConfig.NoAccount;
            NoOptions = _launchManager.LaunchConfig.NoOptions;
            NoStore = _launchManager.LaunchConfig.NoStore;
            NoCatalog = _launchManager.LaunchConfig.NoCatalog;
            NoNews = _launchManager.LaunchConfig.NoNews;
            NoLogout = _launchManager.LaunchConfig.NoLogout;
        }

        public override void UpdateLaunchManager()
        {
            _launchManager.LaunchConfig.Force32Bit = Force32Bit;
            _launchManager.LaunchConfig.Downloader = (Downloader)Downloaders.IndexOf(SelectedDownloader);
            _launchManager.LaunchConfig.NoSound = NoSound;
            _launchManager.LaunchConfig.NoAccount = NoAccount;
            _launchManager.LaunchConfig.NoOptions = NoOptions;
            _launchManager.LaunchConfig.NoStore = NoStore;
            _launchManager.LaunchConfig.NoCatalog = NoCatalog;
            _launchManager.LaunchConfig.NoNews = NoNews;
            _launchManager.LaunchConfig.NoLogout = NoLogout;
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
