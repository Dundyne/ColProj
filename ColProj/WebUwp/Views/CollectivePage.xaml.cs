using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WebUwp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class CollectivePage : Page
    {
        public DisplayDataViewModel ViewModel = new DisplayDataViewModel();
        private int SelectedIndex;
        public CollectivePage()
        {
            this.InitializeComponent();
            Loaded += CollectivePage_LoadedAsync;
        }



        private async void CollectivePage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
            await ViewModel.LoadUsersAsync().ConfigureAwait(true);
            await ViewModel.LoadCollectivesAsync().ConfigureAwait(true);
            await ViewModel.LoadPostsAsync().ConfigureAwait(true);
            if (ViewModel.GetCurrentCollective() != default)
            {
                Welcome.Text = ViewModel.GetCurrentCollective().Name;
            }
            else
            {
                this.Frame.Navigate(typeof(CollectiveListPage));
                await new MessageDialog("This collective does not exist", "Invalid Collective").ShowAsync();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserPage));
        }

        private void CreatePost_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(CreatePostPage));

        }

        private void PostsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = PostsList.SelectedIndex;
            SelectedIndex = index;
            if (SelectedIndex > -1)
            {
#pragma warning disable CA1305 // Specify IFormatProvider
                ViewModel.SaveSetting("CurrentPost", ViewModel.Posts[SelectedIndex].Id.ToString());
#pragma warning restore CA1305 // Specify IFormatProvider
            }

        }

        private void OpenPost_Click(object sender, RoutedEventArgs e)
        {
            if (PostsList.SelectedItem != null)
            {
                this.Frame.Navigate(typeof(ShowPostPage));
            }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            else ExceptionBlock.Text = "Please select a post";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        }

        private async void DeletePost_Click(object sender, RoutedEventArgs e)
        {
            if (PostsList.SelectedItem != null)
            {
                if (await ViewModel.DeletePostAsync().ConfigureAwait(true))
                {
                    await ViewModel.LoadPostsAsync().ConfigureAwait(true);
                }
                else
                {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    ExceptionBlock.Text = "Cannot delete this post";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                }

            }
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            else ExceptionBlock.Text = "Please select a post";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        }
    }
}
