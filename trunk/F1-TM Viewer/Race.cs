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

using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Data;
using System.Net;
using System.Text;

namespace F1_TM_Viewer
{
    /// <summary>
    /// Summary description for Race.
    /// </summary>
    public class Race
    {
        public Lap[] laps;

        public AllPitStops pitstops = new AllPitStops();

        public AllDrivers driver_team = new AllDrivers();

        public List<Part> Car1Wear = new List<Part>();
        public List<Part> Car2Wear = new List<Part>();
        public Boolean WearPresent = false;

        public int counter = 0;
        public Race(int count)
        {
            counter = count;
            laps = new Lap[count];
            for (int i = 0; i < count; i++)
                laps[i] = new Lap();
        }
        public void assign(int lap, int pos, String name, String team, String time, String dist, String pits, String mist, String stat)
        {
            int change = 0;
            if (lap > 1)
            {
                change = laps[lap - 1].getPos(name, team);
                if (change != 0)
                    change = change - pos;
            }
            laps[lap].assign(lap, pos, name, team, time, dist, pits, stat, change, mist);
        }
        public void display()
        {
            string disp = "";
            for (int i = 0; i < counter; i++)
            {
                disp += laps[i].getString() + "\n";
            }
            MessageBox.Show(disp);
        }


        public Boolean AddCarWear(int DriverNumber, String name, String wear, String reliability)
        {
            try
            {
                Part p = new Part(name, wear, reliability);
                if (DriverNumber == 1)
                    Car1Wear.Add(p);
                else
                    Car2Wear.Add(p);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean AddCarWear(int DriverNumber, string name, int wear, int reliability)
        {
            Part p = new Part(name, wear, reliability);
            if (DriverNumber == 0)
                Car1Wear.Add(p);
            else
                Car2Wear.Add(p);

            return true;
        }

        public Boolean AddPit(String lapNumber, String driver, String team, String tyres, String fuel, String mistake, String total)
        {
            return pitstops.AddPit(lapNumber, driver, team, driver_team.getDriverID(driver), tyres, fuel, mistake, total);
        }
    }
}
