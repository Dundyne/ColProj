using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebUwp.Core.DataHandlers;
using WebUwp.Core.Helpers;
using WebUwp.Core.Models;
using WebUwp.Helpers;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace WebUwp.ViewModels
{
    public class CollectiveViewModel : Observable
    {
        public CollectiveViewModel()
        {
        }


        /// <summary>Creates the collective asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="size">The size.</param>
        internal async Task CreateCollectiveAsync(string name, string description, int size)
        {
            var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
            var collective = new Collective() { Name = name, Description = description, Size = size, OwnerId = currentUser.Id };
            var collectiveJson = JsonConvert.SerializeObject(collective);
            var collectiveStringContent = new StringContent(collectiveJson, Encoding.UTF8, "application/json");
            Collective x = await CrudHandler.CreateCollective<Collective>(collectiveStringContent, ReadSetting("AuthInfo")).ConfigureAwait(false);
            collectiveStringContent.Dispose();
            await JoinCollectiveAsync(x.Id).ConfigureAwait(true);

        }



        /// <summary>Joins the collective asynchronous.</summary>
        /// <param name="collectiveid">The collectiveid.</param>
        /// <returns></returns>
        internal async Task<bool> JoinCollectiveAsync(int collectiveid)
        {
            var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
            var collectiveuser = new CollectiveUser() { CollectiveId = collectiveid, UserId = currentUser.Id };
            var collectiveuserJson = JsonConvert.SerializeObject(collectiveuser);
#pragma warning disable CA2000 // Dispose objects before losing scope
            var userStringContent = new StringContent(collectiveuserJson, Encoding.UTF8, "application/json");
#pragma warning restore CA2000 // Dispose objects before losing scope
            if (await CrudHandler.JoinCollective(userStringContent, ReadSetting("AuthInfo")).ConfigureAwait(true))
            {
                userStringContent.Dispose();
                return true;
            }
            else return false;


        }

        /// <summary>Updates the collective asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        internal async Task<bool> UpdateCollectiveAsync(string name, string description, int size)
        {
            var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
#pragma warning disable CA1305 // Specify IFormatProvider
            var collective = new Collective() { Id = Int32.Parse(ReadSetting("CurrentCollective")), OwnerId = currentUser.Id, Name = name, Description = description, Size = size };
#pragma warning restore CA1305 // Specify IFormatProvider
            Uri collectiveUri = new Uri("http://localhost:4000/Collectives/" + collective.Id);
            var collectiveJson = JsonConvert.SerializeObject(collective);
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            Collective currentCollective = await CrudHandler.GetGenericEntityAsync<Collective>(collectiveUri, ReadSetting("AuthInfo"));
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            var collectiveStringContent = new StringContent(collectiveJson, Encoding.UTF8, "application/json");
            if (currentUser.Id == currentCollective.OwnerId)
            {
                await CrudHandler.PutGenericAsync(collectiveStringContent, ReadSetting("AuthInfo"), collectiveUri).ConfigureAwait(false);
                collectiveStringContent.Dispose();
                return true;
            }
            else
            {
                collectiveStringContent.Dispose();
                await new MessageDialog("You are not allowed to edit this Collective, please pick one from the Manage Collectives Page", "Not allowed").ShowAsync();
                return false;
            }
        }
        /// <summary>  Checks the Internet Connectivity</summary>
        public async void InternetConnectivity()
        {
            if (NetworkInformation.GetInternetConnectionProfile() == null)
            {
                await new MessageDialog("Features may be unavailable, please reconnect your Internet", "No Internet Connection").ShowAsync();
            }
        }

        /// <summary>Saves the setting.</summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="settingValue">The setting value.</param>
        public void SaveSetting(string settingName, string settingValue)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Saving your setting  
            localSettings.Values[settingName] = settingValue;
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
