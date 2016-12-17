using myBOOK.data;
using myBOOK.data.EntityObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBOOK.data.Interfaces
{
   public interface IRepository
    {
        Task<Users> IsUserDataCorrect(string login, string password);
        bool DoesBookExists(string bookname, string author);
        List<Books> ChooseUserBooksOfCategory(string login, UserToBook.Categories category);
        List<ReviewView> ChooseReviewForABook(string BookName,string Author);
        double ViewRatingForABook(string name, string author);
        List<Books> SearchABook(string name, string author);
        List<Books> ShowRecommendations(string login);
        bool SearchInUserToBookOfCategory(Users user, Books book, UserToBook.Categories category);
        UserToBook GetUserToBookTuple(Users user, Books book, UserToBook.Categories category);
        bool GetBookFromAddingForm(Users user, Books book, UserToBook.Categories category);
        void DeleteUserToBook(Users user, Books book, UserToBook.Categories category);
        Books ActualBooks(Books book);
        void UpdateBook(Books book);
        bool AddBookToDatabase(Books book);
        void AddOrChangeScore(Users user, Books book, int score);
        Users ActualUser(Users user);
        void Registrate(Users user);
        List<BookView> ChooseUserScoresToShow(Users user);
    }
}
