﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Music_Player
{
    class Connection
    {
        private List<string> fileConnection = new List<string>();

        public List<string> FileConnection
        {
            get
            {
                return fileConnection;
            }
            set
            {
                fileConnection = value;
            }
        }

        // This will get textfile with songs and playlists
        // then will read the contents of the file
        public Connection(string directoryPath)
        {
            string[] folderNames; // Stores the Path of the Folder Playlists

            string cutEndpath = "Music Player";
            int indexOfBinPath = directoryPath.IndexOf(cutEndpath); // Gets the index of where the cut off for the URL begins

            string pathFiletext = directoryPath.Substring(0, indexOfBinPath);
            string[] getfile = Directory.GetFiles(pathFiletext, "PlaylistAndSong*", SearchOption.AllDirectories); // Gest the textfile location named PlaylistAndSong

            StreamReader readTextFile = new StreamReader(getfile[0]);                 

            string textFileLine;

            while (!(readTextFile.EndOfStream)) // Reads all the lines in the textfile and stops when gets to the end of the textfile text
            {
                if (readTextFile != null)
                {                  
                     textFileLine = readTextFile.ReadLine();// Gets Path of playlist
                    
                    if (textFileLine != "Mp3 Music will go" && textFileLine != "")
                    {
                        if (Directory.Exists(textFileLine))
                        {
                            folderNames = Directory.GetFiles(textFileLine, "*", SearchOption.TopDirectoryOnly);// Gets the music in the Playlist

                            if (folderNames == null || folderNames.Length == 0 || folderNames.Contains(".jpg"))// Checks if playlist has no music
                            {
                                fileConnection.Add(textFileLine); // Adds Playlist Path that has no music in it
                            }
                            else
                            {
                                for (int i = 0; i < folderNames.Length; i++)
                                {
                                    if (folderNames[i].Contains(".mp3"))
                                    {
                                        fileConnection.Add(folderNames[i]); // Adds Path of Playlist and music in it
                                    }                                  
                                }
                            }
                        }                                       
                    }                 
                }
            }
            readTextFile.Close();
        }

        // Wiil be used to write over the textfile and give it new paths with path to playlist selected removed
        public Connection(List<string> pathsPutTextfile, string compareToRemove)
        {
            string[] folderNames;
            HashSet<string> pathsToPutBack = new HashSet<string>();
            HashSet<string> pathsToDelete = new HashSet<string>();

            string pathTextFile = Directory.GetCurrentDirectory();

            string cutEndPath = "Music Player";
            int indexOffPath = pathTextFile.IndexOf(cutEndPath);

            pathTextFile = pathTextFile.Substring(0, indexOffPath);
            string[] playlistFile = Directory.GetFiles(pathTextFile, "PlaylistAndSong*", SearchOption.AllDirectories); // Gets the path to the textfile PlaylistAndSong          

            for(int i = 0; i < pathsPutTextfile.Count; i++)
            {
                if (!pathsPutTextfile[i].Contains(compareToRemove))
                {
                    if (pathsPutTextfile[i].Contains(".mp3"))
                    {
                        folderNames = pathsPutTextfile[i].Split('\\');
                        folderNames = folderNames.Take(folderNames.Length - 1).ToArray(); // Takes all the elements in the array except the last one

                        string joinArray = String.Join("\\", folderNames); // Joins the array into one string using this \

                        pathsToPutBack.Add(joinArray);
                    }
                    else
                    {
                        pathsToPutBack.Add(pathsPutTextfile[i]);
                    }                 
                }
                else
                {                   
                    if (pathsPutTextfile[i].Contains(".mp3"))
                    {
                        folderNames = pathsPutTextfile[i].Split('\\');
                        pathsToDelete.Add(String.Join("\\", folderNames.Take(folderNames.Length - 1).ToArray()));
                    }
                    else
                    {
                        pathsToDelete.Add(pathsPutTextfile[i]);
                    }                   
                }
            }
          
            for (int j = 0; j < pathsToDelete.Count; j++) // Will be used to delete the Directory 
            {
                string deleteFile = pathsToDelete.ElementAt(j);
                Directory.Delete(deleteFile, true);
                //Directory.Delete(deleteFile); // I used true to delete the files the directory but could also use a try catch to tell the user to move the files inside it to another location
            }

            StreamWriter writeToTextfile = new StreamWriter(playlistFile[0]);

            for (int h = 0; h < pathsToPutBack.Count; h++) // Gets all the paths to put back into the textfile after the playlist selected is chosen to be deleted
            {
                string fileWriteLine = pathsToPutBack.ElementAt(h);
                writeToTextfile.WriteLine(fileWriteLine);
            }
            writeToTextfile.Close();
            writeToTextfile.Dispose();
        }

        // Sends List of all the URL paths to Fomr1 to be loaded into the Comobobox and ListBox
        public List<string> fullPathName()
        {
            List<string> paths = fileConnection;
            return paths;
        }
    }
}
