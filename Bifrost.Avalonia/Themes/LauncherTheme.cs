using Avalonia.Media.Imaging;

namespace Bifrost.Avalonia.Themes
{
    public class LauncherTheme
    {
        public const string DefaultName = "Built-In";

        public string Name { get; }
        public int SortOrder { get; }
        public Bitmap Background { get; }

        public LauncherTheme(string name, int sortOrder, Bitmap background)
        {
            Name = name;
            SortOrder = sortOrder;
            Background = background;
        }
    }
}
