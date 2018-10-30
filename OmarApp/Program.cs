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

            Task<JsonList<ArtistModel>> artistsResults = openTidlClient.SearchArtists("Nancy Ajram", 0, 4);

            Console.WriteLine("Wait until all the artists had been fetched correctly!");
            artistsResults.Wait();

            foreach (var artistModel in artistsResults.Result.Items)
            {
                Console.WriteLine("The Artist Model ID : " + artistModel.Id);
                Console.WriteLine("The Artist Model Name : " + artistModel.Name);
                Console.WriteLine("The Artist Model URL : " + artistModel.Url + "\n");

                Task<JsonList<AlbumModel>> artistAlbumsObject = 
                openTidlClient.GetArtistAlbums(artistModel.Id,
                                               OpenTidl.Enums.AlbumFilter.ALBUMS,0,200);
                Console.WriteLine("Wait until all the albums for the specific artist had been fetched correctly!");
                artistAlbumsObject.Wait();

                Console.WriteLine("Albums for " + artistModel.Name);
                foreach (var albumModelObject in artistAlbumsObject.Result.Items)
                {
                    Console.WriteLine("\t Album Name : " + albumModelObject.Title);
                    Console.WriteLine("\t Album Artist Name : " + albumModelObject.Artist.Name);
                    Console.WriteLine("\t Album Duration : " + albumModelObject.Duration);
                    Console.WriteLine("\t Album ID : " + albumModelObject.Id);
                    Console.WriteLine("\t Album Number of Tracks : " + albumModelObject.NumberOfTracks);
                    Console.WriteLine("\t Album Number of Volumes : " + albumModelObject.NumberOfVolumes);
                    Console.WriteLine("\t Album Release Date : " + albumModelObject.ReleaseDate);
                    Console.WriteLine("\t Album Stream Start Date: " + albumModelObject.StreamStartDate);
                    Console.WriteLine("\t Album URL : " + albumModelObject.Url + "\n");
                }
            }

            String userName = loginSession.Result.GetUser().Result.Username;
            Console.WriteLine("Log Out this user : " + userName);
            Task<EmptyModel> logoutResult = loginSession.Result.Logout();
            Console.WriteLine("Wait until this user : " + userName + " Logged Out!");
            logoutResult.Wait();
            Console.WriteLine("The user : " + userName + " Logged Out Successfully!");
            Console.ReadLine();
        }
    }
}
