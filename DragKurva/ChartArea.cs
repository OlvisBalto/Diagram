using System;
using System.Collections.Generic;
using System.Text;
//************************
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;



namespace DragKurva
{
    class ChartArea
    {
        private Panel pan1 = new Panel();
        private Panel chartpanel = new Panel();
        private int chartpanmargin = 2;
        private Rectangle plotrect = new Rectangle();

        private Color chartBackColor = Color.Snow;
        private Color chartBorderColor = Color.Thistle;
        private string axislableX = "";
        private string axislableY = "";
        private Font labelFont = new Font("Arial", 10, FontStyle.Regular);
        private Color labelFontColor = Color.Black;
        private Font titleFont = new Font("Arial", 12, FontStyle.Regular);
        private Color titleFontColor = Color.Black;
        private SizeF axisXlableSize = new SizeF();
        private SizeF axisYlableSize = new SizeF();
        private int infoitems = 6;

        private float yrange = 1;
        private float xrange = 1;
        private Color plotBackColor = Color.White;
        private Color plotBorderColor = Color.Blue;
        //private float xLimMin = 0f;      // dummyvärde
        //private float xLimMax = 10f;     // dummyvärde
        //private float yLimMin = 0f;      // dummyvärde
        //private float yLimMax = 10f;     // dummyvärde
        private DashStyle gridPattern = DashStyle.Solid;
        private Color gridColor = Color.Red;
        private bool isXGrid = true;
        private bool isYGrid = true;
        private Font tickFont = new Font("Arial", 7, FontStyle.Regular);
        private Color tickFontColor = Color.Black;
        private Color lineColor = Color.Red;
        private DashStyle lineDash = DashStyle.Solid;

        Hashtable htaxiscoordsY = new Hashtable();
        private Loggar logg = new Loggar();

        public float Yrange
        {
            get { return yrange; }
            set { yrange = value; }
        }

        public float Xrange
        {
            get { return xrange; }
            set { xrange = value; }
        }

        //*********************************************************
        // sätt upp chartarean
        public void SetChartPanel(Form1 form, Panel pan, int rightleftmarg, int topbottmarg)
        {
            // ge diagramarean rätt placering (Left Top)
            //  och storlek (Width Height)i fönstret
            pan1 = pan;
            pan1.Left = 190;
            pan1.Top = 50;
            pan1.Width = form.Width - pan1.Left - rightleftmarg;
            pan1.Height = form.Height - pan1.Top - topbottmarg;
            pan1.BackColor = Color.White;
           
        }
        //---------------------------------------------------------
        public Panel Pan1
        {
            get { return pan1; }
            set { pan1 = value; }
        }

        
        //**********************************************************
        // Draw ChartArea
        public void DrawChartArea(Graphics g, Panel chartpan, String labX, ArrayList labY, String tit, string getfrom, string areagetfrom)
        {
            chartpanel = chartpan;
            /* chartpanel.X placering av chartpanel i fönster
             * chartpanel.Y placering av chartpanel i fönster
             * chartleft = diagramarea X översta vänstra hörnet
             * charttop = diagramarea Y översta vänstra hörnet
             * chartwidth = diagramarea X högerkanten
             * chartheight = diagramarea Y nederkant
             * labX = dataserie som används som X-axel (kan bara var 1 i denna lösning)
             * labY = dataserier på Y-axeln ( kan vara fler i denna lösning)
             * tit = diagramareans huvudtitel
             * */
            int charttop = 0 + chartpanmargin;
            int chartleft = 0 + chartpanmargin;
            int chartwidth = chartpan.Width - 2*chartpanmargin;
            int chartcoordXmax = chartwidth + chartpanmargin;
            int chartheight = chartpan.Height - 2*chartpanmargin;
            int chartcoordYmax = chartheight + chartpanmargin;

            ////++++++++++ Visualiserar diagramarean ++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ///* pdiam = omskrivande kvadraten = prickens diameter
            // * prad = prickens radie
            // * */
            //int pchartdiam = 6;
            //int pchartrad = pchartdiam / 2;
            //// Graphics c = e.Graphics;
            //SolidBrush dBrush = new SolidBrush(Color.Green);
            //Rectangle circ = new Rectangle(chartleft - pchartrad, charttop - pchartrad, pchartdiam, pchartdiam);
            //g.FillPie(dBrush, circ, 0, 360);
            ////****
            //circ = new Rectangle(chartleft - pchartrad, chartcoordYmax - pchartrad, pchartdiam, pchartdiam);
            //g.FillPie(dBrush, circ, 0, 360);
            ////****
            //circ = new Rectangle(chartcoordXmax - pchartrad, charttop - pchartrad, pchartdiam, pchartdiam);
            //g.FillPie(dBrush, circ, 0, 360);
            ////****
            //circ = new Rectangle(chartcoordXmax - pchartrad, chartcoordYmax - pchartrad, pchartdiam, pchartdiam);
            //g.FillPie(dBrush, circ, 0, 360);
            //dBrush.Dispose();
            ////c.Dispose();
            ////+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // sätt diagramarea penna och pensel 
            Rectangle chartrect = new Rectangle(chartleft, charttop, chartwidth, chartheight);
            Pen aPen = new Pen(chartBorderColor, 1f);
            SolidBrush aBrush = new SolidBrush(chartBackColor);
   //         g.FillRectangle(aBrush, chartrect);
   //         g.DrawRectangle(aPen, chartrect);

            /* för att kunna räkna ut hur stor yta som är tillgänglig
             * för kurvplottarna måste titelns text och axlarnas texter
             * bestämmas innan tillgänglig diagramarean kan räknas ut
             * */

            // titeltext:
            SolidBrush bBrush = new SolidBrush(titleFontColor);
            SizeF titlelableSize = g.MeasureString(tit, titleFont);
            if (tit.ToUpper() != "NO TITLE")
            {
                g.DrawString(tit, titleFont, bBrush,
                            new Point(chartrect.Left + chartrect.Width / 2 - (int)titlelableSize.Width / 2,
                                      chartrect.Top ));
            }

            // text X-axel
            aBrush = new SolidBrush(labelFontColor);
            axislableX = labX.TrimEnd('X');
            axisXlableSize = g.MeasureString(axislableX, labelFont);
            g.DrawString(axislableX, labelFont, aBrush,
                         new Point(chartcoordXmax - (int)axisXlableSize.Width,
                         chartcoordYmax - (int)axisXlableSize.Height));

            // text Y-axlar
            /* sätt primärkurva på index 0 
             * i labYorder om den är invald
             * */
            htaxiscoordsY.Clear();
            String[] labYorder = new String[labY.Count];
            bool zero = false;
            // är primärkurvan vald
            for (int i = 0; i < labY.Count; i++)
            {
                if (labY[i].ToString() == "TensionY")
                {
                    labYorder[0] = labY[i].ToString().TrimEnd('Y');
                    zero = true;
                }
            }
            int j = 0;
            /* om primärkurvan är invald 
             * börja på index 1 i labYorder
             * */
            if (zero == true)
            {
                j++;
            }
            // läs in övrigt invalda kurvor
            for (int i = 0; i < labY.Count; i++)
            {
                if (labY[i].ToString() == "TensionY" && zero == true)
                {
                    // do nothing
                }
                else
                {
                    labYorder[j] = labY[i].ToString().TrimEnd('Y');
                    j++;
                }
            }
            float axiscoordXright = 0;
            float textspace = 5;

            // text för primär y-axel
            axislableY = labYorder[0].ToString();
            axisYlableSize = g.MeasureString(axislableY.ToString(), labelFont);
            g.DrawString(axislableY.ToString(), labelFont, aBrush,
                         chartrect.Left,
                         chartrect.Top + (int)titlelableSize.Height);
            htaxiscoordsY[labYorder[0]] = chartrect.Left + axisYlableSize.Width;

            // text för övriga y-axlar
            axiscoordXright = chartwidth;
            bool severalaxis = false;
            for (int i = 1; i < labYorder.Length; i++)
            {
                axislableY = labYorder[i].ToString();
                axisYlableSize = g.MeasureString(axislableY.ToString(), labelFont);
                if (i == 1)
                {
                    axiscoordXright = axiscoordXright - axisYlableSize.Width;
                }
                else
                {
                    axiscoordXright = axiscoordXright - axisYlableSize.Width / 2;
                }
                g.DrawString(axislableY.ToString(), labelFont, aBrush,
                             axiscoordXright, 
                             chartrect.Top + (int)titlelableSize.Height);
                htaxiscoordsY[labYorder[i].ToString()] = axiscoordXright + axisYlableSize.Width / 2;
                axiscoordXright = axiscoordXright - axisYlableSize.Width / 2 - textspace;
                severalaxis = true;
            }
            axiscoordXright = axiscoordXright + axisYlableSize.Width / 2 + textspace;
            if (severalaxis == false)
            {
                axiscoordXright = chartwidth - (int)axisXlableSize.Width;
            }
            aPen.Dispose();
            aBrush.Dispose();

            // bestäm plotarean begränsningar för invalda kurvplottar
            int plotcoordXleft = (int)(chartrect.Left + float.Parse(htaxiscoordsY[labYorder[0]].ToString()));
            int plotcoordYtop = (int)(chartrect.Top + axisYlableSize.Height + (int)titlelableSize.Height);
            int plotcoordXright = (int)(axiscoordXright);
            int plotcoordYbottom = (int)(chartrect.Height - axisYlableSize.Height);

            plotrect = new Rectangle(plotcoordXleft, plotcoordYtop, plotcoordXright, plotcoordYbottom);

            ////++++++++++ Visualiserar plotarean ++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ///* pdiam = omskrivande kvadraten = prickens diameter
            // * prad = prickens radie
            // * */
            //int pplotdiam = 10;
            //int pplotrad = pplotdiam / 2;
            //// Graphics c = e.Graphics;
            //SolidBrush eBrush = new SolidBrush(Color.Blue);
            //Rectangle plotcirc = new Rectangle(plotcoordXleft - pplotrad, plotcoordYtop - pplotrad, pplotdiam, pplotdiam);
            //g.FillPie(eBrush, plotcirc, 0, 360);
            ////****
            //plotcirc = new Rectangle(plotcoordXleft - pplotrad, plotcoordYbottom - pplotrad, pplotdiam, pplotdiam);
            //g.FillPie(eBrush, plotcirc, 0, 360);
            ////****
            //plotcirc = new Rectangle(plotcoordXright - pplotrad, plotcoordYtop - pplotrad, pplotdiam, pplotdiam);
            //g.FillPie(eBrush, plotcirc, 0, 360);
            ////****
            //plotcirc = new Rectangle(plotcoordXright - pplotrad, plotcoordYbottom - pplotrad, pplotdiam, pplotdiam);
            //g.FillPie(eBrush, plotcirc, 0, 360);
            //eBrush.Dispose();
            ////c.Dispose();
            ////+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++        

        } // public void DrawChartArea(Graphics g, Panel chartpan, String labX, ArrayList labY, String tit)

        //****************************
        public Rectangle Plotrect
        {
            get { return plotrect; }
            set { plotrect = value; }
        }

        
        //**********************************************************
        // Draw PlotArea
        // parea = Plotrect ovan
        public void DrawPlotArea(Graphics g, Rectangle parea, String[][] pdata, ArrayList rp02vallinesdata, ArrayList plotvalpointsdata, string getfrom, string areagetfrom)
        {

            //string gf = getfrom;
            //string agf = areagetfrom;
            //logg.Write("sätt kurvfärg " + "  " + axisplace + "  " + curvenameY + "  " + getfrom + "  " + areagetfrom);
            //foreach (DictionaryEntry entry in htaxiscoordsY)
            //{
            //    logg.Write("DrawPlotArea()   htaxiscoordsY=" + entry.Key + " :: " + entry.Value);
            //}
            //logg.Write("DrawPlotArea()  pdata=" + pdata[1][0] + pdata[1][1]);

            String markX = "";
            String markY = "";
            String curvenameX = "";
            String curvenameY = "";
            float xlimmax = 0;
            float ylimmax = 0;
            float xlimmin = 0;
            float ylimmin = 0;
            int xflytmedel = 0;
            int yflytmedel = 0;
            int curvepointsX = 0;
            int curvepointsY = 0;

            // rita plotarea ( ytan för axlarna + kurvplotten - texter)
            Rectangle plotarea = new Rectangle(parea.Left, parea.Top, parea.Width, parea.Height);

            Pen aPen = new Pen(plotBorderColor, 1f);
            Brush aBrush = new SolidBrush(plotBackColor);

            //++++++++++ Visualiserar plotarean ++++++++++++++++++++++++++++++++++++++++++++++++++++++
            /* pdiam = omskrivande kvadraten = prickens diameter
             * prad = prickens radie
             * */
            int ppaxdiam = 6;
            int ppaxrad = ppaxdiam / 2;
            Brush cBrush = new SolidBrush(Color.Red);
            Rectangle pcirc = new Rectangle(plotarea.Left - ppaxrad, plotarea.Top - ppaxrad, ppaxdiam, ppaxdiam);
            g.FillPie(cBrush, pcirc, 0, 360);
            //****
            pcirc = new Rectangle(plotarea.Left - ppaxrad, plotarea.Height - ppaxrad, ppaxdiam, ppaxdiam);
            g.FillPie(cBrush, pcirc, 0, 360);
            //****
            pcirc = new Rectangle(plotarea.Width - ppaxrad, plotarea.Top - ppaxrad, ppaxdiam, ppaxdiam);
            g.FillPie(cBrush, pcirc, 0, 360);
            ////****
            pcirc = new Rectangle(plotarea.Width - ppaxrad, plotarea.Height - ppaxrad, ppaxdiam, ppaxdiam);
            g.FillPie(cBrush, pcirc, 0, 360);
            //++++++++++

            //****************************************
            // skapa x-axel
            Color XaxelColor = Color.Brown;
            Pen fPen = new Pen(XaxelColor, 2f);
            g.DrawLine(fPen, plotarea.X, plotarea.Height, plotarea.Width, plotarea.Height);
            fPen.Dispose();


            //****************************************
            // förbered kurv- och y-axelplottar
            
            int curvestart = 0;
            int pdatacurvestartindexX = 0;
            int pdatacurveendindexX = 0;
            float plotoffsetX = 0;
            int plotstartindexX = 0;
            int plotendindexX = 0;
            int i = 0;
            while ( curvestart < pdata.Length)
            {
                i = curvestart;
                // plocka sönder pdata
                markX = pdata[i + 0][0];
                markY = pdata[i + 0][1];
                curvenameX = pdata[i + 1][0];
                curvenameY = pdata[i + 1][1];
                xlimmax = float.Parse(pdata[i + 2][0]);
                ylimmax = float.Parse(pdata[i + 2][1]);
                xlimmin = float.Parse(pdata[i + 3][0]);
                ylimmin = float.Parse(pdata[i + 3][1]);
                xflytmedel = Int32.Parse(pdata[i + 4][0]);
                yflytmedel = Int32.Parse(pdata[i + 4][1]);
                curvepointsX = Int32.Parse(pdata[i + 5][0]);
                curvepointsY = Int32.Parse(pdata[i + 5][1]);
                float axisplace = 0;

                // +1 då k - 1 i plottningen skall vara lika med min-index
                // +infoitems är antalet info poster i böjan av varje kurvas data
                // +xflytmedel då vissa kurvor har flytande medelvärdesberäkning
                // curvstart är startindexet för varje ny kurva i pdata = visablecurvs
                // curvepointsX är antalet punkter i kurvan som håller på att bearbetas
                pdatacurvestartindexX = curvestart + infoitems + (xflytmedel / 2);
                pdatacurveendindexX = curvestart + curvepointsX;

                //sätt startindex för pdata till det som stämmer
                //med valt xlimmin. behövs då område angivits med 
                //gummibandsrektangel
                plotstartindexX = pdatacurvestartindexX;
                for (int j = pdatacurvestartindexX; j < pdatacurveendindexX; j++)
                {
                    if (float.Parse(pdata[j][0]) <= xlimmin)
                    {
                        plotstartindexX = j;
                    }
                }
                //sätt slutindex för pdata till det som stämmer
                //med valt xlimmax. behövs då område angivits med 
                //gummibandsrektangel
                for (int j = pdatacurvestartindexX; j < pdatacurveendindexX; j++)
                {
                    if (float.Parse(pdata[j][0]) <= xlimmax)
                    {
                        plotendindexX = j;
                    }
                }

                //-----------------------------------
                // sätt kurvfärg
                try
                {
                    switch (curvenameY)
                    {
                        case "ForceY":
                            gridColor = Color.Blue;
                            lineColor = Color.Blue;
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        case "StrainY":
                            gridColor = Color.BurlyWood;
                            lineColor = Color.BurlyWood;
                            //string gf = getfrom;
                            //string agf = areagetfrom;
                            //logg.Write("sätt kurvfärg " + "  " + axisplace + "  " + curvenameY + "  " + getfrom + "  " + areagetfrom);
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        case "TensionY":
                            gridColor = Color.Chartreuse;
                            lineColor = Color.Chartreuse;
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        case "chWidthY":
                            gridColor = Color.DarkCyan;
                            lineColor = Color.DarkCyan;
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        case "PathY":
                            gridColor = Color.DarkOrange;
                            lineColor = Color.DarkOrange;
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        case "EmodulY":
                            gridColor = Color.DarkOliveGreen;
                            lineColor = Color.DarkOliveGreen;
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        case "EmodulvelY":
                            gridColor = Color.HotPink;
                            lineColor = Color.HotPink;
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        case "EmodulaccY":
                            gridColor = Color.Brown;
                            lineColor = Color.Brown;
                            axisplace = (float)htaxiscoordsY[curvenameY.TrimEnd('Y')];
                            break;
                        default:
                            gridColor = Color.Black;
                            lineColor = Color.Black;
                            break;

                    } // switch (curvenameY)
                }
                catch
                {
                    logg.Write("trolig orsak; hittar ingen kurva " + curvenameY + ",  i htaxiscoordsY");
                    foreach (DictionaryEntry cu in htaxiscoordsY)
                    {
                        logg.Write("htaxiscoordsY: Key = " + (string)cu.Key + "  Value = " + (string)cu.Value + "\n");
                    }
                }

                //****************************************
                // skapa y-axel
                aPen = new Pen(gridColor, 2f);
                g.DrawLine(aPen, axisplace, plotarea.Y, axisplace, plotarea.Height);

                //****************************************
                // plotta kurvorna i pdata
                // bestäm plotskalfaktorerna yrange o xrange
                if (ylimmax - ylimmin != 0.0)
                {
                    yrange = (plotarea.Height - plotarea.Y) / (ylimmax - ylimmin);
                }
                else
                {
                    yrange = 0;
                }
                if (xlimmax - xlimmin != 0.0)
                {
                    xrange = (plotarea.Width - plotarea.X) / (xlimmax - xlimmin);
                }
                else
                {
                    xrange = 0;
                }
                logg.Write("xrange: = " + xrange);

                // plotoffsetX = antal pixlar från 
                // kurvansstartpunkt till valt xlimmin
                plotoffsetX = xlimmin * xrange;
                

                aPen = new Pen(lineColor, 1f);
                aPen.DashStyle = lineDash;

                //try
                //{
                    for (int k = plotstartindexX + 1; k < plotendindexX; k++)
                    {
                        //if (float.Parse(pdata[k][0]) * xrange + plotarea.X > plotarea.Width
                        //    || plotarea.Height - float.Parse(pdata[k][1]) * yrange < 0)
                        //{
                        //    // do nothing   // rita inte om punkten ligger utanför ritarean
                        //}
                        g.DrawLine(aPen, (float.Parse(pdata[k - 1][0]) - xlimmin) * xrange + plotarea.X, plotarea.Height - float.Parse(pdata[k - 1][1]) * yrange,
                                         (float.Parse(pdata[k][0]) - xlimmin) * xrange + plotarea.X, plotarea.Height - float.Parse(pdata[k][1]) * yrange);
                    }
                //}
                //catch
                //{
                //    // do nothing
                //}
                curvestart = curvestart + curvepointsX;

                //****************************************
                // rita Rp02 linjerna
                if (curvenameY == "TensionY")
                {
                    for (int m = 0; m < rp02vallinesdata.Count; m++)
                    {
                        ArrayList alrp02ld = (ArrayList)rp02vallinesdata[m];
                        /* alrp02ld
                         * alrp02ld[0] = linjenamn
                         * alrp02ld[1] = lutning
                         * alrp02ld[2] = konstant
                         * */
                        if (float.Parse(alrp02ld[1].ToString()) == 0)
                        {
                            // do nothing
                        }
                        else
                        {
                            // bestäm mätvärdena för vänstra punkten
                            float plotvallineY0 = 0;
                            float plotvallineX0 = (plotvallineY0 - float.Parse(alrp02ld[2].ToString())) / float.Parse(alrp02ld[1].ToString());
                            if (plotvallineX0 < 0)
                            {
                                plotvallineY0 = float.Parse(alrp02ld[1].ToString()) * xlimmin + float.Parse(alrp02ld[2].ToString());
                                plotvallineX0 = xlimmin;
                            }


                            // bestäm mätvärdena för högra punkten
                            float plotvallineY1 = ylimmax;
                            float plotvallineX1 = (plotvallineY1 - float.Parse(alrp02ld[2].ToString())) / float.Parse(alrp02ld[1].ToString());
                            if (plotvallineX1 > xlimmax)
                            {
                                plotvallineY1 = float.Parse(alrp02ld[1].ToString()) * xlimmax + float.Parse(alrp02ld[2].ToString());
                                plotvallineX1 = xlimmax;
                            }

                            // beräkna plotpunkterna
                            float plotpixlineX0 = plotvallineX0 * xrange + plotarea.X - plotoffsetX;
                            float plotpixlineY0 = plotarea.Height - plotvallineY0 * yrange;
                            float plotpixlineX1 = plotvallineX1 * xrange + plotarea.X - plotoffsetX;
                            float plotpixlineY1 = plotarea.Height - plotvallineY1 * yrange;
                            g.DrawLine(aPen, plotpixlineX0, plotpixlineY0, plotpixlineX1, plotpixlineY1);

                            // plotta gränserna som ligger till grund för emodulberäkningen
                            int empointdiam = 6;
                            int empointrad = empointdiam / 2;
                            Brush emBrush = new SolidBrush(Color.Red);
                            if (alrp02ld[0].ToString() == "Emodul")
                            {
                                float plotpixintervalXbottom = float.Parse(alrp02ld[5].ToString()) * xrange + plotarea.X - plotoffsetX;
                                float plotpixintervalYbottom = plotarea.Height - float.Parse(alrp02ld[6].ToString()) * yrange;
                                float plotpixintervalXtop = float.Parse(alrp02ld[3].ToString()) * xrange + plotarea.X - plotoffsetX;
                                float plotpixintervalYtop = plotarea.Height - float.Parse(alrp02ld[4].ToString()) * yrange;
                                
                                if (plotpixintervalXbottom < plotarea.Left)
                                {
                                    // do nothing
                                }
                                else
                                {
                                    Rectangle axcirc = new Rectangle((int)plotpixintervalXbottom - empointrad, (int)plotpixintervalYbottom - empointrad, empointdiam, empointdiam);
                                    g.FillPie(emBrush, axcirc, 0, 360);
                                }
                                if (plotpixintervalXtop < plotarea.Left)
                                {
                                    // do nothing
                                }
                                else
                                {
                                    Rectangle axcirc = new Rectangle((int)plotpixintervalXtop - empointrad, (int)plotpixintervalYtop - empointrad, empointdiam, empointdiam);
                                    g.FillPie(emBrush, axcirc, 0, 360);
                                }
                            }
                            //cBrush.Dispose();
                            emBrush.Dispose();

                        } // else   --if (float.Parse(ald[1].ToString()) == 0)--

                    } // for (int m = 0; m < ldata.Count; m++)

                    //****************************************
                    // plotta kalkylerade egenskapsvärden
                    int properypointdiam = 8;
                    int properypointrad = properypointdiam / 2;
                    Brush propBrush = new SolidBrush(Color.Blue);
                    for (int m = 0; m < plotvalpointsdata.Count; m++)
                    {
                        ArrayList alvalpoint = (ArrayList)plotvalpointsdata[m];
                        float plotpixpropertypointX = float.Parse(alvalpoint[0].ToString()) * xrange + plotarea.X - plotoffsetX;
                        float plotpixpropertypointY = plotarea.Height - float.Parse(alvalpoint[1].ToString()) * yrange;
                        Rectangle axcirc = new Rectangle((int)plotpixpropertypointX - properypointrad, (int)plotpixpropertypointY - properypointrad, properypointdiam, properypointdiam);
                        g.FillPie(propBrush, axcirc, 0, 360);
                        //logg.Write("plotY  " + "  " + alvalpoint[1].ToString());
                    }
                    propBrush.Dispose();
                    //logg.Write("  ");

                } // if (curvenameY == "TensionY")

                //****************************************
                // Create the y-axis tick marks
                aBrush = new SolidBrush(tickFontColor);
                if (ylimmax == 0.0)
                {
                    // do nothing
                }
                else
                {
                    double dY = Scaletick(plotarea.Height, ylimmax, ylimmin, (int)axisYlableSize.Height);    //skaldel i skalsort ex mm
                    double dYack = 0;
                    float fY = 0;
                    if (yrange == 0.0)
                    {
                        //do nothing "(dYack - ylimmin) * yrange" är alltid < "plotarea.Height - plotarea.Y" 
                    }
                    else
                    {
                        while (dYack * yrange < plotarea.Height - plotarea.Y)
                        {
                            // rita skalstrecket
                            fY = plotarea.Height - (float)dYack * (float)yrange;
                            PointF yAxisPoint = new PointF(axisplace, fY);
                            g.DrawLine(Pens.Black, yAxisPoint, new PointF(axisplace + 4f,
                                               yAxisPoint.Y));
                            // skriv ut texten
                            StringFormat sFormat = new StringFormat();
                            sFormat.Alignment = StringAlignment.Far;
                            SizeF sizeYTick = g.MeasureString(dYack.ToString(), tickFont);
                            g.DrawString(dYack.ToString(), tickFont, aBrush,
                                        new PointF(axisplace, yAxisPoint.Y - sizeYTick.Height / 2), sFormat);
                            // skapa y-värde för nästa skalstreck
                            dYack = dYack + dY;

                        } // while (dYack * yrange < plotarea.Height)
                    } // else  -- if (yrange == 0.0)

                } // else  --if (ylimmax == 0.0)

            } // while ( start < pdata.Length)

            //****************************************
            // Create the x-axis tick marks
            aBrush = new SolidBrush(tickFontColor);
            double dXval = Scaletick(plotarea.Width + plotarea.X, xlimmax, xlimmin, (int)axisXlableSize.Width);
            double dXvalack = 0;
            double dXvalacknum = dXval;
            float fX = 0;
            float fXnum = 0;

            if ((float)dXval * (float)xrange < 45)
            {
                dXval = 2 * dXval;
                dXvalacknum = dXval;
            }
            if ((float)dXval * (float)xrange < 45)
            {
                dXval = 2.5 * dXval;
                dXvalacknum = dXval;
            }

            // justera antal pixlar från xlimmin till första tickmark
            double dXvalscaleadjustment = dXval - xlimmin;
            dXvalack = dXvalscaleadjustment;

            // bygg skalan
            while (dXvalack * xrange < plotarea.Width - plotarea.X)
            {
                fX = (float)dXvalack * (float)xrange + plotarea.X;
                fXnum = (float)dXvalacknum;

                if (fX > plotarea.Left)
                {
                    PointF yAxisPoint = new PointF(fX, plotarea.Height);
                    g.DrawLine(Pens.Black, yAxisPoint, new PointF(yAxisPoint.X,
                                       yAxisPoint.Y + 5f));

                    StringFormat sFormat = new StringFormat();
                    sFormat.Alignment = StringAlignment.Far;
                    SizeF sizeXTick = g.MeasureString(fXnum.ToString(), tickFont);
                    g.DrawString((fXnum).ToString(), tickFont, aBrush,
                                new PointF(yAxisPoint.X + sizeXTick.Width / 2, yAxisPoint.Y + 6f), sFormat);
                }
                
                dXvalack = dXvalack + dXval;
                dXvalacknum = dXvalacknum + dXval;

            } // while (dXack * xrange < plotarea.Width + plotarea.X)

            aPen.Dispose();
            aBrush.Dispose();

 
            //// Create vertical gridlines:
            //SizeF tickFontSize = g.MeasureString("A", tickFont);
            //float fX, fY;
            //if (isYGrid == true)
            //{
            //    aPen = new Pen(gridColor, 1f);
            //    aPen.DashStyle = gridPattern;
            //    for (fX = parea.Left + xTick; fX < parea.Width; fX += xTick)
            //    {
            //        g.DrawLine(aPen, Point2D(new PointF(fX, parea.Top)),
            //            Point2D(new PointF(fX, parea.Height)));
            //    }
            //}

            //// Create horizontal gridlines:
            //if (IsXGrid == true)
            //{
            //    aPen = new Pen(GridColor, 1f);
            //    aPen.DashStyle = GridPattern;
            //    for (fY = YLimMin + YTick; fY < YLimMax; fY += YTick)
            //    {
            //        g.DrawLine(aPen, Point2D(new PointF(XLimMin, fY)),
            //            Point2D(new PointF(XLimMax, fY)));
            //    }
            //}


        } // public void DrawPlotArea(Graphics g, Rectangle parea, String[][] pdata, ArrayList ldata)
    
        //**********************************************************
        private double Scaletick(int linelength, double valmax, double valm1n, int lbwidth)
        {
            double scaletick = 0.0;
            int tenpot = 10;
            int i = 0;
            double sc = valmax;     //sc mätvärde
            double screm = 0.0;     //mätvärdets decimaldel
            double scint = 0.0;     //mätvärdets heltalsdel

            // mätvärden mindre än 1 utefter axeln
            if (valmax < 1.0)
            {
                if (valmax == 0.0 || sc == 0.0)
                {
                    // do nothing
                }
                else
                {
                    // tag reda på 10-potensen
                    while (sc < 1.0)
                    while (sc < 10.0)   // ger tätare steg
                    {
                        i++;
                        sc = sc * tenpot;
                    }
                }
                screm = sc % 1;
                scint = sc - screm;
                //if (scint < 8.0)
                //{
                    scaletick = 1;
                    for (int p = 0; p < i; p++)
                    {
                        scaletick = scaletick / tenpot;
                    }
                //}
                //else
                //{
                //    scaletick = 2;
                //    for (int p = 0; p < i; p++)
                //    {
                //        scaletick = scaletick / tenpot;
                //    }
                //}
            }

            // mätvärden större än eller lika med 1 utefter axeln
            else
            {
                screm = (valmax / 10) % 1;
                scint = valmax / 10 - screm;
                // tag reda på 10-potensen
                while (scint > 1)
                //while (scint > 10)    // ger tätare steg
                {
                    i++;
                    scint = scint / tenpot;
                }
                //if (scint < 8.0)
                //{
                    scaletick = 1;
                    for (int p = 0; p < i; p++)
                    {
                        scaletick = scaletick * tenpot;
                    }
                //}
                //else
                //{
                //    scaletick = 2;
                //    for (int p = 0; p < i; p++)
                //    {
                //        scaletick = scaletick * tenpot;
                //    }
                //}
            }
            return scaletick;   // returnera skaldelar i antal pixel

        } // private int Scaletick(int linelength, double valmax, int lbwidth)

        //**********************************************************

    } // class ChartArea

} // namespace DragKurva
