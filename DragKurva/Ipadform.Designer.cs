namespace DragKurva
{
    partial class Ipadform
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
            this.lbInfo = new System.Windows.Forms.Label();
            this.tbIPadress = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btAvbryt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Location = new System.Drawing.Point(34, 9);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(159, 13);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "Ange en giltig Ip adress och port";
            // 
            // tbIPadress
            // 
            this.tbIPadress.Location = new System.Drawing.Point(88, 31);
            this.tbIPadress.Name = "tbIPadress";
            this.tbIPadress.Size = new System.Drawing.Size(94, 20);
            this.tbIPadress.TabIndex = 1;
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOK.Location = new System.Drawing.Point(37, 84);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(64, 25);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btAvbryt
            // 
            this.btAvbryt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAvbryt.Location = new System.Drawing.Point(130, 84);
            this.btAvbryt.Name = "btAvbryt";
            this.btAvbryt.Size = new System.Drawing.Size(52, 25);
            this.btAvbryt.TabIndex = 3;
            this.btAvbryt.Text = "Avbryt";
            this.btAvbryt.UseVisualStyleBackColor = true;
            this.btAvbryt.Click += new System.EventHandler(this.btAvbryt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IPadress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(88, 57);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(94, 20);
            this.tbPort.TabIndex = 5;
            // 
            // Ipadform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 119);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btAvbryt);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tbIPadress);
            this.Controls.Add(this.lbInfo);
            this.Name = "Ipadform";
            this.Text = "Hoppsan!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.TextBox tbIPadress;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btAvbryt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
    }
}