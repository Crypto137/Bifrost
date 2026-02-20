using Avalonia.Controls;
using Avalonia.Interactivity;
using Bifrost.Avalonia.Views.Dialogs;
using Bifrost.Core.ClientManagement;

namespace Bifrost.Avalonia.Views.Options;

public partial class OptionsWindow : Window
{
    private const int NumCategories = 5;

    private readonly OptionsUserControl[] _categoryControls = new OptionsUserControl[NumCategories];

    private readonly ClientLauncher _clientLauncher;

    public OptionsWindow()
    {
        InitializeComponent();

        _categoryControls[0] = GeneralUserControl;
        _categoryControls[1] = BifrostUserControl;
        _categoryControls[2] = LoggingUserControl;
        _categoryControls[3] = AdvancedUserControl;
        _categoryControls[4] = AboutUserControl;

        ShowCategory(0);
    }

    public OptionsWindow(ClientLauncher clientLauncher) : this()
    {
        _clientLauncher = clientLauncher;

        InitializeCategoryControls();
    }

    private void ShowCategory(int index)
    {
        for (int i = 0; i < NumCategories; i++)
        {
            OptionsUserControl categoryControl = _categoryControls[i];
            if (categoryControl != null)
                categoryControl.IsVisible = i == index;
        }
    }

    private void InitializeCategoryControls()
    {
        foreach (OptionsUserControl categoryControl in _categoryControls)
            categoryControl?.Initialize(_clientLauncher);
    }

    private bool UpdateClientLauncher(out string message)
    {
        message = string.Empty;

        // Don't update ClientLauncher until we get confirmation from all controls that all input is valid.
        foreach (OptionsUserControl categoryControl in _categoryControls)
        {
            if (categoryControl?.ValidateInput(out message) == false)
                return false;
        }

        // Now do the update.
        foreach (OptionsUserControl categoryControl in _categoryControls)
            categoryControl?.UpdateClientLauncher();

        return true;
    }

    #region EventHandler

    private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox listBox)
            ShowCategory(listBox.SelectedIndex);
    }

    private async void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        if (UpdateClientLauncher(out string message) == false)
        {
            await MessageBoxWindow.Show(this, $"Failed to apply changes: {message}", "Error");
            return;
        }

        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    #endregion
}