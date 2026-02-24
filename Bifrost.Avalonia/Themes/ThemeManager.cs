using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Bifrost.Avalonia.Themes
{
    public class ThemeManager
    {
        private const string ThemeArchiveFileName = "Bifrost.Themes.zip";

        private readonly Dictionary<string, LauncherTheme> _themes = new(StringComparer.OrdinalIgnoreCase);

        public ICollection<LauncherTheme> Themes { get => _themes.Values; }

        public static ThemeManager Instance { get; } = new();

        private ThemeManager()
        {
        }

        public void Initialize(string baseDirectory)
        {
            string themeArchiveFilePath = Path.Combine(baseDirectory, ThemeArchiveFileName);
            if (File.Exists(themeArchiveFilePath) == false)
                return;

            try
            {
                using ZipArchive themeArchive = ZipFile.Open(themeArchiveFilePath, ZipArchiveMode.Read);
                foreach (ZipArchiveEntry entry in themeArchive.Entries)
                {
                    string name = Path.GetFileNameWithoutExtension(entry.FullName);

                    using Stream zipStream = entry.Open();
                    using MemoryStream buffer = new();

                    zipStream.CopyTo(buffer);
                    buffer.Seek(0, SeekOrigin.Begin);
                    Bitmap background = new(buffer);

                    AddTheme(new(name, 0, background));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return;
            }
        }

        public LauncherTheme GetTheme(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            if (_themes.TryGetValue(name, out LauncherTheme theme) == false)
                return null;

            return theme;
        }

        public void AddTheme(LauncherTheme theme)
        {
            _themes[theme.Name] = theme;
        }
    }
}
