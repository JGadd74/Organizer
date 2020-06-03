using System;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using MenuFramework;

namespace OrganizeFolder
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Organizer MyOrganizer = new Organizer();
           // MyOrganizer.MainMenu.runMenu();
           foreach(string directory in MyOrganizer.Directories)
            {
                Console.WriteLine(directory);
            }
           
        }
    }
}
