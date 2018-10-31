using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmarApp;
using OpenTidl.Methods;
using OpenTidl.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmarApp.Tests
{
    [TestClass()]
    public class TestInfrastructureClassTests
    {
        OmarApp.TestInfrastructureClass testInfrastructureClass = new TestInfrastructureClass();
        OpenTidl.OpenTidlClient openTidlClient;
        static ArtistModel[] artistsList;


        [TestInitialize]
        public void Initialize()
        {
            OpenTidl.ClientConfiguration clientConfiguration = OpenTidl.ClientConfiguration.Default;
            openTidlClient = new OpenTidl.OpenTidlClient(clientConfiguration);
            artistsList = testInfrastructureClass.getAllArtists(openTidlClient, "Nancy Ajram");
        }

        [TestMethod()]
        public void loginUsertoTidalStreamingServiceTest()
        {
            String username = "omar13489@gmail.com";
            String password = "Zubur123!";

            OpenTidlSession loginSessionResult = testInfrastructureClass.loginUsertoTidalStreamingService(openTidlClient, username, password);

            String FirstName = loginSessionResult.GetUser().Result.FirstName;
            FirstName.Should().Equals("Omar");
            String LastName = loginSessionResult.GetUser().Result.LastName;
            LastName.Should().Equals("Alkhateeb");
            String email = loginSessionResult.GetUser().Result.Email;
            email.Should().Equals("omar13489@gmail.com");
        }

        [TestMethod()]
        public void getAllArtistsTest()
        {
            ArrayList artistIDsActual = new ArrayList();
            ArrayList artistIDsExpected = new ArrayList();
            artistIDsExpected.Add(3506179);
            artistIDsExpected.Add(7871351);
            artistIDsExpected.Add(9602331);
            artistsList = testInfrastructureClass.getAllArtists(openTidlClient, "Nancy Ajram");
            foreach (var artistModel in artistsList)
            {
                Console.WriteLine("The Artist Model ID : " + artistModel.Id);
                Console.WriteLine("The Artist Model Name : " + artistModel.Name);
                Console.WriteLine("The Artist Model URL : " + artistModel.Url + "\n");
                artistIDsActual.Add(artistModel.Id);
            }

            artistIDsActual.Should().BeEquivalentTo(artistIDsExpected);

        }

            

        [TestMethod()]
        public void getAllAlbumsTest()
        {
            ArrayList nancyAlbumsExpected = new ArrayList();
            ArrayList nancyAlbumsActual = new ArrayList();
            nancyAlbumsExpected.Add("Nancy Ajram: Super Hits Collection");
            nancyAlbumsExpected.Add("Sweet Songs");
            nancyAlbumsExpected.Add("Nancy 9");
            nancyAlbumsExpected.Add("Nancy 7");
            nancyAlbumsExpected.Add("Super Nancy");
            nancyAlbumsExpected.Add("Shakhbat Shakhabit");
            nancyAlbumsExpected.Add("Greatest Hits (Delux Edition)");
            nancyAlbumsExpected.Add("Greatest Hits of Nancy Ajram");
            nancyAlbumsExpected.Add("Greatest Hits");
            nancyAlbumsExpected.Add("Betfakar Fi Eih");
            nancyAlbumsExpected.Add("Shakhbat Shakhabit");
            nancyAlbumsExpected.Add("Ya Tabtab");
            nancyAlbumsExpected.Add("Ah W Noss");
            nancyAlbumsExpected.Add("Ya Salam");
            nancyAlbumsExpected.Add("Sheel Oyoonak Anni");
            nancyAlbumsExpected.Add("Mihtagalak");
            foreach (var artistModel in artistsList)
            {
                if(artistModel.Id.Equals(3506179))
                {
                    AlbumModel[] artistAlbums = testInfrastructureClass.getAllAlbums(openTidlClient, artistModel);
                    foreach (var albumModelObject in artistAlbums)
                    {
                        Console.WriteLine("\t Album Name : " + albumModelObject.Title);
                        nancyAlbumsActual.Add(albumModelObject.Title);
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


            }

            nancyAlbumsActual.Should().BeEquivalentTo(nancyAlbumsExpected);
        }
    }
}