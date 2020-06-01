using System;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.InteropServices;

namespace OrganizeFolder
{
    class Program
    {
        enum DepoFolder
        {
            Videos,
            Images,
            Executables,
            ISOs,
            Compressed,
            PDFs,
            Other
        }
        static void Main(string[] args)
        {
            // goal: organize loose files into folders based on filetype
            // ex. organize downloads folder into images, docs and videos folders

            // Suggestion: Let user define categories by name and extensions



            //Set to organize whatever folder the exe is in.  also make it ignore this exe

            


            string activeUser = Environment.UserName;
            string Main = @"C:\Users\" + activeUser + @"\Downloads";
            string Videos = Path.Combine(Main, "Videos");
            string Images = Path.Combine(Main, "Images");
            string Executables = Path.Combine(Main, "Executables");
            string ISOs = Path.Combine(Main, "ISOs");
            string Compressed = Path.Combine(Main, "Compressed Files");
            string PDFs = Path.Combine(Main, "PDFs");
            string Other = Path.Combine(Main, "Other");

            string[] Depositories = new string[] { Videos, Images, Executables, ISOs, Compressed, PDFs, Other };
                       
            var Files = Directory.EnumerateFiles(Downloads);



            foreach (string file in Files)
            {
                //if (!File.Exists(Path.Combine(Downloads, "Test.txt"))) File.Create(Path.Combine(Downloads, "Test.txt"));
                if (Path.GetExtension(file) == ".exe" && !Directory.Exists(Executables)) Directory.CreateDirectory(Executables);
                else if (Path.GetExtension(file) == ".mp4" || Path.GetExtension(file) == ".avi" || Path.GetExtension(file) == ".mkv" && !Directory.Exists(Videos)) Directory.CreateDirectory(Videos);
                else if (Path.GetExtension(file) == ".jpg") Directory.CreateDirectory(Images);
                else if (Path.GetExtension(file) == ".iso") Directory.CreateDirectory(ISOs);
                else if (Path.GetExtension(file) == ".7z" || Path.GetExtension(file) == ".rar" || Path.GetExtension(file) == ".zip" && !Directory.Exists(Compressed)) Directory.CreateDirectory(Compressed);
                else if (Path.GetExtension(file) == ".pdf" && !Directory.Exists(PDFs)) Directory.CreateDirectory(PDFs);
                else if (!Directory.Exists(Other)) Directory.CreateDirectory(Other);

            }



            foreach (string file in Files)
            {
                string fileName = Path.GetFileName(file);
                if(Path.GetExtension(file) == ".avi" || Path.GetExtension(file) == ".mp4" || Path.GetExtension(file) == ".mkv")
                {
                    File.Move(file, Path.Combine(Videos, fileName));
                }
                else if(Path.GetExtension(file) == ".jpg")
                {
                    File.Move(file, Path.Combine(Images, fileName));
                }
                else if (Path.GetExtension(file) == ".exe")
                {
                    File.Move(file, Path.Combine(Executables, fileName));
                }
                else if (Path.GetExtension(file) == ".iso")
                {
                    File.Move(file, Path.Combine(ISOs, fileName));
                }
                else if (Path.GetExtension(file) == ".zip" || Path.GetExtension(file) == ".rar" || Path.GetExtension(file) == ".7z")
                {
                    File.Move(file, Path.Combine(Compressed, fileName));
                }
                else
                {
                    File.Move(file, Path.Combine(Other, fileName));
                }

            }
            Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}
