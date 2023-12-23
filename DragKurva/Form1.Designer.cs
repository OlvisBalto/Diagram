namespace DragKurva
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbMapp = new System.Windows.Forms.Label();
            this.tbDirectory = new System.Windows.Forms.TextBox();
            this.rbStdForceX = new System.Windows.Forms.RadioButton();
            this.lbX = new System.Windows.Forms.Label();
            this.rbTrueStrainX = new System.Windows.Forms.RadioButton();
            this.rbTrueTensionX = new System.Windows.Forms.RadioButton();
            this.rbChangeWidthX = new System.Windows.Forms.RadioButton();
            this.rbStandardPathX = new System.Windows.Forms.RadioButton();
            this.lbY = new System.Windows.Forms.Label();
            this.gbX = new System.Windows.Forms.GroupBox();
            this.gbY = new System.Windows.Forms.GroupBox();
            this.cbEmodulaccY = new System.Windows.Forms.CheckBox();
            this.cbEmodulvelY = new System.Windows.Forms.CheckBox();
            this.cbEmodulY = new System.Windows.Forms.CheckBox();
            this.cbPathY = new System.Windows.Forms.CheckBox();
            this.cbChWidthY = new System.Windows.Forms.CheckBox();
            this.cbTensionY = new System.Windows.Forms.CheckBox();
            this.cbStrainY = new System.Windows.Forms.CheckBox();
            this.cbForceY = new System.Windows.Forms.CheckBox();
            this.chartpanel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.lbXvalue = new System.Windows.Forms.Label();
            this.lbYvalue = new System.Windows.Forms.Label();
            this.tbXvalue = new System.Windows.Forms.TextBox();
            this.tbYvalue = new System.Windows.Forms.TextBox();
            this.tbXmax = new System.Windows.Forms.TextBox();
            this.tbXmin = new System.Windows.Forms.TextBox();
            this.tbEmodul = new System.Windows.Forms.TextBox();
            this.tbRp02 = new System.Windows.Forms.TextBox();
            this.lbXmax = new System.Windows.Forms.Label();
            this.lbXmin = new System.Windows.Forms.Label();
            this.lbEmodul = new System.Windows.Forms.Label();
            this.lbRp02 = new System.Windows.Forms.Label();
            this.bGetFile = new System.Windows.Forms.Button();
            this.tbfmedel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbfiltkonst = new System.Windows.Forms.TextBox();
            this.btHelaKurvan = new System.Windows.Forms.Button();
            this.lbReh = new System.Windows.Forms.Label();
            this.tbReh = new System.Windows.Forms.TextBox();
            this.lbRel = new System.Windows.Forms.Label();
            this.tbRel = new System.Windows.Forms.TextBox();
            this.lbRm = new System.Windows.Forms.Label();
            this.tbRm = new System.Windows.Forms.TextBox();
            this.lbAg = new System.Windows.Forms.Label();
            this.tbAg = new System.Windows.Forms.TextBox();
            this.lbA80 = new System.Windows.Forms.Label();
            this.tbA80 = new System.Windows.Forms.TextBox();
            this.tbEmodmax = new System.Windows.Forms.TextBox();
            this.lbEmodmax = new System.Windows.Forms.Label();
            this.lbEmodmin = new System.Windows.Forms.Label();
            this.tbEmodmin = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.rbAnalys = new System.Windows.Forms.RadioButton();
            this.rbProvning = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gbX.SuspendLayout();
            this.gbY.SuspendLayout();
            this.chartpanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMapp
            // 
            resources.ApplyResources(this.lbMapp, "lbMapp");
            this.lbMapp.Name = "lbMapp";
            // 
            // tbDirectory
            // 
            resources.ApplyResources(this.tbDirectory, "tbDirectory");
            this.tbDirectory.Name = "tbDirectory";
            // 
            // rbStdForceX
            // 
            resources.ApplyResources(this.rbStdForceX, "rbStdForceX");
            this.rbStdForceX.Name = "rbStdForceX";
            this.rbStdForceX.TabStop = true;
            this.rbStdForceX.UseVisualStyleBackColor = true;
            this.rbStdForceX.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // lbX
            // 
            resources.ApplyResources(this.lbX, "lbX");
            this.lbX.Name = "lbX";
            // 
            // rbTrueStrainX
            // 
            resources.ApplyResources(this.rbTrueStrainX, "rbTrueStrainX");
            this.rbTrueStrainX.Checked = true;
            this.rbTrueStrainX.Name = "rbTrueStrainX";
            this.rbTrueStrainX.TabStop = true;
            this.rbTrueStrainX.UseVisualStyleBackColor = true;
            this.rbTrueStrainX.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // rbTrueTensionX
            // 
            resources.ApplyResources(this.rbTrueTensionX, "rbTrueTensionX");
            this.rbTrueTensionX.Name = "rbTrueTensionX";
            this.rbTrueTensionX.UseVisualStyleBackColor = true;
            this.rbTrueTensionX.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // rbChangeWidthX
            // 
            resources.ApplyResources(this.rbChangeWidthX, "rbChangeWidthX");
            this.rbChangeWidthX.Name = "rbChangeWidthX";
            this.rbChangeWidthX.TabStop = true;
            this.rbChangeWidthX.UseVisualStyleBackColor = true;
            this.rbChangeWidthX.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // rbStandardPathX
            // 
            resources.ApplyResources(this.rbStandardPathX, "rbStandardPathX");
            this.rbStandardPathX.Name = "rbStandardPathX";
            this.rbStandardPathX.TabStop = true;
            this.rbStandardPathX.UseVisualStyleBackColor = true;
            this.rbStandardPathX.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // lbY
            // 
            resources.ApplyResources(this.lbY, "lbY");
            this.lbY.Name = "lbY";
            // 
            // gbX
            // 
            this.gbX.Controls.Add(this.rbStdForceX);
            this.gbX.Controls.Add(this.rbStandardPathX);
            this.gbX.Controls.Add(this.lbX);
            this.gbX.Controls.Add(this.rbTrueStrainX);
            this.gbX.Controls.Add(this.rbChangeWidthX);
            this.gbX.Controls.Add(this.rbTrueTensionX);
            resources.ApplyResources(this.gbX, "gbX");
            this.gbX.Name = "gbX";
            this.gbX.TabStop = false;
            // 
            // gbY
            // 
            this.gbY.Controls.Add(this.cbEmodulaccY);
            this.gbY.Controls.Add(this.cbEmodulvelY);
            this.gbY.Controls.Add(this.cbEmodulY);
            this.gbY.Controls.Add(this.cbPathY);
            this.gbY.Controls.Add(this.cbChWidthY);
            this.gbY.Controls.Add(this.cbTensionY);
            this.gbY.Controls.Add(this.cbStrainY);
            this.gbY.Controls.Add(this.cbForceY);
            this.gbY.Controls.Add(this.lbY);
            resources.ApplyResources(this.gbY, "gbY");
            this.gbY.Name = "gbY";
            this.gbY.TabStop = false;
            // 
            // cbEmodulaccY
            // 
            resources.ApplyResources(this.cbEmodulaccY, "cbEmodulaccY");
            this.cbEmodulaccY.Name = "cbEmodulaccY";
            this.cbEmodulaccY.UseVisualStyleBackColor = true;
            this.cbEmodulaccY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // cbEmodulvelY
            // 
            resources.ApplyResources(this.cbEmodulvelY, "cbEmodulvelY");
            this.cbEmodulvelY.Name = "cbEmodulvelY";
            this.cbEmodulvelY.UseVisualStyleBackColor = true;
            this.cbEmodulvelY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // cbEmodulY
            // 
            resources.ApplyResources(this.cbEmodulY, "cbEmodulY");
            this.cbEmodulY.Checked = true;
            this.cbEmodulY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEmodulY.Name = "cbEmodulY";
            this.cbEmodulY.UseVisualStyleBackColor = true;
            this.cbEmodulY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // cbPathY
            // 
            resources.ApplyResources(this.cbPathY, "cbPathY");
            this.cbPathY.Name = "cbPathY";
            this.cbPathY.UseVisualStyleBackColor = true;
            this.cbPathY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // cbChWidthY
            // 
            resources.ApplyResources(this.cbChWidthY, "cbChWidthY");
            this.cbChWidthY.Name = "cbChWidthY";
            this.cbChWidthY.UseVisualStyleBackColor = true;
            this.cbChWidthY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // cbTensionY
            // 
            resources.ApplyResources(this.cbTensionY, "cbTensionY");
            this.cbTensionY.Checked = true;
            this.cbTensionY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTensionY.Name = "cbTensionY";
            this.cbTensionY.UseVisualStyleBackColor = true;
            this.cbTensionY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // cbStrainY
            // 
            resources.ApplyResources(this.cbStrainY, "cbStrainY");
            this.cbStrainY.Checked = true;
            this.cbStrainY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStrainY.Name = "cbStrainY";
            this.cbStrainY.UseVisualStyleBackColor = true;
            this.cbStrainY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // cbForceY
            // 
            resources.ApplyResources(this.cbForceY, "cbForceY");
            this.cbForceY.Name = "cbForceY";
            this.cbForceY.UseVisualStyleBackColor = true;
            this.cbForceY.Click += new System.EventHandler(this.rbStdForceX_Click);
            // 
            // chartpanel2
            // 
            this.chartpanel2.Controls.Add(this.label1);
            this.chartpanel2.Controls.Add(this.tbID);
            this.chartpanel2.Controls.Add(this.lbXvalue);
            this.chartpanel2.Controls.Add(this.lbYvalue);
            this.chartpanel2.Controls.Add(this.tbXvalue);
            this.chartpanel2.Controls.Add(this.tbYvalue);
            resources.ApplyResources(this.chartpanel2, "chartpanel2");
            this.chartpanel2.Name = "chartpanel2";
            this.chartpanel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.chartpanel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.chartpanel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbID
            // 
            resources.ApplyResources(this.tbID, "tbID");
            this.tbID.Name = "tbID";
            // 
            // lbXvalue
            // 
            resources.ApplyResources(this.lbXvalue, "lbXvalue");
            this.lbXvalue.Name = "lbXvalue";
            // 
            // lbYvalue
            // 
            resources.ApplyResources(this.lbYvalue, "lbYvalue");
            this.lbYvalue.Name = "lbYvalue";
            // 
            // tbXvalue
            // 
            resources.ApplyResources(this.tbXvalue, "tbXvalue");
            this.tbXvalue.Name = "tbXvalue";
            // 
            // tbYvalue
            // 
            resources.ApplyResources(this.tbYvalue, "tbYvalue");
            this.tbYvalue.Name = "tbYvalue";
            this.tbYvalue.TextChanged += new System.EventHandler(this.tbYvalue_TextChanged);
            // 
            // tbXmax
            // 
            resources.ApplyResources(this.tbXmax, "tbXmax");
            this.tbXmax.Name = "tbXmax";
            this.tbXmax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbXmax_KeyDown);
            // 
            // tbXmin
            // 
            resources.ApplyResources(this.tbXmin, "tbXmin");
            this.tbXmin.Name = "tbXmin";
            this.tbXmin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbXmin_KeyDown);
            // 
            // tbEmodul
            // 
            resources.ApplyResources(this.tbEmodul, "tbEmodul");
            this.tbEmodul.Name = "tbEmodul";
            // 
            // tbRp02
            // 
            resources.ApplyResources(this.tbRp02, "tbRp02");
            this.tbRp02.Name = "tbRp02";
            // 
            // lbXmax
            // 
            resources.ApplyResources(this.lbXmax, "lbXmax");
            this.lbXmax.Name = "lbXmax";
            // 
            // lbXmin
            // 
            resources.ApplyResources(this.lbXmin, "lbXmin");
            this.lbXmin.Name = "lbXmin";
            // 
            // lbEmodul
            // 
            resources.ApplyResources(this.lbEmodul, "lbEmodul");
            this.lbEmodul.Name = "lbEmodul";
            // 
            // lbRp02
            // 
            resources.ApplyResources(this.lbRp02, "lbRp02");
            this.lbRp02.Name = "lbRp02";
            // 
            // bGetFile
            // 
            resources.ApplyResources(this.bGetFile, "bGetFile");
            this.bGetFile.Name = "bGetFile";
            this.bGetFile.UseVisualStyleBackColor = true;
            this.bGetFile.Click += new System.EventHandler(this.bGetFile_Click);
            // 
            // tbfmedel
            // 
            resources.ApplyResources(this.tbfmedel, "tbfmedel");
            this.tbfmedel.Name = "tbfmedel";
            this.tbfmedel.TextChanged += new System.EventHandler(this.tbfmedel_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tbfiltkonst
            // 
            resources.ApplyResources(this.tbfiltkonst, "tbfiltkonst");
            this.tbfiltkonst.Name = "tbfiltkonst";
            this.tbfiltkonst.TextChanged += new System.EventHandler(this.tbfiltkonst_TextChanged);
            // 
            // btHelaKurvan
            // 
            resources.ApplyResources(this.btHelaKurvan, "btHelaKurvan");
            this.btHelaKurvan.Name = "btHelaKurvan";
            this.btHelaKurvan.UseVisualStyleBackColor = true;
            this.btHelaKurvan.Click += new System.EventHandler(this.btHelaKurvan_Click);
            // 
            // lbReh
            // 
            resources.ApplyResources(this.lbReh, "lbReh");
            this.lbReh.Name = "lbReh";
            // 
            // tbReh
            // 
            resources.ApplyResources(this.tbReh, "tbReh");
            this.tbReh.Name = "tbReh";
            // 
            // lbRel
            // 
            resources.ApplyResources(this.lbRel, "lbRel");
            this.lbRel.Name = "lbRel";
            // 
            // tbRel
            // 
            resources.ApplyResources(this.tbRel, "tbRel");
            this.tbRel.Name = "tbRel";
            // 
            // lbRm
            // 
            resources.ApplyResources(this.lbRm, "lbRm");
            this.lbRm.Name = "lbRm";
            // 
            // tbRm
            // 
            resources.ApplyResources(this.tbRm, "tbRm");
            this.tbRm.Name = "tbRm";
            // 
            // lbAg
            // 
            resources.ApplyResources(this.lbAg, "lbAg");
            this.lbAg.Name = "lbAg";
            // 
            // tbAg
            // 
            resources.ApplyResources(this.tbAg, "tbAg");
            this.tbAg.Name = "tbAg";
            // 
            // lbA80
            // 
            resources.ApplyResources(this.lbA80, "lbA80");
            this.lbA80.Name = "lbA80";
            // 
            // tbA80
            // 
            resources.ApplyResources(this.tbA80, "tbA80");
            this.tbA80.Name = "tbA80";
            // 
            // tbEmodmax
            // 
            resources.ApplyResources(this.tbEmodmax, "tbEmodmax");
            this.tbEmodmax.Name = "tbEmodmax";
            this.tbEmodmax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbEmodmax_KeyDown);
            // 
            // lbEmodmax
            // 
            resources.ApplyResources(this.lbEmodmax, "lbEmodmax");
            this.lbEmodmax.Name = "lbEmodmax";
            // 
            // lbEmodmin
            // 
            resources.ApplyResources(this.lbEmodmin, "lbEmodmin");
            this.lbEmodmin.Name = "lbEmodmin";
            // 
            // tbEmodmin
            // 
            resources.ApplyResources(this.tbEmodmin, "tbEmodmin");
            this.tbEmodmin.Name = "tbEmodmin";
            this.tbEmodmin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbEmodmin_KeyDown);
            // 
            // rbAnalys
            // 
            resources.ApplyResources(this.rbAnalys, "rbAnalys");
            this.rbAnalys.Checked = true;
            this.rbAnalys.Name = "rbAnalys";
            this.rbAnalys.TabStop = true;
            this.rbAnalys.UseVisualStyleBackColor = true;
            this.rbAnalys.Click += new System.EventHandler(this.rbAnalys_Click);
            // 
            // rbProvning
            // 
            resources.ApplyResources(this.rbProvning, "rbProvning");
            this.rbProvning.Name = "rbProvning";
            this.rbProvning.UseVisualStyleBackColor = true;
            this.rbProvning.Click += new System.EventHandler(this.rbProvning_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.rbProvning);
            this.groupBox1.Controls.Add(this.rbAnalys);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbEmodmax);
            this.Controls.Add(this.lbAg);
            this.Controls.Add(this.lbEmodmax);
            this.Controls.Add(this.lbA80);
            this.Controls.Add(this.lbEmodmin);
            this.Controls.Add(this.tbAg);
            this.Controls.Add(this.tbEmodmin);
            this.Controls.Add(this.tbA80);
            this.Controls.Add(this.lbRm);
            this.Controls.Add(this.tbRm);
            this.Controls.Add(this.lbRel);
            this.Controls.Add(this.tbRel);
            this.Controls.Add(this.lbReh);
            this.Controls.Add(this.tbReh);
            this.Controls.Add(this.btHelaKurvan);
            this.Controls.Add(this.tbfiltkonst);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbfmedel);
            this.Controls.Add(this.bGetFile);
            this.Controls.Add(this.lbRp02);
            this.Controls.Add(this.lbEmodul);
            this.Controls.Add(this.lbXmin);
            this.Controls.Add(this.lbXmax);
            this.Controls.Add(this.tbRp02);
            this.Controls.Add(this.tbEmodul);
            this.Controls.Add(this.tbXmin);
            this.Controls.Add(this.tbXmax);
            this.Controls.Add(this.chartpanel2);
            this.Controls.Add(this.gbY);
            this.Controls.Add(this.gbX);
            this.Controls.Add(this.lbMapp);
            this.Controls.Add(this.tbDirectory);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.gbX.ResumeLayout(false);
            this.gbX.PerformLayout();
            this.gbY.ResumeLayout(false);
            this.gbY.PerformLayout();
            this.chartpanel2.ResumeLayout(false);
            this.chartpanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMapp;
        private System.Windows.Forms.TextBox tbDirectory;
        private System.Windows.Forms.RadioButton rbStdForceX;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.RadioButton rbTrueStrainX;
        private System.Windows.Forms.RadioButton rbTrueTensionX;
        private System.Windows.Forms.RadioButton rbChangeWidthX;
        private System.Windows.Forms.RadioButton rbStandardPathX;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.GroupBox gbX;
        private System.Windows.Forms.GroupBox gbY;
        private System.Windows.Forms.Panel chartpanel2;
        private System.Windows.Forms.TextBox tbXmax;
        private System.Windows.Forms.TextBox tbXmin;
        private System.Windows.Forms.TextBox tbEmodul;
        private System.Windows.Forms.TextBox tbRp02;
        private System.Windows.Forms.Label lbXmax;
        private System.Windows.Forms.Label lbXmin;
        private System.Windows.Forms.Label lbEmodul;
        private System.Windows.Forms.Label lbRp02;
        private System.Windows.Forms.CheckBox cbTensionY;
        private System.Windows.Forms.CheckBox cbStrainY;
        private System.Windows.Forms.CheckBox cbForceY;
        private System.Windows.Forms.CheckBox cbChWidthY;
        private System.Windows.Forms.CheckBox cbEmodulY;
        private System.Windows.Forms.CheckBox cbPathY;
        private System.Windows.Forms.CheckBox cbEmodulvelY;
        private System.Windows.Forms.Button bGetFile;
        private System.Windows.Forms.TextBox tbfmedel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbfiltkonst;
        private System.Windows.Forms.CheckBox cbEmodulaccY;
        private System.Windows.Forms.Button btHelaKurvan;
        private System.Windows.Forms.Label lbReh;
        private System.Windows.Forms.TextBox tbReh;
        private System.Windows.Forms.Label lbRel;
        private System.Windows.Forms.TextBox tbRel;
        private System.Windows.Forms.Label lbRm;
        private System.Windows.Forms.TextBox tbRm;
        private System.Windows.Forms.Label lbAg;
        private System.Windows.Forms.TextBox tbAg;
        private System.Windows.Forms.Label lbA80;
        private System.Windows.Forms.TextBox tbA80;
        private System.Windows.Forms.TextBox tbEmodmax;
        private System.Windows.Forms.Label lbEmodmax;
        private System.Windows.Forms.Label lbEmodmin;
        private System.Windows.Forms.TextBox tbEmodmin;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RadioButton rbProvning;
        private System.Windows.Forms.RadioButton rbAnalys;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbXvalue;
        private System.Windows.Forms.Label lbYvalue;
        private System.Windows.Forms.TextBox tbXvalue;
        private System.Windows.Forms.TextBox tbYvalue;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbID;
    }
}

