using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using WebUwp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WebUwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public UserViewModel ViewModel { get; } = new UserViewModel();
        private readonly string namePattern = @"^[A-Za-z0-9]{1,15}$";
        private readonly string UsernamePattern = @"^[A-Za-z0-9]{5,15}$";
        private readonly string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        private bool isFirstNameValid;
        private bool isLastNameValid;
        private bool isUsernameNameValid;
        private bool isPasswordValid;
        public RegisterPage()
        {
            this.InitializeComponent();
            Loaded += RegisterPage_LoadedAsync;
        }



        private void RegisterPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ExceptionBlock.Text = "";
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string username = Username.Text;
            string password = Password.Password;
            if (isFirstNameValid && isLastNameValid && isUsernameNameValid && isPasswordValid)
            {
                try
                {
                    if (await ViewModel.RegisterUserAsync(firstName, lastName, username, password).ConfigureAwait(true))
                    {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                        ExceptionBlock.Text = "Successfully Registered";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                    }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    else ExceptionBlock.Text = "Registering Failed - Username may already exist";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                }
                catch (Exception ex)
                {

                    ExceptionBlock.Text = "Registering Failed: " + ex;
                }
            }
            else
            {
                if (!isFirstNameValid)
                {
                    ExceptionBlock.Text += "FirstName is wrong - Only letters, numbers, and underscore; 1 to 15 characters \n";
                }
                if (!isLastNameValid)
                {
                    ExceptionBlock.Text += "LastName is wrong - Only letters, numbers, and underscore; 1 to 15 characters \n";
                }
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



        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            isFirstNameValid = Regex.IsMatch(FirstName.Text, namePattern);

            if (isFirstNameValid)
            {
                FirstName.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isFirstNameValid)
            {
                FirstName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            isLastNameValid = Regex.IsMatch(LastName.Text, namePattern);

            if (isLastNameValid)
            {
                LastName.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isLastNameValid)
            {
                LastName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
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

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
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
    }
}
