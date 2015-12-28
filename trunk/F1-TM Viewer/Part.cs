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
    public class Part
    {
        String name;
        int wear;
        int reliability;



        #region Properties

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public int Wear
        {
            get
            {
                return this.wear;
            }
            set
            {
                this.wear = value;
            }
        }

        public int Reliability
        {
            get
            {
                return this.reliability;
            }
            set
            {
                this.reliability = value;
            }
        }
        #endregion

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wear"></param>
        /// <param name="reliability"></param>
        public Part(string name, int wear, int reliability)
        {
            this.name = name;
            this.wear = wear;
            this.reliability = reliability;
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wear"></param>
        /// <param name="reliability"></param>
        public Part(String name, String wear, String reliability)
        {
            this.name = name;
            this.wear = Convert.ToInt32(wear);
            this.reliability = Convert.ToInt32(reliability);
        }

        public override string ToString()
        {
            return name + "(" + wear + "/" + reliability + ")";
        }
    }
}
