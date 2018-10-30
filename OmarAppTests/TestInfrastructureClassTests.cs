using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmarApp;
using OpenTidl.Methods;
using System;
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
        [TestMethod()]
        public void loginUsertoTidalStreamingServiceTest()
        {
            OpenTidl.ClientConfiguration clientConfiguration = OpenTidl.ClientConfiguration.Default;
            OpenTidl.OpenTidlClient openTidlClient = new OpenTidl.OpenTidlClient(clientConfiguration);
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
            Assert.Fail();
        }

        [TestMethod()]
        public void getAllAlbumsTest()
        {
            Assert.Fail();
        }
    }
}