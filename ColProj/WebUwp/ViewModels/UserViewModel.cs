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
    public class UserViewModel : Observable
    {
        public UserViewModel()
        {
        }

        /// <summary>Registers the user asynchronous.</summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        internal async Task<bool> RegisterUserAsync(string firstname, string lastname, string username, string password)
        {
            var user = new UserDto() { FirstName = firstname, LastName = lastname, Username = username, Password = password };
            var userJson = JsonConvert.SerializeObject(user);
            var userStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            if (await CrudHandler.Register(userStringContent).ConfigureAwait(false))
            {
                userStringContent.Dispose();
                return true;
            }

            else userStringContent.Dispose();
            return false;

        }

        /// <summary>Logins the user asynchronous.</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        internal async Task<bool> LoginUserAsync(string username, string password)
        {

            var user = new UserDto() { Username = username, Password = password };
            var userJson = JsonConvert.SerializeObject(user);
            var userStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            User x = await CrudHandler.Login<User>(userStringContent).ConfigureAwait(false);
            var currentUserJson = JsonConvert.SerializeObject(x);
            userStringContent.Dispose();
            //var CurrentUserStringContent = new StringContent(CurrentUserJson, Encoding.UTF8, "application/json");
            if (x.Username == username)
            {
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                SaveSetting("AuthInfo", svcCredentials);
                SaveSetting("CurrentUser", currentUserJson);
                return true;
            }
            else return false;

        }

        /// <summary>Updates the user asynchronous.</summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        internal async Task<bool> UpdateUserAsync(string firstname, string lastname, string username, string password)
        {

            var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
            var user = new UserDto() { FirstName = firstname, LastName = lastname, Username = username, Password = password };
            var userJson = JsonConvert.SerializeObject(user);
            var userStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            if (await CrudHandler.PutGenericAsync(userStringContent, ReadSetting("AuthInfo"), new Uri("http://localhost:4000/Users/" + currentUser.Id)).ConfigureAwait(false))
            {
                userStringContent.Dispose();
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                SaveSetting("AuthInfo", svcCredentials);

                Uri UsersUri = new Uri("http://localhost:4000/Users/" + currentUser.Id);
                User x = await CrudHandler.GetGenericEntityAsync<User>(UsersUri, ReadSetting("AuthInfo")).ConfigureAwait(false);
                var CurrentUserJson = JsonConvert.SerializeObject(x);
                if (x.Username == username)
                {
                    SaveSetting("CurrentUser", CurrentUserJson);
                }
                return true;
            }
            else userStringContent.Dispose();
            return false;
        }

        /// <summary>Checks the Internet Connectivity</summary>
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
