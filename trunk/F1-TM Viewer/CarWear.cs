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
    public partial class CarWear : Form
    {
        List<Part> Wear;
        int x = 0;
        int y = 0;
        int pW = 12;
        int pH = 12;

        public CarWear()
        {
            InitializeComponent();
        }

        public CarWear(List<Part> carWear, int dNo)
        {
            InitializeComponent();
            newWear(carWear, dNo);


            Invalidate();
            WearPanel.Invalidate();
        }

        public void newWear(List<Part> carWear, int dNo)
        {

            Wear = carWear;

            if (Wear.Count > 0)
            {
                this.Text = "Driver #" + dNo.ToString() + " Car Wear";
            }
            else
                this.Text = "No car wear for this driver";
        }

        public void RePaint(PaintEventArgs e)
        {
            OnPaint(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (Wear.Count > 0)
            {
                x = WearPanel.Width;
                y = WearPanel.Height;

                Graphics g = WearPanel.CreateGraphics();

                // draw the pattern
                try
                {
                    g.Clear(Color.White);

                    Decimal yMax = (Decimal)(y - pH * 2 - 90);

                    for (int i = 0; i < Wear.Count; i++)
                    {
                        Part p = Wear[i];


                        int x1 = (i + 1) * x / (Wear.Count + 1) - pW;
                        Decimal y1 = yMax * p.Wear / 100;
                        Decimal y2 = yMax * p.Reliability / 100;
                        //Decimal y3 = yMax;

                        int yc = pH;

                        g.DrawRectangle(Pens.Black, x1, yc, pW * 2, (int)yMax);

                        if (y2 > 0)
                        {
                            if (y2 > yMax)
                                g.FillRectangle(Brushes.Green, x1, (int)(yc), pW * 2, (int)yMax);
                            else
                                g.FillRectangle(Brushes.Green, x1, (int)(yc + Math.Ceiling(yMax - y2)), pW * 2, (int)y2);
                        }

                        if (y1 > 0)
                            g.FillRectangle(Brushes.Red, x1 + (pW), (int)(yc + Math.Ceiling(yMax - y1)), (pW), (int)y1);


                        DrawText(g, p.Wear.ToString() + "%", (float)(x1 + pW * 2), (float)(yc + yMax - y1), true, Color.Red, 7f);
                        if (y2 > yMax)
                            DrawText(g, p.Reliability.ToString() + "%", (float)(x1), (float)(yc), false, Color.White, 7f);
                        else
                            DrawText(g, p.Reliability.ToString() + "%", (float)(x1), (float)(yc + yMax - y2), false, Color.White, 7f);

                        DrawText(g, p.Name, (float)(x1), (float)(yc + yMax), true, Color.Black, 10f);
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

            base.OnPaint(e);
        }

        private static void DrawText(Graphics g, String s, float x, float y, bool vertical, Color c, float size)
        {
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            using (Brush tempForeBrush = new System.Drawing.SolidBrush(c))
                if (vertical)
                    g.DrawString(s, new Font(FontFamily.GenericSansSerif, size), tempForeBrush, x, y, drawFormat);
                else
                    g.DrawString(s, new Font(FontFamily.GenericSansSerif, size), tempForeBrush, x, y);
        }
    }
}
