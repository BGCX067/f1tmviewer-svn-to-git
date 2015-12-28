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
    public partial class DriverPits : Form
    {
        List<Pits> pits;
        int x = 0;
        int y = 0;
        int pW = 20;
        int pH = 12;

        public DriverPits(List<Pits> p)
        {
            InitializeComponent();
            pits = p;

            if (pits.Count > 0)
            {
                this.Text = pits[0].Driver + " - " + pits[0].Team;
                x = PitPanel.Width;
                y = PitPanel.Height;
            }
            else
                this.Text = "No pits for this driver";


            Invalidate();
            PitPanel.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (pits.Count > 0)
            {

                Graphics g = PitPanel.CreateGraphics();

                // draw the pattern
                try
                {
                    g.Clear(Color.White);

                    Decimal yMax = (Decimal)(y - pH * 2);

                    DrawText(g, "Tyres", 0f, 0f, false, Color.Green);
                    DrawText(g, "Fuel", 0f, 10f, false, Color.Red);
                    DrawText(g, "Mistake", 0f, 20f, false, Color.Blue);
                    DrawText(g, "Pit Entry", 0f, 30f, false, Color.Orange);

                    for (int i = 0; i < pits.Count; i++)
                    {
                        Pits p = pits[i];

                        int x1 = (i + 1) * x / (pits.Count + 1) - pW;
                        Decimal y1 = yMax * p.Tyres / p.Total;
                        Decimal y2 = yMax * p.Fuel / p.Total;
                        Decimal y3 = yMax * p.Mistake / p.Total;

                        int yc = pH;

                        g.FillRectangle(Brushes.Yellow, x1, yc, pW * 2, (int)yMax);                ///In lap

                        if (y1 > 0)
                        {
                            g.FillRectangle(Brushes.Green, x1, yc, pW * 2, (int)y1);                 ///Tyre
                            yc += (int)(y1);
                            DrawText(g, p.Tyres.ToString() + "s", (float)(x1), (float)(yc - y1 / 2), false, Color.Black);
                        }

                        if (y2 > 0)
                        {
                            g.FillRectangle(Brushes.Red, x1, yc, pW * 2, (int)y2);       ///Fuel
                            yc += (int)y2;
                            DrawText(g, p.Fuel.ToString() + "s", (float)(x1), (float)(yc - y2 / 2), false, Color.Black);
                        }

                        if (y3 > 0)
                        {
                            g.FillRectangle(Brushes.Blue, x1, yc, pW * 2, (int)y3); ///Mistake
                            yc += (int)y3;
                            DrawText(g, p.Mistake.ToString() + "s", (float)(x1), (float)(yc - y3 / 2), false, Color.Black);
                        }

                        //DrawText(g, p.Total.ToString() + "s", (float)(x1), (float)yMax, false);

                        DrawText(g, "Lap " + p.LapNumber.ToString(), (float)(x1), (float)0, false, Color.Black);
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

        private static void DrawText(Graphics g, String s, float x, float y, bool vertical, Color c)
        {
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            using (Brush tempForeBrush = new System.Drawing.SolidBrush(c))
                if (vertical)
                    g.DrawString(s, new Font(FontFamily.GenericSansSerif, 7f), tempForeBrush, x, y, drawFormat);
                else
                    g.DrawString(s, new Font(FontFamily.GenericSansSerif, 7f), tempForeBrush, x, y);
        }
    }
}
