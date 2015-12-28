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
using System.Xml;

namespace F1_TM_Viewer
{
    class XMLReader
    {
        public Race race;

        private int DisplayLap = 0;
        public int displayLap
        {
            get
            {
                if (DisplayLap < 1)
                    DisplayLap = 1;
                else if (DisplayLap > maxLap)
                    DisplayLap = maxLap;

                return DisplayLap;
            }
            set
            {
                DisplayLap = value;
            }
        }
        private int MaxLap = 1;
        public int maxLap
        {
            set
            {
                if (displayLap > value)
                    displayLap = value;
                else
                    displayLap = 1;
                if (value >= 0)
                    MaxLap = value;
            }
            get
            {
                return MaxLap;
            }
        }

        public Boolean loaded = false;

        public String message = "";

        public Boolean Load(String path)
        {
            loaded = LoadXML(path);
            return loaded;
        }

        private Boolean LoadXML(String path)
        {
            DebugLog.writeSeparator();
            DebugLog.writeString("Loading xml", path);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            message = "";
            try
            {
                XmlNode root = null;
                foreach (XmlNode n in xDoc.ChildNodes)
                    if (n.Name.ToLower() == "telemetry")
                    {
                        root = n;
                        break;
                    }

                Decimal version = 0;
                String application = "";
                ReadXMLVersion(root, ref version, ref application);
                DebugLog.writeString("Version", version.ToString());
                if (version < 1 || application != "f1-tm")
                    return false;

                XmlNode raceNode = FindXMLChildNode(root, "Race");
                List<XmlNode> laps = FindXMLChildNodes(raceNode, "Lap");
                XmlAttribute lapsCountAtt = FindXMLAttribute(raceNode, "Laps");
                DebugLog.writeString("LapsCount", laps.Count.ToString());
                if (laps.Count != Convert.ToInt32(lapsCountAtt.Value))
                    return false;

                race = new Race(laps.Count + 1);
                maxLap = laps.Count;

                Boolean loadedDriverTeams = false;
                foreach (XmlNode lap in laps)
                {
                    List<XmlNode> rows = FindXMLChildNodes(lap, "Row");
                    if (!loadedDriverTeams)
                    {
                        foreach (XmlNode Row in rows)
                        {
                            String Name = CleanUp(FindXMLChildNode(Row, "Driver").InnerText);
                            String Team = CleanUp(FindXMLChildNode(Row, "Team").InnerText);

                            race.driver_team.AddDriver(Name, Team);
                        }

                        //list1.Items.Clear();
                        //list2.Items.Clear();

                        //list2.Items.AddRange(race.driver_team.getDrivers());
                        //list1.Items.AddRange(race.driver_team.getTeams());

                        loadedDriverTeams = true;
                    }

                    foreach (XmlNode Row in rows)
                    {
                        int lapNo = Convert.ToInt32(FindXMLAttribute(lap, "Number").Value);
                        int Position = Convert.ToInt32(CleanUp(FindXMLChildNode(Row, "Position").InnerText));
                        String Name = CleanUp(FindXMLChildNode(Row, "Driver").InnerText);
                        String Team = CleanUp(FindXMLChildNode(Row, "Team").InnerText);
                        String Time = CleanUp(FindXMLChildNode(Row, "LapTime").InnerText);
                        String Distance = CleanUp(FindXMLChildNode(Row, "Distance").InnerText);
                        String PitTime = CleanUp(FindXMLChildNode(Row, "PitStopTime").InnerText);
                        String Mistake = CleanUp(FindXMLChildNode(Row, "Mistake").InnerText);
                        String Message = CleanUp(FindXMLChildNode(Row, "Message").InnerText);
                        race.assign(lapNo, Position, Name, Team, Time, Distance, PitTime, Mistake, Message);
                    }
                }

                DebugLog.writeString("Laps read");

                XmlNode pitNode = FindXMLChildNode(root, "PitStops");
                if (pitNode != null)
                {
                    List<XmlNode> stops = FindXMLChildNodes(pitNode, "Stop");
                    if (stops.Count > 0)
                        race.pitstops.PitsPresent = true;

                    foreach (XmlNode stop in stops)
                    {
                        String LapNumber = CleanUp(FindXMLChildNode(stop, "LapNumber").InnerText);
                        String Driver = CleanUp(FindXMLChildNode(stop, "Driver").InnerText);
                        String Team = CleanUp(FindXMLChildNode(stop, "Team").InnerText);
                        String Tyres = CleanUp(FindXMLChildNode(stop, "Tyres").InnerText);
                        String Fuel = CleanUp(FindXMLChildNode(stop, "Fuel").InnerText);
                        String Mistake = CleanUp(FindXMLChildNode(stop, "Mistake").InnerText);
                        String Total = CleanUp(FindXMLChildNode(stop, "Total").InnerText);

                        if (race.AddPit(LapNumber, Driver, Team, Tyres, Fuel, Mistake, Total) == false)
                        {
                            message += "Non fatal error in file format in pits stop section. Pit stop information will be unavailable\n";
                            race.pitstops.PitsPresent = false;
                            break;
                        }
                    }
                }

                DebugLog.writeString("Pits read");

                XmlNode wearNode = FindXMLChildNode(root, "CarWear");
                if (wearNode != null)
                {
                    List<XmlNode> cars = FindXMLChildNodes(wearNode, "Car");
                    if (cars.Count > 0)
                        race.WearPresent = true;

                    for (int i = 0; i < cars.Count; i++)
                    {
                        List<XmlNode> parts = FindXMLChildNodes(cars[i], "Part");
                        int carNo = Convert.ToInt32(FindXMLAttribute(cars[i], "DriverNumber").Value);

                        foreach (XmlNode part in parts)
                        {
                            String Name = CleanUp(FindXMLChildNode(part, "Name").InnerText);
                            String Wear = CleanUp(FindXMLChildNode(part, "Wear").InnerText);
                            String Reliability = CleanUp(FindXMLChildNode(part, "Reliability").InnerText);

                            if (race.AddCarWear(carNo, Name, Wear, Reliability) == false)
                            {
                                message += ("Non fatal error in file format in pits stop section. Pit stop information will be unavailable\n");
                                race.WearPresent = false;
                                break;
                            }
                        }
                    }

                    DebugLog.writeString("Wear read");

                    race.Car1Wear.Sort(CompareCarWearNames);
                    race.Car2Wear.Sort(CompareCarWearNames);
                }

                DebugLog.writeString("Done reading");
                DebugLog.writeSeparator();
            }
            catch (Exception ex)
            {
                message += ("Error in file format:\n" + ex.Message);
                DebugLog.writeString("Message Returned", message);
                DebugLog.writeError(ex);
                return false;
            }

            return true;
        }
        private static int CompareCarWearNames(Part x, Part y)
        {
            return x.Name.CompareTo(y.Name);
        }

        private static String CleanUp(String s)
        {
            return s.Replace("\n", "").Replace("\r", "").TrimStart(' ').TrimEnd(' ');
        }

        private List<XmlNode> FindXMLChildNodes(XmlNode parent, String tagName)
        {
            List<XmlNode> nodes = new List<XmlNode>();
            foreach (XmlNode n in parent.ChildNodes)
                if (n.Name.ToLower() == tagName.ToLower())
                {
                    nodes.Add(n);
                }
            return nodes;
        }

        private XmlNode FindXMLChildNode(XmlNode parent, String tagName)
        {
            foreach (XmlNode n in parent.ChildNodes)
                if (n.Name.ToLower() == tagName.ToLower())
                {
                    return n;
                }
            return null;
        }

        private XmlAttribute FindXMLAttribute(XmlNode parent, String attName)
        {
            foreach (XmlAttribute a in parent.Attributes)
                if (a.Name.ToLower() == attName.ToLower())
                {
                    return a;
                }
            return null;
        }

        private void ReadXMLVersion(XmlNode root, ref Decimal version, ref String application)
        {
            foreach (XmlAttribute a in root.Attributes)
            {
                if (a.Name.ToLower() == "version")
                    version = Convert.ToDecimal(a.Value);
                else if (a.Name.ToLower() == "application")
                    application = a.Value.ToLower();
            }
        }
    }
}
