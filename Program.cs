using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace VinylLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //Find the file
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "VinylLibrary.csv");
            var tempPath = Path.Combine(directory.FullName, "tempfile.csv");
            var albumsRead = ReadAlbumData(fileName);

            StringBuilder menu = new StringBuilder();
            menu.Append("\n");
            menu.Append("\n");
            menu.Append("\nWelcome to the Vinyl Library");
            menu.Append("\n----------------------------");
            menu.Append("\nTo see the entire collection, enter 1.");
            menu.Append("\nTo add an album, enter 2");
            menu.Append("\nTo remove an album, enter 3");
            menu.Append("\nTo check out an album, enter 4");
            menu.Append("\n----------------------------");
            menu.Append("\nEnter Q to quit");

            Console.WriteLine(menu.ToString());

            var input = Console.ReadLine();
            while (input.ToLower() != "q")
            {
                switch (input)
                {

                    //Retrieve entire collection
                    case "1":
                        PrintList(albumsRead);
                        Console.WriteLine(menu.ToString());
                        break;

                    //Add an album        
                    case "2":
                        var album = new Album();
                        Console.WriteLine("Please enter an album title: ");
                        album.AlbumTitle = Console.ReadLine();
                        Console.WriteLine("Please enter the artist's name: ");
                        album.ArtistName = Console.ReadLine();
                        Console.WriteLine("Please enter the album's genre: ");
                        album.Genre = Console.ReadLine();
                        Console.WriteLine("Please enter the year the album was released: ");
                        album.YearReleased = Convert.ToInt32(Console.ReadLine());
                        AddAlbum(album, "VinylLibrary.csv");
                        ReadAlbumData(fileName);
                        PrintList(albumsRead);
                        Console.WriteLine(menu.ToString());
                        break;

                    //Remove an album
                    case "3":
                        
                        Console.WriteLine("\n\r");
                        PrintList(albumsRead);
                        Console.WriteLine("\n\r");
                        Console.WriteLine("Which album would you like to\n\r" +
                                          "remove from the collection?\n\r" +
                                          "Please enter the name of the album: ");
                        var titleToRemove = Console.ReadLine();
                        //Console.WriteLine(string.Join(",", FindAlbum(albumTitle, "VinylLibrary.csv", 1)));
                        RemoveAlbum(titleToRemove, "VinylLibrary.csv");
                        Console.ReadLine();
                        
                        break;

                    //Borrow an album
                    case "4":

                        Console.WriteLine("\n\r");
                        PrintList(albumsRead);
                        Console.WriteLine("Which album would you like to borrow?");
                        Console.WriteLine("Please enter the album name: ");
                        var albumToBorrow = Console.ReadLine();
                        break;
                }

                input = Console.ReadLine();
            }

        }

        //Read from the file
        public static List<Album> ReadAlbumData(string filepath)
        {
            var albumData = new List<Album>();

            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                albumData = csv.GetRecords<Album>().ToList();
            }

            return albumData;
        }


        //Add album to file
        private static void AddAlbum(Album album, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(@filepath, true))
            {
                //writer.WriteLine(Environment.NewLine);
                writer.Write(
                    $"\n{album.ArtistName},{album.AlbumTitle},{album.Genre},{album.YearReleased},{album.OnLoan},{album.Borrower}");
            }
        }


        //Remove an album 
        private static void RemoveAlbum(string albumTitle, string filepath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            string tempPath = Path.Combine(directory.FullName, "tempfile.csv");

            var albumsRead = ReadAlbumData(filepath);
            using (var writer = new StreamWriter(File.OpenWrite(tempPath)))
            {
                foreach (var item in albumsRead)
                {
                    if (item.AlbumTitle != albumTitle)
                    {
                        File.Delete(filepath);
                        File.Move(tempPath, filepath);
                    }
                }

            }

            //if (File.Exists(tempPath))
            //{
            //    File.Delete(filepath);
            //    File.Move(tempPath, filepath);
            //}

        }

        //Borrow an album

        //Write album to list
        static void PrintList(List<Album> albums)
        {
            foreach (var album in albums)
            {
                Console.WriteLine(album.ToString());
            }
        }


        ////Write added album to the file
        //private static void AddAlbum(string ArtistName, string AlbumTitle, string Genre, int YearReleased, bool OnLoan,
        //    string Borrower, string filepath)
        //{
        //    using (StreamWriter writer = new StreamWriter(@filepath, true))
        //    {
        //        //writer.WriteLine(Environment.NewLine);
        //        writer.Write($"\n{ArtistName},{AlbumTitle},{Genre},{YearReleased},{OnLoan},{Borrower}");
        //    }
        //}



        ////Searches for an album
        //public static string[] FindAlbum(string searchTerm, string filepath, int positionOfSearchTerm)
        //{
        //    positionOfSearchTerm--;
        //    string[] albumNotFound = { "Album not found" };

        //    try
        //    {
        //        string[] lines = File.ReadAllLines(@filepath);
        //        for (int i = 0; i < lines.Length; i++)
        //        {
        //            string[] fields = lines[i].Split(',');
        //            if (AlbumFound(searchTerm, fields, positionOfSearchTerm))
        //            {
        //                Console.Write(("Album found: "));
        //                return fields;
        //            }
        //        }

        //        return albumNotFound;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error occurred");
        //        return albumNotFound;
        //        throw new ApplicationException("Error occurred", ex);
        //    }
        //}



        ////Determines if album found
        //public static bool AlbumFound(string searchTerm, string[] album, int positionOfSearchTerm)
        //{
        //    if (album[positionOfSearchTerm].Equals(searchTerm))
        //    {
        //        return true;
        //    }

        //    return false;
        //}



        ////Remove an album from the file
        //public static void RemoveAlbum(string searchTerm, string filepath, int positionOfSearchTerm)
        //{
        //    //positionOfSearchTerm--;
        //    string tempFile = "temp.txt";
        //    bool deleted = false;

        //    try
        //    {

        //        string[] lines = File.ReadAllLines(@filepath);
        //        for (int i = 0; i < lines.Length; i++)
        //        {
        //            string[] fields = lines[i].Split(',');

        //            if (!(AlbumFound(searchTerm, fields, positionOfSearchTerm)) || deleted)
        //            {
        //                AddAlbum(fields[0],
        //                    fields[1],
        //                    fields[2],
        //                    Convert.ToInt32(fields[3]),
        //                    Convert.ToBoolean(fields[4]),
        //                    fields[5],
        //                    @tempFile);
        //            }
        //            else
        //            {
        //                deleted = true;
        //                Console.WriteLine("Album is deleted");
        //            }
        //        }

        //        File.Delete(@filepath);
        //        File.Move(tempFile, filepath);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
    }
}
