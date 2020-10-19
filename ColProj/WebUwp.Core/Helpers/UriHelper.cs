using System;
using System.Collections.Generic;
using System.Text;

namespace WebUwp.Core.Helpers
{
    public static class UriHelper
    {
        //Users
        public static Uri UsersUri = new Uri("http://localhost:4000/Users/");
        public static Uri RegisterUri = new Uri("http://localhost:4000/Users/register");
        public static Uri AuthenticateUri = new Uri("http://localhost:4000/Users/authenticate");

        //Collective
        public static Uri CollectivesUri = new Uri("http://localhost:4000/Collectives/");
        public static Uri CollectiveCountUri = new Uri("http://localhost:4000/Collectives/collectivecount");

        //CollectiveUser
        public static Uri CollectiveUsersUri = new Uri("http://localhost:4000/CollectiveUsers/");

        //Posts
        public static Uri PostsUri = new Uri("http://localhost:4000/Posts/");

    }
}
