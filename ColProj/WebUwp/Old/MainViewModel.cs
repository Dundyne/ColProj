using WebUwp.Helpers;
using System.Collections.ObjectModel;
using WebUwp.Core.Models;
using System.Threading.Tasks;
using System;
using WebUwp.Core.Helpers;
using System.Collections.Generic;
using WebUwp.Core.DataHandlers;
using System.Net.Http;

using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace WebUwp.ViewModels
{
    public class MainViewModel : Observable
    {
        /*public User CurrentUser { get; set; }
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<Collective> Collectives { get; set; } = new ObservableCollection<Collective>();
        public ObservableCollection<CollectiveUser> CollectiveUsers { get; set; } = new ObservableCollection<CollectiveUser>();*/
        public MainViewModel()
        {
        }
        /*internal async Task LoadUsersAsync()
        {
            Users.Clear();
            IList<User> listUsers = await CrudHandler.GetGenericArrayAsync<User>(UriHelper.UsersUri, readSetting("AuthInfo")).ConfigureAwait(true);
            foreach (User user in listUsers)
                Users.Add(user);
        }

        internal async Task LoadCollectiveUsersAsync()
        {
            CollectiveUsers.Clear();
            IList<CollectiveUser> listCollectiveUsers = await CrudHandler.GetGenericArrayAsync<CollectiveUser>(UriHelper.CollectiveUsersUri, readSetting("AuthInfo")).ConfigureAwait(true);
            foreach (CollectiveUser collectiveuser in listCollectiveUsers)
                CollectiveUsers.Add(collectiveuser);
        }

        internal async Task LoadCollectivesAsync()
        {

            await LoadCollectiveUsersAsync();
            IList<CollectiveUser> relevantCollectiveUsers = new List<CollectiveUser>();
            var testUser = System.Text.Json.JsonSerializer.Deserialize<User>(readSetting("CurrentUser"));
            foreach (var collectiveuser in CollectiveUsers)
            {
                if (testUser.Id == collectiveuser.UserId)
                {
                    relevantCollectiveUsers.Add(collectiveuser);
                }
            }

            foreach (var collectiveuser in relevantCollectiveUsers)
            {
                 Uri CollectivesUri = new Uri("http://localhost:4000/Collectives/" + collectiveuser.CollectiveId);
                Collective t = await CrudHandler.GetGenericEntityAsync<Collective>(CollectivesUri, readSetting("AuthInfo"));
                Collectives.Add(t); 
            }
        }

        /*internal async void CreateCollectiveAsync(string name, string description, int size)
        {
            var testUser = System.Text.Json.JsonSerializer.Deserialize<User>(readSetting("CurrentUser"));
            var Collective = new Collective() { Name = name, Description = description, Size = size, OwnerId = testUser.Id };
            var CollectiveJson = JsonConvert.SerializeObject(Collective);
            var UserStringContent = new StringContent(CollectiveJson, Encoding.UTF8, "application/json");
            await CrudHandler.CreateCollective(UserStringContent, readSetting("AuthInfo"));

            int collectiveid = await CrudHandler.GetCount(UriHelper.CollectiveCountUri);
            JoinCollectiveAsync(collectiveid);
        }*/

        /*internal async void JoinCollectiveAsync(int collectiveid)
        {
            var testUser = System.Text.Json.JsonSerializer.Deserialize<User>(readSetting("CurrentUser"));
            var collectiveuser = new CollectiveUser() { CollectiveId = collectiveid, UserId = testUser.Id };
            var CollectiveuserJson = JsonConvert.SerializeObject(collectiveuser);
            var UserStringContent = new StringContent(CollectiveuserJson, Encoding.UTF8, "application/json");
            await CrudHandler.JoinCollective(UserStringContent, readSetting("AuthInfo"));
        }

        internal async void RegisterUserAsync(string firstname, string lastname, string username, string password)
        {
            var user = new UserDto() { FirstName =  firstname, LastName = lastname, Username = username, Password = password};
            var UserJson = JsonConvert.SerializeObject(user);
            var UserStringContent = new StringContent(UserJson, Encoding.UTF8, "application/json");
            await CrudHandler.Register(UserStringContent);
        }

        internal async Task<bool> LoginUserAsync(string username, string password)
        {
            var user = new UserDto() {Username = username, Password = password };
            var UserJson = JsonConvert.SerializeObject(user);
            var UserStringContent = new StringContent(UserJson, Encoding.UTF8, "application/json");
            User x = await CrudHandler.Login<User>(UserStringContent);
            var CurrentUserJson = JsonConvert.SerializeObject(x);
            var CurrentUserStringContent = new StringContent(CurrentUserJson, Encoding.UTF8, "application/json");
            if (x.Username == username)
            {
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                saveSetting("AuthInfo", svcCredentials);
                saveSetting("CurrentUser", CurrentUserJson);
                await LoadUsersAsync();

                return true;
            }
            else return false;
        }
        public void saveSetting(string settingName, string settingValue)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Saving your setting  
            localSettings.Values[settingName] = settingValue;
        }

        public string readSetting(string settingName)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Reading and returning your setting value
            var value = localSettings.Values[settingName];
            var test = Convert.ToString(value);
            if (value != null)

                return test;
            else
                return "";
        }
        */
    }
}
