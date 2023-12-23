using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//***********************
using System.Net;


namespace DragKurva
{
    public partial class Ipadform : Form
    {
        IPAddress ipadress;
        Int32 port = 0;

        //******************************************************
        public Ipadform()
        {
            InitializeComponent();
        }

       //******************************************************
        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                ipadress = IPAddress.Parse(tbIPadress.Text);
                port = Int32.Parse(tbPort.Text);
                this.Close();
            }
            catch
            {
                ipadress = null;
                port = 0;
            }

        }

        //******************************************************
        public IPAddress Ipadress
        {
            get { return ipadress; }
        }
        public Int32 Port
        {
            get { return port; }
        }

        //******************************************************
        private void btAvbryt_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}