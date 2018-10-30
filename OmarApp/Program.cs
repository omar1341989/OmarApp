using OpenTidl.Methods;
using OpenTidl.Models;
using OpenTidl.Models.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenTidl.ClientConfiguration clientConfiguration = OpenTidl.ClientConfiguration.Default;
            OpenTidl.OpenTidlClient openTidlClient = new OpenTidl.OpenTidlClient(clientConfiguration);
            Task<OpenTidlSession> loginSession = openTidlClient.LoginWithUsername("omar13489@gmail.com", "Zubur123!");
            Console.WriteLine("Waiting Until the Login Complete...");
            loginSession.Wait();
            Console.WriteLine("The Login Process had been ended succesfully with this User Info : \n" 
                + "Date Of Birth : " + loginSession.Result.GetUser().Result.DateOfBirth + "\n" 
                + "First Name : " + loginSession.Result.GetUser().Result.FirstName + "\n"
                + "Last Name : " + loginSession.Result.GetUser().Result.LastName + "\n"
                + "Gender : " + loginSession.Result.GetUser().Result.Gender + "\n"
                + "Email : " + loginSession.Result.GetUser().Result.Email + "\n"
                + "Username : " + loginSession.Result.GetUser().Result.Username + "\n");

            Task<JsonList<ArtistModel>> artistsResults = openTidlClient.SearchArtists("Nancy Ajram", 0, 9999);
            
            foreach (var artistModel in artistsResults.Result.Items)
            {
                Console.WriteLine("The Artist Model ID : " + artistModel.Id);
                Console.WriteLine("The Artist Model Name : " + artistModel.Name);
                Console.WriteLine("The Artist Model URL : " + artistModel.Url + "\n");
            }

            Console.WriteLine("Log Out this user : " + loginSession.Result.GetUser().Result.Username);
            Task<EmptyModel> logoutResult = loginSession.Result.Logout();
            logoutResult.Wait();
            Console.ReadLine();
        }
    }
}
