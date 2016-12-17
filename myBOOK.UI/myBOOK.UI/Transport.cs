using myBOOK.data;
using myBOOK.data.EntityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.UI
{
    class Transport
    {
        public void TransportToUserToBooks()
        {
            using (Context c = new Context())
            {
                /*var list = c._PastReadBooks.Select( s => new 
                {
                    User = s.User,
                    Book = s.Book,
                });
                var resultList = new List<UserToBook>();
                foreach (var item in list)
                {
                    resultList.Add(new UserToBook {
                        User = c.User.Find(item.User.Login),
                        Book = c._Book.Find(item.Book.BookName,item.Book.Author),
                        Category = UserToBook.Categories.PastRead });
                }
                c.UserToBook.AddRange(resultList);
                c.SaveChanges();
                var list = c._Favourite.Select(s => new
                {
                    User = s.User,
                    Book = s.Book,
                });
                var resultList = new List<UserToBook>();
                foreach (var item in list)
                {
                    resultList.Add(new UserToBook
                    {
                        User = c.User.Find(item.User.Login),
                        Book = c._Book.Find(item.Book.BookName, item.Book.Author),
                        Category = UserToBook.Categories.Favourite
                    });
                }
                c.UserToBook.AddRange(resultList);
                c.SaveChanges();
                */
            }
        }
    }
}
