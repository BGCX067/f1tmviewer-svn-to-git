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
    public class AllPitStops
    {
        public List<Pits> pits = new List<Pits>();
        public Decimal PitLength = 0;
        public Boolean PitsPresent = false;

        public Boolean AddPit(String lapNumber, String driver, String team, int driverid, String tyres, String fuel, String mistake, String total)
        {
            try
            {
                Pits p = new Pits(lapNumber, driver, team, driverid, tyres, fuel, mistake, total);
                pits.Add(p);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Pits> getDriverPits(int driverID)
        {
            List<Pits> temp = new List<Pits>();

            foreach (Pits p in pits)
            {
                if (p.DriverID == driverID)
                    temp.Add(p);
            }

            return temp;
        }
    }
}
