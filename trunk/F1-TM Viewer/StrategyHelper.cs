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
    public partial class StrategyHelper : Form
    {
        int[] grip = { 95, 85, 75, 65 };
        int[] wear = { 4, 3, 2, 1 };

        public StrategyHelper()
        {
            InitializeComponent();
        }

        public void lb_Tyre_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateNumbers();
        }

        public void tb_starting_TextChanged(object sender, EventArgs e)
        {
            updateCalculations();
        }

        public void tb_wear_TextChanged(object sender, EventArgs e)
        {
            updateCalculations();
        }

        public void num_lap_ValueChanged(object sender, EventArgs e)
        {
            updateCalculations();
        }

        public void updateNumbers()
        {
            if (lb_Tyre.SelectedIndex < 0 || lb_Tyre.SelectedIndex > 3)
                return;
            tb_starting.Text = grip[lb_Tyre.SelectedIndex].ToString();
            tb_wear.Text = wear[lb_Tyre.SelectedIndex].ToString();
        }

        public void updateCalculations()
        {
            try
            {
                if (tb_starting.Text == "" || tb_wear.Text == "")
                    return;

                int start = Convert.ToInt32(tb_starting.Text);
                int rate = Convert.ToInt32(tb_wear.Text);

                tb_remaining.Text = (start - num_lap.Value * rate).ToString() + "%";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
