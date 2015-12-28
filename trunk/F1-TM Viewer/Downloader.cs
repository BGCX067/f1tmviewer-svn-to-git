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
using System.Net;

namespace F1_TM_Viewer
{
    public partial class Downloader : Form
    {
        private const String websiteAdd = "http://81.179.9.119/f1manager/offlineviewer.asp?file=yes&un=";

        private FilesManager mObj;
        private FilesManagerUpdate notify;
        private BackgroundWorker bw;

        private int seconds = 0;

        public Downloader(FilesManager fm, FilesManagerUpdate listener)
        {
            InitializeComponent();

            mObj = fm;
            notify = listener;

            tb_folder.Text = mObj.RootFolder.FullName;
            tb_username.Text = Properties.Settings.Default.userName;
        }

        private void bt_download_Click(object sender, EventArgs e)
        {
            enableAll(false);

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            resultLabel.Text = "Preparing telemetry file on the Server." + Environment.NewLine + "This may take 1-5 minutes.";

            //Save username
            Properties.Settings.Default.userName = tb_username.Text;
            Properties.Settings.Default.Save();

            //String msg = openWeb(websiteAdd + tb_username.Text, tb_folder.Text + "\\", "_" + today, ".xml");
            bw.RunWorkerAsync();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                resultLabel.Text = "Cancelled";
                progressBar1.Visible = false;
            }
            else
                if (e.Error != null)
                    resultLabel.Text = e.Error.Message;
                else if ((String)e.Result == "")
                {
                    notify(mObj);

                    //Close the form
                    this.Close();
                }
                else
                {
                    resultLabel.Text = (String)e.Result;
                }

            enableAll(true);
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.ProgressPercentage == 0)
                resultLabel.Text = "Generating telemetry file";
            else
            {
                resultLabel.Text = "Downloading " + e.ProgressPercentage.ToString() + "%";
                progressBar1.Visible = true;
                progressBar1.Value = e.ProgressPercentage;
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            String today = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString()
                + "-" + DateTime.Now.Day.ToString();

            e.Result = openWeb(websiteAdd + tb_username.Text, tb_folder.Text + "\\", "_" + today, ".xml", e);
        }


        public String openWeb(string webPath, string path, string name, string ext, DoWorkEventArgs e)
        {
            DebugLog.writeString("Downloading", webPath);

            try
            {
                // used on each read operation
                byte[] buf = new byte[8192];

                // prepare the web page we will be asking for
                HttpWebRequest request = (HttpWebRequest)
                    WebRequest.Create(webPath);

                //Change Timeout to 5 minutes
                request.Timeout = 300000;

                // execute the request
                HttpWebResponse response = (HttpWebResponse)
                    request.GetResponse();



                // we will read data via the response stream
                Stream resStream = response.GetResponseStream();

                int count = 0;
                String s = response.Headers.Get("Content-Disposition");
                String s2 = response.Headers.Get("Content-Length");

                if (s == null)
                {
                    name = "Download" + name;
                }
                else if (s.StartsWith("filename="))
                    name = s.Replace("filename=", "").Replace(".xml;", "") + name;
                else
                    name = "Download" + name;

                int totalSize = 100;
                if (s2 != null)
                    totalSize = Convert.ToInt32(s2);

                int i = 1;
                String temp = path + name + ext;
                while (File.Exists(temp))
                {
                    temp = path + name + "_" + i.ToString() + ext;
                    i++;
                }

                FileStream fs = new FileStream(temp, FileMode.Create);

                try
                {
                    int run = 0;
                    do
                    {
                        if (bw.CancellationPending)
                        {
                            e.Cancel = true;
                            fs.Close();
                            if (File.Exists(temp))
                                File.Delete(temp);
                            return "Cancelled";
                        }
                        else
                        {
                            // fill the buffer with data
                            count = resStream.Read(buf, 0, buf.Length);

                            //Check if invalid username error, only in the first run
                            if (run < 1 && Encoding.ASCII.GetString(buf, 0, count).Contains("No Username"))
                            {
                                fs.Close();
                                if (File.Exists(temp))
                                    File.Delete(temp);
                                return "Invalid Username";
                            }

                            //Replace invalid characters with an underscore
                            for (int x = 0; x < count; x++)
                                if (buf[x] > 126 || buf[x] < 32)
                                    buf[x] = 95;

                            //write it out to file
                            fs.Write(buf, 0, count);
                            run += count;

                            bw.ReportProgress(run * 100 / totalSize);
                        }
                    }
                    while (count > 0); // any more data to read?

                }
                catch (Exception ew)
                {
                    fs.Close();
                    if (File.Exists(temp))
                        File.Delete(temp);

                    DebugLog.writeString("Download Write Error");
                    DebugLog.writeError(ew);
                    return "Error during file write/download:" + Environment.NewLine + ew.Message;
                }

                fs.Close();
                DebugLog.writeString("Download done");
                DebugLog.writeSeparator();

                //Select the downloaded file
                mObj.SelectedFile = temp;

                return "";
            }
            catch (Exception ex)
            {
                DebugLog.writeString("Download Error");
                DebugLog.writeError(ex);
                return ex.Message;
            }
        }

        private void Downloader_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw != null)
                if (bw.IsBusy)
                {
                    resultLabel.Text = "Cancelling..." + Environment.NewLine + "Please wait.";
                    bw.CancelAsync();
                }
        }

        private void enableAll(Boolean t)
        {
            tb_folder.Enabled = t;
            tb_username.Enabled = t;
            bt_download.Enabled = t;
            seconds = 0;
            downloadTimer.Enabled = !t;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            DateTime dt = new DateTime(1, 1, 1, seconds / 60 / 60, (seconds / 60) % 60, seconds % 60);
            this.Text = "Time Elapsed: " + dt.ToString("H:mm:ss");
        }
    }
}
