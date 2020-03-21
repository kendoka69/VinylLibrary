using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylLibrary
{
    public class Album
    {
        public int IdNum { get; set;  }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public string Genre { get; set; }
        public int YearReleased { get; set; }
        public bool OnLoan { get; set; }
        public string Borrower { get; set; }

        /*public Album(string ArtistName, string AlbumTitle, string Genre, int YearReleased, bool OnLoan, string Borrower)
        {
            this.ArtistName = ArtistName;
        }*/

        /*public Album()
        { 
            
        }*/

        public override string ToString()
        {
            return AlbumTitle + ", " + ArtistName;
        }
    }
}

