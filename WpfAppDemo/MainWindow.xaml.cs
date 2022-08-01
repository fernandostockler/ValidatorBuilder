namespace WpfAppDemo;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        _ = LoginEmailTextBox.Focus();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        LoginPasswordBox.Clear();
        LoginEmailTextBox.Clear();
        _ = LoginEmailTextBox.Focus();
        ((MainWindowViewModel)DataContext).EmailTextBoxValue = string.Empty;
    }

    private void LoginEmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        => ((MainWindowViewModel)DataContext)
            .EmailTextBoxValue = ((TextBox)sender).Text;

    private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        => ((MainWindowViewModel)DataContext)
            .PasswordBoxValue = ((PasswordBox)sender).Password;
}