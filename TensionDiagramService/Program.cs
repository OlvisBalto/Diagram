using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TensionDiagramService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        //private static ServiceHost serviceHost;


        static void Main()
        {
            //// automagenererat
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



            ////av mig----------------------
            //// Self-host the service
            //StartService();

            //Console.WriteLine("Press Enter to exit.");
            //Console.ReadLine();

            //// Stop the service when the user presses Enter
            //StopService();

        }

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
