using System;
using System.Collections.Generic;
using System.Text;
//********************
using System.IO;
using System.Collections;

namespace DragKurva
{
    class Curves
    {
        private int num = 0;
        private ArrayList standardforce = new ArrayList();
        private ArrayList truestrain = new ArrayList();
        private ArrayList truetension = new ArrayList();
        private ArrayList changeinwidth = new ArrayList();
        private ArrayList standardpath = new ArrayList();
        private ArrayList testtime = new ArrayList();
        private ArrayList emodul = new ArrayList();
        private ArrayList emodulvel = new ArrayList();
        private ArrayList emodulacc = new ArrayList();

        private int infoitems = 6;
        //**************************************************************************
        public void ReadDataFile(String mtrl, String fm)
        {
            double stdforcemax = 0;
            double truestrainmax = 0;
            double truetensionmax = 0;
            double changeinwidthmax = 0;
            double standardpathmax = 0;
            double testtimemax = 0;
            double emmax = 0;
            double emvelmax = 0;
            double emaccmax = 0;
            double stdforcemin = 0;
            double truestrainmin = 0;
            double changeinwidthmin = 0;
            double truetensionmin = 0;
            double standardpathmin = 0;
            double testtimemin = 0;
            double emmin = 0;
            double emvelmin = 0;
            double emaccmin = 0;

            String mestext = "";

            // buffra mätdata från fil
            using (StreamReader oParsad = new StreamReader(mtrl))
            //using stänger strömmen efter EOF
            {
                String line = "";
                while ((line = oParsad.ReadLine()) != null)
                {
                    mestext = mestext + line;   //mestext är hela datafilen
                }
            }

            standardforce.Clear();
            truestrain.Clear();
            truetension.Clear();
            changeinwidth.Clear();
            standardpath.Clear();
            testtime.Clear();
            emodul.Clear();
            emodulvel.Clear();
            emodulacc.Clear();


            //Parsning
            String lables = "";
            int meslength = mestext.Length;

            //stega förbi inledande text
            num = 0;
            while (Char.Parse(String.Copy(mestext.Substring(num, 1))) < 0x30 || Char.Parse(String.Copy(mestext.Substring(num, 1))) > 0x39)
            {
                {
                    lables = lables + Char.Parse(String.Copy(mestext.Substring(num, 1)));
                    num++;
                }
            }

            // bygg värdevektorer
            String strvalue = "";

            int numberingrp = 0;
            while (num < meslength)
            {
                //kontrollera om det är ett flyttal ascii 30=0 39=9 
                while (num < meslength
                        && ((Char.Parse(String.Copy(mestext.Substring(num, 1))) > 0x2F
                        && Char.Parse(String.Copy(mestext.Substring(num, 1))) < 0x3A)
                        || String.Copy(mestext.Substring(num, 1)) == "."
                        || String.Copy(mestext.Substring(num, 1)) == "e"
                        || String.Copy(mestext.Substring(num, 1)) == "+"
                        || String.Copy(mestext.Substring(num, 1)) == "-"))
                {
                    //läs och spara siffran_flyttalet
                    strvalue = strvalue + String.Copy(mestext.Substring(num, 1));
                    num++;
                }
                num++;

                if (strvalue != "")
                {
                    switch (numberingrp)
                    {
                        case 0:
                            strvalue = strvalue.Replace("e", "E");
                            strvalue = strvalue.Replace(".", ",");
                            standardforce.Add(strvalue);
                            if (double.Parse(strvalue) > stdforcemax)
                            {
                                stdforcemax = double.Parse(strvalue);
                            }
                            if (double.Parse(strvalue) < stdforcemin)
                            {
                                stdforcemin = double.Parse(strvalue);
                            }
                            strvalue = "";
                            break;
                        case 1:
                            strvalue = strvalue.Replace("e", "E");
                            strvalue = strvalue.Replace(".", ",");
                            truestrain.Add(strvalue);                   // strain = töjning
                            if (double.Parse(strvalue) > truestrainmax)
                            {
                                truestrainmax = double.Parse(strvalue);
                            }
                            if (double.Parse(strvalue) < truestrainmin)
                            {
                                truestrainmin = double.Parse(strvalue);
                            }
                            strvalue = "";

                            break;
                        case 2:
                            strvalue = strvalue.Replace("e", "E");
                            strvalue = strvalue.Replace(".", ",");
                            truetension.Add(strvalue);                  // tension = spänning
                            if (double.Parse(strvalue) > truetensionmax)
                            {
                                truetensionmax = double.Parse(strvalue);
                            }
                            if (double.Parse(strvalue) < truetensionmin)
                            {
                                truetensionmin = double.Parse(strvalue);
                            }
                            strvalue = "";
                            break;
                        case 3:
                            strvalue = strvalue.Replace("e", "E");
                            strvalue = strvalue.Replace(".", ",");
                            changeinwidth.Add(strvalue);
                            if (double.Parse(strvalue) > changeinwidthmax)
                            {
                                changeinwidthmax = double.Parse(strvalue);
                            }
                            if (double.Parse(strvalue) < changeinwidthmin)
                            {
                                changeinwidthmin = double.Parse(strvalue);
                            }
                            strvalue = "";
                            break;
                        case 4:
                            strvalue = strvalue.Replace("e", "E");
                            strvalue = strvalue.Replace(".", ",");
                            standardpath.Add(strvalue);
                            if (double.Parse(strvalue) > standardpathmax)
                            {
                                standardpathmax = double.Parse(strvalue);
                            }
                            if (double.Parse(strvalue) < standardpathmin)
                            {
                                standardpathmin = double.Parse(strvalue);
                            }
                            strvalue = "";
                            break;
                        case 5:
                            strvalue = strvalue.Replace("e", "E");
                            strvalue = strvalue.Replace(".", ",");
                            testtime.Add(strvalue);
                            if (double.Parse(strvalue) > testtimemax)
                            {
                                testtimemax = double.Parse(strvalue);
                            }
                            if (double.Parse(strvalue) < testtimemin)
                            {
                                testtimemin = double.Parse(strvalue);
                            }
                            strvalue = "";
                            break;
                        default:
                            break;

                    } //switch (numberingrp)

                    //stega upp till nästa siffra i gruppen
                    if (numberingrp == 5)
                    {
                        numberingrp = 0;
                    }
                    else
                    {
                        numberingrp++;
                    }

                } // if (strvalue != "")

            } // while (Char.Parse(String.Copy(mestext.Substring(num, 1))) != null)

            // flytmedel för kurvor utan medelvärdesbildning
            int standardforceflytmedel = 0;
            int truestrainflytmedel = 0;
            int truetensionflytmedel = 0;
            int changeinwidthflytmedel = 0;
            int standardpathflytmedel = 0;
            int testtimeflytmedel = 0;

            // infoitems läggs till för inledande kurvinfouppgifter
            int standardforcecount = standardforce.Count + infoitems;
            int truestraincount = truestrain.Count + infoitems;
            int truetensioncount = truetension.Count + infoitems;
            int changeinwidthcount = changeinwidth.Count + infoitems;
            int standardpathcount = standardpath.Count + infoitems;
            int testtimecount = testtime.Count + infoitems;

            // inkludera namn, min, max, flytmedel och antal mätpunkter i de rapporterade kurvorna
            standardforce.Insert(0, "Mark");
            standardforce.Insert(1, "ForceY");
            standardforce.Insert(2, stdforcemax.ToString());
            standardforce.Insert(3, stdforcemin.ToString());
            standardforce.Insert(4, standardforceflytmedel.ToString());
            standardforce.Insert(5, standardforcecount.ToString());
            truestrain.Insert(0, "Mark");
            truestrain.Insert(1, "StrainY");
            truestrain.Insert(2, truestrainmax.ToString());
            truestrain.Insert(3, truestrainmin.ToString());
            truestrain.Insert(4, truestrainflytmedel.ToString());
            truestrain.Insert(5, truestraincount.ToString());
            truetension.Insert(0, "Mark");
            truetension.Insert(1, "TensionY");
            truetension.Insert(2, truetensionmax.ToString());
            truetension.Insert(3, truetensionmin.ToString());
            truetension.Insert(4, truetensionflytmedel.ToString());
            truetension.Insert(5, truetensioncount.ToString());
            changeinwidth.Insert(0, "Mark");
            changeinwidth.Insert(1, "chWidthY");
            changeinwidth.Insert(2, changeinwidthmax.ToString());
            changeinwidth.Insert(3, changeinwidthmin.ToString());
            changeinwidth.Insert(4, changeinwidthflytmedel.ToString());
            changeinwidth.Insert(5, changeinwidthcount.ToString());
            standardpath.Insert(0, "Mark");
            standardpath.Insert(1, "PathY");
            standardpath.Insert(2, standardpathmax.ToString());
            standardpath.Insert(3, standardpathmin.ToString());
            standardpath.Insert(4, standardpathflytmedel.ToString());
            standardpath.Insert(5, standardpathcount.ToString());
            testtime.Insert(0, "Mark");
            testtime.Insert(1, "TestTimeY");
            testtime.Insert(2, testtimemax.ToString());
            testtime.Insert(3, testtimemin.ToString());
            testtime.Insert(4, testtimeflytmedel.ToString());
            testtime.Insert(5, testtimecount.ToString());

            //**************************************************************************
            //skapa E-modul diffkvot1
            double em = 0.0;
            int emflytmedel = 0;

            if (fm == "" || fm == null)
            {
                // do nothing
            }
            else
            {
                emflytmedel = Int32.Parse(fm);
            }
            int emfmhalv = emflytmedel / 2;
            if (emflytmedel == 0)
            {
                emflytmedel = 1;
            }
            for (int m = 0; m < emfmhalv; m++)
            {
                emodul.Add("0,0");
            }

            //
            for (int i = 4 + emfmhalv + 1; i < truestrain.Count - emfmhalv; i++)
            {
                em = 0;
                for (int k = 0; k < emflytmedel; k++)
                {
                    if (double.Parse(truestrain[i + emfmhalv - k].ToString()) - double.Parse(truestrain[i + emfmhalv - k - 1].ToString()) != 0.0 ||
                        double.Parse(truestrain[i + emfmhalv - k].ToString()) - double.Parse(truestrain[i + emfmhalv - k - 1].ToString()) == Single.NaN)
                    {
                        // summera mätvärdena inom emflytmedel-intervallet
                        em = em + (double.Parse(truetension[i + emfmhalv - k].ToString()) - double.Parse(truetension[i + emfmhalv - k - 1].ToString())) / (double.Parse(truestrain[i + emfmhalv - k].ToString()) - double.Parse(truestrain[i + emfmhalv - k - 1].ToString()));
                    }
                    else
                    {
                        em = 0.0;
                    }
                }
                em = em / emflytmedel;
                if (em > emmax)
                {
                    emmax = em;
                }
                if (em < emmin)
                {
                    emmin = em;
                }
                emodul.Add(em.ToString());

            } // for (int i = emfmhalv + 1; i < truestrain.Count - emfmhalv; i++)

            //**************************************************************************
            //skapa E-modul FörändringsHastighet diffkvot2
            double emvel = 0.0;
            int emvelflytmedel = 10;

            if (fm == "" || fm == null)
            {
                // do nothing
            }
            else
            {
                emvelflytmedel = Int32.Parse(fm);
            }
            int emfmvelhalv = emvelflytmedel / 2;
            if (emvelflytmedel == 0)
            {
                emvelflytmedel = 1;
            }
            for (int m = 0; m < emfmvelhalv; m++)
            {
                emodulvel.Add("0,0");
            }
            int topcount = 0;
            if (truestrain.Count > emodul.Count)
            {
                topcount = emodul.Count;
            }
            else
            {
                topcount = truestrain.Count;
            }

            for (int i = 4 + emfmvelhalv + 1; i < topcount - emfmvelhalv; i++)
            {
                emvel = 0;
                for (int k = 0; k < emvelflytmedel; k++)
                {
                    if (double.Parse(truestrain[i + emfmvelhalv - k].ToString()) - double.Parse(truestrain[i + emfmvelhalv - k - 1].ToString()) != 0.0 ||
                        double.Parse(truestrain[i + emfmvelhalv - k].ToString()) - double.Parse(truestrain[i + emfmvelhalv - k - 1].ToString()) == Single.NaN)
                    {
                        emvel = emvel + (double.Parse(emodul[i + emfmvelhalv - k].ToString()) - double.Parse(emodul[i + emfmvelhalv - k - 1].ToString())) / (double.Parse(truestrain[i + emfmvelhalv - k].ToString()) - double.Parse(truestrain[i + emfmvelhalv - k - 1].ToString()));
                    }
                    else
                    {
                        emvel = emvel + 0.0;
                    }
                }
                emvel = emvel / emvelflytmedel;
                if (emvel > emvelmax)
                {
                    emvelmax = emvel;
                }
                if (emvel < emvelmin)
                {
                    emvelmin = emvel;
                }
                emodulvel.Add(emvel.ToString());

            } // for (int i = emfmvelhalv + 1; i < topcount - emfmvelhalv; i++)

            //**************************************************************************
            //skapa E-modul FörändringsAcceleration diffkvot3
            double emacc = 0.0;
            int emaccflytmedel = 1;
            int emfmacchalv = emaccflytmedel / 2;

            if (emaccflytmedel == 0)
            {
                emaccflytmedel = 1;
            }
            for (int m = 0; m < emfmacchalv; m++)
            {
                emodulacc.Add("0,0");
            }
            topcount = 0;
            if (truestrain.Count > emodulvel.Count)
            {
                topcount = emodulvel.Count;
            }
            else
            {
                topcount = truestrain.Count;
            }

            //
            for (int i = 4 + emfmacchalv + 1; i < topcount - emfmacchalv; i++)
            {
                emacc = 0;
                for (int k = 0; k < emaccflytmedel; k++)
                {
                    if (double.Parse(truestrain[i + emfmacchalv - k].ToString()) - double.Parse(truestrain[i + emfmacchalv - k - 1].ToString()) != 0.0 ||
                        double.Parse(truestrain[i + emfmacchalv - k].ToString()) - double.Parse(truestrain[i + emfmacchalv - k - 1].ToString()) == Single.NaN)
                    {
                        emacc = emacc + (double.Parse(emodulvel[i + emfmacchalv - k].ToString()) - double.Parse(emodulvel[i + emfmacchalv - k - 1].ToString())) / (double.Parse(truestrain[i + emfmacchalv - k].ToString()) - double.Parse(truestrain[i + emfmacchalv - k - 1].ToString()));
                    }
                    else
                    {
                        emacc = emacc + 0.0;
                    }
                }
                emacc = emacc / emaccflytmedel;
                if (emacc > emaccmax)
                {
                    emaccmax = emacc;
                }
                if (emacc < emaccmin)
                {
                    emaccmin = emacc;
                }
                emodulacc.Add(emacc.ToString());

            } // for (int i = emfmacchalv + 1; i < topcount - emfmacchalv; i++)

            int emodulcount = emodul.Count + infoitems;
            int emodulvelcount = emodulvel.Count + infoitems;
            int emodulacccount = emodulacc.Count + infoitems;


            // inkludera namn, min, max, flytmedel och antal mätpunkter i de beräknade kurvorna
            emodul.Insert(0, "Mark");
            emodul.Insert(1, "EmodulY");
            emodul.Insert(2, emmax.ToString());
            emodul.Insert(3, emmin.ToString());
            emodul.Insert(4, emflytmedel.ToString());
            emodul.Insert(5, emodulcount.ToString());
            emodulvel.Insert(0, "Mark");
            emodulvel.Insert(1, "EmodulvelY");
            emodulvel.Insert(2, emvelmax.ToString());
            emodulvel.Insert(3, emvelmin.ToString());
            emodulvel.Insert(4, emvelflytmedel.ToString());
            emodulvel.Insert(5, emodulvelcount.ToString());
            emodulacc.Insert(0, "Mark");
            emodulacc.Insert(1, "EmodulaccY");
            emodulacc.Insert(2, emaccmax.ToString());
            emodulacc.Insert(3, emaccmin.ToString());
            emodulacc.Insert(4, emaccflytmedel.ToString());
            emodulacc.Insert(5, emodulacccount.ToString());

            ////***************
            //// Test
            //// p kortar av kurvan till ett mindre antal punkter
            //// avmarkera om ett sådant prov vill göras
            //int p = 300;
            //standardforce.RemoveRange(p, standardforce.Count - p);
            //truestrain.RemoveRange(300, truestrain.Count - p);
            //truetension.RemoveRange(300, truetension.Count - p);
            //changeinwidth.RemoveRange(300, changeinwidth.Count - p);
            //standardpath.RemoveRange(300, standardpath.Count - p);
            //testtime.RemoveRange(300, testtime.Count - p);
            //emodul.RemoveRange(30, emodul.Count - p);
            //emodulvel.RemoveRange(300, emodulvel.Count - p);
            //emodulacc.RemoveRange(300, emodulacc.Count - p);
            ////****************

        } // public void ReadDataFile(    )    

        //**************************************************************************************
        public ArrayList StandardForce
        {
            get { return standardforce; }
        }
        public ArrayList Truestrain
        {
            get { return truestrain; }
        }
        public ArrayList Truetension
        {
            get { return truetension; }
        }
        public ArrayList Changeinwidth
        {
            get { return changeinwidth; }
        }
        public ArrayList Standardpath
        {
            get { return standardpath; }
        }
        public ArrayList Testtime
        {
            get { return testtime; }
        }
        public ArrayList Emodul
        {
            get { return emodul; }
        }
        public ArrayList Emodulvel
        {
            get { return emodulvel; }
        }
        public ArrayList Emodulacc
        {
            get { return emodulacc; }
        }

    } // class Curves

} // namespace DragKurva

