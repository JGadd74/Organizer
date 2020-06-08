﻿using System.Collections;
using System.Collections.Generic;
using Save;

namespace OrganizeFolder
{
    public class ExtensionsKit
    {
        public List<string[]> ExtensionCategories = new List<string[]>() 
        { 
            audio , 
            compressed , 
            diskMedia , 
            database , 
            email , 
            executable , 
            font , 
            image , 
            internet , 
            presentation , 
            programming , 
            spreadsheet , 
            system ,
            video , 
            wordProcessor
        };

        public List<string[]> CustomCategories = SaveMaster.GetSavedCategories();

        public void addCustomCategory(string[] CustomCategory)
        {
            CustomCategories.Add(CustomCategory);
        }
        public string[] getCustomtCategoryNames()
        {
            List<string> nameList = new List<string>();

            foreach (string[] category in CustomCategories)
            {
                nameList.Add(category[0]);
            }
            nameList.TrimExcess();
            return nameList.ToArray();
        }

        public string[] getCategoryNames()
        {
            List<string> nameList = new List<string>();

            foreach (string[] category in ExtensionCategories)
            {
                nameList.Add(category[0]);
            }
            nameList.TrimExcess();
            return nameList.ToArray();
        }



        public static string[] audio = new string[] {"Audio Files" , ".aif", ".cda" , ".mid" , ".midi" , ".mp3" , ".mpa" , ".ogg" , ".wav" , ".wma" , ".wpl"};

        public static string[] compressed = new string[] {"Compressed Files" , ".7z" , ".arj" , ".deb" , ".pkg" , ".rar" , ".rpm" , ".tar.gz" , ".z" , ".zip"};

        public static string[] diskMedia = new string[] {"Disk Media Files" , ".bin" , ".dmg" , ".iso" , ".toast" , ".vcd"};

        public static string[] database = new string[] { "Database Files", ".csv", ".dat", ".db", ".dbf", ".log", ".mdb", ".sav", ".sql", ".tar", ".xml"};

        public static string[] email = new string[] { "Email Files", ".email", ".eml" , ".emlx" , ".msg" , ".oft" , ".ost" , ".pst" , ".vcf"};

        public static string[] executable = new string[] { "Executable Files", ".apk", ".bat" , ".bin" , ".cgi" , ".pl" , ".com" , ".exe" , ".gadget" , ".jar" , ".msi" , ".py" , ".wsf"};

        public static string[] font = new string[] { "Font Files", ".fnt", ".fon" , ".otf" , ".ttf"};

        public static string[] image = new string[] { "Image Files", ".ai", ".bmp" , ".gif" , ".ico" , ".jpeg" , ".jpg" , ".png", ".ps" , ".psd" , ".svg" , ".tif" , ".tiff"};

        public static string[] internet = new string[] { "Internet Files", ".asp", ".aspx" , ".cer" , ".cfm" , ".cgi" , ".pl" , ".css" , ".htm" , ".html" , ".js" , ".jsp" , ".part" , ".php" , ".py" , ".rss" , ".xhtml" , ".torrent"};

        public static string[] presentation = new string[] { "Presentation Files", ".key", ".odp" , ".pps" , ".ppt" , ".pptx" };

        public static string[] programming = new string[] { "Programming Files", ".c" , ".class" , ".cpp" , ".cs" , ".h" , ".java" , ".pl" , ".sh" , ".swift" , ".vb"};

        public static string[] spreadsheet = new string[] { "Spreadsheet Files", ".ods", ".xls" , ".xlsm" , ".xlsx"};

        public static string[] system = new string[] { "System Files", ".bak", ".cab" , ".cfg" , ".cpl" , ".cur" , ".dll" , ".dmp" , ".drv" , ".icns" , ".ico" , ".ini" , ".lnk" , ".msi" , ".sys" , ".tmp"};

        public static string[] video = new string[] { "Video Files", ".3g2", ".3gp" , ".avi" , ".flv" , ".h264" , ".m4v" , ".mkv" , ".mov" , ".mp4" , ".mpg" , ".mpeg" , ".rm" , ".swf" , ".vob" , ".wmv"};

        public static string[] wordProcessor = new string[] { "Word Processor Files", ".doc", ".docx" , ".odt" , ".pdf" , ".rtf" , ".tex" , ".txt" , ".wpd"};          
    
    }
}
