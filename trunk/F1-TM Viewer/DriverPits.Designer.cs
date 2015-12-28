namespace F1_TM_Viewer
{
    partial class DriverPits
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
            this.PitPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // WearPanel
            // 
            this.PitPanel.Location = new System.Drawing.Point(12, 12);
            this.PitPanel.Name = "WearPanel";
            this.PitPanel.Size = new System.Drawing.Size(346, 229);
            this.PitPanel.TabIndex = 0;
            // 
            // DriverPits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(370, 253);
            this.Controls.Add(this.PitPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DriverPits";
            this.Text = "DriverPits";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PitPanel;

    }
}