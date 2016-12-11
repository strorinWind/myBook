using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class SearchGenreForRecommendationsViewModel
    {
        public int NumberOfRepeatedGenres{ get; set; }
        public int Count{ get; set; }
        public PastReadBooks FavouriteGenre { get; set; }
    }
}
