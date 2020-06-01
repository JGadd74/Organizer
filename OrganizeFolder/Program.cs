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



            //Set to organize whatever folder the exe is in.  also make it ignore this exe

            


            string activeUser = Environment.UserName;
            string Downloads = @"C:\" + activeUser + @"\Jon-k\Downloads";
            string Videos = Path.Combine(Downloads, "Videos");
            string Images = Path.Combine(Downloads, "Images");
            string Executables = Path.Combine(Downloads, "Executables");
            string ISOs = Path.Combine(Downloads, "ISOs");
            string Compressed = Path.Combine(Downloads, "Compressed Files");
            string PDFs = Path.Combine(Downloads, "PDFs");
            string Other = Path.Combine(Downloads, "Other");

            string[] Depositories = new string[] { Videos, Images, Executables, ISOs, Compressed, PDFs, Other };


            foreach(string Destination in Depositories) // change this so it only creates directories for existing file types, no need for empty folders.
            {
                if (!Directory.Exists(Destination))
                {
                    Directory.CreateDirectory(Destination);
                }
            }



            var Files = Directory.EnumerateFiles(Downloads);



            foreach (string file in Files)
            {
                if (Path.GetExtension(file) == ".exe") Directory.CreateDirectory(Executables);
                else if (Path.GetExtension(file) == ".mp4" || Path.GetExtension(file) == ".avi" || Path.GetExtension(file) == ".mkv") Directory.CreateDirectory(Videos);
                else if (Path.GetExtension(file) == ".jpg") Directory.CreateDirectory(Images);
                else if (Path.GetExtension(file) == ".iso") Directory.CreateDirectory(ISOs);
                else if (Path.GetExtension(file) == ".7z" || Path.GetExtension(file) == ".rar" || Path.GetExtension(file) == ".zip") Directory.CreateDirectory(Compressed);
                else if (Path.GetExtension(file) == ".pdf") Directory.CreateDirectory(PDFs);
                else Directory.CreateDirectory(Other);

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
