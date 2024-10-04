using Caliburn.Micro;
using System;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace Bifrost.Wpf.ViewModels
{
    public class ThirdPartyLicensesViewModel : Screen
    {
        public string LicenseText { get; }

        public ThirdPartyLicensesViewModel()
        {
            Uri uri = new("/Resources/ThirdPartyLicenses.txt", UriKind.Relative);
            StreamResourceInfo sri = Application.GetResourceStream(uri);

            using (StreamReader reader = new(sri.Stream))
                LicenseText = reader.ReadToEnd();
        }
    }
}
