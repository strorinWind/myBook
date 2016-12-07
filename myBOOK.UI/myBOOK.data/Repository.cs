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

        public bool IsLoginRepeated (string login)
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

        public List<Books> ChooseUsersFavouriteBooks (string login)
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

        public double ViewRatingForABook(string name,string author)
        {
            using (Context c = new Context())
            {
             var result= (from s in c._Score
                              where s.Book.BookName == name && s.Book.Author == author
                              select s.Value).Average();
                return result;
            }
        }

        public List<Books> SearchABook (string name, string author)
        {
            using (Context c = new Context())
            {

                var result = (from s in c._Book
                             where (s.BookName==name && s.Author==author|| 
                             s.Author==author  ||
                             s.BookName == name )
                             select s).ToList();

                return result;
            }
        }

       
    }
}
