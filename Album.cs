using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VinylLibrary
{

    public class Album
    {
        public int ID { get; private set; } = Interlocked.Increment(ref GlobalAlbumId);
        public static int GlobalAlbumId;
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public string Genre { get; set; }
        public int YearReleased { get; set; }
        public bool OnLoan { get; set; }
        public string Borrower { get; set; }
        public List<Album> Albums;

        public Album()
        {
            Albums = new List<Album>();
        }

        public override string ToString()
        {
            return ID + "," + AlbumTitle + ", " + ArtistName + ", " + Genre + ", " + YearReleased + ", " + OnLoan + ", " + Borrower;
        }
    }
}