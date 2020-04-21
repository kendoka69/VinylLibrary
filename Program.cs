using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Runtime.CompilerServices;

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
            var albums = ReadAlbumData(fileName);

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
                        PrintList(albums);
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
                        //Console.ReadLine();

                        AddAlbum(album, "VinylLibrary.csv");
                        albums = ReadAlbumData(fileName);
                        Console.WriteLine(menu.ToString());
                        break;

                    //Remove an album,
                    case "3":
                        Console.WriteLine("Which album would you like to\n\rremove from the collection?");
                        Console.WriteLine("Please enter the album name: ");
                        break;

                    //Borrow an album
                    case "4":
                        Console.WriteLine("Which album would you like to borrow?");
                        Console.WriteLine("Please enter the album name: ");
                        break;
                }

                input = Console.ReadLine();
            }

        }

        //Read from the file
        public static List<Album> ReadAlbumData(string filepath)
        {
            var albumData = new List<Album>();

            //return albumData;
            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ",";
                csv.Configuration.MissingFieldFound = null;

                while (csv.Read())
                {
                    //string line = "";
                    var album = csv.GetRecord<Album>();

                    albumData.Add(album);
                }
            }

            return albumData;

        }

        private static void AddAlbum(Album album, string filepath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "VinylLibrary.csv");

            using (StreamWriter writer = new StreamWriter(@filepath, true))
            {

                writer.WriteLine(album.ArtistName + "," + album.AlbumTitle + "," + album.Genre + "," + album.YearReleased.ToString() + "," + album.OnLoan.ToString() + "," + album.Borrower);
            }

        }

        private static void PrintList(List<Album> albums)
        {
            foreach (var album in albums)
            {
                Console.WriteLine(album.ToString());
            }
        }


    }
}
