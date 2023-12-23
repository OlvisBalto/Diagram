namespace TensionDiagramService
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
            this.btHostService = new System.Windows.Forms.Button();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.btStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btHostService
            // 
            this.btHostService.Location = new System.Drawing.Point(61, 50);
            this.btHostService.Name = "btHostService";
            this.btHostService.Size = new System.Drawing.Size(75, 23);
            this.btHostService.TabIndex = 0;
            this.btHostService.Text = "HostService";
            this.btHostService.UseVisualStyleBackColor = true;
            this.btHostService.Click += new System.EventHandler(this.btHostService_Click);
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(194, 52);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(502, 20);
            this.tbHost.TabIndex = 1;
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(65, 100);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(70, 26);
            this.btStop.TabIndex = 2;
            this.btStop.Text = "Stopp";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.tbHost);
            this.Controls.Add(this.btHostService);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btHostService;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.Button btStop;
    }
}