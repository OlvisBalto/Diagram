using System;
using System.Collections.Generic;
using System.Text;
//********************
using System.IO;
using System.Collections;
//using System.Math;


namespace DragKurva
{
    class CurveHandling
    {
        private ArrayList curv = new ArrayList();
        private ArrayList standardforce = new ArrayList();
        private ArrayList truestrain = new ArrayList();
        private ArrayList truetension = new ArrayList();
        private ArrayList changeinwidth = new ArrayList();
        private ArrayList standardpath = new ArrayList();
        private ArrayList testtime = new ArrayList();
        private ArrayList emodul = new ArrayList();
        private ArrayList emodulvel = new ArrayList();
        private ArrayList emodulacc = new ArrayList();
        private ArrayList lines = new ArrayList();
        private ArrayList points = new ArrayList();
        private bool analysemode = true;
        private bool testmode = false;
        private String curvemarkX = "";
        private String curvemarkY = "";
        private int datastartindex = 6;
        private String[][] visablecurves = new String[1][];
        private string endflag = "";

        private double curveXmax = 0;
        private double curveXmin = 0;
        private double curveYmax = 0;
        private double curveYmin = 0;
        private double emax = 0.0;
        private double exmax = 0.0;
        private String tbemodul = "";
        private double plotvalemaxYtop = 0.0;
        private double plotvalemaxXtop = 0.0;
        private double plotvalemaxYbottom = 0.0;
        private double plotvalemaxXbottom = 0.0;
        private int plotvalexmaxXtopindex = 0;
        private int plotvalexmaxXbottomindex = 0;

        private double rmval = 0.0;
        private String tbrm = "";
        private double plotvalrmY;
        private double plotvalrmX;
        
        private double rp02val = 0.0;
        private String tbrp02 = "";
        private double plotvalrp02Y;
        private double plotvalrp02X;

        private double rehval = 0.0;
        private String tbreh = "";
        private double plotvalrehY;
        private double plotvalrehX;

        private double relval = 0.0;
        private String tbrel = "";
        private double plotvalrelY;
        private double plotvalrelX;

        private double agval = 0.0;
        private String tbag = "";
        private double plotvalagY;
        private double plotvalagX;

        private double a80val = 0.0;
        private String tba80 = "";
        private double plotvala80Y;
        private double plotvala80X;

        private Loggar logg = new Loggar();

        public ArrayList StandardForce
        {
            set { standardforce = value; }
        }
        public ArrayList Truestrain
        {
            set { truestrain = value; }
        }
        public ArrayList Truetension
        {
            set { truetension = value; }
        }
        public ArrayList Changeinwidth
        {
            set { changeinwidth = value; }
        }
        public ArrayList Standardpath
        {
            set { standardpath = value; }
        }
        public ArrayList Testtime
        {
            set { testtime = value; }
        }
        public ArrayList Emodul
        {
            set { emodul = value; }
        }
        public ArrayList Emodulvel
        {
            set { emodulvel = value; }
        }
        public ArrayList Emodulacc
        {
            set { emodulacc = value; }
        }
        public bool Analysemode
        {
            set { analysemode = value; }
        }
        public bool Testmode
        {
            set { testmode = value; }
        }
        public string Endflag
        {
            set { endflag = value; }
        }

        //**********************************************************************
        public void HandleCurves(String rX, ArrayList rY, String xmax, String xmin, String fk, String tbemax, String tbemin)
        {
            //for (int f = 0; f < rY.Count; f++)
            //{
            //    logg.Write("HandleCurves  rX " + rX + "    rY " + rY[f]);
            //}
            //M�TV�RDENA FR�N FIL FINNS NU I VAR SIN ArrayList
            //SKAPA kurvinformationslista med invalda kurvor f�r visning
            bool parsingerror = false;
            String[][] curves = new String[1][];
            String[][] cpoints = new String[1][];
            int curvetop = 0;
            /* curves-formatet p� listan med kurvor f�r visning 
             * kurva 1 [0][0] X-mark
             * kurva 1 [0][1] Y-mark
             * kurva 1 [1][0] X-kurva namn
             * kurva 1 [1][1] Y-kurva namn
             * kurva 1 [2][0] curveXmax
             * kurva 1 [2][1] curveYmax
             * kurva 1 [3][0] curveXmin
             * kurva 1 [3][1] curveYmin
             * kurva 1 [4][0] curveXflytmedel
             * kurva 1 [4][1] curveYflytmedel
             * kurva 1 [5][0] antal X-punkter
             * kurva 1 [5][1] antal Y-punkter = X-punkter
             * kurva 1 [6 osv][0] X-v�rde
             * kurva 1 [6 osv][1] Y-v�rde
             * kurva 2 [kurva 1+0][0] X-mark
             * kurva 2 [kurva 1+0][1] Y-mark
             * kurva 2 [kurva 1+1][0] X-kurva namn
             * kurva 2 [kurva 1+1][1] Y-kurva namn
             * kurva 2 [kurva 1+2][0] curveXmax
             * kurva 2 [kurva 1+2][1] curveYmax
             * kurva 2 [kurva 1+3][0] curveXmin
             * kurva 2 [kurva 1+3][1] curveYmin
             * kurva 2 [kurva 1+4][0] curveXflytmedel
             * kurva 2 [kurva 1+4][1] curveYflytmedel
             * kurva 2 [kurva 1+5][0] antal X-punkter
             * kurva 2 [kurva 1+5][1] antal Y-punkter = X-punkter
             * kurva 2 [kurva 1+6 osv][0] X-v�rde
             * kurva 2 [kurva 1+6 osv][1] Y-v�rde
             * osv
             * */
            int mark = 0;
            //int name = 1;
            int max = 2;
            int min = 3;
            int flyt = 4;
            //int items = 5;
            

            // skapa inledande info f�r kurvan
            for (int i = 0; i < rY.Count; i++)
            {
                if (rX == "ForceX")
                {
                    curvemarkX = standardforce[mark].ToString();
                    curveXmax = double.Parse(standardforce[max].ToString());
                    curveXmin = double.Parse(standardforce[min].ToString());
                }
                else if (rX == "StrainX")
                {
                    curvemarkX = truestrain[mark].ToString();
                    curveXmax = double.Parse(truestrain[max].ToString());
                    curveXmin = double.Parse(truestrain[min].ToString());
                }
                else if (rX == "TensionX")
                {
                    curvemarkX = truetension[mark].ToString();
                    curveXmax = double.Parse(truetension[max].ToString());
                    curveXmin = double.Parse(truetension[min].ToString());
                }
                else if (rX == "chWidthX")
                {
                    curvemarkX = changeinwidth[mark].ToString();
                    curveXmax = double.Parse(changeinwidth[max].ToString());
                    curveXmin = double.Parse(changeinwidth[min].ToString());
                }
                else if (rX == "PathX")
                {
                    curvemarkX = standardpath[mark].ToString();
                    curveXmax = double.Parse(standardpath[max].ToString());
                    curveXmin = double.Parse(standardpath[min].ToString());
                }
                // s�tt manuellt best�mda xgr�nser
                if (xmax != "Auto")
                {
                    try
                    {
                        curveXmax = double.Parse(xmax);
                    }
                    catch
                    {
                        //curveXmax = 0.0;
                        parsingerror =true;
                    }
                }
                else
                {
                    // do nothing
                    //curveXmax har r�tt v�rde
                }

                if (xmin != "Auto")
                {
                    try
                    {
                        curveXmin = double.Parse(xmin);
                    }
                    catch
                    {
                        //curveXmin = 0.0;
                        parsingerror = true;
                    }
                }
                else
                {
                    // do nothing
                    //curveXmin har r�tt v�rde
                }

                //----------------------------
                if (rY[i].ToString() == "ForceY")
                {
                    curvemarkY = standardforce[mark].ToString();
                    curveYmax = double.Parse(standardforce[max].ToString());
                    curveYmin = double.Parse(standardforce[min].ToString());
                }
                else if (rY[i].ToString() == "StrainY")
                {
                    curvemarkY = truestrain[mark].ToString();
                    curveYmax = double.Parse(truestrain[max].ToString());
                    curveYmin = double.Parse(truestrain[min].ToString());
                }
                else if (rY[i].ToString() == "TensionY")
                {
                    curvemarkY = truetension[mark].ToString();
                    curveYmax = double.Parse(truetension[max].ToString());
                    curveYmin = double.Parse(truetension[min].ToString());
                }
                else if (rY[i].ToString() == "chWidthY")
                {
                    curvemarkY = changeinwidth[mark].ToString();
                    curveYmax = double.Parse(changeinwidth[max].ToString());
                    curveYmin = double.Parse(changeinwidth[min].ToString());
                }
                else if (rY[i].ToString() == "PathY")
                {
                    curvemarkY = standardpath[mark].ToString();
                    curveYmax = double.Parse(standardpath[max].ToString());
                    curveYmin = double.Parse(standardpath[min].ToString());
                }
                else if (rY[i].ToString() == "EmodulY")
                {
                    curvemarkY = emodul[mark].ToString();
                    curveYmax = double.Parse(emodul[max].ToString());
                    curveYmin = double.Parse(emodul[min].ToString());
                }
                else if (rY[i].ToString() == "EmodulvelY")
                {
                    curvemarkY = emodulvel[mark].ToString();
                    curveYmax = double.Parse(emodulvel[max].ToString());
                    curveYmin = double.Parse(emodulvel[min].ToString());
                }
                else if (rY[i].ToString() == "EmodulaccY")
                {
                    curvemarkY = emodulacc[mark].ToString();
                    curveYmax = double.Parse(emodulacc[max].ToString());
                    curveYmin = double.Parse(emodulacc[min].ToString());
                }
                
                // det finns en arraylist med m�tv�rden f�r respektive provegenskap
                // v�lj kurva utg�ende fr�n radioknapps- och checkknappsval
                ArrayList xval = new ArrayList();
                ArrayList yval = new ArrayList();
                int xflytmedel = 0;
                int yflytmedel = 0;
                
                if (rX == "ForceX")
                {
                    xval = standardforce;
                    xflytmedel = int.Parse(standardforce[flyt].ToString());
                }
                else if (rX == "StrainX")
                {
                    xval = truestrain;
                    xflytmedel = int.Parse(truestrain[flyt].ToString());
                }
                else if (rX == "TensionX")
                {
                    xval = truetension;
                    xflytmedel = int.Parse(truetension[flyt].ToString());
                }
                else if (rX == "chWidthX")
                {
                    xval = changeinwidth;
                    xflytmedel = int.Parse(changeinwidth[flyt].ToString());
                }
                else if (rX == "PathX")
                {
                    xval = standardpath;
                    xflytmedel = int.Parse(standardpath[flyt].ToString());
                }
                //---------------------

                if (rY[i].ToString() == "ForceY")
                {
                    yval = standardforce;
                    yflytmedel = int.Parse(standardforce[flyt].ToString());
                }
                else if (rY[i].ToString() == "StrainY")
                {
                    yval = truestrain;
                    yflytmedel = int.Parse(truestrain[flyt].ToString());
                }
                else if (rY[i].ToString() == "TensionY")
                {
                    yval = truetension;
                    yflytmedel = int.Parse(truetension[flyt].ToString());
                    if (analysemode == true || endflag == "slut")
                    {
                        // best�m Emodulen
                        SetEmodule(truestrain, truetension, tbemax, tbemin);
                        tbemodul = emax.ToString();
                        // skapa underlag f�r Rp02, Rm, Reh, Rel, Ag o A80
                        Lineparameters(emax, plotvalemaxXtop, truestrain, truetension);
                        Tensiondata(truestrain, truetension, lines, plotvalexmaxXtopindex);
                        tbrp02 = rp02val.ToString();
                        tbrm = rmval.ToString();
                        if (rehval != 0.0)
                        {
                            if (rehval > 0 && relval > 0)
                            {
                                tbreh = rehval.ToString();
                                tbrel = relval.ToString();
                            }
                            else
                            {
                                if (rehval > 0)
                                {
                                    tbreh = rehval.ToString();
                                }
                                else if (rehval < 0)
                                {
                                    tbreh = (rehval * (-1)).ToString() + " mysko";
                                }
                                if (relval > 0)
                                {
                                    tbrel = relval.ToString();
                                }
                                if (relval < 0)
                                {
                                    tbrel = (relval * (-1)).ToString() + " mysko";
                                }
                            }
                        } // if (rehval != 0.0)
                        else
                        {
                            tbreh = "Finns ej";
                            tbrel = "Finns ej";
                        }
                        tbag = agval.ToString();
                        tba80 = a80val.ToString();

                    } // if (analysemode == true)
                    else
                    {
                        // om man varit inne i analysmode kan
                        // lines och points inneh�lla data
                        // dessa skall inte visas i testmode
                        if ( lines.Count > 0) { lines.Clear(); }
                        if (points.Count > 0) { points.Clear(); }
                    }
                } // else if (rY[i].ToString() == "TensionY")
                else if (rY[i].ToString() == "chWidthY")
                {
                    yval = changeinwidth;
                    yflytmedel = int.Parse(changeinwidth[flyt].ToString());
                }
                else if (rY[i].ToString() == "PathY")
                {
                    yval = standardpath;
                    yflytmedel = int.Parse(standardpath[flyt].ToString());
                }
                else if (rY[i].ToString() == "EmodulY")
                {
                    Filter1(emodul, xval, Int32.Parse(fk));
                    yval = curv;
                    yflytmedel = int.Parse(emodul[flyt].ToString());
                }
                else if (rY[i].ToString() == "EmodulvelY")
                {
                    Filter1(emodulvel, xval, Int32.Parse(fk));
                    yval = curv;
                    yflytmedel = int.Parse(emodulvel[flyt].ToString());
                }
                else if (rY[i].ToString() == "EmodulaccY")
                {
                    Filter1(emodulacc, xval, 5);
                    yval = curv;
                    yflytmedel = int.Parse(emodulacc[flyt].ToString());
                }

                //************************************************************
                int pnumber = 0;
                pnumber = yval.Count;
                if (yval.Count == 0)
                {
                    // Do nothing cpoints redan dimensionerad
                }
                else
                {
                    // dimensionera kurvan till dess antal punkter
                    int points = 0;
                    pnumber = 0;
                    if (xval.Count >= yval.Count)
                    {
                        points = yval.Count;
                        String[][] cp = new String[curvetop + yval.Count][];
                        cpoints = cp;
                        // om antalet y-v�rden best�mmer hur m�nga punkter som kommer att kunna
                        // plottas m�ste curveXmax justeras till x-v�rdet vid maximalt y-index
                    }
                    else
                    {
                        points = xval.Count;
                        String[][] cp = new String[curvetop + xval.Count][];
                        cpoints = cp;
                    }
                    //         k�lla    m�l         antal
                    Array.Copy(curves, cpoints, curves.Length);
                    // n�sta kurvas data
                    cpoints[curvetop] = new String[2];
                    cpoints[curvetop][0] = curvemarkY;
                    cpoints[curvetop][1] = curvemarkY;
                    curvetop++;

                    cpoints[curvetop] = new String[2];
                    cpoints[curvetop][0] = rX;
                    cpoints[curvetop][1] = rY[i].ToString();
                    curvetop++;
                    
                    if (parsingerror == false)
                    {
                        cpoints[curvetop] = new String[2];
                        cpoints[curvetop][0] = curveXmax.ToString();
                        cpoints[curvetop][1] = curveYmax.ToString();
                        curvetop++;
                        cpoints[curvetop] = new String[2];
                        cpoints[curvetop][0] = curveXmin.ToString();
                        cpoints[curvetop][1] = curveYmin.ToString();
                        curvetop++;

                        
                    }
                    else
                    {
                        cpoints[curvetop] = new String[2];
                        cpoints[curvetop][0] = "0.0";
                        cpoints[curvetop][1] = "0.0";
                        curvetop++;
                        cpoints[curvetop] = new String[2];
                        cpoints[curvetop][0] = "0.0";
                        cpoints[curvetop][1] = "0.0";
                        curvetop++;
                    }
                    cpoints[curvetop] = new String[2];
                    cpoints[curvetop][0] = xflytmedel.ToString();
                    cpoints[curvetop][1] = yflytmedel.ToString();
                    curvetop++;
                    cpoints[curvetop] = new String[2];
                    cpoints[curvetop][0] = points.ToString();
                    cpoints[curvetop][1] = points.ToString();
                    curvetop++;

                    // data b�rjar p� index datastartindex
                    for (int k = datastartindex; k < points; k++)
                    {
                        cpoints[curvetop] = new String[2];
                        cpoints[curvetop][0] = xval[k].ToString();
                        cpoints[curvetop][1] = yval[k].ToString();
                        curvetop++;
                    }
                } // else  -- if (xval.Count >= yval.Count)
               
                curves = cpoints;

                //logg.Write("Curvehandling  curvesindex " + i);
                //logg.Write("rX " + rX + "    rY " + rY[i]);
                //logg.Write("r1  " + curves[0][0] + "r2  " + curves[0][1]);
                //logg.Write("r1  " + curves[1][0] + "r2  " + curves[1][1]);
                //logg.Write("r1  " + curves[2][0] + "r2  " + curves[2][1]);
                logg.Write("[3][0]  " + curves[3][0] + "[3][1]  " + curves[3][1]);
                //logg.Write("r1  " + curves[4][0] + "r2  " + curves[4][1]);
                //logg.Write("r1  " + curves[5][0] + "r2  " + curves[5][1]);
                //logg.Write("r1  " + curves[6][0] + "r2  " + curves[6][1]);
                //logg.Write("r1  " + curves[7][0] + "r2  " + curves[7][1]);


            } // for (int i = 0; i < rY.Count; i++)
            visablecurves = curves;
            
        } // public void Create(String dir, String mtrl)

        
        //*******************************************************************
        public String[][] Visablecurves
        {
            get { return visablecurves; }
        }
        public ArrayList Lines
        {
            get { return lines; }
        }
        public ArrayList Points
        {
            get { return points; }
        }
         public String Tbemodul
        {
            get { return tbemodul; }
            set { tbemodul = value; }
        }
        public String Tbrp02
        {
            get { return tbrp02; }
            set { tbrp02 = value; }
        }
        public String Tbreh
        {
            get { return tbreh; }
            set { tbreh = value; }
        }
        public String Tbrel
        {
            get { return tbrel; }
            set { tbrel = value; }
        }
        public String Tbrm
        {
            get { return tbrm; }
            set { tbrm = value; }
        }
        public String Tbag
        {
            get { return tbag; }
            set { tbag = value; }
        }
        public String Tba80
        {
            get { return tba80; }
            set { tba80 = value; }
        }


        //**************************************************************************
        //Filtrera datat i en kurva
        void Filter1(ArrayList ycur, ArrayList xcur, int ffpp)
        {
            curv = ycur;
            int fp = 0;
            double[] yd = new double[ffpp];
            double[] xd = new double[ffpp];

            if (ffpp == 0 )
            {
                ffpp = 2;
            }

            // data b�rjar p� index datastartindex
            for (int i = datastartindex; i < curv.Count - ffpp; i++)
            {
                fp = ffpp;
                // best�m lutningen fr punkt "i" till ett antal efterf�ljande punkter, "fp" = antalet punkter
                for (int k = 0; k < ffpp ; k++)
                {
                    if (double.Parse(xcur[i + k].ToString()) - double.Parse(xcur[i].ToString()) == 0.0 ||
                        double.Parse(xcur[i + k].ToString()) - double.Parse(xcur[i].ToString()) == Single.NaN)
                    {
                        yd[k] = 0.0;
                        xd[k] = double.Parse(xcur[i].ToString());
                        fp--;
                    }
                    else
                    {
                        // best�m lutningen
                        yd[k] = (double.Parse(curv[i + k].ToString()) - double.Parse(curv[i].ToString())) / (double.Parse(xcur[i + k].ToString()) - double.Parse(xcur[i].ToString()));
                        xd[k] = double.Parse(xcur[i + k].ToString()) - double.Parse(xcur[i].ToString());
                    }
                }
                double curv1 = 0.0;
                // summera lutningarna
                for (int k = 0; k < ffpp; k++)
                {
                    curv1 = curv1 + yd[k];
                }
                // skapa lutningsmedelv�rde
                if (fp == 0)
                {
                    curv1 = 0.0;
                }
                else
                {
                    curv1 = curv1 / fp;
                }

                //s�tt n�stkommande punkts v�rde mha lutningsmedelv�rdet
                double a = double.Parse(curv[i].ToString());
                double delta_a = 0.0;
                for ( int m = 1; m < ffpp; m++)
                {
                    if ( yd[m] == 0.0)
                    {
                        // do nothing
                    }
                    else 
                    {
                        delta_a = curv1 * xd[m] / m;
                    }
                }
                double b = a + delta_a;
                curv[i + 1] = b.ToString();

            } // for (int i = 0; i < curv.Count - ffpp; i++)

        } // LinjerFilter(ArrayList cur)


        //****************************************************************
        void SetEmodule(ArrayList strain, ArrayList tension, String emaxbox, String eminbox)
        {
            ArrayList ydata = new ArrayList();
            ArrayList xdata = new ArrayList();
            float y1proc = 0;   // y-v�rdet d�r emodulbest�mningen kan avslutas
            float x1proc = 0;   // x-v�rdet d�r emodulbest�mningen kan avslutas
            bool satt = false;

            // best�m y-v�dets �versta gr�ns d�r 
            // emodulbest�mningen kan avslutas
            // data b�rjar p� index datastartindex
            for (int i = datastartindex; i < strain.Count; i++)  // data b�rjar p� index 5
            {
                if (float.Parse(strain[i].ToString()) > 0.01 && satt == false)
                {
                    y1proc = float.Parse(tension[i].ToString());
                    x1proc = float.Parse(strain[i].ToString());
                    satt = true;
                }
            }

            // 20% av y1proc anv�nds som underlag f�r best�mning av Emodul
            float y12proc = y1proc * (float)0.2;    // sp�nningsintervallet som flyttas efter tensionkurvan
            float yproc = 0;
            float ydiff = 0;
            emax = 0.0;
            exmax = 0.0;
            // k �r startindex f�r intervallunders�kningen
            // datat i ArrayList-tension b�rjar p� index datastartindex
            int k = datastartindex;  
            // s� l�nge y-v�rdet �r l�gre �n den satta gr�nsen y1proc
            // g�r en ny unders�kning
            while (yproc < y1proc * 0.8)
            {
                xdata.Clear();
                ydata.Clear();
                ydiff = 0;
                // i �r index f�r punkter som stegas igenom i intervallet
                int i = k;  // i s�tts lika med startindex f�r intervallet
                // l�gg till "minstakvadaratdata" s� l�nge ydiff �r mindre �n 20% av y1proc
                while (ydiff < y12proc)
                {
                    ydiff = float.Parse(tension[i].ToString()) - float.Parse(tension[k].ToString());
                    ydata.Add(tension[i]);  //spara y-v�rdena som ing�r i intervallet
                    xdata.Add(strain[i]);   //spara x-v�rdena som ing�r i intervallet
                    i++;
                }
                // spara toppen av minstakvadratdata intervallet
                yproc = float.Parse(tension[i - 1].ToString());
                k++; // startindex �kas med 1



                // emodul enligt minsta kvadaratmetoden
                //E_LeastSquare(xval, yval);    // �nnu ej gjord




                // E_Sekant lutningen mellan max ydata och min ydata i aktuellt intervall
                // om lutningen �verstiger tidigare sparat v�rde ers�tter det med det sparade
                if (E_Sekant(xdata, ydata) > emax)
                {
                    emax = E_Sekant(xdata, ydata) - E_Sekant(xdata, ydata) % 1;
                    // spara samtidigt mittpunkten f�r strain i det unders�kta intervallet
                    exmax = double.Parse(strain[(k - 1) + (i - 1 - k) / 2].ToString());
                    // spara kurvplottens m�tv�rdespunkter f�r �vre och undre gr�ns i intervallet med max emodul
                    plotvalemaxYtop = yproc;
                    plotvalemaxXtop = float.Parse(strain[i - 1].ToString());
                    plotvalexmaxXtopindex = i - 1;
                    plotvalemaxYbottom = float.Parse(tension[k - 1].ToString());
                    plotvalemaxXbottom = float.Parse(strain[k - 1].ToString());
                    plotvalexmaxXbottomindex = k - 1;
                }

            } // while (yproc < y1proc)

            //om emodul_gr�nser har skrivits in i n�gon av textboxarna
            
            if (emaxbox != "Auto" || eminbox != "Auto" )
            {
                int m = datastartindex;      // m�tdata b�rjar p� index datastartindex
                int n = datastartindex;      // m�tdata b�rjar p� index datastartindex
                if (emaxbox != "Auto")
                {
                    while (float.Parse(tension[m].ToString()) < float.Parse(emaxbox))
                    {
                        m++;
                    }
                }
                else
                {
                    m = plotvalexmaxXtopindex;
                }
                if (eminbox != "Auto")
                {

                    while (float.Parse(tension[n].ToString()) < float.Parse(eminbox))
                    {
                        n++;
                    }
                }
                else
                {
                    n = plotvalexmaxXbottomindex;
                }
            
                //skapa ny xdata och ydata
                if (m != 0)
                {
                    plotvalexmaxXtopindex = m;
                }
                if (n != 0)
                {
                    plotvalexmaxXbottomindex = n;
                }
                ydata.Clear();
                xdata.Clear();
                for (int i = plotvalexmaxXbottomindex; i < plotvalexmaxXtopindex; i++)
                {
                    ydata.Add(tension[i]);  //spara y-v�rdena som ing�r i intervallet
                    xdata.Add(strain[i]);   //spara x-v�rdena som ing�r i intervallet
                }

                emax = E_Sekant(xdata, ydata) - E_Sekant(xdata, ydata) % 1;
                // spara samtidigt mittpunkten f�r strain i det unders�kta intervallet
                exmax = double.Parse(strain[(plotvalexmaxXbottomindex - 1) + (plotvalexmaxXtopindex - 1 - plotvalexmaxXbottomindex) / 2].ToString());
                // spara kurvplottens m�tv�rdespunkter f�r �vre och undre gr�ns i intervallet med max emodul
                plotvalemaxYtop = plotvalemaxYtop = float.Parse(tension[plotvalexmaxXtopindex - 1].ToString());
                plotvalemaxXtop = float.Parse(strain[plotvalexmaxXtopindex - 1].ToString());
                plotvalexmaxXtopindex = plotvalexmaxXtopindex - 1;
                plotvalemaxYbottom = float.Parse(tension[plotvalexmaxXbottomindex - 1].ToString());
                plotvalemaxXbottom = float.Parse(strain[plotvalexmaxXbottomindex - 1].ToString());
                plotvalexmaxXbottomindex = plotvalexmaxXbottomindex - 1;
            }
   
        } // void SetEmodule(double emod, double x, ArrayList strain, ArrayList tension)


        //****************************************************************
        void Lineparameters(double emod, double x, ArrayList strain, ArrayList tension)
        {
            lines.Clear();

            double C = 0.0;
            double k = 0.0;
            bool flag = false;
            double tens = 0.0;

            // x �r strain-mittpunkten i det intervall med st�rst emodul
            // leta r�tt p� sp�nningen vid x
            // data b�rjar p� index datastartindex i ArrayListorna
            for (int i = datastartindex; i < strain.Count; i++)
            {
                if (double.Parse(strain[i].ToString()) >= x && flag == false)
                {
                    // spara y-v�rdet tension
                    tens = double.Parse(tension[i].ToString());
                    flag = true;
                }
            }
            // OBS OBS OBS OBS OBS OBS
            // f�r att f� mittpunkten mer exakt borde en interpolation 
            // g�ras mellan tens och tttt. P�verkar dock ej intervallets
            // l�ge d�rf�r g�rs det ej. 
            // OBS OBS OBS OBS OBS OBS

            // r�t linje genom y-tension vid x f�r max emodul
            // best�m kurvkonstanten C
            C = tens - x * emod;
            k = emod;
            
            ArrayList emline = new ArrayList();
            emline.Add("Emodul");
            emline.Add(k);
            emline.Add(C);
            emline.Add(plotvalemaxXtop);
            emline.Add(plotvalemaxYtop);
            emline.Add(plotvalemaxXbottom);
            emline.Add(plotvalemaxYbottom);

            // r�t linje genom Rp02
            // best�m x- v�rde f�r y = 0
            // xcut(sk�rning med x-axel) �r lika med y = 0 minus kurvkonstant(C) dividerat med kurvlutningen k
            double xcut = (0.0 - C)/ k;
            // x f�r linjen genom Rp02
            double xrp = 0.0 + 0.002;
            double Crp = 0.0 - xrp * emod;

            ArrayList rpline = new ArrayList();
            rpline.Add("Rp02linje");
            rpline.Add(k);
            rpline.Add(Crp);

            // OBS OBS OBS OBS 
            // Rm �r �nnu ej framr�knat linjen f�r Ag s�tts d�rf�r senare
            // i paragrafen Tensiondata
            // OBS OBS OBS OBS 

            // sl� samman skapade linjer i en ArrayList f�r �verf�ring
            lines.Add(emline);
            lines.Add(rpline);
            /*lines
             * emline identifikation
             * emline k lutning emodul-linje
             * emline C konstant emodul-linje
             * emline pvalYtopemax intervalltoppX
             * emline pvalXtopemax intervalltoppY
             * emline pvalYbottomemax intervallbottenX
             * emline pvalXbottomemax intervallbottenY
             * rpline identifikation
             * rpline k lutning rp02-linje = emodul
             * rpline C konstant rp02-linje
             * */

        } // void Lineparameters(double emod, double x, ArrayList strain, ArrayList tension)

        //****************************************************************
        double E_Sekant(ArrayList xval, ArrayList yval)
        {
            double e = 0.0; // diffkvoten = emodul
            int p = 0;
            if (xval.Count <= 0 || yval.Count <= 0)
            {
                e = 0.0;
            }
            else
            {
                p = xval.Count - 1;
                double exdiff = float.Parse(xval[p].ToString()) - float.Parse(xval[0].ToString());
                double eydiff = float.Parse(yval[p].ToString()) - float.Parse(yval[0].ToString());
                if (exdiff != 0)
                {
                    e = eydiff / exdiff;
                }
                else
                {
                    e = 0.0;
                }
            }
            return e;

        } // double E_Sekant(ArrayList xval, ArrayList yval)
        
        //****************************************************************
        void E_LeastSquare(ArrayList xval, ArrayList yval)
        {

        } // void E_LeastSquare(ArrayList xval, ArrayList yval)

        //****************************************************************
        void Tensiondata(ArrayList straincur, ArrayList tenscur, ArrayList lin, int emodtopindex)
        {
            plotvalrp02X = 0;
            plotvalrp02Y = 0;
            plotvalrmX = 0;
            plotvalrmY = 0;
            plotvalrehX = 0;
            plotvalrehY = 0;
            plotvalrelX = 0;
            plotvalrelY = 0;
            plotvalagX = 0;
            plotvalagY = 0;
            plotvala80X = 0;
            plotvala80Y = 0;

            //-----------------------------------
            // Rp02
            double rp = 0.0;
            int rpindex = 0;
            bool satt = false;
            ArrayList rpline = new ArrayList();
            rpline = (ArrayList)lin[1];
            // data b�rjar p� index datastartindex
            for (int i = datastartindex; i < straincur.Count; i++)
            {
                rp = double.Parse(straincur[i].ToString()) * double.Parse(rpline[1].ToString()) + double.Parse(rpline[2].ToString());
                /* om sp�nningskurvans v�rde �r mindre �n v�rdet f�r rp02-kurvan 
                 * har man hittat rp02 vid t�jningen med index i
                 * sp�nningen s�tts till v�rdet f�r tenscur[i]
                 * egentligen skall sp�nning och t�jning s�ttas som sk�rningspunkten
                 * mellan linjerna tenscur[i - 1] till tenscur[i] och
                 * straincur[i - 1] till straincur[i]. H�r interpoleras dock bara t�jningen
                 * i n�sta steg d� felet genom att inte interpolera sp�nningen �r litet
                 * */
                if (double.Parse(tenscur[i].ToString()) < rp && satt == false)
                {
                    //double t = double.Parse(tenscur[i - 1].ToString());
                    //double tt = double.Parse(tenscur[i - 2].ToString());
                    rp02val = double.Parse(tenscur[i].ToString());
                    satt = true;
                    plotvalrp02Y = rp02val;
                    plotvalrp02X = float.Parse(straincur[i].ToString());
                    rpindex = i;
                }
            }
            // interpolera t�jningen
            if (rpindex <= 0)
            {
                // do nothing
            }
            else
            {
                double linetens1 = double.Parse(straincur[rpindex - 1].ToString()) * double.Parse(rpline[1].ToString()) + double.Parse(rpline[2].ToString());
                double linetens2 = double.Parse(straincur[rpindex].ToString()) * double.Parse(rpline[1].ToString()) + double.Parse(rpline[2].ToString());
                double curvetens = rp02val;
                double strain1 = double.Parse(straincur[rpindex - 1].ToString());
                double strain2 = double.Parse(straincur[rpindex].ToString());
                double interpolatedstrain = (curvetens / (linetens2 - linetens1) * (strain2 - strain1) + 1) * strain1;
                plotvalrp02X = interpolatedstrain;
            }

            rp02val = rp02val - rp02val % 1;

            //logg.Write("---------------------------");
            //logg.Write("rp02 " + rp02val);

            //-----------------------------------
            // Rm
            rmval = 0.0;
            int rmindex = 0;
            if (rpindex < datastartindex)
            {
                rpindex = datastartindex;    // m�tdata b�rjar p� index datastartindex
            }
            for (int i = rpindex; i < straincur.Count; i++)
            {
                if (double.Parse(tenscur[i].ToString()) > rmval)
                {
                    rmval = double.Parse(tenscur[i].ToString());
                    plotvalrmY = rmval;
                    plotvalrmX = float.Parse(straincur[i].ToString());
                    rmindex = i;
                }
            }
            rmval = rmval - rmval % 1;
            //logg.Write("rm " + rmval);

            //-----------------------------------
            // Reh
            int p = emodtopindex;
            double temptop = 0.0;
            // s�tt signal om att riktig reh ej �nnu �r funnen
            bool truetop = false;
            while (truetop == false)
            {
                // om inte sp�nningen v�nt ned�t stega fram en punkt i kurvan
                while (double.Parse(tenscur[p + 1].ToString()) - double.Parse(tenscur[p].ToString()) > 0.0)
                {
                    p++;
                }
                // sp�nningen har v�nt ned�t
                temptop = double.Parse(tenscur[p].ToString());
                truetop = true;

                // kontrollera om det �r ett skenbart reh
                // filtrera bort om det �r en tillf�llig topp f�r reh
                for (int i = p; i < p + 15; i++)
                {
                    if (double.Parse(tenscur[i].ToString()) > temptop)
                    {
                        truetop = false;
                    }
                }
                p++;
            }
            rehval = double.Parse(tenscur[p].ToString());
            plotvalrehY = rehval;
            plotvalrehX = float.Parse(straincur[p].ToString());
            // tag bort decimalerna
            rehval = rehval - rehval % 1;
            //logg.Write("reh " + rehval);

            //-----------------------------------
            // Rel
            relval = rehval;
            // stega f�rst iv�g en bit fr�n reh
            int ppstop = p + 50;

            for (int pp = p; pp < rmindex; pp++)
            {
                if (double.Parse(tenscur[pp].ToString()) < relval)
                {
                    relval = double.Parse(tenscur[pp].ToString());
                    plotvalrelY = relval;
                    plotvalrelX = float.Parse(straincur[pp].ToString());
                }
            }
            p = ppstop;
            // kontrollera sedan hela v�gen fram till att rel blir st�rre �n reh
            while (double.Parse(tenscur[p].ToString()) - rehval < 0.0 && p < rmindex)
            {
                if (double.Parse(tenscur[p].ToString()) < relval)
                {
                    relval = double.Parse(tenscur[p].ToString());
                    plotvalrelY = relval;
                    plotvalrelX = float.Parse(straincur[p].ToString());
                }
                p++;
            }
            relval = relval - relval % 1;
            //logg.Write("rel " + relval);

            //-----------------------------------
            // signalera FEL FEL FEL
            // om Reh ligger inom 1% av Rm �r det tecken p� att
            // inget riktigt flytg�nsomr�de finns markera detta 
            // genom att s�tta Reh och Rel= 0
            if (rehval > rmval * 0.99)
            {
                rehval = 0;
                relval = 0;
                //logg.Write("reh " + rehval + "  rel " + relval);
            }
            // signalera FEL FEL FEL
            // om reh och rp02 i praktiken �r lika signaleras
            // att n�got �r underligt genom att g�ra reh negativt
            if (rehval > rp02val * 0.99 && rehval < rp02val * 1.01)
            {
                rehval = rehval * (-1);
                //logg.Write("reh " + rehval + "  rel " + relval);
            }

            //-----------------------------------
            // Ag
            ArrayList eline = new ArrayList();
            eline = (ArrayList)lines[0];
            // eline[1] = lutningen (k) f�r linjen emodul
            double xrm = float.Parse(straincur[rmindex].ToString());
            double Crm = rmval - xrm * double.Parse(eline[1].ToString());
            if (double.Parse(eline[1].ToString()) == 0.0)
            {
                agval = 0;
            }
            else
            {
                agval = (0.0 - Crm) / double.Parse(eline[1].ToString());
            }

            ArrayList agline = new ArrayList();
            agline.Add("Agline");
            agline.Add(double.Parse(eline[1].ToString()));  // k
            agline.Add(Crm);                                // C
            lines.Add(agline);

            plotvalagX = agval;
            plotvalagY = 0.0;
            //logg.Write("ag " + agval);

            //-----------------------------------
            // A80
            double a80valtensY = 0.0;
            int a80index = 0;
            // leta r�tt p� brottpunkten
            double perpendicularlength = 0.0;
            int istart = tenscur.Count - 1;
            for (int i = istart; i > rmindex; i--)
            {
                // anv�nd -1/k som lutning p� normalen till lutning k �NNU EJ GJORT
                double rmY = double.Parse(tenscur[rmindex].ToString()) - double.Parse(tenscur[tenscur.Count - 1].ToString());
                double rmX = double.Parse(straincur[straincur.Count - 1].ToString()) - double.Parse(straincur[rmindex].ToString());
                double alfarmline = Math.Atan(rmY / rmX);
                
                double tensY = double.Parse(tenscur[i].ToString()) - double.Parse(tenscur[tenscur.Count - 1].ToString());
                
                double tensX = double.Parse(straincur[straincur.Count - 1].ToString()) - double.Parse(straincur[i].ToString());
                double betatenspoint = Math.Atan(tensY / tensX);
                double gammadiffangle = betatenspoint - alfarmline;
                double tenslengthX = Math.Pow(tensX, 2.0);
                double tenslengthY = Math.Pow(tensY, 2.0);
                double tenslength = Math.Sqrt(tenslengthX + tenslengthY);
                double perplength = Math.Abs(tenslength * Math.Sin(gammadiffangle));

                if (perplength > perpendicularlength)
                {
                    perpendicularlength = perplength;
                    a80index = i;
                }
            }
            a80valtensY = double.Parse(tenscur[a80index].ToString());

            if (a80index == 0)
            {
                a80index = rmindex;
            }
            // kalkylera linje genom brottpunkten
            // eline[1] = lutningen (k) f�r linjen emodul
            double xa80 = double.Parse(straincur[a80index].ToString());
            double Ca80 = a80valtensY - xa80 * double.Parse(eline[1].ToString());
            //a80val = (0.0 - Ca80) / double.Parse(eline[1].ToString());
            if (double.Parse(eline[1].ToString()) == 0.0)
            {
                a80val = 0;
            }
            else
            {
                a80val = (0.0 - Ca80) / double.Parse(eline[1].ToString());
            }

            ArrayList a80line = new ArrayList();
            a80line.Add("A80line");
            a80line.Add(double.Parse(eline[1].ToString()));  // k
            a80line.Add(Ca80);                               // C
            lines.Add(a80line);

            plotvala80X = a80val;
            plotvala80Y = 0.0;
            //logg.Write("a80 " + a80val);

            //-----------------------------------
            // skapa pointarray f�r plottning
            ArrayList rp02 = new ArrayList();
            rp02.Add(plotvalrp02X);
            rp02.Add(plotvalrp02Y);
            ArrayList rm = new ArrayList();
            rm.Add(plotvalrmX);
            rm.Add(plotvalrmY);
            ArrayList reh = new ArrayList();
            reh.Add(plotvalrehX);
            reh.Add(plotvalrehY);
            ArrayList rel = new ArrayList();
            rel.Add(plotvalrelX);
            rel.Add(plotvalrelY);
            ArrayList ag = new ArrayList();
            ag.Add(plotvalagX);
            ag.Add(plotvalagY);
            ArrayList a80 = new ArrayList();
            a80.Add(plotvala80X);
            a80.Add(plotvala80Y);

            points.Clear();
            points.Add(rp02);
            points.Add(rm);
            if (plotvalrehY != 0.0)
            {
                points.Add(reh);
            }
            if (plotvalrelY != 0.0)
            {
                points.Add(rel);
            }
            points.Add(ag);
            points.Add(a80);

        } // void Rp02(ArrayList straincur, ArrayList tenscur, ArrayList lin)

    } // class CurveHandling

} // namespace DragKurva
