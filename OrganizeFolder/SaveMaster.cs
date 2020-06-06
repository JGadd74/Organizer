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
        public static List<string[]> SavedCategories = GetSavedCategories();
        
        private static void CheckSaveFile()
        {
            if (!File.Exists(SaveFile))
            {
                File.Create(SaveFile);
            }
        }


        public static List<string[]> GetSavedCategories()
        {
            CheckSaveFile();

            List<string[]> FormattedSave = new List<string[]>(); // list of categories
         
            string[] unformattedFile = File.ReadAllLines(SaveFile);  // Whole file as string[]

            List<string> categoryTemplate = new List<string>(); // Temp holder for category

            bool isFirstCategory = true;
            foreach(string line in unformattedFile)
            {
                if(line[0] == '.')
                {
                    categoryTemplate.Add(line);
                }
                else
                {
                    if (!isFirstCategory)
                    {
                        isFirstCategory = false;
                        FormattedSave.Add(categoryTemplate.ToArray()); // add temp to list of categories
                    }
                    categoryTemplate.Clear();
                    categoryTemplate.Add(line);
                }
            }
            return FormattedSave; // return list of custom categories saved in SaveFile
        }

        public static void SaveCustomCategory(string[] Category)
        {
                File.AppendAllLines(SaveFile, Category);
        }

        public static void SortCustomCategories()
        {
            //Sort categories into alphabetical order and overwrite
            SavedCategories.Sort();
        }













       
    }
}
