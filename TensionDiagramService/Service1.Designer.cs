namespace TensionDiagramService
{
    partial class Service1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btServiceStart = new System.Windows.Forms.Button();
            // 
            // btServiceStart
            // 
            this.btServiceStart.Location = new System.Drawing.Point(0, 0);
            this.btServiceStart.Name = "btServiceStart";
            this.btServiceStart.Size = new System.Drawing.Size(75, 23);
            this.btServiceStart.TabIndex = 0;
            this.btServiceStart.Text = "Starta";
            this.btServiceStart.UseVisualStyleBackColor = true;
            // 
            // Service1
            // 
            this.ServiceName = "Service1";

        }

        #endregion

        private System.Windows.Forms.Button btServiceStart;
    }
}
