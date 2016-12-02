using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.data
{
    public class Repository
    {
        public bool IsUserDataCorrect(string login, string password)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._User
                              where s.Login == login && s.Password == password
                              select s).ToList();

                if (result != null)
                {
                    return true;
                }
                else
                    return false;
            }

        }

        public bool IsLoginRepeated (string login)
        {
            using (Context c = new Context())
            {
                var result = (from s in c._User
                              where s.Login == login
                              select s).ToList();

                if (result != null)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public List<Favourite> ChooseUsersFavouriteBooks (string login)
        {

            using (Context c = new Context())
            {
                var result = (from s in c._Favourite
                              where s.User.Login == login
                              select s).ToList();
                return result;
            }

        }

        public List<FutureReadBooks> ChooseUsersFutureBooks(string login)
        {

            using (Context c = new Context())
            {
                var result = (from s in c._FutureReadBooks
                              where s.User.Login == login
                              select s).ToList();
                return result;
            }

        }

        public List<PastReadBooks> ChooseUsersPastBooks(string login)
        {

            using (Context c = new Context())
            {
                var result = (from s in c._PastReadBooks
                              where s.User.Login == login
                              select s).ToList();
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

        public double ViewRatingForABook(string BookName)
        {

            using (Context c = new Context())
            {
             var result= (from s in c._Score
                              where s.Book.BookName == BookName
                              select s.Value).Average();
                return result;
            }

        }



    }
}
