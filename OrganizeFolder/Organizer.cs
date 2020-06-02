using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using MenuFramework;

namespace OrganizeFolder
{
    public class Organizer
    {
        // goal: organize loose files into folders based on filetype
        // ex. organize downloads folder into images, docs and videos folders etc

        // Suggestion: Let user define categories by name and extensions


        public Menu MainMenu = new Menu(MenuType.ScrollInput, "PSI. Organizer");



        public Organizer()
        {
            MainMenu.addMethod(OrganizeByDefaults, "Organize Downloads");
            MainMenu.addMethod(MainMenu.exitMenuLoop, "Exit");
        }


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

        string[] Depositories = new string[] { Videos, Images, Executables, ISOs, Compressed, PDFs, Shortcuts, Other };

        private static IEnumerable<string> Files = Directory.EnumerateFiles(Main);

        




        static bool OrganizeByDefaults()
        {
            foreach (string file in Files) // optimize, combine two if/else blocks
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
