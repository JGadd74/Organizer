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
        public Menu SettingsMenu = new Menu(MenuType.ScrollInput, "Settings");
        public Menu HelpMenu = new Menu(MenuType.ScrollInput, "Help");
        public Menu TutorialMenu = new Menu(MenuType.TextBody, "Tutorial");
        public Menu AboutFileExtensionsMenu = new Menu(MenuType.ScrollInput, "About File Extensions");
        public Menu CustomFileExtensionsMenu = new Menu(MenuType.ScrollInput, "Custom Categories");
        public Menu AboutOrganizerMenu = new Menu(MenuType.TextBody, "About PSI. Folder Organizer");

        private static string activeUser = Environment.UserName;

        private static string Main = @"C:\Users\" + activeUser + @"\Downloads";
        private static string Other = Path.Combine(Main, "Unknown type");

        public List<string> Directories = new List<string>();
        ExtensionsKit Ekit = new ExtensionsKit();
        private static IEnumerable<string> Files = Directory.EnumerateFiles(Main);

        

        public Organizer()
        {
            populateDirectories();
            populateAboutFileExtensionsMenu();
            populateCustomFileExtensionsMenu();

            MainMenu.addMethod(organizeByDefaults, "Organize Downloads");
            MainMenu.addMethod(SettingsMenu.runMenu, "Settings");
            MainMenu.addMethod(MainMenu.exitMenuLoop, "Exit");

            SettingsMenu.addMethod(SettingsMenu.exitMenuLoop, "Return");
            SettingsMenu.addMethod(HelpMenu.runMenu, "Help");

            HelpMenu.addMethod(HelpMenu.exitMenuLoop, "Return");
            HelpMenu.addMethod(TutorialMenu.runMenu, "Tutorial");
            HelpMenu.addMethod(AboutFileExtensionsMenu.runMenu, "File Exensions");
            HelpMenu.addMethod(CustomFileExtensionsMenu.runMenu, "Custom Categories");
            HelpMenu.addMethod(AboutOrganizerMenu.runMenu, "About PSI. Organizer");
        }

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
            foreach(string[] category in Ekit.ExtensionCategories)
            {
                string ExtensionsColumn = "";
                bool isFirstTime = true;
                foreach(string extension in category)
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
                AboutFileExtensionsMenu.addMethod(tMenu.runMenu, category[0]);
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
