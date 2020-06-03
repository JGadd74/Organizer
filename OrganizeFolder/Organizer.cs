using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using MenuFramework;
using FileExtensions;

namespace OrganizeFolder
{
    public class Organizer
    {
        // goal: organize loose files into folders based on filetype
        // ex. organize downloads folder into images, docs and videos folders etc

        // Suggestion: Let user define categories by name and extensions




        public Menu MainMenu = new Menu(MenuType.ScrollInput, "PSI. Folder Organizer");
        public Menu SettingsMenu = new Menu(MenuType.ScrollInput, "Settings");
        public Menu HelpMenu = new Menu(MenuType.ScrollInput, "Help");
        public Menu TutorialMenu = new Menu(MenuType.TextBody, "Tutorial");
        public Menu AboutFileExtensionsMenu = new Menu(MenuType.ScrollInput, "About File Extensions");
        public Menu AboutOrganizerMenu = new Menu(MenuType.TextBody, "About PSI. Folder Organizer");

        private static string activeUser = Environment.UserName;

        private static string Main = @"C:\Users\" + activeUser + @"\Downloads";
        private static string Videos = Path.Combine(Main, "Videos");
        private static string Images = Path.Combine(Main, "Images");
        private static string Executables = Path.Combine(Main, "Executables");
        private static string ISOs = Path.Combine(Main, "ISOs");
        private static string Compressed = Path.Combine(Main, "Compressed Files");
        private static string PDFs = Path.Combine(Main, "PDFs");
        private static string Shortcuts = Path.Combine(Main, "Shortcuts");
        private static string Other = Path.Combine(Main, "Other");

        public List<string> Directories = new List<string>();
        ExtensionsKit Ekit = new ExtensionsKit();
        private static IEnumerable<string> Files = Directory.EnumerateFiles(Main);



        public Organizer()
        {
            
            populateDirectories();
            populateAboutFileExtensionsMenu();

            MainMenu.addMethod(organizeByDefaultsExtended, "Organize Downloads");
            MainMenu.addMethod(SettingsMenu.runMenu, "Settings");
            MainMenu.addMethod(MainMenu.exitMenuLoop, "Exit");
            SettingsMenu.addMethod(SettingsMenu.exitMenuLoop, "Return");
            SettingsMenu.addMethod(HelpMenu.runMenu, "Help");
            HelpMenu.addMethod(HelpMenu.exitMenuLoop, "Return");
            HelpMenu.addMethod(TutorialMenu.runMenu, "Tutorial");
            HelpMenu.addMethod(AboutFileExtensionsMenu.runMenu, "File Exensions");
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

        //List<Menu> AboutFileExtensionMenus = new List<Menu>();

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
        public bool test()
        {
            return true;
        }
        /// <summary>
        /// for loop for iterater
        /// create textonly menu
        /// add text[i] to body
        /// add method to aboutfileextensions menu 
        /// </summary>
        /// <returns></returns>
        public bool organizeByDefaultsExtended()
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

       

        public bool OrganizeByDefaults()
        {
            foreach (string file in Files) 
            {
                string fileName = Path.GetFileName(file);
                if (Path.GetExtension(file) == ".avi" || Path.GetExtension(file) == ".mp4" || Path.GetExtension(file) == ".mkv")
                {
                    if (!Directory.Exists(Videos)) Directory.CreateDirectory(Videos);
                    File.Move(file, Path.Combine(Videos, fileName));
                }
                else if (Path.GetExtension(file) == ".jpg")
                {
                    if (!Directory.Exists(Images)) Directory.CreateDirectory(Images);
                    File.Move(file, Path.Combine(Images, fileName));
                }
                else if (Path.GetExtension(file) == ".exe")
                {
                    if (!Directory.Exists(Executables)) Directory.CreateDirectory(Executables);
                    File.Move(file, Path.Combine(Executables, fileName));
                }
                else if (Path.GetExtension(file) == ".iso")
                {
                    if (!Directory.Exists(ISOs)) Directory.CreateDirectory(ISOs);
                    File.Move(file, Path.Combine(ISOs, fileName));
                }
                else if (Path.GetExtension(file) == ".zip" || Path.GetExtension(file) == ".rar" || Path.GetExtension(file) == ".7z")
                {
                    if (!Directory.Exists(Compressed)) Directory.CreateDirectory(Compressed);
                    File.Move(file, Path.Combine(Compressed, fileName));
                }
                else if (Path.GetExtension(file) == ".pdf")
                {
                    if (!Directory.Exists(PDFs)) Directory.CreateDirectory(PDFs);
                    File.Move(file, Path.Combine(PDFs, fileName));
                }
                else if (Path.GetExtension(file) == ".lnk")
                {
                    if (!Directory.Exists(Shortcuts)) Directory.CreateDirectory(Shortcuts);
                    File.Move(file, Path.Combine(Shortcuts, fileName));
                }
                else
                {
                    if (!Directory.Exists(Other)) Directory.CreateDirectory(Other);
                    File.Move(file, Path.Combine(Other, fileName));
                }
            }
            return true;
        }
                   
    }
}
