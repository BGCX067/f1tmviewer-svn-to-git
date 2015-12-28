namespace F1_TM_Viewer
{
    partial class pitStops
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
            this.drawer = new System.Windows.Forms.Panel();
            this.driversList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // drawer
            // 
            this.drawer.Location = new System.Drawing.Point(133, 12);
            this.drawer.Name = "drawer";
            this.drawer.Size = new System.Drawing.Size(864, 378);
            this.drawer.TabIndex = 2;
            // 
            // driversList
            // 
            this.driversList.BackColor = System.Drawing.SystemColors.Control;
            this.driversList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.driversList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.driversList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.driversList.ForeColor = System.Drawing.Color.Black;
            this.driversList.ItemHeight = 18;
            this.driversList.Location = new System.Drawing.Point(7, 12);
            this.driversList.Name = "driversList";
            this.driversList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.driversList.Size = new System.Drawing.Size(120, 378);
            this.driversList.TabIndex = 3;
            this.driversList.SelectedIndexChanged += new System.EventHandler(this.driversList_SelectedIndexChanged);
            // 
            // pitStops
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 401);
            this.Controls.Add(this.driversList);
            this.Controls.Add(this.drawer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "pitStops";
            this.Text = "Pit Stops - Select driver to view details";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel drawer;
        public System.Windows.Forms.ListBox driversList;
    }
}