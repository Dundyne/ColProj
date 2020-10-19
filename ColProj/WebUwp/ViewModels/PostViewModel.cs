using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebUwp.Core.DataHandlers;
using WebUwp.Core.Models;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace WebUwp.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
        }
        /// <summary>Creates the post asynchronous.</summary>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        internal async Task CreatePostAsync(string title, string content)
        {
            var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
            var collectiveId = ReadSetting("CurrentCollective");
#pragma warning disable CA1305 // Specify IFormatProvider
            var post = new Post() { OwnerId = currentUser.Id, CollectiveId = Int32.Parse(collectiveId), Title = title, Content = content };
#pragma warning restore CA1305 // Specify IFormatProvider
            var postJson = JsonConvert.SerializeObject(post);
            var postStringContent = new StringContent(postJson, Encoding.UTF8, "application/json");
            await CrudHandler.CreatePost(postStringContent, ReadSetting("AuthInfo")).ConfigureAwait(false);
            postStringContent.Dispose();
        }

        /// <summary>Checks the Internet Connectivity</summary>
        public async void InternetConnectivity()
        {
            if (NetworkInformation.GetInternetConnectionProfile() == null)
            {
                await new MessageDialog("Features may be unavailable, please reconnect your Internet", "No Internet Connection").ShowAsync();
            }
        }
        /// <summary>Reads the setting.</summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns></returns>
        public string ReadSetting(string settingName)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Reading and returning your setting value
            var value = localSettings.Values[settingName];
#pragma warning disable CA1305 // Specify IFormatProvider
            var test = Convert.ToString(value);
#pragma warning restore CA1305 // Specify IFormatProvider
            if (value != null)

                return test;
            else
                return "";
        }
    }
}
