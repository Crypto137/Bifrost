using Avalonia.Controls;
using Avalonia.Interactivity;
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
    }

    private void ShowCategory(int index)
    {
        for (int i = 0; i < NumCategories; i++)
        {
            UserControl categoryControl = _categoryControls[i];
            if (categoryControl != null)
                categoryControl.IsVisible = i == index;
        }
    }

    #region EventHandler

    private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox listBox)
            ShowCategory(listBox.SelectedIndex);
    }

    private void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    #endregion
}