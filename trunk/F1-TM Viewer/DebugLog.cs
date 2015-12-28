using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace F1_TM_Viewer
{
    class DebugLog
    {
        static TextWriter fs;
        static Boolean debugMode = false;

        static public Boolean Enabled
        {
            get
            {
                return debugMode;
            }
            set
            {
                debugMode = value;
                if (debugMode)
                {
                    fs = new StreamWriter("debug.txt", true, Encoding.Default);
                }
                else
                {
                    close();
                }
            }
        }

        public static void writeSeparator()
        {
            if (debugMode)
            {
                for (int i = 0; i < 10; i++)
                    fs.Write("-");
                fs.WriteLine();
                fs.Flush();
            }
        }

        public static void writeString(String s)
        {
            if (debugMode)
            {
                fs.WriteLine(s);
                fs.Flush();
            }
        }

        public static void writeString(String key, String s)
        {
            if (debugMode)
            {
                fs.WriteLine(key + ": " + s);
                fs.Flush();
            }
        }

        public static void writeError(Exception e)
        {
            if (debugMode)
            {
                writeSeparator();
                fs.WriteLine("\tMessage:");
                fs.WriteLine("\t" + e.Message);
                fs.WriteLine("\tSource:");
                fs.WriteLine("\t" + e.Source);
                fs.WriteLine("\tStack:");
                fs.WriteLine("\t" + e.StackTrace);
                writeSeparator();
                fs.Flush();
            }
        }

        public static void close()
        {
            if (fs != null)
            {
                try
                {
                    fs.Flush();
                    fs.Close();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
