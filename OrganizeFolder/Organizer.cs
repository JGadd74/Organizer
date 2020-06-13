using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using MenuFramework;


namespace OrganizeFolder
{
    // goal: organize loose files into folders based on filetype
    // ex. organize downloads folder into images, docs and videos folders etc
    // currently only targeted at downloads folder
    public class Organizer
    { 
        public Menu MainMenu = new Menu(MenuType.ScrollInput, "PSI. Folder Organizer");
        private Menu SettingsMenu = new Menu(MenuType.ScrollInput, "Settings");
        private Menu EditCustomsMenu = new Menu(MenuType.ScrollInput, "Edit custom file categories");
        private Menu HelpMenu = new Menu(MenuType.ScrollInput, "Help");
        private Menu TutorialMenu = new Menu(MenuType.TextBody, "Tutorial");
        private Menu AboutFileExtensionsMenu = new Menu(MenuType.ScrollInput, "About File Extensions");
        private Menu CustomFileExtensionsMenu = new Menu(MenuType.ScrollInput, "Custom Categories");
        private Menu AboutOrganizerMenu = new Menu(MenuType.TextBody, "About PSI. Folder Organizer");

        private static string activeUser = Environment.UserName;

        private static string Main = @"C:\Users\" + activeUser + @"\Downloads";
        private static string Other = Path.Combine(Main, "Unknown type");

        private List<string> Directories = new List<string>();
        private ExtensionsKit Ekit = new ExtensionsKit();
        private static IEnumerable<string> Files = Directory.EnumerateFiles(Main);

        

        public Organizer()
        {
            populateDirectories();
            populateAboutFileExtensionsMenu();
            populateCustomFileExtensionsMenu();

            SetupMainMenu();
            SetupSettingsMenu();
            SetupHelpMenu();
            SetupEditMenu();

        }
        private void SetupMainMenu()
        {
            MainMenu.addMethod(organizeByDefaults, "Organize Downloads");
            MainMenu.addMethod(SettingsMenu.runMenu, "Settings");
            MainMenu.addMethod(MainMenu.exitMenuLoop, "Exit");
        }
        private void SetupSettingsMenu()
        {
            SettingsMenu.addMethod(SettingsMenu.exitMenuLoop, "Return");
            SettingsMenu.addMethod(HelpMenu.runMenu, "Help");
            SettingsMenu.addMethod(EditCustomsMenu.runMenu, "Category Editor");
        }
        private void SetupHelpMenu()
        {
            HelpMenu.addMethod(HelpMenu.exitMenuLoop, "Return");
            HelpMenu.addMethod(TutorialMenu.runMenu, "Tutorial");
            HelpMenu.addMethod(AboutFileExtensionsMenu.runMenu, "File Exensions");
            HelpMenu.addMethod(CustomFileExtensionsMenu.runMenu, "Custom Categories");
            HelpMenu.addMethod(AboutOrganizerMenu.runMenu, "About PSI. Organizer");
        }
        private void SetupEditMenu()
        {
            EditCustomsMenu.addMethod(EditCustomsMenu.exitMenuLoop, "Return");
            EditCustomsMenu.addMethod(CreateCategory, "Create new category");
            //EditCustomsMenu.addMethod( "Create New File Category");
            //EditCustomsMenu.addMethod( "Delete Custom Category");
            //EditCustomsMenu.addMethod( "View Your Custom Categories");
        }
        private bool CreateCategory()
        {

           
            Menu GetCatNameMenu = new Menu(MenuType.TextInput, "Enter name of your new file category.  Cannot begin with a symbol.");
            Menu areYouSure = new Menu(MenuType.ScrollInput, "temp");
            bool yes() { GetCatNameMenu.exitMenuLoop(); areYouSure.exitMenuLoop(); return true; };
            bool no() { GetCatNameMenu.exitMenuLoop(); areYouSure.exitMenuLoop(); CreateCategory();return true; };
                        
            areYouSure.addMethod(yes, "Yes");
            areYouSure.addMethod(no, "No");

            GetCatNameMenu.runMenu(out string newCategoryName);

            areYouSure.SetHeaderPrompt("Are You sure You want to name your category " + newCategoryName + "?");
            areYouSure.runMenu();

            ;
            Ekit.addCustomCategory(AddExtensionsToCategory(newCategoryName));
            return true;
        }
        private string[] AddExtensionsToCategory(string name)
        {
            List<string> CategoryBuilder = new List<string>();
            CategoryBuilder.Add(name);
            Menu selectExtensions = new Menu(MenuType.ScrollInput, "Select file extensions to associate with you new category.");
            // add extensions as methods, added to custom category when run
            string[] extensions = Ekit.GetExtensionsArray();

            bool temp() { return true; }

            selectExtensions.addMethod(selectExtensions.exitMenuLoop, "Complete Your Custom Category");

            foreach(string extension in extensions)
            {
                selectExtensions.addMethod(temp, extension);
            }
            selectExtensions.runMenu(out int[] selectedExtensions); // array of options on which the user hit enter???

            string[] exts = Ekit.GetExtensionsArray();
            foreach(int Ext in selectedExtensions)
            {
                CategoryBuilder.Add(exts[Ext]);
            }

            return CategoryBuilder.ToArray();
        }
        /// <summary>
        ///     need to get that categoryBuilder out somehow???
        /// </summary>
        
        public void populateDirectories()
        {
            foreach (string name in Ekit.getCategoryNames())
            {
                Directories.Add(Path.Combine(Main, name));
            }
            Directories.Add(Other);
        }
        public void populateAboutFileExtensionsMenu()
        {
            AboutFileExtensionsMenu.addMethod(AboutFileExtensionsMenu.exitMenuLoop, "Return");
            foreach(string[] category in Ekit.ExtensionCategories) // check each category
            {
                string ExtensionsColumn = "";
                bool isFirstTime = true;
                foreach(string extension in category) // add extensions to list while ignoring the category name at [0]
                {
                    if (!isFirstTime)
                    {
                        ExtensionsColumn += extension;
                        ExtensionsColumn += '\n';
                    }
                    isFirstTime = false;
                }
                Menu tMenu = new Menu(MenuType.TextBody, category[0]); // create listing from category with category listed as header
                tMenu.SetBodyText(ExtensionsColumn);
                AboutFileExtensionsMenu.addMethod(tMenu.runMenu, category[0]); // add category to menu with category name as option label
            }
        }
        public void populateCustomFileExtensionsMenu()
        {
            CustomFileExtensionsMenu.addMethod(CustomFileExtensionsMenu.exitMenuLoop, "Return");
            foreach (string[] category in Ekit.CustomCategories)
            {
                string ExtensionsColumn = "";
                bool isFirstTime = true;
                foreach (string extension in category)
                {
                    if (!isFirstTime)
                    {
                        ExtensionsColumn += extension;
                        ExtensionsColumn += '\n';
                    }
                    isFirstTime = false;
                }
                Menu tMenu = new Menu(MenuType.TextBody, category[0]);
                tMenu.SetBodyText(ExtensionsColumn);
                CustomFileExtensionsMenu.addMethod(tMenu.runMenu, category[0]);
            }
        }
        public bool organizeByDefaults()
        {
            foreach (string[] category in Ekit.ExtensionCategories)
            {
                foreach(string extension in category)
                {
                    foreach(string file in Files)
                    {
                        if(Path.GetExtension(file) == extension)
                        {
                            if(!Directory.Exists(Path.Combine(Main, category[0])))
                            {
                                Directory.CreateDirectory(Path.Combine(Main, category[0]));
                            }
                            string fileName = Path.GetFileName(file);
                            File.Move(file, Path.Combine(Main, category[0], fileName) , true);
                        }
                    }
                }
            }
           return true;
        }
    }
}
