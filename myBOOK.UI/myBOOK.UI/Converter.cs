using myBOOK.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.UI
{
    class Converter
    {
        public List<BookView> ConvertToBookView(List<Books> list)
        {
            var repo = new Repository();
            var resultList = new List<BookView>();
            for (int i = 0; i < list.Count; i++)
            {
                resultList.Add(new BookView
                {
                    BookName = list[i].BookName,
                    Author = list[i].Author,
                    Description = list[i].Description,
                    Genre = list[i].Genre,
                    LoadingLink = list[i].LoadingLink,
                    Rating = repo.ViewRatingForABook(list[i].BookName, list[i].Author)
                });
            }
            return resultList;
        }

        public List<BookView> ConvertToScore(List<Books> list,Users user)
        {
            var repo = new Repository();
            var resultList = new List<BookView>();
            for (int i = 0; i < list.Count; i++)
            {
                var b = new BookView
                {
                    BookName = list[i].BookName,
                    Author = list[i].Author,
                    Rating = repo.GetScore(user,list[i]),
                };
                resultList.Add(b);
            }
            return resultList;
        }

        public List<ReviewView> ConvertToReviewView(List<Reviews> list)
        {
            var repo = new Repository();
            var resultList = new List<ReviewView>();
            for (int i = 0; i < list.Count; i++)
            {
                var b = new ReviewView
                {
                    FullName = list[i].User.FullName,
                    //Author = list[i].Author,
                    //Rating = repo.GetScore(user, list[i]),
                    ReviewText = list[i].ReviewText
                };
                resultList.Add(b);
            }
            return resultList;
        }
    }
}
