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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace F1_TM_Viewer
{
    public partial class pitStops : Form
    {
        Race race;
        int x;
        int y;

        public pitStops()
        {
        }

        public pitStops(Race r)
        {
            InitializeComponent();
            newPitstop(r);
        }

        public void newPitstop(Race r)
        {
            race = r;
            driversList.Items.Clear();
            driversList.Items.AddRange(race.driver_team.getDrivers());
        }

        public void RePaint(PaintEventArgs e)
        {
            OnPaint(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = drawer.CreateGraphics();
            String[] col = new string[20];
            Pen myPen;

            x = drawer.Width / (race.counter - 1);
            y = drawer.Height / 20;

            col[0] = "Red";
            col[1] = "Blue";
            col[2] = "LightGreen";
            col[3] = "Yellow";
            col[4] = "Aqua";
            col[5] = "Green";
            col[6] = "Violet";
            col[7] = "Brown";
            col[8] = "Orange";
            col[9] = "DarkBlue";
            col[10] = "Gold";
            col[11] = "Crimson";
            col[12] = "Khaki";
            col[13] = "Magenta";
            col[14] = "Chocolate";
            col[15] = "LightBlue";
            col[16] = "LawnGreen";
            col[17] = "Indigo";
            col[18] = "DarkGray";
            col[19] = "Pink";

            try
            {
                g.Clear(Color.White);


                // generate the points
                Point[] lsegs = new Point[1];
                for (int c = 1; c < 21; c++)
                {
                    List<Pits> stops = race.pitstops.getDriverPits(c - 1);
                    lsegs = new Point[race.counter];

                    int stop = 0;

                    float xt = x / 4,
                        yt = y * (c - 1) + y / 2;
                    lsegs[0] = new Point((int)xt, (int)yt);
                    for (int t = 1; t < race.counter; t++)
                    {
                        xt += x;
                        yt = y * (c - 1) + y / 2;
                        lsegs[t] = new Point((int)xt, (int)yt);

                        myPen = new Pen(Color.FromName(col[stop]), (float)2.7);
                        g.DrawLine(myPen, lsegs[t - 1], lsegs[t]);
                        if (stop < stops.Count)
                            if (stops[stop].LapNumber == t)
                            {
                                stop++;
                                using (Brush tempForeBrush = new System.Drawing.SolidBrush(Color.Black))
                                    g.DrawString(t.ToString(), new Font("Arial", 7), tempForeBrush,
                                        lsegs[t - 1].X + x / 2, 1 + (c - 1) * y - 3);
                            }
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

            base.OnPaint(e);
        }

        public void driversList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (driversList.SelectedIndex >= 0)
            {
                DriverPits dp = new DriverPits(race.pitstops.getDriverPits(driversList.SelectedIndex));
                //pitGraph dp = new pitGraph(race.pitstops.getDriverPits(driversList.SelectedIndex));
                dp.Show(this);

                driversList.SelectedIndex = -1;
            }
        }
    }
}
