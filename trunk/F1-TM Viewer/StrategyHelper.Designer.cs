namespace F1_TM_Viewer
{
    partial class StrategyHelper
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
            this.lb_Tyre = new System.Windows.Forms.ListBox();
            this.num_lap = new System.Windows.Forms.NumericUpDown();
            this.tb_remaining = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_starting = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_wear = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_lap)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_Tyre
            // 
            this.lb_Tyre.FormattingEnabled = true;
            this.lb_Tyre.Items.AddRange(new object[] {
            "Tyre 1",
            "Tyre 2",
            "Tyre 3",
            "Tyre 4"});
            this.lb_Tyre.Location = new System.Drawing.Point(16, 29);
            this.lb_Tyre.Name = "lb_Tyre";
            this.lb_Tyre.Size = new System.Drawing.Size(85, 56);
            this.lb_Tyre.TabIndex = 1;
            this.lb_Tyre.SelectedIndexChanged += new System.EventHandler(this.lb_Tyre_SelectedIndexChanged);
            // 
            // num_lap
            // 
            this.num_lap.Location = new System.Drawing.Point(16, 125);
            this.num_lap.Name = "num_lap";
            this.num_lap.Size = new System.Drawing.Size(62, 20);
            this.num_lap.TabIndex = 3;
            this.num_lap.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_lap.ValueChanged += new System.EventHandler(this.num_lap_ValueChanged);
            // 
            // tb_remaining
            // 
            this.tb_remaining.Enabled = false;
            this.tb_remaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_remaining.Location = new System.Drawing.Point(16, 200);
            this.tb_remaining.Name = "tb_remaining";
            this.tb_remaining.ReadOnly = true;
            this.tb_remaining.Size = new System.Drawing.Size(93, 29);
            this.tb_remaining.TabIndex = 2;
            this.tb_remaining.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Tyre Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lap Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tyre grip remaining";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Starting Tyre Grip";
            // 
            // tb_starting
            // 
            this.tb_starting.Location = new System.Drawing.Point(221, 29);
            this.tb_starting.Name = "tb_starting";
            this.tb_starting.Size = new System.Drawing.Size(93, 20);
            this.tb_starting.TabIndex = 6;
            this.tb_starting.TextChanged += new System.EventHandler(this.tb_starting_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Wear Rate";
            // 
            // tb_wear
            // 
            this.tb_wear.Location = new System.Drawing.Point(221, 65);
            this.tb_wear.Name = "tb_wear";
            this.tb_wear.Size = new System.Drawing.Size(93, 20);
            this.tb_wear.TabIndex = 8;
            this.tb_wear.TextChanged += new System.EventHandler(this.tb_wear_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(129, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 130);
            this.label6.TabIndex = 22;
            this.label6.Text = "1. Select Tyre Compound \r\nOR\r\n1. Enter/change Starting Tyre Grip or Wear Rate\r\n2." +
                " Enter the Lap Number\r\n3. Tyre Grip Remaining is shown\r\n\r\n\r\nTyre fails after gri" +
                "p falls below 5%";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StrategyHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 245);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_wear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_starting);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_remaining);
            this.Controls.Add(this.num_lap);
            this.Controls.Add(this.lb_Tyre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StrategyHelper";
            this.Text = "StrategyHelper";
            ((System.ComponentModel.ISupportInitialize)(this.num_lap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox lb_Tyre;
        public System.Windows.Forms.NumericUpDown num_lap;
        public System.Windows.Forms.TextBox tb_remaining;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tb_starting;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox tb_wear;
        public System.Windows.Forms.Label label6;
    }
}