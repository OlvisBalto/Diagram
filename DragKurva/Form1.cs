using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//***********************
using System.Drawing.Drawing2D;
using System.Collections;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;



namespace DragKurva
{
    public partial class Form1 : Form
    {
        private int windowrightleftmagin = 0;
        private int windowtopbottommargin = 0;
        private String filename = "";
        private ChartArea chartarea = new ChartArea();
        private Rectangle chartpanelrect = new Rectangle();

        private Rectangle plotarea = new Rectangle();
        private String radioX = "";
        private ArrayList radioY = new ArrayList();
        private String title = "DragProvsKurvor";
        private String plotXvaluemax = "";
        private String plotXvaluemin = "";
        private String fmedel = "";
        private String fkonst = "";
        private String emodmax = "";
        private String emodmin = "";

        private ArrayList force = new ArrayList();
        private ArrayList strain = new ArrayList();
        private ArrayList tension = new ArrayList();
        private ArrayList width = new ArrayList();
        private ArrayList path = new ArrayList();
        private ArrayList time = new ArrayList();
        private ArrayList em = new ArrayList();
        private ArrayList emvel = new ArrayList();
        private ArrayList emacc = new ArrayList();

        private Curves cur = new Curves();
        private CurveHandling curhand = new CurveHandling();

        private Bitmap bmp = new Bitmap(10, 10); // dummy
        private Bitmap chartpart = new Bitmap(10, 10);
        private Point chartre = new Point(0, 0);
        private float xrange = 0;

        private bool newfile = false;
        private bool numcurvechanged = false;
        private bool testmode = false;
        private bool analysemode = true;


        private Point myOriginPoint = new Point();  //Represents the Ox, Oy
        private Point myIntermediatePoint = new Point();  //Represents the Ix, Iy
        private Pen mySelectionRectangle = new Pen(Brushes.Blue);  //Pen that will be used to draw the rectangle
        private Pen mySelectionPen = new Pen(Brushes.Blue);  //Pen that will be used to draw the rectangle
        private bool moveflag = true;

        private Loggar logg = new Loggar();

        private Thread Thread1;
        private bool threadstarted = false;
        private TestWatchServer Server1;
        //private TestWatchServer machineserver2;
        //private TestWatchServer machineserver3;
        //private TestWatchServer machineserver4;
        //private Server s = new Server();
        private Server s;
        private int nn = 0;
        private string tbX = "";
        private string tbY = "";
        private string ipadress = "";
        private string ipport = "";

        private string getfrom = "";
        private string areagetfrom = "";
        //private bool rubberband = false;

         
        //*************************************************************
        public Form1()
        {
            //logg.Write("Form1()) in");

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();

            // ange marginalerna p� ytan "chartpanel2" som anv�nds 
            // f�r att rita ut kurvorna
            windowrightleftmagin = 30;
            windowtopbottommargin = 50;
            SetStyle(ControlStyles.ResizeRedraw, true);

            // ange chartare, den yta som kurvorna skall h�lla sig inom
            chartarea.SetChartPanel(this, chartpanel2, windowrightleftmagin, windowtopbottommargin);
            // s�tt f�nstrets "chartpanel2"-parametrar till vad som kalkylerats
            // vad som kalkylerats i "chartarea"
            chartpanel2 = chartarea.Pan1;
            // skap koppling mellan "chartpanel2"'s paint metod och 
            // en variant i detta program "drawpanel1Paint" som skall styra
            // vad som skall ritas n�r onPaint aktiveras
            // "drawpanel1Paint" �r definierad som metod l�ngre ner i denna kod
            chartpanel2.Paint += new PaintEventHandler(drawpanel1Paint);

            // "chartpanelrect" �r en rektangel med "chartpanel2"'s m�tt
            chartpanelrect.X = chartpanel2.Left;
            chartpanelrect.Y = chartpanel2.Top;
            chartpanelrect.Width = chartpanel2.Width;
            chartpanelrect.Height = chartpanel2.Height;

            //logg.Write("Form1()) ut");

        } // public Form1()

        //*************************************************************
        private void Form1_Load(object sender, EventArgs e)
        {
            testmode = rbProvning.Checked;
            analysemode = rbAnalys.Checked;
            // initiera "Server1" med kunskap om vilka textboxar i "Form1"
            // som skall ha info f�rutom sj�lva diagrammet
            Server1 = new TestWatchServer(textBox1, tbXvalue, tbYvalue, tbID);


            Server se = new Server();
            String line = "";
            int lineindex = 0;
            int num = 0;
            // h�mta anslutningsdata f�r servern
            // "DragKurva.ini" ligger p� samma st�lle som "DragKurva.exe"
            using (StreamReader oParsad = new StreamReader("DragKurva.ini"))
            //using st�nger str�mmen efter EOF
            {
                while ((line = oParsad.ReadLine()) != null)
                {
                    lineindex = line.LastIndexOf(" ");
                    switch (num)
                    {
                        case 0:
                            fmedel = line.Substring(lineindex, line.Length - lineindex);
                            break;
                        case 1:
                            fkonst = line.Substring(lineindex, line.Length - lineindex);
                            break;
                        case 2:
                            se.Ipadress = line.Substring(lineindex, line.Length - lineindex);
                            break;
                        case 3:
                            se.Ipport = Int32.Parse(line.Substring(lineindex, line.Length - lineindex));
                            break;
                        case 4:
                            logg.Loglocation = line.Substring(lineindex, line.Length - lineindex);
                            break;
                        default:
                            break;
                    }
                    num++;
                }
            }
        } // private void Form1_Load(object sender, EventArgs e)

        //*************************************************************
        private void drawpanel1Paint(object sender, PaintEventArgs e)
        {
            // denna rutin g�s igenom varje g�ng onPaint aktiveras i
            // h�ndelsehanteraren kopplingen �r gjord i "Form()"
            //logg.Write("drawpanel1Paint()) in");

            Graphics g = e.Graphics;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // "radioY" anger vika kurvor som �r valda attt visas
            if (radioY.Count == 0)
            {
                // om radioY.Count = 0 �r inga kurvor invalda
                // detta �r fallet vid uppstart
                // do nothing;
            }
            else
            {   // "radioY" inneh�ller ett antal kurvor som skall visas
                

                ////++++++++++ Visualiserar diagrampanelen ++++++++++++++++++++++++++++++++++++++++++++++++++++++
                ///* pdiam = omskrivande kvadraten = prickens diameter
                // * prad = prickens radie
                // * */
                //int pdiam = 10;
                //int prad = pdiam / 2;
                ////Graphics g = e.Graphics;
                //Brush dBrush = new SolidBrush(Color.Red);
                //Rectangle circ = new Rectangle(chartarea.Pan1.Left - 190 - prad, chartarea.Pan1.Top - 50 - prad, pdiam, pdiam);
                //g.FillPie(dBrush, circ, 0, 360);
                ////****
                //circ = new Rectangle(chartarea.Pan1.Left - 190 - prad, chartarea.Pan1.Height - prad, pdiam, pdiam);
                //g.FillPie(dBrush, circ, 0, 360);
                ////****
                //circ = new Rectangle(chartarea.Pan1.Width - prad, chartarea.Pan1.Top - 50 - prad, pdiam, pdiam);
                //g.FillPie(dBrush, circ, 0, 360);
                ////****
                //circ = new Rectangle(chartarea.Pan1.Width - prad, chartarea.Pan1.Height - prad, pdiam, pdiam);
                //g.FillPie(dBrush, circ, 0, 360);
                //dBrush.Dispose();
                ////+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                // en instans av "curhand" har gjort i ordning kurvorna som skall visas
                // samtidigt skapa de linjer och punkter som �r asossierade med n�gon kurva
                // "plotdata" inneh�ller kurvpunkter och styrdata f�r uppritningen av kurvorna
                // "linesdata inneh�ller data f�r uppritning av asossierade linjer
                // pointsdata inneh�ller data f�r uts�ttning av as0ssierade punkter
                String[][] plotdata = curhand.Visablecurves;
                ArrayList linesdata = curhand.Lines;
                ArrayList pointsdata = curhand.Points;

                if (plotdata.Length == 1)
                {
                    // do nothing
                }
                else
                {
                    // s�tt ut rubriker f�r diagrammet och dess axlar
                    chartarea.DrawChartArea(g, chartarea.Pan1, radioX, radioY, title, getfrom, areagetfrom);
                    plotarea = chartarea.Plotrect;

                    if (tension.Count > 6)
                    {
                        // rita ut kurvorna och dess axlar
                        chartarea.DrawPlotArea(g, plotarea, plotdata, linesdata, pointsdata, getfrom, areagetfrom);
                        xrange = chartarea.Xrange;
                    }
                }

                newfile = false;
                numcurvechanged = false;

                g.Dispose();

            } // else --if (radioY.Count == 0) 

            //logg.Write("drawpanel1Paint()) ut");

        } // private void drawpanel1Paint(object sender, PaintEventArgs e)

        //**************************************************************
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //logg.Write("Form1_SizeChanged()) in");

            chartpanel2.Width = this.Width - chartpanel2.Left - windowrightleftmagin;
            chartpanel2.Height = this.Height - chartpanel2.Top - windowtopbottommargin;
            //areagetfrom = "Form1_SizeChanged";
            //logg.Write("Form1_SizeChanged() " + "  " + getfrom + "  " + areagetfrom);
            GetCurves("Form1_SizeChanged");
            chartpanel2.Invalidate();


        } // private void Form1_SizeChanged(object sender, EventArgs e)

        //**************************************************************
        private void rbStdForceX_Click(object sender, EventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                numcurvechanged = true;
                //areagetfrom = "rbStdForceX_Click";
                //logg.Write("rbStdForceX_Click() " + "  " + getfrom + "  " + areagetfrom);
                GetCurves("rbStdForceX_Click");
                chartpanel2.Invalidate();
            }

        } // private void rbStdForceX_Click(object sender, EventArgs e)

        //*****************************************************************************
        private void btHelaKurvan_Click(object sender, EventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                tbXmax.Text = "Auto";
                tbXmin.Text = "Auto";
                //areagetfrom = "btHelaKurvan_Click";
                //logg.Write("btHelaKurvan_Click() " + "  " + getfrom + "  " + areagetfrom);
                GetCurves("btHelaKurvan_Click");
                chartpanel2.Invalidate();
            }
        }

        //**************************************************************
        private void tbXmax_KeyDown(object sender, KeyEventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        if (float.Parse(tbXmax.Text) == 0)
                        {
                            // do nothing
                        }
                        else
                        {
                            plotXvaluemax = tbXmax.Text;
                            //areagetfrom = "tbXmax_KeyDown";
                            //logg.Write("tbXmax_KeyDown() " + "  " + getfrom + "  " + areagetfrom);
                            GetCurves("tbXmax_KeyDown");
                            chartpanel2.Invalidate();
                        }
                    }
                    catch
                    {
                        tbXmax.Text = "Auto";
                        plotXvaluemax = "Auto";
                    }
                }

                //areagetfrom = "tbXmax_KeyDown";
                //logg.Write("tbXmax_KeyDown() " + "  " + getfrom + "  " + areagetfrom);
                GetCurves("tbXmax_KeyDown");
                chartpanel2.Invalidate();
            }

        }  // private void tbXmax_KeyDown(object sender, KeyEventArgs e)
       
        //**************************************************************
        private void tbXmin_KeyDown(object sender, KeyEventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        if (float.Parse(tbXmin.Text) == 0)
                        {
                            // do nothing
                        }
                        else
                        {
                            plotXvaluemin = tbXmin.Text;
                            //areagetfrom = "tbXmin_KeyDown";
                            //logg.Write("tbXmin_KeyDown() " + "  " + getfrom + "  " + areagetfrom);
                            GetCurves("tbXmin_KeyDown");
                            chartpanel2.Invalidate();
                        }
                    }
                    catch
                    {
                        tbXmin.Text = "Auto";
                        plotXvaluemin = "Auto";
                    }
                }

                //areagetfrom = "tbXmin_KeyDown";
                //logg.Write("tbXmin_KeyDown() " + "  " + getfrom + "  " + areagetfrom);
                GetCurves("tbXmin_KeyDown");
                chartpanel2.Invalidate();
            }

        } // private void tbXmin_KeyDown(object sender, KeyEventArgs e)

        //**************************************************************
        private void bGetFile_Click(object sender, EventArgs e)
        {
            //logg.Write("bGetFile_Click()) in");
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                OpenFileDialog getfile = new OpenFileDialog();
                DialogResult result = getfile.ShowDialog();

                string name = "";
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                if (result == DialogResult.OK) // Test result.
                {
                    name = getfile.FileName;
                    if (name == "" || name == null)
                    {
                        MessageBox.Show("Filen kan ej hittas", "S�kfel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        filename = name;
                        // s�tt "tbXmax.Text" och "tbXmin.Text" till Auto
                        // f�r att visa hela kurvan n�r den visas f�rsta g�ngen
                        tbXmax.Text = "Auto";
                        tbXmin.Text = "Auto";
                        newfile = true;

                        // inaktivera att gummibandsrektangel visas n�r musen r�rs
                        moveflag = false;
                        
                        //areagetfrom = "bGetFile_Click";
                        //logg.Write("bGetFile_Click() " + "  " + getfrom + "  " + areagetfrom);
                        
                        // hoppa till styrning av vad som skall visas
                        GetCurves("bGetFile_Click");
                        chartpanel2.Invalidate();
                    }

                } // if (result == DialogResult.OK)

            } // else  --if (rbProvning.Checked == true)--
            
        } // private void bGetFile_Click(object sender, EventArgs e)

        //**************************************************************
        private void tbfmedel_TextChanged(object sender, EventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                //areagetfrom = "tbfmedel_TextChanged";
                //logg.Write("tbfmedel_TextChanged() " + "  " + getfrom + "  " + areagetfrom);
                GetCurves("tbfmedel_TextChanged");
                fmedel = tbfmedel.Text;
            }

        } // private void textBox5_TextChanged(object sender, EventArgs e)

        //**************************************************************
        private void tbfiltkonst_TextChanged(object sender, EventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                //areagetfrom = "tbfiltkonst_TextChanged";
                //logg.Write("tbfiltkonst_TextChanged() " + "  " + getfrom + "  " + areagetfrom);
                GetCurves("tbfiltkonst_TextChanged");
                if (tbfiltkonst.Text == "" || tbfiltkonst.Text == "1" || tbfiltkonst.Text == null)
                {
                    fkonst = "2";
                }
                else
                {
                    fkonst = tbfiltkonst.Text;
                }
                chartpanel2.Invalidate();
            }

        } // private void textBox6_TextChanged(object sender, EventArgs e)

        //**************************************************************
        private void tbEmodmax_KeyDown(object sender, KeyEventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    emodmax = tbEmodmax.Text;
                    //areagetfrom = "tbEmodmax_KeyDown";
                    //logg.Write("tbEmodmax_KeyDown() " + "  " + getfrom + "  " + areagetfrom);
                    GetCurves("tbEmodmax_KeyDown");
                    chartpanel2.Invalidate();
                }
            }

        } // private void tbEmodmax_KeyDown(object sender, KeyEventArgs e)

        //**************************************************************
        private void tbEmodmin_KeyDown(object sender, KeyEventArgs e)
        {
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    emodmin = tbEmodmin.Text;
                    //areagetfrom = "tbEmodmin_KeyDown";
                    //logg.Write("tbEmodmin_KeyDown() " + "  " + getfrom + "  " + areagetfrom);
                    GetCurves("tbEmodmin_KeyDown");
                    chartpanel2.Invalidate();
                }
            }

        } // private void tbEmodmin_KeyDown(object sender, KeyEventArgs e)

        //**************************************************************
        // definitioner f�r gummibandsrektangel som flyttats till b�rjan i filen
        ////private Point myOriginPoint = new Point();  //Represents the Ox, Oy
        ////private Point myIntermediatePoint = new Point();  //Represents the Ix, Iy
        ////private Pen mySelectionRectangle = new Pen(Brushes.Blue);  //Pen that will be used to draw the rectangle

        //**************************************************************
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            //logg.Write("OnMouseDown()) in");
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                if (e.Button == MouseButtons.Left) //Draw the rectangle only when the left mouse button is pressed
                {
                    myOriginPoint.X = e.X;  //Represents the current mouse's X location
                    if (myOriginPoint.X < plotarea.Left)
                    {
                        myOriginPoint.X = 0 + plotarea.Left;  //Represents the current mouse's X location
                    }
                    myOriginPoint.Y = e.Y;  //Represents the current mouse's Y location

                    bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    Graphics g = Graphics.FromImage(bmp);

                    // hela chartpanel2
                    Size bmpseiz = new Size(chartpanel2.Width, chartpanel2.Height);

                    // kopiera hela chartpanel2
                    Point controlScreenPoint = chartpanel2.PointToScreen(Point.Empty);
                    g.CopyFromScreen(controlScreenPoint.X, controlScreenPoint.Y, 0, 0, bmpseiz);
                    g.Dispose();

                } // if (e.Button == MouseButtons.Left)
                else if (e.Button == MouseButtons.Right)
                {
                    bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    Graphics g = Graphics.FromImage(bmp);

                    // hela chartpanel2
                    Size bmpseiz = new Size(chartpanel2.Width, chartpanel2.Height);

                    // kopiera hela chartpanel2
                    Point controlScreenPoint = chartpanel2.PointToScreen(Point.Empty);
                    g.CopyFromScreen(controlScreenPoint.X, controlScreenPoint.Y, 0, 0, bmpseiz);
                    g.Dispose();

                } // else if (e.Button == MouseButtons.Right)

            } // else  --if (rbProvning.Checked == true)--

        } // private void OnMouseDown(object sender, MouseEventArgs e)

        //**************************************************************
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //logg.Write("OnMouseMove()) in");
            //rubberband = false;

            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                // rita upp gummibandsrektangeln s� l�nge musen r�rs
                if (e.Button == MouseButtons.Left && moveflag == true)
                {
                    myIntermediatePoint.X = e.X;
                    if (myIntermediatePoint.X > plotarea.Left + plotarea.Width)
                    {
                        myIntermediatePoint.X = plotarea.Left + plotarea.Width;
                    }
                    myIntermediatePoint.Y = e.Y;

                    // rita bitmappen p� a som �r chartpane2's Graphics
                    Graphics a = chartpanel2.CreateGraphics();  //To draw over the form
                    a.DrawImage(bmp, 0, 0);


                    //Color.Thistle
                    //drawing the horizontal line -- nearer edge  
                    a.DrawLine(mySelectionPen, myOriginPoint.X, myOriginPoint.Y, myIntermediatePoint.X, myOriginPoint.Y);
                    //drawing the vertical line -- farther edge
                    a.DrawLine(mySelectionPen, myIntermediatePoint.X, myIntermediatePoint.Y, myIntermediatePoint.X, myOriginPoint.Y);
                    //drawing the horizontal line -- farther edge
                    a.DrawLine(mySelectionPen, myIntermediatePoint.X, myIntermediatePoint.Y, myOriginPoint.X, myIntermediatePoint.Y);
                    //drawing the vertical line -- nearer edge
                    a.DrawLine(mySelectionPen, myOriginPoint.X, myOriginPoint.Y, myOriginPoint.X, myIntermediatePoint.Y);
                    a.Dispose();


                } // if (e.Button == MouseButtons.Left)
                else if (e.Button == MouseButtons.Right)
                {
                    myIntermediatePoint.X = e.X;
                    myIntermediatePoint.Y = e.Y;

                    // rita bitmappen p� a som �r chartpane2's Graphics
                    Graphics a = chartpanel2.CreateGraphics();  //To draw over the form
                    a.DrawImage(bmp, 0, 0);

                    //drawing the vertical line 
                    a.DrawLine(mySelectionPen, myIntermediatePoint.X, 50, myIntermediatePoint.X, plotarea.Height);

                    a.Dispose();

                } // else if (e.Button == MouseButtons.Right)

            } // else  --if (rbProvning.Checked == true)--

        } // private void OnMouseMove(object sender, MouseEventArgs e)

        //*****************************************************************************
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            //logg.Write("OnMouseUp()) in");
            if (testmode == true)
            {
                // do nothing
            }
            else
            {
                if (e.Button == MouseButtons.Left) //Draw the rectangle only when the left mouse button is released
                {
                    if (newfile == false)
                    {
                        myIntermediatePoint.X = e.X;
                        myIntermediatePoint.Y = e.Y;
                        
                        // skapa n�got att rita p� till chartpanel2
                        Graphics aGraphics = chartpanel2.CreateGraphics();
                        aGraphics.DrawImage(bmp, 0, 0);
                        aGraphics.Dispose();

                        float plotXpixmax = 0;
                        float plotXpixmin = 0;
                        //float plotYpixmax = 0;    // anv�nds inte �n
                        //float plotYpixmin = 0;    // anv�nds inte �n

                        if (myOriginPoint.X >= plotarea.Left)
                        {
                            plotXpixmin = myOriginPoint.X - plotarea.Left;
                        }
                        else
                        {
                            plotXpixmin = 0;
                        }

                        if (myIntermediatePoint.X > plotarea.Left)
                        {
                            plotXpixmax = myIntermediatePoint.X - plotarea.Left;
                        }
                        else
                        {
                            plotXpixmax = plotarea.Width - plotarea.Left;
                        }

                        // justera s� �vre v�nstra h�rnet �r starpunkten och 
                        // nedre h�gra h�rnet �r slutpunkten
                        if (plotXpixmin <= plotXpixmax)
                        {
                            // do nothing   plotXpixmin = plotXpixmin;
                            //              plotXpixmax = plotXpixmax;
                        }
                        else
                        {
                            plotXpixmin = plotXpixmax;
                            plotXpixmax = plotXpixmin;
                        }

                        if (xrange != 0)
                        {
                            // om musknappen tryckte och sl�ppts p� samma st�lle 
                            // allts� ingen rektangel ritad
                            if (plotXpixmax == plotXpixmin)
                            {
                                tbXmax.Text = "Auto";
                                tbXmin.Text = "Auto";
                            }
                            // annars rektangel ritad
                            else
                            {
                                if (tbXmin.Text == "Auto")
                                {
                                    tbXmin.Text = "0";
                                }
                                plotXvaluemax = ((plotXpixmax / xrange) + float.Parse(tbXmin.Text)).ToString();  // origo skall plussas p�
                                tbXmax.Text = plotXvaluemax;
                                plotXvaluemin = ((plotXpixmin / xrange) + float.Parse(tbXmin.Text)).ToString();  // origo skall plussas p�
                                tbXmin.Text = plotXvaluemin;

                                logg.Write("plotXpixmin: " + plotXpixmin);
                                logg.Write("tbXmin.Text: " + tbXmin.Text);
                                logg.Write("plotXvaluemin: " + plotXvaluemin);
                            }
                        }

                    } // if (newfile == false)
                    else
                    {
                        newfile = false;
                        moveflag = true;
                    }
                    // OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS
                    // f�r att kunna ta hand om periodiska och slutna kurvor
                    // m�ste indexet pixmax och pixmin vidarebefordras 
                    // ist�llet f�r valuemax och valuemin
                    // OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS OBS 


                    //areagetfrom = "OnMouseUp";
                    //logg.Write("OnMouseUp() " + "  " + getfrom + "  " + areagetfrom);
                    GetCurves("OnMouseUp");
                    chartpanel2.Invalidate();
                    //rubberband = false;

                } // if (e.Button == MouseButtons.Left)
                else if (e.Button == MouseButtons.Right) //Draw the rectangle only when the left mouse button is pressed
                {

                }

            } // else  --if (rbProvning.Checked == true)--

        } //  private void OnMouseUp(object sender, MouseEventArgs e)

        //*****************************************************************************
        void GetCurves(string from)
        {
            //logg.Write("GetCurves()) in");
            //logg.Write("testmode �r " + testmode);

            if (testmode == false)
            {   // data som kommer fr�n ny fil ellerredan ligger f�rvisning visas
                if (filename != "" && filename != null)
                {
                    // g� igenom och vilka kurvor som skall visas "radioY"
                    // bes�m vilken x-axel som skall anv�ndas "radioX"
                    // i h�ndelse att en ny fil valts eller antal visade kurvor �ndrats
                    if (newfile == true || numcurvechanged == true)
                    {
                        //logg.Write("newfile==true");
                        tbDirectory.Text = filename;
                        radioY.Clear();
                        tbEmodmax.Text = "Auto";
                        tbEmodmin.Text = "Auto";
                        fmedel = tbfmedel.Text;
                        if (fkonst == "" || fkonst == "1" || fkonst == null) { fkonst = "2"; }
                        if (rbStdForceX.Checked == true) { radioX = "ForceX"; }
                        if (cbForceY.Checked == true) { radioY.Add("ForceY"); }
                        if (rbTrueStrainX.Checked == true) { radioX = "StrainX"; }
                        if (cbStrainY.Checked == true) { radioY.Add("StrainY"); }
                        if (rbTrueTensionX.Checked == true) { radioX = "TensionX"; }
                        if (cbTensionY.Checked == true) { radioY.Add("TensionY"); }
                        if (rbChangeWidthX.Checked == true) { radioX = "chWidthX"; }
                        if (cbChWidthY.Checked == true) { radioY.Add("chWidthY"); }
                        if (rbStandardPathX.Checked == true) { radioX = "PathX"; }
                        if (cbPathY.Checked == true) { radioY.Add("PathY"); }
                        if (cbEmodulY.Checked == true) { radioY.Add("EmodulY"); }
                        if (cbEmodulvelY.Checked == true) { radioY.Add("EmodulvelY"); }
                        if (cbEmodulaccY.Checked == true) { radioY.Add("EmodulaccY"); }

                        if (newfile == true)
                        {
                            // l�s in kurvdata fr�n fil respektive kurva finns 
                            // sedan i var sin ArrayList "cur.xxx" d�r xxx �r kuvtyp                            //
                            // "fmedel" anger hur m�nga punkter som skall anv�ndas
                            // vid ber�kning av emodulen med hj�lp av en sekantmetod
                            // som flyter efter kurvan 
                            cur.ReadDataFile(filename, fmedel);
                        }

                    } // if (newfile == true || numcurvechanged == true)

                    //logg.Write("if testmode==false");
                    // ArrayListor f�r respektive kurvtyp
                    force = cur.StandardForce;
                    strain = cur.Truestrain;
                    tension = cur.Truetension;
                    width = cur.Changeinwidth;
                    path = cur.Standardpath;
                    time = cur.Testtime;
                    em = cur.Emodul;
                    emvel = cur.Emodulvel;
                    emacc = cur.Emodulacc;

                    // skicka �ver kurvorna til kurvhanteringsinstansen "curhand"
                    curhand.StandardForce = force;
                    curhand.Truestrain = strain;
                    curhand.Truetension = tension;
                    curhand.Changeinwidth = width;
                    curhand.Standardpath = path;
                    curhand.Testtime = time;
                    curhand.Emodul = em;
                    curhand.Emodulvel = emvel;
                    curhand.Emodulacc = emacc;

                    // s�tt en del styrparametrar f�r visningen
                    plotXvaluemax = tbXmax.Text;
                    plotXvaluemin = tbXmin.Text;
                    fkonst = tbfiltkonst.Text;
                    emodmax = tbEmodmax.Text;
                    emodmin = tbEmodmin.Text;
                    logg.Write("GetCurves() plotXvaluemin: " + plotXvaluemin);

                    //for (int f = 0; f < radioY.Count; f++)
                    //{
                    //    logg.Write("GetCurves() HandleCurves i getCurves analys--  radioX " + radioX + "    radioY " + radioY[f]);
                    //}
                    // skapa en jaggad matris d�r dimension(0) �r kurvtyp
                    // och dimension(1) �r kurvdata och styrparametrar
                    // kurvunderlagen �r redan �verskickade ovan
                    curhand.HandleCurves(radioX, radioY, plotXvaluemax, plotXvaluemin, fkonst, emodmax, emodmin);
                    
                } // if (filename != "" && filename != null)
                else
                {
                    // do nothing
                }

            } // if (testmode == false)
            else
            {   // data som kommer fr�n klient visas

                //logg.Write("else testmode==false");
                ArrayList temp = new ArrayList();
                tension = s.Tens;
                strain = s.Strai;
                temp.Add(tension.Count.ToString());
                temp.Add(strain.Count.ToString());

                curhand.Truestrain = strain;
                curhand.Truetension = tension;
                //curhand.Testtime = time;

                plotXvaluemax = "Auto";
                plotXvaluemin = "Auto";
                fkonst = tbfiltkonst.Text;
                emodmax = "Auto";
                emodmin = "Auto";

                //kolla att de 5 f�rsta posterna finns itension
                // d� har inl�sningen fr�n klienten b�rjat 
                // och antal poster i �verf�ringen kan s�ttas av temp
                if (tension.Count < 5)
                {

                    // do nothing
                    // data har �nnu inte b�rjat komma ft�n klienten
                }
                else
                {
                    tension[5] = temp[0];
                    strain[5] = temp[1];
                    //for (int f = 0; f < radioY.Count; f++)
                    //{
                    //    logg.Write("GetCurves() HandleCurves i getCurves provning--  radioX " + radioX + "    radioY " + radioY[f]);
                    //}
                    if ("slut" == textBox1.Text)
                    {
                        curhand.Endflag = "slut";
                    }
                    curhand.HandleCurves(radioX, radioY, plotXvaluemax, plotXvaluemin, fkonst, emodmax, emodmin);
                    curhand.Endflag = "";
                }

            } // else  --if (testmode == false)--

            // visa en del v�rden
            tbEmodul.Text = curhand.Tbemodul;   // satt till heltal i classen CurveHandling
            tbRp02.Text = curhand.Tbrp02;       // satt till heltal i classen CurveHandling
            tbReh.Text = curhand.Tbreh;         // satt till heltal i classen CurveHandling
            tbRel.Text = curhand.Tbrel;         // satt till heltal i classen CurveHandling
            tbRm.Text = curhand.Tbrm;           // satt till heltal i classen CurveHandling
            if (curhand.Tbag != "" && curhand.Tbag.Length > 2)
            {
                tbAg.Text = curhand.Tbag.Substring(2, 2);
            }
            else
            {
                tbAg.Text = "";
            }
            if (curhand.Tba80 != "" && curhand.Tba80.Length > 2)
            {
                tbA80.Text = curhand.Tba80.Substring(2, 2);
            }
            else
            {
                tbA80.Text = "";
            }

        } // void GetCurves()

        //*****************************************************************************
        private void rbAnalys_Click(object sender, EventArgs e)
        {
            //logg.Write("rbAnalys_Click()) in");
            testmode = false;
            analysemode = true;
            curhand.Testmode = testmode;
            curhand.Analysemode = analysemode;

            if (testmode == false)
            {
                // om tr�den ej �r ig�ng d� f�rs�k att starta
                // servern misslyckades �r flaggan threadstarted 
                // �nd� lika med true d� flaggan sattes vid startf�rs�ket
                // s�tt om den till false s� kan ett nytt f�rs�k g�ras
                if (Thread1.IsAlive == false)
                {
                    threadstarted = false;
                }
               // logg.Write("rbAnalys_Click " + rbProvning.Checked);
                newfile = true;
                //areagetfrom = "rbAnalys_Click";
                //logg.Write("rbAnalys_Click() " + "  " + getfrom + "  " + areagetfrom);
                GetCurves("rbAnalys_Click");
                Server1.Serverstarted = true;
                Server1.Analysflag = true;
                tbXvalue.Text = "";
                tbYvalue.Text = "";
                tbID.Text = "";

            } // if (rbProvning.Checked == false)
            else
            {
                // do nothing
            }

            chartpanel2.Invalidate();
            
        } // private void rbAnalys_CheckedChanged(object sender, EventArgs e)

        //*****************************************************************************
        private void rbProvning_Click(object sender, EventArgs e)
        {
            //logg.Write("rbProvning_Click()) in");
            testmode = true;
            analysemode = false;
            curhand.Testmode = testmode;
            curhand.Analysemode = analysemode;
            //////radioX = "";
            //////radioY.Clear();
            //////radioX = "StrainX";
            //////radioY.Add("TensionY");
            // nn s�tts lika med 30 f�r att kurvan 
            // skall ritas upp vid f�rsta ing�ng i
            // tbYvalue_TextChanged
            nn = 30;

            if (testmode == true)
            {
                //logg.Write("rbProvning.Checked " + rbProvning.Checked);
                if (threadstarted == true)
                {
                    //logg.Write("Thread1 alive");
                    bool al = Thread1.IsAlive;
                    Server1.Analysflag = false;
                }
                else
                {
                    // skapa tr�den genom att ange hur den skall
                    // startas, h�r genom anrop av ServerStart i objektet Server1
                    Thread1 = new Thread(new ThreadStart(Server1.ServerStart));
                    // starta tr�den
                    Thread1.Start();
                    s = Server1.s;
                    threadstarted = true;
                    //Thread1.Name = "RKM";
                    //logg.Write("rbProvning.Checked Thread1 started");
                }

            } // if (rbProvning.Checked == true)
            else
            {
                // do nothing
            }

        } // private void rbProvning_Click(object sender, EventArgs e)

        //*****************************************************************************
        private void tbYvalue_TextChanged(object sender, EventArgs e)
        {
            //logg.Write("tbYvalue_TextChanged()) in");
            if (testmode == true)
            {
                if (tbYvalue.Text == "slut")
                {
                    // s�tt nn = 30 f�r att de sist inkommande data
                    // skall ritas ut �ven om de �r f�rre
                    nn = 30;
                    // l�gg ut sist inkommna m�tv�rde i testboxen f�r tension
                    tbYvalue.Text = tbY;
                    
                }
                else
                {
                    tbY = tbYvalue.Text;
                }
                nn++;
                if (nn > 30)
                {
                    radioX = "";
                    radioY.Clear();
                    radioX = "StrainX";
                    radioY.Add("TensionY");
                    //logg.Write("tbYvalue_TextChanged  radioX " + radioX + "   radioY  " + radioY[0]);
                    //for (int w = 0; w < tension.Count; w++)
                    //{
                    //    logg.Write(w + "  " + tension[w]);
                    //}
                    //logg.Write("tbYvalue_TextChanged numbers " + "  " + tension.Count + "  " + strain.Count);
                    newfile = false;
                    numcurvechanged = false;
                    //areagetfrom = "tbYvalue_TextChanged";
                    //logg.Write("tbYvalue_TextChanged() " + "  " + getfrom + "  " + areagetfrom);
                    
                    GetCurves("tbYvalue_TextChanged");

                    if (tension.Count < 6)
                    {
                        // do nothing
                        // data har �nnu inte b�rjat komma ft�n klienten
                    }
                    else
                    {
                        chartpanel2.Invalidate();
                    }
                    nn = 0;

                } // if (nn > 30)

            } // if (testmode == true)
            else
            {
                //logg.Write("tbYvalue_TextChanged()  do nothing");
                // do nothing
            }

        } // private void tbYvalue_TextChanged(object sender, EventArgs e)

        //*****************************************************************************
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);

        } // private void Form1_FormClosing(object sender, FormClosingEventArgs e)

        //*****************************************************************************

    } // public partial class Form1 : Form

} // namespace DragKurva