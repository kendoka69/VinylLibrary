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
            var fileName = Path.Combine(directory.FullName, "VinylLibrary2.csv");
          
            var albums = ReadAlbumData(fileName);
            //Console.WriteLine(fileContents);

            /*Album album = new Album();
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
            menu.Append("\nTo sort albums by artist, press 2");
            menu.Append("\nTo sort albums by title, press 3");
            menu.Append("\n----------------------------");
            menu.Append("\nEnter Q to quit");

            Console.WriteLine(menu.ToString());

            var fileContents = albums;
            var input = Console.ReadLine();
            while (input.ToLower() != "q")
            {
                switch (input)
                {
                    case "1":
                        PrintList(fileContents);
                        Console.WriteLine(fileContents);
                        Console.WriteLine(menu.ToString());

                        break;
                    //case "2":
                        

                }
                Console.ReadLine();
            }

        }

        //Read from the file
        public static List<Album> ReadAlbumData(string fileName)
        {
            var albumData = new List<Album>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    Album album = new Album();
                    string[] value = line.Split(',');

                    int parseInt;
                    if (int.TryParse(value[4], out parseInt))
                    {
                        album.YearReleased = parseInt;
                    }
                    album.ArtistName = value[1];
                    album.AlbumTitle = value[2];
                    album.Genre = value[3];
                    //album.OnLoan = value[5];
                    album.Borrower = value[6];

                    albumData.Add(album);
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
