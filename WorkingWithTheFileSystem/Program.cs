using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithTheFileSystem
{
    class Program
    {
        static void Main()
        {
            Console.Write("Please enter a path: ");
            string path = Console.ReadLine(); 

            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

            foreach (var dir in dirs)
            {
                Console.WriteLine(dir);
            }
            Console.WriteLine(new string('-', 20));

            var files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var info = new FileInfo(file);
                Console.WriteLine($"{Path.GetFileName(file)}: {info.Length} bytes");
            }

            string newPath = path + "1";
            bool directoryExist = Directory.Exists(newPath);
            if (directoryExist)
            {
                Console.WriteLine("The directory exists"); ;
            }
            else
            {
                Directory.CreateDirectory(newPath);
            }

            files = Directory.GetFiles(path);
            string destinationFolder = newPath;
            foreach (var file in files)
            {
                File.Copy(file, $"{destinationFolder}{ Path.GetFileName(file) }", true);
            }
            destinationFolder = newPath + "\\NewFolder\\";
            Directory.CreateDirectory(destinationFolder);
            foreach (var file in files)
            {
                File.Move(file, $"{destinationFolder}{ Path.GetFileName(file) }");
            }

            var newFile = File.Create(destinationFolder + "\\New Text Document1.txt");
            using (StreamReader streamReader = new StreamReader(destinationFolder + "\\New Text Document.txt"))
            {
                using (StreamWriter streamWriter = new StreamWriter(newFile))
                {
                    streamWriter.WriteLine(streamReader.ReadToEnd());
                }
            }
            Console.ReadKey();        
        }
    }
}
