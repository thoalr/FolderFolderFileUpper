/* Name:        FolderFolderFileUpper.cs ( FFFU for short )
 * Description: This program will search every subdirectory in a specified path
 *              and check if a directory only contains a subdirectory of the same 
 *              name and move the contents of the subdirectory up into the main 
 *              directory and then delete the subdirectory.
 *              Directorys can be passed as command line arguments
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFolderFileUpper
{
    class Program
    {
        const String desc = "This program will search every subdirectory in a specified path and check if " +
            "a directory only contains a subdirectory of the same name and move the contents of the " +
            "subdirectory up into the main directory and delete the subdirectory.";

        //static void fffuRecursive(string dir)
        //{
        //    string[] direct = Directory.GetDirectories(dir);

        //    foreach (string d in direct)
        //    {
        //        string[] subdirect = Directory.GetDirectories(d);
        //        if (subdirect.Length == 1)
        //        {
        //            if (d.Substring(d.LastIndexOf(@"\")) == subdirect[0].Substring(subdirect[0].LastIndexOf(@"\")))
        //            {
        //                string[] sourcefiles = Directory.GetFiles(subdirect[0]);
        //                foreach (string sourcefile in sourcefiles)
        //                {
        //                    string fileName = Path.GetFileName(sourcefile);
        //                    string destFile = Path.Combine(d, fileName);

        //                    File.Move(sourcefile, destFile);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            fffuRecursive(subdirect);
        //        }

        //    }
        //}

        static void fffuRecursive(string d)
        {
            Console.WriteLine("Checking directory = \n" + d);

            string[] subdirect = Directory.GetDirectories(d);

            Console.WriteLine("Sub directories = \n");
            foreach (string s in subdirect) Console.WriteLine(s);

            if (subdirect.Length == 1 && Directory.GetFiles(d).Length == 0 ) // only one subdirectory and no other files in parent directory
            {
                if (d.Substring(d.LastIndexOf(@"\")) == subdirect[0].Substring(subdirect[0].LastIndexOf(@"\"))) // match parent and subdirectory name
                {
                    Console.WriteLine("The directory fullfills fffu conditions\n");
                    Console.WriteLine("Moving files:");
                    string[] sourcefiles = Directory.GetFiles(subdirect[0]);
                    foreach (string sourcefile in sourcefiles)
                    {
                        Console.WriteLine(sourcefile);
                        string fileName = Path.GetFileName(sourcefile);
                        string destFile = Path.Combine(d, fileName);

                        File.Move(sourcefile, destFile);
                    }
                    Console.WriteLine("Deleting the now empty sub directory\n");
                    Directory.Delete(subdirect[0]);
                }
            }
            if ( subdirect.Length > 1)
            {
                Console.WriteLine("Checking further subdirectories recursivly\n");
                foreach( string s in subdirect )
                    fffuRecursive(s);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                for(int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine("\nCalling recuresive function to implement fffu.\n");
                    try
                    {
                        fffuRecursive(args[i]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("An exception occured.\nMake sure none files in the specified are being accessed\nby any other program.");
                    }
                }
            }
            else
            {
                Console.WriteLine(desc);
                while (true)
                {
                    Console.WriteLine("\nPut in the directory to be searched:");
                    String dir = (Console.ReadLine());

                    Console.WriteLine("\nCalling recuresive function to implement fffu.\n");
                    try
                    {
                        fffuRecursive(dir);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("An exception occured.\nMake sure none files in the specified are being accessed\nby any other program.");
                    }

                    Console.Write("\nDo you want to go again? (Y/N):");
                    string c = Console.ReadLine();
                    if (c == "N") break;
                }
            }
        }
    }
}
