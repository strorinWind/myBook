using myBOOK.data;
using myBOOK.data.EntityObjects;
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

        private void Updatebookboxes()
        {
            var converter = new Converter();
            var repo = new Repository();
            var res = converter.ConvertToBookView(repo.ChooseUserBooksOfCategory(User.Login, UserToBook.Categories.PastRead));
            PastBookList.ItemsSource = res;

            res = converter.ConvertToBookView(repo.ChooseUserBooksOfCategory(User.Login, UserToBook.Categories.FutureRead));
            FutureBookList.ItemsSource = res;

            res = converter.ConvertToBookView(repo.ChooseUserBooksOfCategory(User.Login, UserToBook.Categories.Favourite));
            FavouriteBookList.ItemsSource = res;

            res = converter.ConvertToScore(repo.ChooseUserScores(User), User);
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
            var repo = new Repository();
            w.AddFoundBook += (b) =>
            {
                if (repo.GetBookFromAddingForm(User, b, UserToBook.Categories.PastRead))
                {
                    MessageBox.Show("Книга успешно добавлена");
                    Updatebookboxes();
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            };
            w.Show();
        }

        private void AddFavourite_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddBookToList(User);
            var repo = new Repository();
            w.AddFoundBook += (b) =>
            {
                if (repo.GetBookFromAddingForm(User, b, UserToBook.Categories.Favourite))
                {
                    MessageBox.Show("Книга успешно добавлена");
                    Updatebookboxes();
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            };
            w.Show();
        }

        private void AddFutureRead_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddBookToList(User);
            var repo = new Repository();
            w.AddFoundBook += (b) =>
            {
                if (repo.GetBookFromAddingForm(User, b, UserToBook.Categories.FutureRead))
                {
                    MessageBox.Show("Книга успешно добавлена");
                    Updatebookboxes();
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            };
            w.Show();
        }

        private void PastBookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PastBookList.SelectedItem != null)
            {
                var w = new BookInfo((Books)PastBookList.SelectedItem, User);
                w.Show();
                w.Closing += (a, a1) => { Updatebookboxes(); };
            }
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
            if (BookList.SelectedItem != null)
            {
                var b = (Books)BookList.SelectedItem;
                var lst = repo.ChooseReviewForABook(b.BookName, b.Author);
                ReviewList.ItemsSource = lst;
            }

        }

        private void DeletePastRead_Click(object sender, RoutedEventArgs e)
        {
            if (PastBookList.SelectedItem != null)
            {
                var repo = new Repository();
                repo.DeleteUserToBook(User, (Books)PastBookList.SelectedItem, UserToBook.Categories.PastRead);
                Updatebookboxes();
            }
        }

        private void DeleteFutureBook_Click(object sender, RoutedEventArgs e)
        {
            if (FutureBookList.SelectedItem != null)
            {
                var repo = new Repository();
                repo.DeleteUserToBook(User, (Books)FutureBookList.SelectedItem, UserToBook.Categories.FutureRead);
                Updatebookboxes();
            }
        }

        private void DeleteFavourite_Click(object sender, RoutedEventArgs e)
        {
            if (FavouriteBookList.SelectedItem != null)
            {
                var repo = new Repository();
                repo.DeleteUserToBook(User, (Books)FavouriteBookList.SelectedItem, UserToBook.Categories.Favourite);
                Updatebookboxes();
            }
        }

        private void MarkAsFavourite_Click(object sender, RoutedEventArgs e) //не работает, исключение выдает
        {
            /*if (PastBookList.SelectedItem != null)
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

            }*/
        }

        private void MoveToFutureBooks_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
