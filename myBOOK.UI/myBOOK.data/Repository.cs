using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using myBOOK.data.Interfaces;
using myBOOK.data.EntityObjects;
using myBOOK.data;
//using myBOOK.UI;

namespace myBOOK.data
{
    public class Repository : IRepository
    {
        Context c = new Context();

        #region Search requests
        public bool DoesBookExists(string bookname, string author)
        {
            if (c._Book.Find(bookname, author) != null)
            {
                return true;
            }
            return false;
        }

        public bool SearchInUserToBookOfCategory(Users user, Books book, UserToBook.Categories category)
        {
            if (c.UserToBook.Any(b => b.User.Login == user.Login
                                        && b.Book.BookName == book.BookName
                                        && b.Book.Author == book.Author
                                        && b.Category == category))
            {
                return true;
            }
            return false;
        }

        public double ViewRatingForABook(string name, string author)
        {
            var result = from s in c._Score
                         where s.Book.BookName == name && s.Book.Author == author
                         select s.Value;
            if (result.Count() > 0)
            {
                return result.Average();
            }
            return 0;
        }

        public Books ActualBooks(Books book)
        {
            return c._Book.Find(book.BookName, book.Author);
        }

        public Users ActualUser(Users user)
        {
            return c.User.Find(user.Login);
        }

        public Reviews ExistsReview(Users user, Books book)
        {
            var result = c._Review.Where(s => s.User.Login == user.Login
                                        && s.Book.BookName == book.BookName
                                        && s.Book.Author == book.Author).FirstOrDefault();
            return result;
        }

        public UserToBook GetUserToBookTuple(Users user, Books book, UserToBook.Categories category)
        {
            var result = c.UserToBook.Where(b => b.User.Login == user.Login
                                                && b.Book.BookName == book.BookName
                                                && b.Book.Author == book.Author
                                                && b.Category == category)
                                                .First();
            return result;
        }

        public async Task<Users> IsUserDataCorrect(string login, string password)
        {
            Guid passwordnum = Encryption.GetHashString(password);
            return await c.User.FirstOrDefaultAsync(s => s.Login == login && s.Password == passwordnum);
        }

        public List<Books> AllBooks()
        {
            return c._Book.ToList();
        }

        public List<ReviewView> ChooseReviewForABook(string BookName, string author)
        {
            var list = (from s in c._Review
                        where s.Book.BookName == BookName && s.Book.Author == author
                        select new ReviewView
                        {
                            FullName = s.User.FullName,
                            ReviewText = s.ReviewText
                        }).ToList();
            return list;
        }

        public List<Books> ChooseUserBooksOfCategory(string login, UserToBook.Categories category)
        {
            var result = (from s in c.UserToBook
                          where s.User.Login == login
                          && s.Category == category
                          select s.Book).ToList();
            return result;
        }

        public List<BookView> ChooseUserScoresToShow(Users user)
        {
            var resultList = new List<BookView>();
            var list = c._Score.Where(s => s.User.Login == user.Login)
                             .Select(s => s.Book).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var book = list[i];
                var score = c._Score.Where(s => s.User.Login == user.Login
                            && s.Book.Author == book.Author
                            && s.Book.BookName == book.BookName).First().Value;
                var b = new BookView
                {
                    BookName = list[i].BookName,
                    Author = list[i].Author,
                    Rating = score,
                };
                resultList.Add(b);
            }
            return resultList;
        }

        public List<Books> SearchABook(string name, string author)
        {
            var result = (from s in c._Book
                          where (s.BookName == name && s.Author == author ||
                          s.Author == author && name == "" ||
                          s.BookName == name && author == "")
                          select s).ToList();
            return result;
        }

        public List<Books> ShowRecommendations(string login)
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
        #endregion


        #region Adding and spdating requests
        public void AddOrChangeReview(Users user, Books book, string reviewText)
        {
            var result = c._Review.Where(s => s.User.Login == user.Login
                                        && s.Book.Author == book.Author
                                        && s.Book.BookName == book.BookName);
            if (result.Count() == 0)
            {
                var review = new Reviews
                {
                    User = c.User.Find(user.Login),
                    Book = c._Book.Find(book.BookName, book.Author),
                    ReviewText = reviewText
                };
                c._Review.Add(review);
            }
            else
            {
                c._Review.Find(result.First().ID).ReviewText = reviewText;
            }
            c.SaveChanges();
        }

        public void AddOrChangeScore(Users user, Books book, int score)
        {
            var result = c._Score.Where(s => s.User.Login == user.Login
                                        && s.Book.Author == book.Author
                                        && s.Book.BookName == book.BookName);
            if (result.Count() == 0)
            {
                Score Bookscore = new Score
                {
                    Book = c._Book.Find(book.BookName, book.Author),
                    User = c.User.Find(user.Login),
                    Value = score
                };
                c._Score.Add(Bookscore);
            }
            else
            {
                c._Score.Find(result.First().ID).Value = score;
            }
            c.SaveChanges();
        }

        public void Registrate(Users user)
        {
            var IsLoginRepeated = (from s in c.User
                                   where s.Login == user.Login
                                   select s).FirstOrDefault();
            if (IsLoginRepeated != null)
            {
                throw new ArgumentException("Этот логин уже используется");
            }
            else
            {
                c.User.Add(user);
                c.SaveChanges();
            }
        }

        public void UpdateBook(Books book)
        {
            var b = c._Book.Find(book.BookName, book.Author);
            b.Description = book.Description;
            b.Genre = book.Genre;
            b.LoadingLink = book.LoadingLink;
            c.SaveChanges();
        }

        public bool AddBookToDatabase(Books book)
        {
            if (!DoesBookExists(book.BookName, book.Author))
            {
                c._Book.Add(book);
                c.SaveChanges();
                return true;
            }
            return false;
        }

        public bool GetBookFromAddingForm(Users user, Books book, UserToBook.Categories category)
        {
            if (!SearchInUserToBookOfCategory(user, book, category))
            {
                var b = new UserToBook
                {
                    User = c.User.Find(user.Login),
                    Book = c._Book.Find(book.BookName, book.Author),
                    Category = category
                };
                c.UserToBook.Add(b);
                c.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        public void DeleteUserToBook(Users user, Books book, UserToBook.Categories category)
        {
            var bookToDelete = GetUserToBookTuple(user, book, category);
            c.Entry(bookToDelete).State = EntityState.Deleted;
            c.SaveChanges();
        }
    }
}
