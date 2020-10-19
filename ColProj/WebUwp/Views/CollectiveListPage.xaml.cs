using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WebUwp.Core.Models;
using WebUwp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;
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
    public sealed partial class CollectiveListPage : Page
    {
        public DisplayDataViewModel ViewModel { get; } = new DisplayDataViewModel();
        private int SelectedIndex;
        public CollectiveListPage()
        {
            this.InitializeComponent();
            Loaded += LoggedInPage_LoadedAsync;
        }



        private async void LoggedInPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
            await ViewModel.LoadCollectivesAsync().ConfigureAwait(true);

        }

        private void GoToCollective_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectivePage));
        }

        private async void LeaveCollective_Click(object sender, RoutedEventArgs e)
        {
            if (CollectiveList.SelectedItem != null)
            {
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
                await ViewModel.LeaveCollectiveAsync();
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
                await ViewModel.LoadCollectivesAsync().ConfigureAwait(true);
            }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            else ExceptionBlock.Text = "Please select a collective to leave";
#pragma warning restore CA1303 // Do not pass literals as localized parameters

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserPage));
        }

        private void CollectiveList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = CollectiveList.SelectedIndex;
            SelectedIndex = index;
            if (SelectedIndex > -1)
            {
#pragma warning disable CA1305 // Specify IFormatProvider
                ViewModel.SaveSetting("CurrentCollective", ViewModel.Collectives[SelectedIndex].Id.ToString());
#pragma warning restore CA1305 // Specify IFormatProvider
            }
        }

        private void ManageCollective_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageCollectivePage));
        }
    }
}
