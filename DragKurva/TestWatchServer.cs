using System;
using System.Collections.Generic;
using System.Text;
//************************
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace DragKurva
{
    class TestWatchServer
    {
        Int32 po;
        IPAddress locadr; 
        private TcpClient client = new TcpClient();
        private NetworkStream stream;
        private TcpListener server;
        private bool serverstarted = false;
        public Server s = new Server();

        private ArrayList strain = new ArrayList();
        private ArrayList tension = new ArrayList();

        private TextBox outputMessage;
        private TextBox outputYval;
        private TextBox outputXval;
        private TextBox outputID;
        private string sa = "servern lyssnar";
        private string xtbval = "x";
        private string ytbval = "y";
        private string id = "";
        private bool analysflag = false;
        private bool firstpost = true;
        private int fields = 4;

        // StreamDataHandle
        int n = 0;
        // är bufferten för liten snubblar servern vid mottagningen
        // leder till att retstr kastas om, 1 blir 2 och 2 blir 1, eller att
        // retstr får ett innehåll som tolkas som fel format i efterbearbetningen
        // bufferten sätts därför till 10000 byte
        Byte[] bytes = new Byte[10000]; 

        int i = 0;
        String[] retstr = new String[4];
        string temp = "";
        int num = 0;
        int f = 0;
        int datalength = 0;

        public bool Serverstarted
        {
            get { return serverstarted; }
            set { serverstarted = value; }
        }
        public string Sa
        {
            set { sa = value; }
        }
        public bool Analysflag
        {
            set { analysflag = value; }
        }
        
        private Loggar logg = new Loggar();

        //*****************************************************************************
        public TestWatchServer(TextBox tbDirectory, TextBox tbXvalue, TextBox tbYvalue, TextBox tbID)
        {      // i anropet heter dessa  textBox1,          tbXvalue,         tbYvalue,         tbID

            // koppla variablerna output* till den komponent
            // som skall ta emot meddelandet från InVoke
            outputMessage = tbDirectory;
            outputXval = tbXvalue;
            outputYval = tbYvalue;
            outputID = tbID;
        }

        //*****************************************************************************
        public void ServerStart()
        {
            Int32 port = s.Ipport;
            
            try
            {
                locadr = IPAddress.Parse(s.Ipadress.Trim());
            }
            catch
            {
                Ipadform Ipad = new Ipadform();
                Ipad.ShowDialog();
                locadr = Ipad.Ipadress;
                port = Ipad.Port;
            }

            if (locadr != null)
            {
                try
                {
                    if (serverstarted == false)
                    {
                        server = new TcpListener(locadr, port);
                        server.Start();
                        serverstarted = true;
                        s.Bbb = server;
                        /* ListeningStart startar en oändlig loop
                         * så länge tråden lever
                         * */
                        ListeningStart(server, serverstarted);
                    }
                    else
                    {
                        server = s.Bbb;
                        /* ListeningStart behöver inte anropas här
                         * lyssningsslingan startades första gången 
                         * radioknappen Provning aktiverades
                         * */
                    }

                } // try
                catch (SocketException ee)
                {
                    logg.Write("SocketException: " + ee);
                    server.Stop();
                }
            }
            else
            {

            }
        
            //finally
            //{
            //    server.Stop();
            //}

        } // private void ServerStart ()

            //*****************************************************************************
        private delegate void DisplayDelegate(string sa, string xtbval, string ytbval, string id);

            //*****************************************************************************
        private void DisplayString(string sa, string xtbval, string ytbval, string id)
        {
            outputMessage.Text = sa;
            outputID.Text = id;
            if (analysflag == false)
            {
                outputXval.Text = xtbval;
                outputYval.Text = ytbval;
            }
            else
            {
                outputXval.Text = "";
                outputYval.Text = "";
            }
        }

            //*****************************************************************************
        public void ListeningStart(TcpListener server, bool listenflag)
        {
            // Börja lysna efter klientanrop.
            while (listenflag == true)
            {
                sa = "servern lyssnar";
                outputMessage.Invoke(new DisplayDelegate(DisplayString), new object[] { sa, xtbval, ytbval, id });
                //logg.Write("Lägger sig att lyssna");
                while (!server.Pending())
                {
                    //////if (analysflag == false)
                    //////{
                    //////    //outputMessage.Invoke(new DisplayDelegate(DisplayString), new object[] { sa, xval, yval });
                    //////}
                }

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                /* om server.AcceptTcpClient() används ensamt utanför
                 * whileslingan lägger den sig i en oändlig loop och
                 * blockerar vidare hantering i programmet tills ett 
                 * clientanrop inkommer. Om så är fallet kommer tråden att vara 
                 * aktiv och aldrig släppa fram ett klick på radioknappen Analys
                 * */
                //logg.Write("Kopplar upp klient");
                client = server.AcceptTcpClient();
                try
                {
    //              logg.Write("StreamDataHandle()) in");
           //         n = 0;
                    String data = null;
                    data = null;
                    sa = "mottager";
                    // Loop to receive all the data sent by the client.
                    i = 0;
                    strain.Clear();
                    tension.Clear();
                    firstpost = true;
                    stream = client.GetStream();
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //logg.Write(data);
                        // Process the data sent by the client.
                        temp = "";
                        num = 0;
                        f = 0;
                        datalength = data.Length;
                        //logg.Write("datalength  " + datalength + "data  " + data);
                        while (num < datalength)
                        {
                            lock (this) // vet inte om detta har någon verkan 
                            {
                                temp = String.Copy(data.Substring(num, 1));
                                if (temp != ";" && temp != " ")
                                {
                                    retstr[f] = retstr[f] + temp;
                                }
                                else
                                {
                                    if (temp == ";")
                                    {
                                        f++;
                                    }
                                    if (f == fields)
                                    {
                                        if (firstpost == true)
                                        {
                                            id = retstr[1];
                                            firstpost = false;
                                        }
                                        // något = retstr[0];
                                        s.Tensval = retstr[1];
                                        s.Straival = retstr[2];
                                        // något = retstr[3];
                                        //logg.Write("ListeningStart()  retstr1=" + retstr[1] + "  retstr2=" + retstr[2]);

                                        ytbval = retstr[1];
                                        xtbval = retstr[2];

                                        for (int r = 0; r < fields; r++)
                                        {
                                            retstr[r] = "";
                                        }
                                        Monitor.PulseAll(this);

                                        if (analysflag == false)
                                        {
                                            outputMessage.Invoke(new DisplayDelegate(DisplayString), new object[] { sa, xtbval, ytbval, id });
                                        }
                                        f = 0;

                                    } // if (f == fields)

                                } // else  --if (temp != ";" && temp != " ")--
                            }

                            num++;

                        } // while (num < datalength )

                    } // while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)

                    sa = "slut";
                    ytbval = "slut";
                    outputMessage.Invoke(new DisplayDelegate(DisplayString), new object[] { sa, xtbval, ytbval, id });
                    
                    //// Send back a response.
                    //byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    //stream.Write(msg, 0, msg.Length);

                    // Shutdown and end connection
                    client.Close();
                    SaveTRAcurves(s.Tens, s.Strai);
                    //SaveMesTension();
                    s.ClearArrays();
                    //logg.Write("client.Close() klar");
                }
                catch (SocketException ee)
                {
                    logg.Write("Datasrömmen försvann" + ee);
                }
            } // while (listenflag == true)

        } // private void StreamDataHandle()


        //*****************************************************************************
        public void SaveTRAcurves(ArrayList tension, ArrayList strain)
        {
            //string id = tension[0].ToString();
            //String line = "";
            //using (StreamWriter tra = new StreamWriter("C:\\DragKurvor\\Loggar\\" + "TRA" + id + ".txt"))
            //{
            //    line = "Standard force;   True Strain;  True tension;Change in widt; Standard path;      TestTime";
            //    tra.WriteLine(line);

            //    for (int i = 0; i < tension.Count; i++)
            //    {
            //        line = " sf; " + strain[i].ToString() + "; " + tension[i].ToString() + ";   cw;   sp;   tt";
            //        tra.WriteLine(line);

            //    }
            //    tra.Close();
            //}
            
        }

        //*****************************************************************************
        public void SaveMesTension()
        {
            
        }

        //*****************************************************************************
        public void ServerStop()
        {
            //server.Stop();
            //serverstarted = false;
        }

    } // class TestWatchServer

} // namespace DragKurva
