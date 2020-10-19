using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Text.RegularExpressions;
using WebUwp.Core.Models;
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
    public sealed partial class CreateCollectivePage : Page
    {
        public CollectiveViewModel ViewModel { get; } = new CollectiveViewModel();
        private readonly string namePattern = @"^[A-Za-z0-9]{1,15}$";
        private readonly string descriptionPattern = @"^[A-Za-z0-9]{1,30}$";
        private readonly string sizePattern = @"^[0-9]$";
        private bool isNameValid;
        private bool isDescriptionValid;
        private bool isSizeValid;
        public CreateCollectivePage()
        {
            this.InitializeComponent();
            Loaded += CreateCollectivePage_LoadedAsync;
        }



        private void CreateCollectivePage_LoadedAsync(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.InternetConnectivity();
        }

        private async void CreateCollective_Click(object sender, RoutedEventArgs e)
        {
            ExceptionBlock.Text = "";
            if (isNameValid && isDescriptionValid && isSizeValid)
            {
                try
                {
#pragma warning disable CA1305 // Specify IFormatProvider
                    await ViewModel.CreateCollectiveAsync(Name.Text, Description.Text, int.Parse(Size.SelectedItem.ToString())).ConfigureAwait(true);
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    ExceptionBlock.Text = "Collective successfully created!";
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                }
                catch (Exception ex)
                {

                    ExceptionBlock.Text = "Failed to create collective: " + ex;
                }
            }
            else
            {
                if (!isNameValid)
                {
                    ExceptionBlock.Text += "Name is wrong - Only letters, numbers, and underscore; 1 to 15 characters \n";
                }
                if (!isDescriptionValid)
                {
                    ExceptionBlock.Text += "Description is wrong - Only letters, numbers, and underscore; 1 to 30 characters \n";
                }
                if (!isSizeValid)
                {
                    ExceptionBlock.Text += "Size is wrong - Pick a number\n";
                }
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserPage));
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            isNameValid = Regex.IsMatch(Name.Text, namePattern);

            if (isNameValid)
            {
                Name.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isNameValid)
            {
                Name.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            isDescriptionValid = Regex.IsMatch(Description.Text, descriptionPattern);
            if (isDescriptionValid)
            {
                Description.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isDescriptionValid)
            {
                Description.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSizeValid = Regex.IsMatch(Size.SelectedItem.ToString(), sizePattern);
            if (isSizeValid)
            {
                Size.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            else if (!isSizeValid)
            {
                Size.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
