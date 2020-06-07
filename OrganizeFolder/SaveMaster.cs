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
        
        
        private static void CheckSaveFile()
        {
            if (!File.Exists(SaveFile))
            {
                File.Create(SaveFile);
            }
        }






        public static List<string[]> GetSavedCategories() // Doesn't return last category?
        {
            CheckSaveFile();

            List<string[]> FormattedSave = new List<string[]>(); // list of categories
         
            string[] unformattedFile = File.ReadAllLines(SaveFile);  // Whole file as string[]

            List<string> categoryTemplate = new List<string>(); // Temp holder for category

            string[] test = new string[] { "Category", ".xt", ".pvc" }; // test purposes
            FormattedSave.Add(test);

            int lineCount = 0;
            bool isFirstCategory = true;
            foreach(string line in unformattedFile) // Check each line in txtfile
            {
                if(line[0] == '.') // if its an extension, add it to string[]
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
            if (lineCount == unformattedFile.Length)
            {
                FormattedSave.Add(categoryTemplate.ToArray());
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
            
        }













       
    }
}
