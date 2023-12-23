using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TensionDiagramService
{
    internal class DisplayTension : IDisplayTension
    {

        private static ServiceHost host;
        //private static TextBox tbHos;





        //********************************************************
        public static string StartService()
        {
            // Specify the base address for the service
            Uri baseAddress = new Uri("net.tcp://localhost:30000/TensionDiagramService.DisplayTension");

            //// Create a ServiceHost instance
            //host = new ServiceHost(typeof(TensionDiagramService.DisplayTension));
            host = new ServiceHost(typeof(TensionDiagramService.DisplayTension), baseAddress);

            // Open the service host
            host.Open();

            string text = "Service is running at: " + host.BaseAddresses[0];

            return text;

        } // private static string StartService()

        //********************************************************
        public static string StopService()
        {
            //// Specify the base address for the service
            //Uri baseAddress = new Uri("net.tcp://localhost:30000/TensionDiagramService.DisplayTension");

            //// Create a ServiceHost instance
            host = new ServiceHost(typeof(TensionDiagramService.DisplayTension));
            //ServiceHost host = new ServiceHost(typeof(TensionDiagramService.DisplayTension));

            //// Add a service endpoint
            //serviceHost.AddServiceEndpoint(typeof(IDisplayTension), new NetTcpBinding(), "DisplayTension");

            //// Add a service endpoint
            //serviceHost.AddServiceEndpoint(typeof(IDisplayTension), new WSHttpBinding(), "DisplayTension");

            //// Enable metadata exchange
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
            //serviceHost.Description.Behaviors.Add(smb);

            // Close the service host
            if (host != null)
            {
                host.Close();
            }

            string text = "Service at: " + host.BaseAddresses[0] + "  CLOSED";

            return text;


        }


        //********************************************************
        public void ResiveTensionMethod(DataPost datapost)
        {
            

        } // public void ResiveTensionMethod(DataPost datapost)
    }

    }
