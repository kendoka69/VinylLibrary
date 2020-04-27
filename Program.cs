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
        static void Main(string[] args)
        {
            //Find the file
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "VinylLibrary.csv");
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
                        Console.WriteLine(menu.ToString());
                        break;

                    //Remove an album
                    case "3":
                    
                        Console.WriteLine("\n\r");
                        PrintList(albumsRead);
                        Console.WriteLine("\n\r");
                        Console.WriteLine("Which album would you like to\n\r" +
                                          "remove from the collection?\n\r" +
                                          "Please enter the ID number of the album: ");
                        var albumRemoveId = Convert.ToInt32(Console.ReadLine());
                        RemoveAlbum(albumRemoveId);
                        break;
                        
                    //Borrow an album
                    case "4":

                        Console.WriteLine("\n\r");
                        PrintList(albumsRead);
                        Console.WriteLine("Which album would you like to borrow?");
                        Console.WriteLine("Please enter the album name: ");
                        var albumBorrowId = Convert.ToInt32(Console.ReadLine());
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
        
        //Write added album to the file
        private static void AddAlbum(Album album, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(@filepath, true))
            {
                //writer.WriteLine(Environment.NewLine);
                writer.Write($"\n{album.ID},{album.ArtistName},{album.AlbumTitle},{album.Genre},{album.YearReleased},{album.OnLoan},{album.Borrower}");               
            }
        }

        //Remove an album from the file
        private static void RemoveAlbum(int idToRemove)
        {
           List<string> lines = new List<string>();
           string line;
           StreamReader file = new StreamReader("VinylLibrary.csv");

           while ((line = file.ReadLine()) != null)
           {
               lines.Add(line);
           }

           lines.Remove(idToRemove.ToString());
           

           //List<string> albumsRead = File.ReadAllLines(filepath).ToList();
           //string albumToRemove = albumsRead[0];
           //albumsRead.Remove(idInput.ToString());
           //File.WriteAllLines(filepath, albumsRead.ToArray());

           // File.Move(tempFile, "VinylLibrary.csv");
           //List<Album> Album = new List<Album>();
           //var item = Album.SingleOrDefault(x => x.ID == idInput);
           //if (item != null)
           //    Album.Remove(item);

           //for (int i = Album.Count - 1; i >= 0; i--)
           //{
           //    if (Album[i].ID == idInput)
           //    {
           //        Album.RemoveAt(i);
           //    }
           //}
           //using (StreamWriter writer = new StreamWriter(@"C:\Test\test.CSV", false))
           //{
           //    foreach (String ablum in album)
           //        writer.WriteLine(album);
           //}

        }

        //Borrow an album

        private static void PrintList(List<Album> albums)
        {
            foreach (var album in albums)
            {
                Console.WriteLine(album.ToString());
            }
        }
    }
}
