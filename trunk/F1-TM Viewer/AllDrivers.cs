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
using System.Text;

namespace F1_TM_Viewer
{
    public class AllDrivers
    {
        private List<String> teams = new List<String>();
        private List<String> drivers = new List<String>();

        private List<String> uniqueTeams = new List<String>();

        public void AddDriver(String Driver, String Team)
        {
            if (getTeamID(Team) == -1)
                uniqueTeams.Add(Team);

            if (getTeamForDriver(Driver) == "")
            {
                drivers.Add(Driver);
                teams.Add(Team);
            }
        }

        public String getTeamForDriver(String Driver)
        {
            for (int i = 0; i < drivers.Count; i++)
                if (Driver == drivers[i])
                    return teams[i];
            return "";
        }

        public int getDriverID(String Driver)
        {
            for (int i = 0; i < drivers.Count; i++)
                if (Driver == drivers[i])
                    return i;
            return -1;
        }

        public int getTeamID(String Team)
        {
            for (int i = 0; i < teams.Count; i++)
                if (Team == teams[i])
                    return i;
            return -1;
        }

        public String[] getDrivers()
        {
            return drivers.ToArray();
        }

        public String[] getTeams()
        {
            return uniqueTeams.ToArray();
        }
    }
}
