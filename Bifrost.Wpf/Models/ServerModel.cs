﻿using System;
using Caliburn.Micro;
using Bifrost.Launcher;

namespace Bifrost.Wpf.Models
{
    public class ServerModel : PropertyChangedBase, ICloneable
    {
        private string _name;
        private string _siteConfigUrl;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => ComboBoxLabel);
            }
        }

        public string SiteConfigUrl
        {
            get => _siteConfigUrl;
            set
            {
                _siteConfigUrl = value;
                NotifyOfPropertyChange(() => SiteConfigUrl);
                NotifyOfPropertyChange(() => ComboBoxLabel);
            }
        }

        public string ComboBoxLabel { get => $"{Name} ({SiteConfigUrl.Split('/')[0]})"; }

        public ServerModel(Server server)
        {
            Name = server.Name;
            SiteConfigUrl = server.SiteConfigUrl;
        }

        public ServerModel(string name, string siteConfigUrl)
        {
            Name = name;
            SiteConfigUrl = siteConfigUrl;
        }

        public Server ToServer() => new(Name, SiteConfigUrl);

        public object Clone()
        {
            return new ServerModel(Name, SiteConfigUrl);
        }
    }
}
