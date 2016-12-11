using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace myBOOK.data
{
    public class Repository
    {
        public Users IsUserDataCorrect(string login, string password)
        {
            using (Context c = new Context())
            {
                Guid passwordnum = Encryption.GetHashString(password);
                var result = from s in c.User
                             where s.Login == login && s.Password == passwordnum
                             select s;

                if (result.Count() > 0)
                {
                    return result.First();
                }
                else
                    return null;
            }
        }

        public bool IsLoginRepeated(string login)
        {
            using (Context c = new Context())
            {
                var result = from s in c.User
                             where s.Login == login
                             select s;

                if (result.Count() == 0)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public List<Books> ChooseUsersFavouriteBooks(string login)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._Favourite
                              where s.User.Login == login
                              select s.Book).ToList();
                return result;
            }
        }

        public List<Books> ChooseUsersFutureBooks(string login)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._FutureReadBooks
                              where s.User.Login == login
                              select s.Book).ToList();
                return result;
            }
        }

        public List<Books> ChooseUsersPastBooks(string login)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._PastReadBooks
                              where s.User.Login == login
                              select s.Book).ToList();
                return result;
            }
        }

        public List<Reviews> ChooseReviewForABook(string BookName)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._Review
                              where s.Book.BookName == BookName
                              select s).ToList();
                return result;
            }
        }

        public double ViewRatingForABook(string name, string author)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._Score
                              where s.Book.BookName == name && s.Book.Author == author
                              select s.Value).Average();
                return result;
            }
        }

        public List<Books> SearchABook(string name, string author)
        {
            using (Context c = new Context())
            {

                var result = (from s in c._Book
                              where (s.BookName == name && s.Author == author ||
                              s.Author == author && name == "" ||
                              s.BookName == name && author == "")
                              select s).ToList();

                return result;
            }
        }

        public List<Books> ShowRecommendations(string login)
        {
            using (Context c = new Context())
            {
                var count_genres = (from s in c._PastReadBooks
                                    where s.User.Login == login
                                    group s by s.Book.Genre into g
                                    select new
                                    {
                                        Count = g.Count(),
                                        FavouriteGenre = g.Key
                                    });
                var favourite_genre = from s in count_genres
                                      where s.Count == count_genres.Max(p => p.Count)
                                      select s.FavouriteGenre;

                var list_of_books = (from s in c._Book
                                     where s.Genre == favourite_genre.First()
                                     select s).ToList();

                for (int i = 0; i < list_of_books.Count(); i++)
                {
                    if (c._PastReadBooks.Any(s => s.Book.BookName == list_of_books[i].BookName 
                                                   && s.Book.Author==list_of_books[i].Author))
                    {
                        list_of_books.RemoveAt(i);
                        i--;
                    }
                }
                return list_of_books;
            }
        }

        public bool SearchInPastBooks(Users user, Books book)
        {
            using (Context c = new Context())
            {
                if (c._PastReadBooks.Any(b => b.User.Login==user.Login 
                                            && b.Book.BookName == book.BookName 
                                            && b.Book.Author==book.Author))
                {
                    return true;
                }
                return false;
            }
        }

        public bool SearchInFutureBooks(Users user, Books book)
        {
            using (Context c = new Context())
            {
                if (c._FutureReadBooks.Any(b => b.User.Login == user.Login
                                            && b.Book.BookName == book.BookName
                                            && b.Book.Author == book.Author))
                {
                    return true;
                }
                return false;
            }
        }

        public bool SearchInFavouriteBooks(Users user, Books book)
        {
            using (Context c = new Context())
            {
                if (c._Favourite.Any(b => b.User.Login == user.Login
                                            && b.Book.BookName == book.BookName
                                            && b.Book.Author == book.Author))
                {
                    return true;
                }
                return false;
            }
        }

        public PastReadBooks GetPastReadBooksTuple(Users user, Books book)
        {
            using (Context c = new Context())
            {
                var result = c._PastReadBooks.Where(b => b.User.Login == user.Login
                                                    && b.Book.BookName == book.BookName
                                                    && b.Book.Author == book.Author)
                                                    .First();
                return result;
            }
        }
    }
}
