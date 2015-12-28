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
    public class dGraph : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        public System.Windows.Forms.Panel drawer;
        public System.Windows.Forms.ListBox drivers;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label dmin;
        public System.Windows.Forms.Label dmax;
        public System.Windows.Forms.Label bmax;
        public System.Windows.Forms.Label bmin;
        public System.Windows.Forms.Label lb1;
        public System.Windows.Forms.CheckBox dispTime;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lb2;

        private Race race;
        private int selected = 0;
        private int lap = 1;
        private float x = 0;
        private float y = 0;
        private string[] best;
        private int[] bestLaps;
        private string[] worst;
        private int[] worstLaps;
        private int rmax = 1, rmin = 1;


        public dGraph(Race r)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            newRace(r);
        }

        public dGraph()
        {
            InitializeComponent();
        }

        public void newRace(Race r)
        {
            race = r;

            drivers.Items.Clear();
            for (int i = 1; i < 21; i++)
                drivers.Items.Add(r.laps[1].d[i].name);
            drivers.SelectedIndex = 0;


            lap = race.counter;

            float[] t;
            string name = race.laps[1].d[1].name;
            string team = race.laps[1].d[1].team;
            best = new string[21];
            bestLaps = new int[21];
            worst = new string[21];
            worstLaps = new int[21];
            int max = 1, min = 1;
            int[] maxs = new int[21];
            int[] mins = new int[21];
            for (int d = 1; d < 21; d++)
            {
                t = new float[lap];
                for (int c = 1; c < lap; c++)
                {
                    t[c] = new float();
                    name = race.laps[1].d[d].name;
                    team = race.laps[1].d[d].team;
                    float time = race.laps[c].getConvertedTime(name, team);
                    t[c] = time;
                    if (t[max] < t[c])
                        max = c;
                    if ((t[min] > t[c] || c == 1) && time != -1)
                        min = c;
                }
                //best[d] = new string();
                bestLaps[d] = new int();
                bestLaps[d] = min;
                best[d] = race.laps[min].getLapTime(name, team);
                float t1 = Lap.ConvertTime(best[d]);
                if (Lap.ConvertTime(best[rmin]) > t1 && Lap.ConvertTime(best[d]) != -1)
                    rmin = d;
                //worst[d] = new string();
                worstLaps[d] = new int();
                worstLaps[d] = max;
                worst[d] = race.laps[max].getLapTime(name, team);
                float t2 = Lap.ConvertTime(worst[d]);
                if (Lap.ConvertTime(worst[rmax]) < t2)
                    rmax = d;
            }
            lb1.Text = "Race Quickest Lap Time: " + best[rmin];
            lb2.Text = "Race Slowest Lap Time: " + worst[rmax];
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dmin = new System.Windows.Forms.Label();
            this.dmax = new System.Windows.Forms.Label();
            this.bmax = new System.Windows.Forms.Label();
            this.bmin = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.dispTime = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawer
            // 
            this.drawer.Location = new System.Drawing.Point(120, 64);
            this.drawer.Name = "drawer";
            this.drawer.Size = new System.Drawing.Size(864, 376);
            this.drawer.TabIndex = 1;
            // 
            // drivers
            // 
            this.drivers.BackColor = System.Drawing.SystemColors.Control;
            this.drivers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.drivers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.drivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.drivers.ForeColor = System.Drawing.Color.Black;
            this.drivers.ItemHeight = 18;
            this.drivers.Location = new System.Drawing.Point(0, 64);
            this.drivers.Name = "drivers";
            this.drivers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.drivers.Size = new System.Drawing.Size(120, 378);
            this.drivers.TabIndex = 2;
            this.drivers.SelectedIndexChanged += new System.EventHandler(this.drivers_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dmax);
            this.groupBox1.Controls.Add(this.dmin);
            this.groupBox1.Location = new System.Drawing.Point(8, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 64);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Stats";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bmax);
            this.groupBox2.Controls.Add(this.bmin);
            this.groupBox2.Location = new System.Drawing.Point(160, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 64);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Race Best";
            // 
            // dmin
            // 
            this.dmin.Location = new System.Drawing.Point(8, 24);
            this.dmin.Name = "dmin";
            this.dmin.Size = new System.Drawing.Size(136, 16);
            this.dmin.TabIndex = 5;
            // 
            // dmax
            // 
            this.dmax.Location = new System.Drawing.Point(8, 40);
            this.dmax.Name = "dmax";
            this.dmax.Size = new System.Drawing.Size(136, 16);
            this.dmax.TabIndex = 6;
            // 
            // bmax
            // 
            this.bmax.Location = new System.Drawing.Point(8, 24);
            this.bmax.Name = "bmax";
            this.bmax.Size = new System.Drawing.Size(152, 16);
            this.bmax.TabIndex = 8;
            // 
            // bmin
            // 
            this.bmin.Location = new System.Drawing.Point(8, 40);
            this.bmin.Name = "bmin";
            this.bmin.Size = new System.Drawing.Size(152, 16);
            this.bmin.TabIndex = 7;
            // 
            // lb1
            // 
            this.lb1.BackColor = System.Drawing.Color.White;
            this.lb1.Location = new System.Drawing.Point(456, 440);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(192, 16);
            this.lb1.TabIndex = 5;
            this.lb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb2
            // 
            this.lb2.BackColor = System.Drawing.Color.White;
            this.lb2.Location = new System.Drawing.Point(456, 48);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(192, 16);
            this.lb2.TabIndex = 6;
            this.lb2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dispTime
            // 
            this.dispTime.Location = new System.Drawing.Point(336, 8);
            this.dispTime.Name = "dispTime";
            this.dispTime.Size = new System.Drawing.Size(128, 24);
            this.dispTime.TabIndex = 7;
            this.dispTime.Text = "Show Lap Times";
            this.dispTime.CheckedChanged += new System.EventHandler(this.dispTime_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(776, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 64);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Legend";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(96, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Crashed";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Blue;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lap  Time";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Yellow;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pit Time";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dGraph
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(970, 456);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dispTime);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.drivers);
            this.Controls.Add(this.drawer);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.lb1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "dGraph";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Driver Graph";
            this.Load += new System.EventHandler(this.graph_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion



        public void graph_Load(object sender, System.EventArgs e)
        {
        }

        public void RePaint(PaintEventArgs pe)
        {
            OnPaint(pe);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = drawer.CreateGraphics();
            x = drawer.Width / (lap + 1);
            y = drawer.Height / 20;

            // draw the pattern
            try
            {
                g.Clear(Color.White);

                float[] t = new float[lap];
                string name = race.laps[1].d[drivers.SelectedIndex + 1].name;
                string team = race.laps[1].d[drivers.SelectedIndex + 1].team;
                int max = 1, min = 1;
                for (int c = 1; c < lap; c++)
                {
                    t[c] = new float();
                    float time = race.laps[c].getConvertedTime(name, team);
                    t[c] = time;
                    if (t[max] < t[c])
                        max = c;
                    if ((t[min] > t[c] || c == 1) && time != -1)
                        min = c;
                    //float time = t[c];
                    if (time == -1)
                        g.FillRectangle(Brushes.Red, x * c, drawer.Height - 20, x / 2, 20);
                    else
                    {
                        float p = race.laps[c].getPitTime(name, team);
                        float h = (time - Lap.ConvertTime(best[rmin]));//((float)time/t[max])
                        p /= (Lap.ConvertTime(worst[rmax]) - Lap.ConvertTime(best[rmin]));
                        h /= (Lap.ConvertTime(worst[rmax]) - Lap.ConvertTime(best[rmin]));
                        g.FillRectangle(Brushes.Blue, x * c, (drawer.Height - 10) * (1 - h) + 5, x / 2, (drawer.Height - 10) * h);
                        g.FillRectangle(Brushes.Yellow, x * c, (drawer.Height - 10) * (1 - h) + 5, x / 2, (drawer.Height - 10) * p);
                        System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                        drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                        if (dispTime.Checked)
                            using (Brush tempForeBrush = new System.Drawing.SolidBrush(Color.Black))
                                g.DrawString(race.laps[c].getLapTime(name, team), new Font("Arial", 7), tempForeBrush, x * c - 2, (drawer.Height - 10) * (1 - h) - 50, drawFormat);
                    }
                }

                dmin.Text = "Best: " + best[drivers.SelectedIndex + 1] + " (Lap" + bestLaps[drivers.SelectedIndex + 1] + ")";
                dmax.Text = "Slowest: " + worst[drivers.SelectedIndex + 1] + " (Lap" + worstLaps[drivers.SelectedIndex + 1] + ")";
                bmin.Text = "Best: " + best[rmin];
                bmax.Text = "Driver: " + race.laps[1].d[rmin].name;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                g.Dispose();
            }

            base.OnPaint(pe);
        }

        public void drivers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (drivers.SelectedIndex > -1 && drivers.SelectedIndex < 21)
            {
                selected = drivers.SelectedIndex + 1;
                Invalidate();
            }
        }

        public void dispTime_CheckedChanged(object sender, System.EventArgs e)
        {
            Invalidate();
        }

    }
}
