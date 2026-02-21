using Avalonia.Controls;
using System;

namespace Bifrost.Avalonia.Extensions
{
    public static class ComboBoxExtensions
    {
        public static void PopulateFromEnum<T>(this ComboBox comboBox) where T: struct, Enum
        {
            ItemCollection items = comboBox.Items;
            items.Clear();

            foreach (T value in Enum.GetValues<T>())
            {
                ComboBoxItem item = new()
                {
                    Content = Enum.GetName(value),
                    Tag = value
                };

                items.Add(item);
            }
        }

        public static T GetSelectedEnumValue<T>(this ComboBox comboBox) where T: struct, Enum
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
            return (T)comboBoxItem.Tag;
        }

        public static void SetSelectedEnumValue<T>(this ComboBox comboBox, T value) where T: struct, Enum
        {
            ItemCollection items = comboBox.Items;

            for (int i = 0; i < items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)items[i];
                if (item.Tag.Equals(value))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }
    }
}
