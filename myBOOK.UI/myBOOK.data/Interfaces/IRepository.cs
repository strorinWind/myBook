using myBOOK.data;
using myBOOK.data.EntityObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBOOK.data.Interfaces
{
   public interface IRepository
    {
        //Searching requests
        bool DoesBookExists(string bookname, string author);
        bool SearchInUserToBookOfCategory(Users user, Books book, UserToBook.Categories category);
        double ViewRatingForABook(string name, string author);

        Books ActualBooks(Books book);
        Users ActualUser(Users user);
        Reviews ExistsReview(Users user, Books book);
        UserToBook GetUserToBookTuple(Users user, Books book, UserToBook.Categories category);
        Task<Users> IsUserDataCorrect(string login, string password);

        List<Books> AllBooks();
        List<ReviewView> ChooseReviewForABook(string BookName, string Author);
        List<Books> ChooseUserBooksOfCategory(string login, UserToBook.Categories category);
        List<BookView> ChooseUserScoresToShow(Users user);
        List<Books> SearchABook(string name, string author);

        //Search with logic
        List<Books> ShowRecommendations(string login);

        //Updating requests
        void AddOrChangeReview(Users user, Books book, string reviewText);
        void AddOrChangeScore(Users user, Books book, int score);
        void Registrate(Users user);
        void UpdateBook(Books book);
        bool AddBookToDatabase(Books book);
        bool GetBookFromAddingForm(Users user, Books book, UserToBook.Categories category);

        //Deleting requests
        void DeleteUserToBook(Users user, Books book, UserToBook.Categories category);
    }
}
