using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;

namespace Bifrost.Avalonia.Views.Dialogs;

public enum MessageBoxType
{
    OK,
    OKCancel,
}

public class MessageBoxSettings
{
    public string Title { get; init; } = "";
    public string Message { get; init; } = "";
    public MessageBoxType Type { get; init; } = MessageBoxType.OK;
}

public partial class MessageBoxWindow : Window
{
    public DialogResult Result { get; private set; } = DialogResult.None;

    public MessageBoxWindow()
    {
        InitializeComponent();
    }

    public MessageBoxWindow(MessageBoxSettings settings) : this()
    {
        Title = settings.Title;
        MessageTextBlock.Text = settings.Message;

        switch (settings.Type)
        {
            case MessageBoxType.OK:
                Grid.SetColumnSpan(OKButton, 2);
                CancelButton.IsVisible = false;
                break;

            case MessageBoxType.OKCancel:
                // no need to do anything as of right now
                break;
        }
    }

    public static async Task<DialogResult> Show(Window owner, string message, string title = "", MessageBoxType type = MessageBoxType.OK)
    {
        MessageBoxSettings settings = new()
        {
            Title = title,
            Message = message,
            Type = type,
        };

        MessageBoxWindow messageBoxWindow = new(settings);
        await messageBoxWindow.ShowDialog(owner);

        return messageBoxWindow.Result;
    }

    private void AcceptInput(DialogResult result)
    {
        Result = result;
        Close();
    }

    #region Event Handlers

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
        AcceptInput(DialogResult.OK);
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        AcceptInput(DialogResult.Cancel);
    }

    #endregion
}