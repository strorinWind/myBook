using myBOOK.data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBOOK.data.Interfaces
{
   public interface IRepository
    {
        Task<Users> IsUserDataCorrect(string login, string password);
        bool IsLoginRepeated(string login);
        bool DoesBookExists(string bookname, string author);
        List<Books> ChooseUsersFavouriteBooks(string login);
        List<Books> ChooseUsersFutureBooks(string login);
        List<Books> ChooseUsersPastBooks(string login);
        List<ReviewView> ChooseReviewForABook(string BookName,string Author);
        double ViewRatingForABook(string name, string author);
        List<Books> SearchABook(string name, string author);
        List<Books> ShowRecommendations(string login);
        bool SearchInPastBooks(Users user, Books book);
        bool SearchInFutureBooks(Users user, Books book);
        bool SearchInFavouriteBooks(Users user, Books book);
        PastReadBooks GetPastReadBooksTuple(Users user, Books book);

    }
}
