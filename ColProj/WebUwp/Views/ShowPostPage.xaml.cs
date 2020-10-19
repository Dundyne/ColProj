using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ShowPostPage : Page
    {
        public DisplayDataViewModel ViewModel = new DisplayDataViewModel();
        public ShowPostPage()
        {
            this.InitializeComponent();
            Loaded += LoggedInPage_LoadedAsync;
        }



        private async void LoggedInPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
            await ViewModel.LoadUsersAsync().ConfigureAwait(true);
            await ViewModel.LoadPostsAsync().ConfigureAwait(true);
            await ViewModel.LoadCollectivesAsync().ConfigureAwait(true);
            Post currentPost = ViewModel.GetCurrentPost();
            Title.Text = currentPost.Title;
            Submitted.Text = "Submitted by: " + ViewModel.GetPostOwner(currentPost.OwnerId).Username;
            Content.Text = currentPost.Content;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectivePage));
        }
    }
}
