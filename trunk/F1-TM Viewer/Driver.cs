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
    /// Summary description for Driver.
    /// </summary>
    public class Driver
    {
        public int lap;
        public int position;
        public string name;
        public string team;
        public string time;
        public string distance;
        public string pits;
        public string status;
        public int change;
        public string mistake;
        public Driver()
        {
            //
            // TODO: Add constructor logic here
            //
            lap = new int();
            position = new int();
            name = new string('0', 1);
            team = new string('0', 1);
            time = new string('0', 1);
            distance = new string(' ', 1);
            pits = new string(' ', 1);
            status = new string(' ', 1);
            change = new int();
        }
    }
}
