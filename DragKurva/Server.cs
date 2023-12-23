using System;
using System.Collections.Generic;
using System.Text;
//*******************************
using System.Net;
using System.Net.Sockets;
using System.Collections;


namespace DragKurva
{
    class Server
    {
        public static String ipadress;
        public static Int32 ipport;
        public static TcpListener bbb;
        public String tensval;
        public String straival;
        public static ArrayList tens = new ArrayList();
        public static ArrayList strai = new ArrayList();

        public String Ipadress
        {
            get { return ipadress; }
            set { ipadress = value; }
        }
        public Int32 Ipport
        {
            get { return ipport; }
            set { ipport = value; }
        }
        public TcpListener Bbb
        {
            get { return bbb; }
            set { bbb = value; }
        }

        public String Tensval
        {
            set { tensval = value; tens.Add(tensval); }
        }
        public String Straival
        {
            set { straival = value; strai.Add(straival); }
        }
        public ArrayList Tens
        {
            get { return tens; }
        }
        public ArrayList Strai
        {
            get { return strai; }
        }

        //*************************************************
        public void ClearArrays()
        {
            Tens.Clear();
            Strai.Clear();
        }


    } // class Server

} // namespace DragKurva
