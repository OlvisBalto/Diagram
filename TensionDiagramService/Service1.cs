using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TensionDiagramService
{
    public partial class Service1 : ServiceBase
    {
        //private static ServiceHost serviceHost;

        public Service1()
        {
            InitializeComponent();
        }


       

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //private void btServiceStart_Click(object sender, EventArgs e)
        //{
        //    //av mig----------------------
        //    // Self-host the service
        //    StartService();

        //    Console.WriteLine("Press Enter to exit.");
        //    Console.ReadLine();

        //    // Stop the service when the user presses Enter
        //    StopService();

        //}

        ////********************************************************
        //private static void StartService()
        //{
        //    // Specify the base address for the service
        //    Uri baseAddress = new Uri("net.tcp://localhost:30000/DisplayTension");

        //    // Create a ServiceHost instance
        //    serviceHost = new ServiceHost(typeof(DisplayTension), baseAddress);

        //    // Add a service endpoint
        //    serviceHost.AddServiceEndpoint(typeof(DisplayTension), new BasicHttpBinding(), "DisplayTension");

        //    //// Enable metadata exchange
        //    //ServiceMetadataBehavior smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
        //    //serviceHost.Description.Behaviors.Add(smb);

        //    // Open the service host
        //    serviceHost.Open();

        //    Console.WriteLine("Service is running at: " + baseAddress);

        //} // private static void StartService()

        ////********************************************************
        //private static void StopService()
        //{
        //    // Close the service host
        //    if (serviceHost != null)
        //    {
        //        serviceHost.Close();
        //    }

        //} //  private static void StopService()

    }
}
