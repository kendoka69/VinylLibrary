using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VinylLibrary
{

    public class Album
    {
        private static int _counter;
        public int ID { get; } = Interlocked.Increment(ref _counter);
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public string Genre { get; set; }
        public int YearReleased { get; set; }
        public bool OnLoan { get; set; }
        public string Borrower { get; set; }
        
        public override string ToString()
        {
            return $"{ID},{AlbumTitle},{ArtistName},{Genre},{YearReleased},{OnLoan},{Borrower}";
        }
    }
}