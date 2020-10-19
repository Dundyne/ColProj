using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using WebUwp.Core.Models;
using WebUwp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class UserPage : Page
    {
        public DisplayDataViewModel ViewModel { get; } = new DisplayDataViewModel();
        public UserPage()
        {
            this.InitializeComponent();
            Loaded += UserPage_LoadedAsync;

        }

        private void UserPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            var currentUser = ViewModel.GetCurrentUser();
            if (currentUser != default(User))
            {
                Welcome.Text = "Welcome " + currentUser.Username + " !";
#pragma warning disable CA1305 // Specify IFormatProvider
                UserId.Text = "User ID: " + currentUser.Id.ToString();
#pragma warning restore CA1305 // Specify IFormatProvider
                FirstName.Text = "First Name: " + currentUser.FirstName;
                LastName.Text = "Last Name: " + currentUser.LastName;
                Username.Text = "Username: " + currentUser.Username;
            }
            else this.Frame.Navigate(typeof(MainPage));


            ViewModel.InternetConnectivity();
        }

        private void CreateCollective_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateCollectivePage));
        }

        private void JoinCollective_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(JoinCollectivePage));
        }

        private void YourCollectives_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectiveListPage));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Clear();
            this.Frame.Navigate(typeof(MainPage));
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateUserPage));
        }
    }
}
