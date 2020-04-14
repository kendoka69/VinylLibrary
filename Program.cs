using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            //Console.WriteLine(fileName);

            /* Album album = new Album();
             album.AlbumTitle = "So";
             album.ArtistName = "Peter Gabriel";
             album.Genre = "progressive pop";
             album.YearReleased = 1986;
             album.OnLoan = false;
             album.Borrower = "";
             Console.WriteLine(album.AlbumTitle);*/

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

            var fileContents = albums;
            var input = Console.ReadLine();
            while (input.ToLower() != "q")
            {
                switch (input)
                {

                    //Retrieve entire collection
                    case "1":
                        PrintList(fileContents);
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
                        Console.WriteLine(menu.ToString());
                        Console.ReadLine();
                        break;

                    //Remove an album,
                    case "3":
                        Console.WriteLine("Which album would you like to\n\rremove from the collection?");
                        Console.WriteLine("Please enter the album name: ");
                        Console.ReadLine();
                        break;

                    //Borrow an album
                    case "4":
                        Console.WriteLine("Which album would you like to borrow?");
                        Console.WriteLine("Please enter the album name: ");
                        Console.ReadLine();
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
            {
                string headerLine = reader.ReadLine();
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    Album album = new Album();
                    string[] value = line.Split(',');
                    int parseInt;
                    bool parseBool;
                    
                    album.AlbumTitle = value[0];
                    album.ArtistName = value[1]; 
                    album.Genre = value[2];

                    if (int.TryParse(value[3], out parseInt))
                    {
                        album.YearReleased = parseInt;
                    }
                    if (bool.TryParse(value[4], out parseBool))
                    {
                        album.OnLoan = parseBool;
                    }

                    album.Borrower = value[5];
                    
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
