using System;
using System.Collections.Generic;
using WebUwp.Core.Models;
using WebUwp.Core.DataHandlers;
using WebUwp.Core.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace HailMary
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            /*IList<User> listStudents = await CrudHandler.GetGenericArrayAsync<User>(UriHelper.UsersUri);

            foreach (var user in listStudents)
            {
                Console.WriteLine(user.ToString());
            }
            */



            var userlogin = new UserDto()
            {
                Username = "ff",
                Password = "333"
            };

            //Convert UserDto Object to JSON data for use in a POST request
            var studentJson = JsonConvert.SerializeObject(userlogin);
            var studentStringContent = new StringContent(studentJson, Encoding.UTF8, "application/json");


            User x = await CrudHandler.Login<User>(studentStringContent);
            if (userlogin.Username == x.Username)
            {
                Console.WriteLine("Login successful");
            }



            var fn = Console.ReadLine();
        }
    }
}
