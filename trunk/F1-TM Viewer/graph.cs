// This file is part of "F1-TM Telemetry Viewer".
// 
// "F1-TM Telemetry Viewer" is free software: you can redistribute it 
// and/or modify it under the terms of the GNU General Public License 
// as published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
// 
// "F1-TM Telemetry Viewer" is distributed in the hope that it will 
// be useful, but WITHOUT ANY WARRANTY; without even the implied 
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with "F1-TM Telemetry Viewer".  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright© Himanshu Kumar [Raja] 2009. All Rights Reserved.
// Contributor(s): [Raja <f1mac@mac.com>].

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace F1_TM_Viewer
{
    /// <summary>
    /// Summary description for graph.
    /// </summary>
    public class graph : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private Race race;
        private int selected = 0;
        private int lap = 1;
        private int x = 0;
        public System.Windows.Forms.ListBox drivers;
        public System.Windows.Forms.Panel drawer;
        private int y = 0;

        public graph()
        {
        }

        public graph(Race r)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            newGraph(r);
        }

        public void newGraph(Race r)
        {
            race = r;

            drivers.Items.Clear();
            drivers.Items.Add("All");
            drivers.SelectedIndex = 0;
            for (int i = 1; i < 21; i++)
                drivers.Items.Add(r.laps[1].d[i].name);
            lap = race.counter;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.drivers = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // drawer
            // 
            this.drawer.Location = new System.Drawing.Point(112, 16);
            this.drawer.Name = "drawer";
            this.drawer.Size = new System.Drawing.Size(856, 376);
            this.drawer.TabIndex = 1;
            // 
            // drivers
            // 
            this.drivers.BackColor = System.Drawing.SystemColors.Control;
            this.drivers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.drivers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.drivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.drivers.ItemHeight = 18;
            this.drivers.Location = new System.Drawing.Point(0, 0);
            this.drivers.Name = "drivers";
            this.drivers.Size = new System.Drawing.Size(112, 378);
            this.drivers.TabIndex = 2;
            this.drivers.SelectedIndexChanged += new System.EventHandler(this.drivers_SelectedIndexChanged);
            // 
            // graph
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(970, 384);
            this.Controls.Add(this.drivers);
            this.Controls.Add(this.drawer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "graph";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Lap Graph";
            this.Load += new System.EventHandler(this.graph_Load);
            this.ResumeLayout(false);

        }
        #endregion



        private void graph_Load(object sender, System.EventArgs e)
        {
        }

        public void RePaint(PaintEventArgs pe)
        {
            OnPaint(pe);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            x = drawer.Width / (lap - 1);
            y = drawer.Height / 20;

            Graphics g = drawer.CreateGraphics();
            String[] col = new string[20];
            Pen myPen;

            col[0] = "Red";
            col[1] = "Blue";
            col[2] = "Green";
            col[3] = "Yellow";
            col[4] = "Violet";
            col[5] = "Brown";
            col[6] = "Orange";
            col[7] = "LightBlue";
            col[8] = "LightGreen";
            col[9] = "DarkBlue";
            col[10] = "Gold";
            col[11] = "Crimson";
            col[12] = "Khaki";
            col[13] = "Magenta";
            col[14] = "Chocolate";
            col[15] = "Aqua";
            col[16] = "LawnGreen";
            col[17] = "Indigo";
            col[18] = "DarkGray";
            col[19] = "Pink";

            // draw the pattern
            try
            {
                g.Clear(Color.White);

                // generate the points
                Point[] lsegs = new Point[1];
                for (int c = 1; c < 21; c++)
                {
                    if (selected == 0 || selected == c)
                    {
                        lsegs = new Point[lap];
                        float xt = x / 4,
                            yt = y * (c - 1) + y / 2;
                        lsegs[0] = new Point((int)xt, (int)yt);
                        string name = race.laps[1].d[c].name;
                        string team = race.laps[1].d[c].team;
                        for (int i = 1; i < lap; i++)
                        {
                            int pos = race.laps[i].getPos(name, team);
                            if (pos == 0)
                                pos = 30;
                            xt += x;
                            yt = y * (pos - 1) + y / 2;
                            lsegs[i] = new Point((int)xt, (int)yt);
                            //lsegs[i] = new Point(lsegs[i-1].X + x, y*(pos-1) + y/2);
                        }

                        //Draw the lines
                        myPen = new Pen(Color.FromName(col[c - 1]), (float)2.7);
                        g.DrawLines(myPen, lsegs);

                        //Draw labels on top of lines, instead of below lines
                        for (int i = 1; i < lap; i++)
                        {
                            using (Brush tempForeBrush = new System.Drawing.SolidBrush(Color.Black))
                                g.DrawString(i.ToString(), new Font("Arial", 7), tempForeBrush, lsegs[i - 1].X + x / 2, 1 + (i % 2 == 0 || selected == 0 ? 0 : 8) + (selected == 0 ? selected : selected - 1) * y - 2);
                        }
                        //Array.Clear(lsegs, 0, lap);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                g.Dispose();
            }
        }


        public void drivers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (drivers.SelectedIndex > -1 && drivers.SelectedIndex < 21)
            {
                selected = drivers.SelectedIndex;
                Invalidate();
            }
        }
    }
}
