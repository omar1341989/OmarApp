using OpenTidl.Methods;
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
            Debug.WriteLine("Waiting Until the Login Complete...");
            loginSession.Wait();
            Console.WriteLine("The Login Process had been ended succesfully with this Login Session " + loginSession.Result.);
            Debug.WriteLine("The Login Process had been ended succesfully with this Login Session " + loginSession.Result);
            Console.ReadLine();
        }
    }
}
