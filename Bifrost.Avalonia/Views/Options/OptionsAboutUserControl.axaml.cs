using Avalonia.Platform;
using System;
using System.IO;
using System.Reflection;

namespace Bifrost.Avalonia.Views.Options;

public partial class OptionsAboutUserControl : OptionsUserControl
{
#if DEBUG
    private const string BuildConfig = "Debug";
#else
        private const string BuildConfig = "Release";
#endif

    public OptionsAboutUserControl()
    {
        InitializeComponent();

        Version version = Assembly.GetEntryAssembly().GetName().Version;
        VersionTextBlock.Text = $"Version {version?.ToString(3)} ({BuildConfig})";

        string licenseText = string.Empty;

        try
        {
            using Stream stream = AssetLoader.Open(new Uri("avares://Bifrost/Assets/License.txt"));
            using StreamReader reader = new(stream);
            licenseText = reader.ReadToEnd();
        }
        catch
        {
            licenseText = "Failed to load license text.";
        }

        LicenseTextBox.Text = licenseText;
    }
}