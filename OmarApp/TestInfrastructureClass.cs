using OpenTidl.Methods;
using OpenTidl.Models;
using OpenTidl.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmarApp
{
    public class TestInfrastructureClass
    {

        public OpenTidl.OpenTidlClient loginUsertoTidalStreamingService(String username, String password)
        {
            OpenTidl.OpenTidlClient openTidlClient = null;
            try
            {
                OpenTidl.ClientConfiguration clientConfiguration = OpenTidl.ClientConfiguration.Default;
                openTidlClient = new OpenTidl.OpenTidlClient(clientConfiguration);
                Task<OpenTidlSession> loginSession = openTidlClient.LoginWithUsername(username, password);
                Console.WriteLine("Waiting Until the Login Complete...");
                loginSession.Wait();
                Console.WriteLine("The Login Process had been ended succesfully with this User Info : \n"
                + "Date Of Birth : " + loginSession.Result.GetUser().Result.DateOfBirth + "\n"
                + "First Name : " + loginSession.Result.GetUser().Result.FirstName + "\n"
                + "Last Name : " + loginSession.Result.GetUser().Result.LastName + "\n"
                + "Gender : " + loginSession.Result.GetUser().Result.Gender + "\n"
                + "Email : " + loginSession.Result.GetUser().Result.Email + "\n"
                + "Username : " + loginSession.Result.GetUser().Result.Username + "\n");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            return openTidlClient;
        }

        public ArtistModel[] getAllArtists(OpenTidl.OpenTidlClient client, String artistName)
        {
            Task<JsonList<ArtistModel>> artistsResults = null;
            try
            {
                artistsResults = client.SearchArtists(artistName, 0, 100);
                Console.WriteLine("Wait until all the artists had been fetched correctly!");
                artistsResults.Wait();
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            
            return artistsResults.Result.Items;
        }

        public AlbumModel[] getAllAlbums(OpenTidl.OpenTidlClient client, ArtistModel artistModel)
        {
            Task<JsonList<AlbumModel>> artistAlbumsObject = null;
            try
            {
                artistAlbumsObject =
                client.GetArtistAlbums(artistModel.Id,
                                               OpenTidl.Enums.AlbumFilter.ALBUMS, 0, 200);
                Console.WriteLine("Wait until all the albums for the specific artist had been fetched correctly!");
                artistAlbumsObject.Wait();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            return artistAlbumsObject.Result.Items;
        }
    }
}
