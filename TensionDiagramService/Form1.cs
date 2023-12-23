using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TensionDiagramService
{
    public partial class Form1 : Form
    {

        //private static ServiceHost serviceHost;
        private static TextBox tbHos;



        //********************************************************
        public Form1()
        {
            InitializeComponent();

        } // public Form1()

        //********************************************************
        private void btHostService_Click(object sender, EventArgs e)
        {

            //av mig----------------------
            // Self-host the service
            //tbHost.Text = StartServiceFromForm();
            //Application.DoEvents();
            StartServiceFromForm();

            //Console.WriteLine("Press Enter to exit.");
            //Console.ReadLine();





        } // private void btHostService_Click(object sender, EventArgs e)

        //********************************************************
        private void btStop_Click(object sender, EventArgs e)
        {
            // Stop the service when the user presses Enter
            StopServiceFromForm();
            Application.DoEvents();


        } // private void btStop_Click(object sender, EventArgs e)

        //********************************************************
        private  void StartServiceFromForm()
        {
            //DisplayTension disten = new DisplayTension();
            string dtex = DisplayTension.StartService();
           
            ////// Specify the base address for the service
            ////Uri baseAddress = new Uri("net.tcp://localhost:30000/TensionDiagramService.DisplayTension");

            ////// Create a ServiceHost instance
            ////serviceHost = new ServiceHost(typeof(TensionDiagramService.DisplayTension), baseAddress);
            //serviceHost = new ServiceHost(typeof(TensionDiagramService.DisplayTension));

            ////// Add a service endpoint
            ////serviceHost.AddServiceEndpoint(typeof(IDisplayTension), new NetTcpBinding(), "DisplayTension");

            ////// Add a service endpoint
            ////serviceHost.AddServiceEndpoint(typeof(IDisplayTension), new WSHttpBinding(), "DisplayTension");

            ////// Enable metadata exchange
            ////ServiceMetadataBehavior smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
            ////serviceHost.Description.Behaviors.Add(smb);

            //// Open the service host
            //serviceHost.Open();

            //string text = "Service is running at: " + serviceHost.BaseAddresses[0];
            string text = "Service is running ";
            tbHost.Text = dtex;
            Application.DoEvents();

            //return text;

        } // private static void StartService()

        //********************************************************
        private void StopServiceFromForm()
        {
            string dtex = DisplayTension.StopService();

            tbHost.Text = dtex;
            Application.DoEvents();

            //// Close the service host
            //if (serviceHost != null)
            //{
            //    serviceHost.Close();
            //}

            //string text = "Service closed at: " + serviceHost.BaseAddresses[0];

            //return text;


        } //  private static void StopService()

    }
}
