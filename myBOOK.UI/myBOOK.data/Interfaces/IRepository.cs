using myBOOK.data;
using myBOOK.data.EntityObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBOOK.data.Interfaces
{
   public interface IRepository
    {
        Task<Users> IsUserDataCorrect(string login, string password);
        bool IsLoginRepeated(string login);
        bool DoesBookExists(string bookname, string author);
        //List<Books> ChooseUsersFavouriteBooks(string login);
        //List<Books> ChooseUsersFutureBooks(string login);
        //List<Books> ChooseUsersPastBooks(string login);
        List<Books> ChooseUserBooksOfCategory(string login, UserToBook.Categories category);
        List<ReviewView> ChooseReviewForABook(string BookName,string Author);
        double ViewRatingForABook(string name, string author);
        List<Books> SearchABook(string name, string author);
        List<Books> ShowRecommendations(string login);
        //bool SearchInPastBooks(Users user, Books book);
        //bool SearchInFutureBooks(Users user, Books book);
        //bool SearchInFavouriteBooks(Users user, Books book);
        bool SearchInUserToBookOfCategory(Users user, Books book, UserToBook.Categories category);
        //PastReadBooks GetPastReadBooksTuple(Users user, Books book);
        UserToBook GetUserToBookTuple(Users user, Books book, UserToBook.Categories category);
        bool GetBookFromAddingForm(Users user, Books book, UserToBook.Categories category);
        void DeleteUserToBook(Users user, Books book, UserToBook.Categories category);
        Books ActualBooks(Books book);
        void UpdateBook(Books book);
    }
}
