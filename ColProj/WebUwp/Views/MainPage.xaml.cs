using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using WebUwp.Helpers;
using WebUwp.ViewModels;
using Windows.Networking.Connectivity;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WebUwp.Views
{
    public sealed partial class MainPage : Page
    {
        public UserViewModel ViewModel { get; } = new UserViewModel();

        private string UsernamePattern = @"[A-Za-z0-9]{5,15}";
        private string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        private bool isUsernameNameValid;
        private bool isPasswordValid;
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_LoadedAsync;

        }



        private void MainPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            isUsernameNameValid = Regex.IsMatch(Username.Text, UsernamePattern);
            if (isUsernameNameValid)
            {
                Username.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isUsernameNameValid)
            {
                Username.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void Password_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            isPasswordValid = Regex.IsMatch(Password.Password, passwordPattern);

            if (isPasswordValid)
            {
                Password.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isPasswordValid)
            {
                Password.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private async void LoginButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ExceptionBlock.Text = "";
            if (isUsernameNameValid && isPasswordValid)
            {
                try
                {
                    if (await ViewModel.LoginUserAsync(Username.Text, Password.Password).ConfigureAwait(true))
                    {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                        ExceptionBlock.Text = "Login Successful";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                        this.Frame.Navigate(typeof(UserPage));
                    }
                }
                catch (Exception ex)
                {
                    ExceptionBlock.Text = ex.Message;
                }
            }
            else
            {
                if (!isUsernameNameValid)
                {
                    ExceptionBlock.Text += "Username is wrong - Only letters, numbers, and underscore; 5 to 15 characters \n";
                }
                if (!isPasswordValid)
                {
                    ExceptionBlock.Text += "Password is wrong - Minimum eight characters, at least one letter and one number \n";
                }
            }
        }

        private void RegisterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(RegisterPage));
        }

    }
}
