using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebUwp.Core.DataHandlers;
using WebUwp.Core.Helpers;
using WebUwp.Core.Models;
using WebUwp.Helpers;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace WebUwp.ViewModels
{
    public class DisplayDataViewModel : Observable
    {
#pragma warning disable CA2227 // Collection properties should be read only
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning disable CA2227 // Collection properties should be read only
        public ObservableCollection<Collective> Collectives { get; set; } = new ObservableCollection<Collective>();
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning disable CA2227 // Collection properties should be read only
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning disable CA2227 // Collection properties should be read only
        public ObservableCollection<Collective> YourCollectives { get; set; } = new ObservableCollection<Collective>();
#pragma warning restore CA2227 // Collection properties should be read only

        public DisplayDataViewModel()
        {
        }


        /// <summary>Loads the collectives asynchronous.</summary>
        internal async Task LoadCollectivesAsync()
        {
            try
            {
                Collectives.Clear();
                var currentUser = JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
                Uri colUri = new Uri("http://localhost:4000/CollectiveUsers/RelevantCollectives/" + currentUser.Id);
                IList<Collective> colList = await CrudHandler.GetGenericArrayAsync<Collective>(colUri, ReadSetting("AuthInfo")).ConfigureAwait(true);
                foreach (var item in colList)
                {
                    Collectives.Add(item);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>Loads your collectives asynchronous.</summary>
        internal void LoadYourCollectivesAsync()
        {
            try
            {
                YourCollectives.Clear();
                var currentUser = JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
                foreach (var item in Collectives)
                {
                    if (currentUser.Id == item.OwnerId)
                    {
                        YourCollectives.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>Deletes the collective asynchronous.</summary>
        internal async Task DeleteCollectiveAsync()
        {
            try
            {
                var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
                var collective = GetCurrentCollective();
                if (currentUser.Id == collective.OwnerId)
                {
                    Uri coldelete = new Uri("http://localhost:4000/Collectives/" + collective.Id);
                    await CrudHandler.DeleteGenericAsync(ReadSetting("AuthInfo"), coldelete).ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>Leaves the collective asynchronous.</summary>
        internal async Task LeaveCollectiveAsync()
        {
            try
            {
                var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
                var collective = GetCurrentCollective();
                if (currentUser.Id != collective.OwnerId)
                {
                    Uri colLeave = new Uri("http://localhost:4000/CollectiveUsers/LeaveCollective/" + collective.Id + "+" + currentUser.Id);
                    await CrudHandler.DeleteGenericAsync(ReadSetting("AuthInfo"), colLeave).ConfigureAwait(true);
                }
                else await new MessageDialog("You cannot leave a collective that you own", "Cannot leave").ShowAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>Deletes the post asynchronous.</summary>
        /// <returns></returns>
        internal async Task<bool> DeletePostAsync()
        {
            var currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
            var post = GetCurrentPost();
            if (currentUser.Id == post.OwnerId)
            {
                Uri postdelete = new Uri("http://localhost:4000/Posts/" + post.Id);
                await CrudHandler.DeleteGenericAsync(ReadSetting("AuthInfo"), postdelete).ConfigureAwait(true);
                return true;
            }
            else return false;

        }

        /// <summary>Loads the users asynchronous.</summary>
        internal async Task LoadUsersAsync()
        {
            try
            {
                Users.Clear();
                Uri usrUri = new Uri("http://localhost:4000/CollectiveUsers/RelevantUsers/" + ReadSetting("CurrentCollective"));
                IList<User> users = await CrudHandler.GetGenericArrayAsync<User>(usrUri, ReadSetting("AuthInfo")).ConfigureAwait(true);

                foreach (var item in users)
                {
                    Users.Add(item);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>Loads the posts asynchronous.</summary>
        internal async Task LoadPostsAsync()
        {
            try
            {
                Posts.Clear();
                Uri postUri = new Uri("http://localhost:4000/Posts/RelevantPosts/" + ReadSetting("CurrentCollective"));
                IList<Post> postList = await CrudHandler.GetGenericArrayAsync<Post>(postUri, ReadSetting("AuthInfo")).ConfigureAwait(true);
                foreach (var item in postList)
                {
                    Posts.Add(item);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
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

        /// <summary>Gets the current user.</summary>
        /// <returns></returns>
        public User GetCurrentUser()
        {
            try
            {
                var currentUser = JsonSerializer.Deserialize<User>(ReadSetting("CurrentUser"));
                return currentUser;
            }
            catch (Exception)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                new MessageDialog("You must log in to access app features", "Not Logged In").ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                return default;
            }

        }

        /// <summary>Gets the current collective.</summary>
        /// <returns></returns>
        public Collective GetCurrentCollective()
        {
            try
            {
                Collective currentCollective = new Collective();
                foreach (var collective in Collectives)
                {
#pragma warning disable CA1305 // Specify IFormatProvider
                    if (collective.Id == Int32.Parse(ReadSetting("CurrentCollective")))
#pragma warning restore CA1305 // Specify IFormatProvider
                    {
                        currentCollective = collective;

                    }
                }
                return currentCollective;
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>Gets the current post.</summary>
        /// <returns></returns>
        public Post GetCurrentPost()
        {
            Post currentPost = new Post();
            foreach (var post in Posts)
            {
#pragma warning disable CA1305 // Specify IFormatProvider
                if (post.Id == Int32.Parse(ReadSetting("CurrentPost")))
#pragma warning restore CA1305 // Specify IFormatProvider
                {
                    currentPost = post;

                }
            }
            return currentPost;
        }

        /// <summary>Gets the post owner.</summary>
        /// <param name="ownerid">The ownerid.</param>
        /// <returns></returns>
        public User GetPostOwner(int ownerid)
        {
            User PostOwner = new User();
            foreach (var item in Users)
            {
                if (item.Id == ownerid)
                {
                    PostOwner = item;
                }
            }
            return PostOwner;
        }
        /// <summary>Checks the Internet Connectivity</summary>
        public async void InternetConnectivity()
        {
            if (NetworkInformation.GetInternetConnectionProfile() == null)
            {
                await new MessageDialog("Features may be unavailable, please reconnect your Internet", "No Internet Connection").ShowAsync();
            }
        }

        /// <summary>Clears this instance.</summary>
        public void Clear()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Saving your setting  
            localSettings.Values.Clear();
        }
    }
}
