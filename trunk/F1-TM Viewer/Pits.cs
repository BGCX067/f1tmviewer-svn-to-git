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
    public class Pits
    {
        int lapNumber;
        String driver;
        String team;
        Decimal tyres;
        Decimal fuel;
        Decimal mistake;
        Decimal total;

        int driverID;

        #region Properties
        public int LapNumber
        {
            get
            {
                return this.lapNumber;
            }
            set
            {
                this.lapNumber = value;
            }
        }

        public int DriverID
        {
            get
            {
                return driverID;
            }
            set
            {
                this.driverID = value;
            }
        }

        public string Driver
        {
            get
            {
                return this.driver;
            }
            set
            {
                this.driver = value;
            }
        }

        public string Team
        {
            get
            {
                return this.team;
            }
            set
            {
                this.team = value;
            }
        }

        public decimal Tyres
        {
            get
            {
                return this.tyres;
            }
            set
            {
                this.tyres = value;
            }
        }

        public decimal Fuel
        {
            get
            {
                return this.fuel;
            }
            set
            {
                this.fuel = value;
            }
        }

        public decimal Mistake
        {
            get
            {
                return this.mistake;
            }
            set
            {
                this.mistake = value;
            }
        }

        public decimal Total
        {
            get
            {
                return this.total;
            }
            set
            {
                this.total = value;
            }
        }
        #endregion

        /// <summary>
        /// </summary>
        /// <param name="lapNumber"></param>
        /// <param name="driver"></param>
        /// <param name="team"></param>
        /// <param name="tyres"></param>
        /// <param name="fuel"></param>
        /// <param name="mistake"></param>
        /// <param name="total"></param>
        public Pits(int lapNumber, string driver, string team, int driverid, decimal tyres, decimal fuel, decimal mistake, decimal total)
        {
            this.lapNumber = lapNumber;
            this.driver = driver;
            this.team = team;
            this.driverID = driverid;
            this.tyres = tyres;
            this.fuel = fuel;
            this.mistake = mistake;
            this.total = total;
        }

        /// <summary>
        /// </summary>
        /// <param name="lapNumber"></param>
        /// <param name="driver"></param>
        /// <param name="team"></param>
        /// <param name="tyres"></param>
        /// <param name="fuel"></param>
        /// <param name="mistake"></param>
        /// <param name="total"></param>
        public Pits(String lapNumber, String driver, String team, int driverid, String tyres, String fuel, String mistake, String total)
        {
            this.lapNumber = Convert.ToInt32(lapNumber);
            this.driver = driver;
            this.team = team;
            this.driverID = driverid;
            this.tyres = Convert.ToDecimal(tyres);
            this.fuel = Convert.ToDecimal(fuel);
            this.mistake = Convert.ToDecimal(mistake);
            this.total = Convert.ToDecimal(total);
        }

    }
}
