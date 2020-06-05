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
        


        public static List<string[]> GetSavedCategories()
        {
            List<string[]> FormattedSave = new List<string[]>(); // this will hold the category
            
            string[] unformattedFile = File.ReadAllLines(SaveFile); 

            List<string> categoryTemplate = new List<string>(); // one category

            bool isCategoryName = true;
            foreach(string line in unformattedFile)
            {
                if(line[0] == '.')
                {
                    
                }
                else if(line[0] != '.') // isname
                {

                }
            }

            
            
            return FormattedSave;
        }
       
    }
}
