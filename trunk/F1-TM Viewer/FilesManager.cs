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
using System.IO;
using System.Collections;

namespace F1_TM_Viewer
{
    public delegate void FilesManagerUpdate(FilesManager fm);

    public class FilesManager
    {
        private DirectoryInfo rootFolder;
        private ArrayList files;
        private String selectedFile;
        private int selectedIndex;

        #region Properties
        public System.IO.DirectoryInfo RootFolder
        {
            get
            {
                return this.rootFolder;
            }
            set
            {
                this.rootFolder = value;
            }
        }

        public System.Collections.ArrayList Files
        {
            get
            {
                return this.files;
            }
            set
            {
                this.files = value;
            }
        }

        /// <summary>
        /// Full path to the selected file
        /// </summary>
        public string SelectedFile
        {
            get
            {
                return this.selectedFile;
            }
            set
            {
                this.selectedFile = value;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
            }
        }
        #endregion

        /// <summary>
        /// </summary>
        /// <param name="rootFolder"></param>
        /// <param name="files"></param>
        /// <param name="selectedFile"></param>
        /// <param name="selectedIndex"></param>
        public FilesManager(DirectoryInfo rootFolder, ArrayList files, string selectedFile, int selectedIndex)
        {
            this.rootFolder = rootFolder;
            this.files = files;
            this.selectedFile = selectedFile;
            this.selectedIndex = selectedIndex;
        }

        public FilesManager()
        {
            this.rootFolder = new DirectoryInfo("C://");
            this.files = new ArrayList();
            this.selectedFile = "";
            this.selectedIndex = -1;
        }
    }
}
