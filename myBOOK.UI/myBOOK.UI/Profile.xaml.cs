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
                    PastBookList.ItemsSource = repo.ChooseUsersPastBooks(User.Login);
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
                    FutureBookList.ItemsSource = repo.ChooseUsersPastBooks(User.Login);
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
                    FavouriteBookList.ItemsSource = repo.ChooseUsersPastBooks(User.Login);
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            }
        }

        public Profile(Users user)
        {
            InitializeComponent();
            User = user;
            var repo = new Repository();
            var pastbooks = repo.ChooseUsersPastBooks(user.Login);
            for (int i = 0; i < pastbooks.Count; i++)
            {
                pastbooks[i] = new BookView
                {
                    BookName = pastbooks[i].BookName,
                    Author = pastbooks[i].Author,
                    Description = pastbooks[i].Description,
                    Genre = pastbooks[i].Genre,
                    LoadingLink = pastbooks[i].LoadingLink,
                    Rating = repo.ViewRatingForABook(pastbooks[i].BookName, pastbooks[i].Author)
                };
            }
            PastBookList.ItemsSource = pastbooks;
            FutureBookList.ItemsSource = repo.ChooseUsersFutureBooks(user.Login);
            FavouriteBookList.ItemsSource = repo.ChooseUsersFavouriteBooks(user.Login);
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
            if (PastBookList.SelectedItem != null)
            {
                using (Context c = new Context())
                {
                    var BookToDelete = (Books)PastBookList.SelectedItem;
                    var repo = new Repository();
                    var pastBookToDelete = repo.GetPastReadBooksTuple(User,BookToDelete);
                    c.Entry(pastBookToDelete).State = EntityState.Deleted;
                    c.SaveChanges();
                    PastBookList.ItemsSource = repo.ChooseUsersPastBooks(User.Login);
                }
            }
        }
    }
}
