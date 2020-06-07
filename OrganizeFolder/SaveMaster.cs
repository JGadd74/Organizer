using System;
using System.Collections.Generic;
using System.IO;

namespace OrganizeFolder
{
    /// <summary>
    /// This will Make the save file and add, remove, access and format file text
    /// </summary>
    public static class SaveMaster
    {
        static string SaveFile = @"C:CustomCategories.txt";
        
        private static void VerifySaveFile()
        {
            if (!File.Exists(SaveFile)) File.Create(SaveFile);
        }

        public static void SaveCustomCategories(List<string[]> CustomCategories)
        {
           
           
            foreach(string[] category in CustomCategories)
            {
                File.WriteAllLines(SaveFile, category);
            }
        }






        public static List<string[]> GetSavedCategories() 
        {
            VerifySaveFile();
            List<string[]> FormattedSave = new List<string[]>(); // list of categories
            string[] unformattedFile = File.ReadAllLines(SaveFile);  // Whole file as string[]
            if (unformattedFile.Length > 0)
            {
                List<string> categoryTemplate = new List<string>(); // Temp holder for category
                int lineCount = 0;
                bool isFirstCategory = true;
                foreach (string line in unformattedFile) // Check each line in txtfile
                {
                    if (line[0] == '.') // if its an extension, add it to string[]
                    {
                        categoryTemplate.Add(line);
                    }
                    else // if it's a category name
                    {
                        if (!isFirstCategory)
                        {
                            FormattedSave.Add(categoryTemplate.ToArray()); // add temp to list of categories
                        }
                        categoryTemplate.Clear();
                        categoryTemplate.Add(line);
                    }
                    lineCount++;
                    isFirstCategory = false;
                }
                if (lineCount == unformattedFile.Length) FormattedSave.Add(categoryTemplate.ToArray());
            }
            
            return FormattedSave; // return list of custom categories saved in SaveFile
        }





















        public static void SortCustomCategories()
        {
            //Sort categories into alphabetical order and overwrite
            
        }













       
    }
}
