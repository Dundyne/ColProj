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
    public sealed partial class CreatePostPage : Page
    {
        public PostViewModel ViewModel { get; } = new PostViewModel();
        private readonly string titlePattern = @"^[A-Za-z0-9]{1,15}$";
        private readonly string contentPattern = @"^[A-Za-z0-9?=!.,*]{1,}$";
        private bool isTitleValid;
        private bool isContentValid;
        public CreatePostPage()
        {
            this.InitializeComponent();
            Loaded += CreatePostPage_LoadedAsync;
        }



        private void CreatePostPage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
        }


        private async void CreatePost_Click(object sender, RoutedEventArgs e)
        {
            ExceptionBlock.Text = "";
            if (isTitleValid && isContentValid)
            {
                try
                {
                    await ViewModel.CreatePostAsync(Title.Text, Content.Text).ConfigureAwait(true);
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    ExceptionBlock.Text = "Post successfully created!";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                }
                catch (Exception ex)
                {

                    ExceptionBlock.Text = "Failed to create post: " + ex;
                }
            }
            else
            {
                if (!isTitleValid)
                {
                    ExceptionBlock.Text += "Title is wrong - Only letters, numbers; 1 to 15 characters \n";
                }
                if (!isContentValid)
                {
                    ExceptionBlock.Text += "Content is wrong - Only letters, numbers, and (?=!.,*); Minimum 1 character\n";
                }
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CollectivePage));
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            isTitleValid = Regex.IsMatch(Title.Text, titlePattern);

            if (isTitleValid)
            {
                Title.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isTitleValid)
            {
                Title.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            isContentValid = Regex.IsMatch(Content.Text, contentPattern);

            if (isContentValid)
            {
                Content.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isContentValid)
            {
                Content.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
    }

}
