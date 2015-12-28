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

namespace F1_TM_Viewer
{
    /// <summary>
    /// Summary description for Laps.
    /// </summary>
    public class Lap
    {
        public Driver[] d = new Driver[21];
        public Lap()
        {
            //
            // TODO: Add constructor logic here
            //
            //d;

            for (int i = 0; i < 21; i++)
                d[i] = new Driver();
        }
        public void assign(int lap, int pos, string name, string team, string time, string dist, string pits, string stat, int chan, String mist)
        {
            //d[pos]=new Driver();

            d[pos].lap = lap;
            d[pos].position = pos;
            d[pos].name = name;
            d[pos].team = team;
            d[pos].time = time;
            if (dist != "0.000s")
                d[pos].distance = dist;
            if (pits != "0s")
                d[pos].pits = pits;
            if (stat != "0")
                d[pos].status = stat;
            d[pos].change = chan;
            d[pos].mistake = mist;
        }
        public string getString()
        {
            string s = "";
            for (int i = 1; i < 21; i++)
                s += d[i].lap.ToString() + d[i].position.ToString() + d[i].name + d[i].team + d[i].time + d[i].distance + d[i].pits + d[i].status + d[i].change.ToString() + "\n";
            return s;
        }
        public int getPos(string name, string team)
        {
            for (int i = 1; i < 21; i++)
                if (d[i].name == name && d[i].team == team)
                    return i;
            return 0;
        }
        public string getLapTime(string name, string team)
        {
            for (int i = 1; i < 21; i++)
                if (d[i].name == name && d[i].team == team && d[i].time != "CRASHED!")
                    return d[i].time;
            return "";
        }

        public float getConvertedTime(string name, string team)
        {
            String s = getLapTime(name, team);
            return ConvertTime(s);
        }

        public static float ConvertTime(String s)
        {
            float result = 0;
            int temp = 0;
            try
            {
                if (s != "" && s.Length > 2)
                {
                    temp = Int32.Parse(s.Substring(0, 1));
                    result = temp * 60 + Int32.Parse(s.Substring(2, 2));
                    if (s.Length > 4)
                        temp = Int32.Parse(s.Substring(5));

                    if (s.Length == 8)
                        result += (float)temp / 1000;
                    else if (s.Length == 7)
                        result += (float)temp / 100;
                    else if (s.Length == 6)
                        result += (float)temp / 10;
                }
                else
                    return -1;
            }
            catch (Exception)
            {
                //MessageBox.Show(s);
                return -1;
            }
            return result;
        }
        public float getPitTime(string name, string team)
        {
            for (int i = 1; i < 21; i++)
                if (d[i].name == name && d[i].team == team && d[i].time != "CRASHED!")
                    if (d[i].pits.Replace("s", "").Replace(" ", "") != "")
                        return float.Parse(d[i].pits.Replace("s", "").Replace(" ", ""));
            return 0;
        }
    }
}
