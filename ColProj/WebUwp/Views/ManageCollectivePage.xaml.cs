using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ManageCollectivePage : Page
    {
        public DisplayDataViewModel ViewModel { get; } = new DisplayDataViewModel();
        private int SelectedIndex;
        public ManageCollectivePage()
        {
            this.InitializeComponent();
            Loaded += LoggedInPage_LoadedAsync;
        }



        private async void LoggedInPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadCollectivesAsync().ConfigureAwait(true);
            ViewModel.LoadYourCollectivesAsync();
            ViewModel.InternetConnectivity();
        }

        private void GoToCollective_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectivePage));
        }

        private async void DeleteCollective_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.GetCurrentUser().Id == ViewModel.GetCurrentCollective().OwnerId)
            {
                await ViewModel.DeleteCollectiveAsync().ConfigureAwait(true);
                await ViewModel.LoadCollectivesAsync().ConfigureAwait(true);
                ViewModel.LoadYourCollectivesAsync();
            }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            else ExceptionBlock.Text = "Please Pick a valid Collective";
#pragma warning restore CA1303 // Do not pass literals as localized parameters

        }

        private void UpdateCollective_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.GetCurrentUser().Id == ViewModel.GetCurrentCollective().OwnerId)
            {
                this.Frame.Navigate(typeof(UpdateCollectivePage));
            }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            else ExceptionBlock.Text = "Please Pick a valid Collective";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectiveListPage));
        }

        private void CollectiveList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = CollectiveList.SelectedIndex;
            SelectedIndex = index;
            if (SelectedIndex > -1)
            {
#pragma warning disable CA1305 // Specify IFormatProvider
                ViewModel.SaveSetting("CurrentCollective", ViewModel.YourCollectives[SelectedIndex].Id.ToString());
#pragma warning restore CA1305 // Specify IFormatProvider
            }

        }
    }
}
