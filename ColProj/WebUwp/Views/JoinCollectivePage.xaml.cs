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
    public sealed partial class JoinCollectivePage : Page
    {
        public CollectiveViewModel ViewModel { get; } = new CollectiveViewModel();
        private readonly string joinPattern = @"^[0-9]*$";
        private bool isJoinValid;
        public JoinCollectivePage()
        {
            this.InitializeComponent();
            Loaded += JoinCollectivePage_LoadedAsync;
        }



        private void JoinCollectivePage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
        }

        private async void JoinCollective_Click(object sender, RoutedEventArgs e)
        {
            if (isJoinValid)
            {
                try
                {
#pragma warning disable CA1305 // Specify IFormatProvider
                    if (await ViewModel.JoinCollectiveAsync(Convert.ToInt32(CollectiveID.Text)).ConfigureAwait(true))
                    {
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                        ExceptionBlock.Text = "Successfully joined";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                    }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    else ExceptionBlock.Text = "Joining failed - invalid ID";
#pragma warning restore CA1303 // Do not pass literals as localized parameters

                }
                catch (Exception ex)
                {
                    ExceptionBlock.Text = "Registering Failed: " + ex;
                }
            }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            else ExceptionBlock.Text = "Please enter a valid ID";
#pragma warning restore CA1303 // Do not pass literals as localized parameters

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserPage));
        }

        private void CollectiveID_TextChanged(object sender, TextChangedEventArgs e)
        {
            isJoinValid = Regex.IsMatch(CollectiveID.Text, joinPattern);

            if (isJoinValid)
            {
                CollectiveID.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isJoinValid)
            {
                CollectiveID.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
