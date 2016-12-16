using myBOOK.data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace myBOOK.UI
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Users User { get; set; }

        private void TabItemSizeRegulation()
        {
            foreach (var item in tabcontrol.Items)
            {
                var i = (TabItem)item;
                i.Width = window.Width / tabcontrol.Items.Count - 5;
            }
        }

        private void GetPastBookFromAddingForm(Books book)
        {
            using (Context c = new Context())
            {
                var repo = new Repository();
                if (!repo.SearchInPastBooks(User, book))
                {
                    var b = new PastReadBooks
                    {
                        User = c.User.Find(User.Login),
                        Book = c._Book.Find(book.BookName, book.Author),
                    };
                    c._PastReadBooks.Add(b);
                    c.SaveChanges();
                    MessageBox.Show("Книга успешно добавлена");
                    Updatebookboxes();
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            }
        }

        private void GetFutureBookFromAddingForm(Books book)
        {
            using (Context c = new Context())
            {
                var repo = new Repository();
                if (!repo.SearchInFutureBooks(User, book))
                {
                    var b = new FutureReadBooks
                    {
                        User = c.User.Find(User.Login),
                        Book = c._Book.Find(book.BookName, book.Author),
                    };
                    c._FutureReadBooks.Add(b);
                    c.SaveChanges();
                    MessageBox.Show("Книга успешно добавлена");
                    Updatebookboxes();
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            }
        }

        private void GetFavouriteBookFromAddingForm(Books book)
        {
            using (Context c = new Context())
            {
                var repo = new Repository();
                if (!repo.SearchInFavouriteBooks(User, book))
                {
                    var b = new Favourite
                    {
                        User = c.User.Find(User.Login),
                        Book = c._Book.Find(book.BookName, book.Author),
                    };
                    c._Favourite.Add(b);
                    c.SaveChanges();
                    MessageBox.Show("Книга успешно добавлена");
                    Updatebookboxes();
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            }
        }

        private void Updatebookboxes()
        {
            var converter = new Converter();
            var repo = new Repository();
            var res = converter.ConvertToBookView(repo.ChooseUsersPastBooks(User.Login));
            PastBookList.ItemsSource = res;

            res = converter.ConvertToBookView(repo.ChooseUsersFutureBooks(User.Login));
            FutureBookList.ItemsSource = res;

            res = converter.ConvertToBookView(repo.ChooseUsersFavouriteBooks(User.Login));
            FavouriteBookList.ItemsSource = res;

            res = converter.ConvertToScore(repo.ChooseUserScores(User),User);
            ScoreList.ItemsSource = res;

            res = converter.ConvertToBookView(repo.ShowRecommendations(User.Login));
            RecomendationList.ItemsSource = res;
        }

        public Profile(Users user)
        {
            InitializeComponent();
            User = user;
            Updatebookboxes();
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TabItemSizeRegulation();
        }

        private void AddPastRead_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddBookToList(User);
            w.AddFoundBook += GetPastBookFromAddingForm;
            w.Show();
        }

        private void AddFavourite_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddBookToList(User);
            w.AddFoundBook += GetFavouriteBookFromAddingForm;
            w.Show();
        }

        private void AddFutureRead_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddBookToList(User);
            w.AddFoundBook += GetFutureBookFromAddingForm;
            w.Show();
        }

        private void DeletePastRead_Click(object sender, RoutedEventArgs e)
        {
            //((TabItem)tabcontrol.SelectedItem).Header
            if (PastBookList.SelectedItem != null)
            {
                using (Context c = new Context())
                {
                    var BookToDelete = (Books)PastBookList.SelectedItem;
                    var repo = new Repository();
                    var pastBookToDelete = repo.GetPastReadBooksTuple(User,BookToDelete);
                    c.Entry(pastBookToDelete).State = EntityState.Deleted;
                    c.SaveChanges();
                    Updatebookboxes();
                }
            }
        }

        private void PastBookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PastBookList.SelectedItem != null)
            {
                var w = new BookInfo((Books)PastBookList.SelectedItem,User);
                w.Show();
                w.Closing += (a, a1) => { Updatebookboxes(); };
            }
        }

        private void FavouriteBookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var a = author.Text;
            var b = bookname.Text;
            var repo = new Repository();
            BookList.ItemsSource = repo.SearchABook(b, a);
        }

        private void BookList_Selected(object sender, RoutedEventArgs e)
        {
            var repo = new Repository();
            //var converter = new Converter();
            if (BookList.SelectedItem != null)
            {
                var b = (Books)BookList.SelectedItem;
                var lst = repo.ChooseReviewForABook(b.BookName, b.Author);
                ReviewList.ItemsSource = lst;
            }

        }

        private void DeleteFutureBook_Click(object sender, RoutedEventArgs e)
        {
            if (FutureBookList.SelectedItem != null)
            {
                using (Context c = new Context())
                {
                    var BookToDelete = (Books)FutureBookList.SelectedItem;
                    var repo = new Repository();
                    var futureBookToDelete = repo.GetFutureReadBooksTuple(User, BookToDelete);
                    c.Entry(futureBookToDelete).State = EntityState.Deleted;
                    c.SaveChanges();
                    Updatebookboxes();
                }
            }
        }

        private void DeleteFavourite_Click(object sender, RoutedEventArgs e)
        {
            if (FavouriteBookList.SelectedItem != null)
            {
                using (Context c = new Context())
                {
                    var BookToDelete = (Books)FavouriteBookList.SelectedItem;
                    var repo = new Repository();
                    var favouriteBookToDelete = repo.GetFavouriteBooksTuple(User, BookToDelete);
                    c.Entry(favouriteBookToDelete).State = EntityState.Deleted;
                    c.SaveChanges();
                    Updatebookboxes();
                }
            }
        }

       /*private void MarkAsFavourite_Click(object sender, RoutedEventArgs e) //не работает, исключение выдает
        {
            if (PastBookList.SelectedItem != null)
            {
                using (Context c = new Context())
                {
                    var BookToMark = (Books)PastBookList.SelectedItem;
                    var b = new Favourite
                    {
                        Book = BookToMark,
                        User=User
                    };
                    c._Favourite.Add(b);
                    c.SaveChanges();
                    MessageBox.Show("Книга успешно добавлена");
                    Updatebookboxes();

                }

            }
        }
        */
        private void MoveToFutureBooks_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
