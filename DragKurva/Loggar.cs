using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DragKurva
{
    class Loggar
    {
        private string loglocation = "";

        public string Loglocation
        {
            set { loglocation = value; }
        }

        //***********************************************************
        public void Write(String text)
        {
            try
            {
                /* använd följande 3 rader som anrop vid lagring i loggfil
                 * 
                 string loggtext = "kan ej hantera " + Limits[k, 12] + ". Endast 1 egenskap per provtillfälle är tillåtet för provtypen."; 
                 Loggar Lo = new Loggar();
                 Lo.Write(loggtext);
                 * 
                 */
                //using (StreamWriter logg = File.AppendText("C:\\DragKurvor\\Loggar\\" + "ChartLogg.txt"))
                using (StreamWriter logg = File.AppendText(loglocation))
                {
                    logg.WriteLine(text);
                    logg.Close();
                }
            }
            catch
            {

            }
        }


    }
}
