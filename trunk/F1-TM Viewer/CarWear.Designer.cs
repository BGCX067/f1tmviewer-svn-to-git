namespace F1_TM_Viewer
{
    partial class CarWear
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
            this.WearPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // WearPanel
            // 
            this.WearPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WearPanel.Location = new System.Drawing.Point(12, 12);
            this.WearPanel.Name = "WearPanel";
            this.WearPanel.Size = new System.Drawing.Size(669, 385);
            this.WearPanel.TabIndex = 0;
            // 
            // CarWear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(693, 409);
            this.Controls.Add(this.WearPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CarWear";
            this.Text = "CarWear";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel WearPanel;

    }
}