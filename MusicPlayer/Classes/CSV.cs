using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Mp3Lib;

namespace MusicPlayer.Classes
{
    public static class CSV
    {
        public static List<string[]> Import(string file, char csvDelimiter, bool ignoreHeadline = false, bool removeQuoteSign = false)
        {
            return ReadCSVFile(file, csvDelimiter, ignoreHeadline, removeQuoteSign);
        }

        public static void CreateCSV()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.InitialDirectory = SettingsForm.DefaultPath;
            open.Filter = "MP3 Files|*.mp3" /*+ "|All Files|*.*"*/;
            open.FilterIndex = 1;
            open.RestoreDirectory = true;

            string[] SongFiles = null;
            if (open.ShowDialog() == DialogResult.OK)
            {
                SongFiles = open.FileNames; //Full path of all selected files
                string line = "";
                string dir = (Path.Combine(Path.GetDirectoryName(SongFiles[0]), SettingsForm.CSVFileName));
                for (int i = 0; i < SongFiles.Length; i++)
                {
                    string FileName = Path.GetFileNameWithoutExtension(SongFiles[i]);
                    //FileInfo file = new FileInfo(SongFiles[i]);
                    
                    string title = "", artist = "", album = "";

                    try
                    {
                        int minus = FileName.LastIndexOf("-");

                        title = FileName.Substring(minus + 2);
                        artist = FileName.Substring(0, minus - 1);
                        album = Path.GetFileName(SongFiles[i]);
                    }
                    catch (Exception e)
                    {
                        artist = album = "";
                        title = Path.GetFileName(@SongFiles[i]);
                    }

                    string newTitle = "", newAlbum = "", newPerformer = "";
                    Mp3File mp3 = new Mp3File(@SongFiles[i]);
                    try
                    {
                        newTitle = mp3.TagHandler.Title;
                        newAlbum = mp3.TagHandler.Album;
                        newPerformer = mp3.TagHandler.Artist;

                        if (newTitle == "")
                            throw new Exception("Title is empty");

                        if (newPerformer == "")
                            throw new Exception("Performer is empty");

                        if (newAlbum == "")
                            throw new Exception("Album is empty");

                        line += newPerformer + "," +  newTitle + "," + newAlbum;
                        if (i != SongFiles.Length - 1)
                            line += "\n";
                    }
                    catch (Exception e)
                    {
                        newTitle = title;
                        newAlbum = album;
                        newPerformer = artist;

                        line += newPerformer + "," + newTitle + "," + newAlbum;
                        if (i != SongFiles.Length - 1)
                            line += "\n";
                    }
                }
                try
                {
                    File.WriteAllText(@dir, line);
                    MessageBox.Show("File " + Path.GetFileName(dir) + " has been successfully created!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("There's been an issue with creating " + Path.GetFileName(dir) + "!");
                }
            }
        }

        private static List<string[]> ReadCSVFile(string filename, char csvDelimiter, bool ignoreHeadline, bool removeQuoteSign)
        {
            string[] result = new string[0];
            List<string[]> lst = new List<string[]>();

            string line;
            int currentLineNumner = 0;
            int columnCount = 0;

            // Read the file
            using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
            {
                while ((line = file.ReadLine()) != null)
                {
                    currentLineNumner++;
                    string[] strAr = line.Split(csvDelimiter);
                    // Save column count of first line
                    if (currentLineNumner == 1)
                        columnCount = strAr.Count();
                    else
                    {
                        //Check column count of every other lines
                        if (strAr.Count() != columnCount)
                        {
                            throw new Exception(string.Format("CSV Import Exception: Wrong column count in line {0}", currentLineNumner));
                        }
                    }

                    if (removeQuoteSign) strAr = RemoveQouteSign(strAr);

                    if (ignoreHeadline)
                    {
                        if (currentLineNumner != 1) lst.Add(strAr);
                    }
                    else
                    {
                        lst.Add(strAr);
                    }
                }
            }
            return lst;
        }

        private static string[] RemoveQouteSign(string[] ar)
        {
            for (int i = 0; i < ar.Count(); i++)
            {
                if (ar[i].StartsWith("\"") || ar[i].StartsWith("'")) ar[i] = ar[i].Substring(1);
                if (ar[i].EndsWith("\"") || ar[i].EndsWith("'")) ar[i] = ar[i].Substring(0, ar[i].Length - 1);

            }
            return ar;
        }

    }
}
