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
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;

namespace F1_TM_Viewer
{
    public partial class Main : Form
    {
        /// <summary>
        /// Files/Folder data object
        /// </summary>
        private FilesManager FileManagerData;

        /// <summary>
        /// File reader + All data stored here
        /// </summary>
        private XMLReader xmlReader;

        private dGraph lapTimes;
        private CarWear dWear1;
        private CarWear dWear2;
        private pitStops pStops;
        private graph driverPositions;
        private StrategyHelper calculator;

        /// <summary>
        /// Driver/Team listbox helper
        /// </summary>
        private bool changed = false;
        private Boolean tabsAdded = false;

        /// <summary>
        /// Timer for watching race
        /// </summary>
        Timer playTimer = new Timer();

        /// <summary>
        /// Updates to displayLap reflected in xmlReader object and GUI
        /// </summary>
        private int displayLap
        {
            get
            {
                return xmlReader.displayLap;
            }
            set
            {
                xmlReader.displayLap = value;
                lapsBar.Value = value;
                populateData();
            }
        }

        public Main(String exe)
        {
            InitializeComponent();

            if (Properties.Settings.Default.debugMode)
            {
                dbmode.Checked = true;
            }

            //initialize reader
            xmlReader = new XMLReader();

            //Initialize tabs
            SetupLapTimesGraph();
            SetupCarWearGraphs();
            SetupPitStopsGraph();
            SetupDriverPositionsGraph();
            SetupCalculator();

            //Add the always visible tabs
            tabs.TabPages.Clear();
            tabs.TabPages.Add(tabFiles);
            tabs.TabPages.Add(tabCalculator);
            tabs.TabPages.Add(tabAbout);

            //root folder management
            String s = Properties.Settings.Default.rootFolder;
            if (!Directory.Exists(s))
                s = exe.Substring(0, exe.LastIndexOf("\\"));
            FileManagerData = new FilesManager(new System.IO.DirectoryInfo(s), new System.Collections.ArrayList(), "", -1);
            this.tb_rootDirectory.Text = FileManagerData.RootFolder.FullName;
            RefreshFolder(true);
        }

        /// <summary>
        /// Called after file manager data is updated, calls functions to load new file and update GUI
        /// </summary>
        /// <param name="fm"></param>
        private void FileManagerUpdates(FilesManager fm)
        {
            FileManagerData = fm;

            //Load file
            if (File.Exists(FileManagerData.SelectedFile))
            {
                xmlReader.Load(FileManagerData.SelectedFile);

                this.Text = "F1-TM Viewer " + FileManagerData.SelectedFile.Replace(FileManagerData.RootFolder.FullName, "");
            }
            //or load selected item from the list
            else if (FileManagerData.SelectedIndex > -1)
            {
                xmlReader.Load(((FileInfo)FileManagerData.Files[FileManagerData.SelectedIndex]).FullName);

                this.Text = "F1-TM Viewer " + ((FileInfo)FileManagerData.Files[FileManagerData.SelectedIndex]).FullName.Replace(FileManagerData.RootFolder.FullName, "");
            }

            //if new file loaded correctly
            if (xmlReader.loaded)
            {
                //update track bar
                setTrackBar();

                //update list boxes for drivers/teams
                list2.Items.AddRange(xmlReader.race.driver_team.getDrivers());
                list1.Items.AddRange(xmlReader.race.driver_team.getTeams());

                //update telemetry display
                populateData();

                //update lap times display
                lapTimes.newRace(xmlReader.race);

                //update car wear displays
                dWear1.newWear(xmlReader.race.Car1Wear, 1);
                dWear2.newWear(xmlReader.race.Car2Wear, 2);

                //update pit stop display
                pStops.newPitstop(xmlReader.race);

                //update driver positions display
                driverPositions.newGraph(xmlReader.race);

                //Add tabs visible only when a file has been loaded
                if (!tabsAdded)
                {
                    tabs.TabPages.Insert(1, tabRace);
                    tabs.TabPages.Insert(2, tabWear1);
                    tabs.TabPages.Insert(3, tabWear2);
                    tabs.TabPages.Insert(4, tabPits);
                    tabs.TabPages.Insert(5, tabPositions);
                    tabs.TabPages.Insert(6, tabLaps);
                    tabsAdded = true;
                }
            }
            else if (File.Exists(FileManagerData.SelectedFile) || FileManagerData.SelectedIndex > -1)
            {
                MessageBox.Show("Telemetry file could not be opened: \n" + xmlReader.message, "Telemetry File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Make telemetry GUI visible/invisible
            makeVisible(xmlReader.loaded);
        }




        #region File/Folder Selection

        /// <summary>
        /// Select a new folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dial = new FolderBrowserDialog();
            if (dial.ShowDialog() == DialogResult.OK)
            {
                String path = dial.SelectedPath;
                try
                {
                    if (!path.Equals(null))
                    {
                        FileManagerData.RootFolder = new DirectoryInfo(path);
                        this.tb_rootDirectory.Text = path;
                        RefreshFolder(false);
                    }
                }
                catch (Exception exct)
                {
                    MessageBox.Show("There was an exception with your selection.\n" + exct.Message);
                }
            }
        }

        /// <summary>
        /// Refresh the xml files list
        /// </summary>
        private void RefreshFolder(Boolean updateFiles)
        {
            ///Make the list
            DirectoryInfo dir;
            if (tb_rootDirectory.Text[tb_rootDirectory.Text.Length - 2] != ':')
            {
                dir = new DirectoryInfo(tb_rootDirectory.Text);
                FileManagerData.Files = new ArrayList();
                indexXMLFiles(dir);
                lb_filesList.Items.Clear();

                //Read the xml file list
                foreach (FileInfo f in FileManagerData.Files)
                {
                    //Add to GUI after formatting
                    lb_filesList.Items.Add(f.FullName.Replace(tb_rootDirectory.Text, ""));
                }

                if (updateFiles)
                    FileManagerUpdates(this.FileManagerData);
            }
            else
                MessageBox.Show("Cannot list this directory as it is the root directory. \nIt will take too long to make a list.");
        }

        /// <summary>
        /// Recursively (Depth first) look into directory to find all xml files
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private ArrayList indexXMLFiles(DirectoryInfo dir)
        {
            // Add xml files from this directory into the _xmlFiles arraylist			
            try
            {
                //FileManagerData.Files.AddRange(dir.GetFiles("*.csv"));
                FileManagerData.Files.AddRange(dir.GetFiles("*.xml"));

                // If there are subdirectories, go into each of them, getting their xml files and adding them to the _xmlFiles arraylist
                if (dir.GetDirectories().Length > 0)
                {
                    DirectoryInfo[] fos = dir.GetDirectories();
                    foreach (DirectoryInfo info in fos)
                    {
                        indexXMLFiles(info);
                    }
                }
            }
            catch (Exception)
            {
                // return null, doing so does not effect the _xmlFiles arraylist
                return null;
            }

            // Returns the 
            return FileManagerData.Files;

        }

        /// <summary>
        /// Open a new telemetry file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dial = new OpenFileDialog();
            dial.Filter = "XML files (*.xml)|*.xml";
            DialogResult dr = dial.ShowDialog();

            if (dr == DialogResult.OK)
            {
                try
                {
                    FileManagerData.SelectedFile = dial.FileName;

                    //if a file was selected
                    if (FileManagerData.SelectedFile != null && File.Exists(FileManagerData.SelectedFile))
                    {
                        FileManagerData.SelectedIndex = -1;

                        FileManagerUpdates(this.FileManagerData);
                    }
                    else
                    {
                        FileManagerData.SelectedFile = "";
                        FileManagerData.SelectedIndex = 0;
                    }
                }
                catch (Exception exct)
                {
                    MessageBox.Show("There was an exception with your selection.\nPlease select again.\n" + exct.Message);
                }
            }
        }

        /// <summary>
        /// Open Downloader window to show options for downloading telemetry files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_download_Click(object sender, EventArgs e)
        {
            Downloader d = new Downloader(FileManagerData, new FilesManagerUpdate(FileManagerUpdates));
            d.ShowDialog();
        }

        /// <summary>
        /// Update state when file selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_filesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileManagerData.SelectedIndex = lb_filesList.SelectedIndex;
            FileManagerData.SelectedFile = "";
            FileManagerUpdates(FileManagerData);
        }
        #endregion

        /// <summary>
        /// Saves state of root folder selected before application exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.rootFolder = FileManagerData.RootFolder.FullName;
            Properties.Settings.Default.debugMode = DebugLog.Enabled;
            Properties.Settings.Default.Save();
            DebugLog.close();
        }

        /// <summary>
        /// Selected tab changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!xmlReader.loaded && tabs.SelectedTab != tabCalculator && tabs.SelectedTab != tabAbout)
            //    tabs.SelectedTab = tabFiles;
        }

        #region Populate telemetry screen
        /// <summary>
        /// Update display of telemetry data
        /// </summary>
        private void populateData()
        {
            if (xmlReader.loaded)
            {
                LapOfLaps.Text = xmlReader.displayLap.ToString() + " of " + xmlReader.maxLap.ToString();

                //Clear all
                //Pos.Items.Clear();
                //name.Items.Clear();
                //team.Items.Clear();
                //laptime.Items.Clear();
                //distance.Items.Clear();
                //pittime.Items.Clear();
                //status.Items.Clear();

                raceView.Rows.Clear();

                //Update using current data
                for (int i = 1; i < 21; i++)
                {
                    string s = xmlReader.race.laps[xmlReader.displayLap].d[i].position.ToString() + " ";
                    if (xmlReader.race.laps[xmlReader.displayLap].d[i].change != 0)
                    {
                        if (xmlReader.race.laps[xmlReader.displayLap].d[i].change > 0)
                            s += "+";
                        s += xmlReader.race.laps[xmlReader.displayLap].d[i].change;
                    }

                    //Populate list based display
                    //Pos.Items.Add(s);
                    //name.Items.Add(xmlReader.race.laps[xmlReader.displayLap].d[i].name);
                    //team.Items.Add(xmlReader.race.laps[xmlReader.displayLap].d[i].team);
                    //laptime.Items.Add(xmlReader.race.laps[xmlReader.displayLap].d[i].time);
                    //distance.Items.Add(xmlReader.race.laps[xmlReader.displayLap].d[i].distance);
                    //pittime.Items.Add(xmlReader.race.laps[xmlReader.displayLap].d[i].pits);
                    //status.Items.Add(xmlReader.race.laps[xmlReader.displayLap].d[i].status);

                    //Populate datagrid based display
                    int rno = raceView.Rows.Add(new DataGridViewRow());
                    raceView.Rows[rno].Height = 14;
                    raceView.Rows[rno].Cells["cPos"].Value = s;
                    raceView.Rows[rno].Cells["cName"].Value = xmlReader.race.laps[xmlReader.displayLap].d[i].name;
                    raceView.Rows[rno].Cells["cTeam"].Value = xmlReader.race.laps[xmlReader.displayLap].d[i].team;
                    raceView.Rows[rno].Cells["cLaptime"].Value = xmlReader.race.laps[xmlReader.displayLap].d[i].time;
                    raceView.Rows[rno].Cells["cDistance"].Value = xmlReader.race.laps[xmlReader.displayLap].d[i].distance;
                    raceView.Rows[rno].Cells["cPittime"].Value = xmlReader.race.laps[xmlReader.displayLap].d[i].pits;
                    raceView.Rows[rno].Cells["cStatus"].Value = xmlReader.race.laps[xmlReader.displayLap].d[i].status;

                }
                populateSelection();

                //Switch to Race tab
                tabs.SelectedTab = tabRace;
            }

        }

        /// <summary>
        /// Select a driver/team in the telemetry display
        /// </summary>
        private void populateSelection()
        {
            //Selection in list based display
            //if (list2.SelectedIndex >= 0)
            //{
            //    name.SelectedItem = (string)list2.SelectedItem;
            //}
            //if (list1.SelectedIndex >= 0)
            //{
            //    for (int i = 0; i < team.Items.Count; i++)
            //        if ((string)team.Items[i] == (string)list1.SelectedItem)
            //            team.SetSelected(i, true);
            //        else
            //            team.SetSelected(i, false);
            //}


            //Selection in datagrid based display
            for (int i = 0; i < raceView.Rows.Count; i++)
                raceView.Rows[i].Selected = ((string)raceView.Rows[i].Cells["cName"].Value == (string)list2.SelectedItem);
            if (list1.SelectedIndex >= 0)
                for (int i = 0; i < raceView.Rows.Count; i++)
                    raceView.Rows[i].Selected = ((string)raceView.Rows[i].Cells["cTeam"].Value == (string)list1.SelectedItem);
        }

        /// <summary>
        /// Make telemetry GUI components visible
        /// </summary>
        /// <param name="v"></param>
        private void makeVisible(Boolean v)
        {
            lapsBar.Visible = v;
            lapsBox.Visible = v;
            watchBox.Visible = v;
            driverBox.Visible = v;
            teamBox.Visible = v;
            telemetryBox.Visible = v;
        }
        #endregion

        #region Driver/Team Listbox GUI
        private void list1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!changed)
            {
                if (list2.SelectedIndex > 0)
                {
                    changed = true;
                    list2.SelectedIndex = -1;
                    //name.SelectedIndex = -1;
                }
            }
            else
                changed = false;
            populateSelection();
        }

        private void list2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!changed)
            {
                if (list1.SelectedIndex >= 0)
                {
                    changed = true;
                    list1.SelectedIndex = -1;
                    //team.SelectedIndex = -1;
                }
            }
            else
                changed = false;
            populateSelection();
        }
        #endregion

        #region Watch Race GUI

        /// <summary>
        /// Start watch race timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_Click(object sender, EventArgs e)
        {
            stop.Enabled = true;
            play.Enabled = false;
            playTimer.Interval = playSlider.Value * 1000;
            playTimer.Tick += new EventHandler(playTimer_Tick);
            playTimer.Start();
        }

        /// <summary>
        /// Handle timer tick, move to next lap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void playTimer_Tick(object sender, EventArgs e)
        {
            if (xmlReader.displayLap < xmlReader.maxLap)
                next.PerformClick();
            else
                stop.PerformClick();
        }

        /// <summary>
        /// Stop watch race timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stop_Click(object sender, EventArgs e)
        {
            stop.Enabled = false;
            play.Enabled = true;
            playTimer.Stop();
        }

        /// <summary>
        /// Update the watch race timer tick interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeSlider_Scroll(object sender, EventArgs e)
        {
            playTimer.Interval = playSlider.Value * 1000;
        }
        #endregion

        #region Lap navigation GUI

        /// <summary>
        /// Initialize max/value of laps trackbar
        /// </summary>
        private void setTrackBar()
        {
            lapsBar.Maximum = xmlReader.maxLap;
            lapsBar.Value = xmlReader.displayLap;
        }

        private void lapsBar_Scroll(object sender, EventArgs e)
        {
            displayLap = lapsBar.Value;
        }

        private void previous_Click(object sender, EventArgs e)
        {
            if (displayLap > 1)
                displayLap--;
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (displayLap < xmlReader.maxLap)
                displayLap++;
        }

        private void first_Click(object sender, EventArgs e)
        {
            displayLap = 1;
        }

        private void last_Click(object sender, EventArgs e)
        {
            displayLap = xmlReader.maxLap;
        }
        #endregion


        #region LapTime Graph GUI
        /// <summary>
        /// Link GUI to objects in Graph class, call only once.
        /// </summary>
        private void SetupLapTimesGraph()
        {
            lapTimes = new dGraph();

            lapTimes.drawer = this.drawer;
            lapTimes.drivers = this.drivers;
            lapTimes.groupBox1 = this.groupBox1;
            lapTimes.groupBox2 = this.groupBox2;
            lapTimes.dmin = this.dmin;
            lapTimes.dmax = this.dmax;
            lapTimes.bmax = this.bmax;
            lapTimes.bmin = this.bmin;
            lapTimes.lb1 = this.lb1;
            lapTimes.dispTime = this.dispTime;
            //lapTimes.groupBox3 = this.groupBox3;
            lapTimes.label1 = this.label1;
            //lapTimes.label2 = this.label2;
            //lapTimes.label3 = this.label3;
            lapTimes.lb2 = this.lb2;
        }

        /// <summary>
        /// Driver selection changed, update Graph class and refresh display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xmlReader.loaded)
            {
                lapTimes.drivers_SelectedIndexChanged(sender, e);
                drawer.Refresh();
            }
        }

        /// <summary>
        /// Re-Draw the graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawer_Paint(object sender, PaintEventArgs e)
        {
            if (xmlReader.loaded)
            {
                lapTimes.RePaint(e);
            }
        }

        /// <summary>
        /// Checkbox changed, call Graph class and refresh display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispTime_CheckedChanged(object sender, EventArgs e)
        {
            if (xmlReader.loaded)
            {
                lapTimes.dispTime_CheckedChanged(sender, e);
                lapTimes.drawer.Refresh();
            }
        }
        #endregion

        #region Car Wear Graph GUI

        private void SetupCarWearGraphs()
        {
            dWear1 = new CarWear();
            dWear2 = new CarWear();

            dWear1.WearPanel = WearPanel1;
            dWear2.WearPanel = WearPanel2;
        }

        private void WearPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (xmlReader.loaded)
            {
                dWear1.RePaint(e);
            }
        }

        private void WearPanel2_Paint(object sender, PaintEventArgs e)
        {
            if (xmlReader.loaded)
            {
                dWear2.RePaint(e);
            }
        }
        #endregion

        #region Pit Stops Graph GUI
        private void driversList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xmlReader.loaded)
            {
                pStops.driversList_SelectedIndexChanged(sender, e);
                drawerPitstops.Refresh();
            }
        }

        private void SetupPitStopsGraph()
        {
            pStops = new pitStops();
            pStops.driversList = this.driversList;
            pStops.drawer = this.drawerPitstops;
        }

        private void drawerPitstops_Paint(object sender, PaintEventArgs e)
        {
            if (xmlReader.loaded)
            {
                pStops.RePaint(e);
            }
        }
        #endregion

        #region Driver Positions Graph GUI
        private void SetupDriverPositionsGraph()
        {
            driverPositions = new graph();
            driverPositions.drawer = this.drawerPositions;
            driverPositions.drivers = this.driverList2;
        }

        private void driverList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xmlReader.loaded)
            {
                driverPositions.drivers_SelectedIndexChanged(sender, e);
                drawerPositions.Refresh();
            }
        }

        private void drawerPositions_Paint(object sender, PaintEventArgs e)
        {
            if (xmlReader.loaded)
            {
                driverPositions.RePaint(e);
            }
        }
        #endregion

        #region Calculator GUI
        private void lb_Tyre_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculator.lb_Tyre_SelectedIndexChanged(sender, e);
        }

        private void num_lap_ValueChanged(object sender, EventArgs e)
        {
            calculator.num_lap_ValueChanged(sender, e);
        }

        private void tb_starting_TextChanged(object sender, EventArgs e)
        {
            calculator.tb_starting_TextChanged(sender, e);
        }

        private void tb_wear_TextChanged(object sender, EventArgs e)
        {
            calculator.tb_wear_TextChanged(sender, e);
        }

        private void SetupCalculator()
        {
            calculator = new StrategyHelper();
            calculator.lb_Tyre = this.lb_Tyre;
            calculator.tb_remaining = this.tb_remaining;
            calculator.tb_starting = this.tb_starting;
            calculator.tb_wear = this.tb_wear;
            calculator.num_lap = this.num_lap;
            calculator.lb_Tyre.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// Handle refresh button click, refresh xml files list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshFolder(false);
        }

        private void dbmode_CheckedChanged(object sender, EventArgs e)
        {
            DebugLog.Enabled = dbmode.Checked;
        }
    }
}
