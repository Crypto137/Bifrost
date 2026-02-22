using Avalonia.Controls;
using System.Linq;

namespace Bifrost.Avalonia.Extensions
{
    public static class GridExtensions
    {
        public static void AddColumns(this Grid grid, params GridLength[] lengths)
        {
            grid.ColumnDefinitions.AddRange(lengths.Select(length => new ColumnDefinition(length)));
        }

        public static void AddToColumn(this Grid grid, Control control, int columnIndex)
        {
            grid.Children.Add(control);
            Grid.SetColumn(control, columnIndex);
        }
    }
}
