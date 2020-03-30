using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Globalization;


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

           // Console.WriteLine(fileContents);

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
                        //Console.WriteLine(fileContents);
                        Console.WriteLine(menu.ToString());
                        break;
                    
                    //Add an album        
                    case "2":
                        Console.WriteLine("Please enter an album title: ");
                        Console.ReadLine();
                        Console.WriteLine("Please enter the artist's name: ");
                        Console.ReadLine();
                        Console.WriteLine("Please enter the album's genre: ");
                        Console.ReadLine();
                        Console.WriteLine("Please enter the year the album was released: ");
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
        public static List<Album> ReadAlbumData(string fileName)
        {
            var albumData = new List<Album>();

            using (var reader = new StreamReader(fileName))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                csv.Configuration.Delimiter = ",";
                csv.Configuration.MissingFieldFound = null;
                while (csv.Read())
                {
                    var album = csv.GetRecord<Album>();
                    albumData.Add(album);
                }
            }


            //return albumData;
            using (var reader = new StreamReader(fileName))
            {            
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    Album album = new Album();
                    string[] value = line.Split(',');
                 
                }
            }
            return albumData;
        }


        //Write to file (will need to add data values)
        private static void WriteTitanicData(List<Album> fileContents)
        {
            using(var writer = File.AppendText("UpdatedAlbumData.csv"))
            {
                writer.WriteLine("ArtistName, AlbumTitle,");
                foreach (var item in fileContents)
                {
                    writer.WriteLine(item.ArtistName + "," + item.AlbumTitle);
                }
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
