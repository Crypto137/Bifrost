using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Bifrost.Wpf.Models;
using Bifrost.Core.Models;
using Bifrost.Core.ClientManagement;

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

        private IWindowManager _windowManager;

        public List<DownloaderModel> Downloaders { get; }

        public OptionsAdvancedViewModel(ClientLauncher clientLauncher) : base(clientLauncher)
        {
            Downloaders = Enum.GetNames(typeof(Downloader)).Select(name => new DownloaderModel(name)).ToList();

            _force32Bit = _clientLauncher.LaunchConfig.Force32Bit;
            _selectedDownloader = Downloaders[(int)_clientLauncher.LaunchConfig.Downloader];
            _noSound = _clientLauncher.LaunchConfig.NoSound;
            _noAccount = _clientLauncher.LaunchConfig.NoAccount;
            _noOptions = _clientLauncher.LaunchConfig.NoOptions;
            _noStore = _clientLauncher.LaunchConfig.NoStore;
            _noCatalog = _clientLauncher.LaunchConfig.NoCatalog;
            _noNews = _clientLauncher.LaunchConfig.NoNews;
            _noLogout = _clientLauncher.LaunchConfig.NoLogout;

            _windowManager = new WindowManager();
        }

        public override void UpdateClientLauncher()
        {
            _clientLauncher.LaunchConfig.Force32Bit = _force32Bit;
            _clientLauncher.LaunchConfig.Downloader = (Downloader)Downloaders.IndexOf(SelectedDownloader);
            _clientLauncher.LaunchConfig.NoSound = _noSound;
            _clientLauncher.LaunchConfig.NoAccount = _noAccount;
            _clientLauncher.LaunchConfig.NoOptions = _noOptions;
            _clientLauncher.LaunchConfig.NoStore = _noStore;
            _clientLauncher.LaunchConfig.NoCatalog = _noCatalog;
            _clientLauncher.LaunchConfig.NoNews = _noNews;
            _clientLauncher.LaunchConfig.NoLogout = _noLogout;
        }

        public void OpenThirdPartyLicenses()
        {
            _windowManager.ShowDialogAsync(new ThirdPartyLicensesViewModel());
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
