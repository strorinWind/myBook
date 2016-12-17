using myBOOK.data.EntityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class Recomendations
    {
        public static List<Books> Show(string login)
        {
            using (Context c = new Context())
            {
                var GenreList = new List<Books.Genres>();
                foreach (var item in Enum.GetValues(typeof(UserToBook.Categories)))
                {
                    var count_genre = from s in c.UserToBook
                                      where s.User.Login == login
                                      && s.Category == (UserToBook.Categories)item
                                      group s by s.Book.Genre into g
                                      select new
                                      {
                                          Count = g.Count(),
                                          FavouriteGenre = g.Key
                                      };
                    GenreList.Add((from s in count_genre
                                   where s.Count == count_genre.Max(p => p.Count)
                                   select s.FavouriteGenre).FirstOrDefault());
                }
                GenreList = GenreList.Distinct().ToList();
                var list_of_books = new List<Books>();
                foreach (var item in GenreList)
                {
                    list_of_books.AddRange((from s in c._Book
                                            where s.Genre == item
                                            select s).ToList());
                }
                Books b;
                for (int i = 0; i < list_of_books.Count(); i++)
                {
                    b = list_of_books[i];
                    foreach (var item in Enum.GetValues(typeof(UserToBook.Categories)))
                    {
                        if (c.UserToBook.Any(s => s.Book.BookName == b.BookName
                                                  && s.Book.Author == b.Author
                                                  && s.Category == (UserToBook.Categories)item
                                                  && s.User.Login == login))
                        {
                            list_of_books.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
                Random ran = new Random();
                List<Books> RandomRecommentations = new List<Books>();
                for (int i = 0; i < list_of_books.Count; i++)
                {
                    int index = ran.Next(0, list_of_books.Count);
                    RandomRecommentations.Add(list_of_books[index]);
                    list_of_books.RemoveAt(index);
                    i--;
                    if (RandomRecommentations.Count == 7)
                    {
                        break;
                    }
                }
                return RandomRecommentations;
            }
        }
    }
}
