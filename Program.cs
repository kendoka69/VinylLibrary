using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CsvHelper;
using System.Globalization;


namespace VinylLibrary
{

    class Program
    {
        
        private static List<Album> _albumsRead;
        static void Main(string[] args)
        {
            //Find the file
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "VinylLibrary.csv");
            _albumsRead = ReadAlbumData(fileName);

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
                        PrintList(_albumsRead);
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
                        _albumsRead = ReadAlbumData(fileName);
                        Console.WriteLine("\n\r");
                        PrintList(_albumsRead);
                        Console.WriteLine(menu.ToString());
                        break;

                    //Remove an album
                    case "3":
                        Console.WriteLine("\n\r");
                        PrintList(_albumsRead);
                        Console.WriteLine("\n\r");
                        Console.WriteLine("Which album would you like to\n\r" +
                                          "remove from the collection?\n\r" +
                                          "Please enter the name of the album: ");
                        var titleToRemove = Console.ReadLine();
                        RemoveAlbum(titleToRemove, "VinylLibrary.csv");
                        _albumsRead = ReadAlbumData(fileName);
                        Console.WriteLine("\n\r");
                        PrintList(_albumsRead);
                        Console.WriteLine(menu.ToString());
                        break;

                    //Borrow an album
                    case "4":
                        Console.WriteLine("\n\r");
                        PrintList(_albumsRead);
                        Console.WriteLine("\n\r");
                        Console.WriteLine("Which album would you like to borrow?\n\r" +
                                          "Please enter the album name: ");
                        var titleToBorrow = Console.ReadLine();
                        Console.WriteLine("Who is borrowing this album?\n\r" +
                                          "Please enter a first name: ");
                        var nameOfBorrower = Console.ReadLine();
                        BorrowAlbum(titleToBorrow, nameOfBorrower, "VinylLibrary.csv");
                        _albumsRead = ReadAlbumData(fileName);
                        Console.WriteLine("\n\r");
                        PrintList(_albumsRead);
                        Console.WriteLine(menu.ToString());
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
                writer.Write(
                    $"\n{album.AlbumTitle},{album.ArtistName},{album.Genre},{album.YearReleased},{album.OnLoan},{album.Borrower}");
            }

        }


        //Remove an album 
        public static void RemoveAlbum(string albumTitle, string filepath)
        {
            _albumsRead = ReadAlbumData(filepath);
            using (var writer = new StreamWriter(filepath, false))
            {
                writer.WriteLine("AlbumTitle,ArtistName,Genre,YearReleased,OnLoan,Borrower");
                foreach (var album in _albumsRead)
                {
                    if (album.AlbumTitle != albumTitle)
                    {
                        //Can be written either way:
                        //writer.WriteLine(item.ToString()); or
                        writer.WriteLine(album);
                        
                    }
                    
                }

            }

        }


        //Borrow an album
        public static void BorrowAlbum(string albumTitle, string borrower, string filepath)
        {
            _albumsRead = ReadAlbumData(filepath);

            using (var writer = new StreamWriter(filepath, false))
            {
                writer.WriteLine("AlbumTitle,ArtistName,Genre,YearReleased,OnLoan,Borrower");
                foreach (var album in _albumsRead)
                {
                    if (album.AlbumTitle == albumTitle)
                    {
                        album.OnLoan = true;
                        album.Borrower = borrower;
                    }
                    
                    writer.WriteLine(album);
                }

            }

        }

        //Write album to 
        static void PrintList(List<Album> albums)
        {
            foreach (var album in albums)
            {
                //Can be written either way:
                //Console.WriteLine(album.ToString()); or
                Console.WriteLine(album);
                   
            }

        }

    }

}
