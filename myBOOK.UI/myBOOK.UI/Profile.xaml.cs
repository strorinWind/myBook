using myBOOK.data;
using myBOOK.data.EntityObjects;
using myBOOK.data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
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
        IRepository repo = Factory.Default.GetRepository();
        Converter converter = new Converter();

        private void TabItemSizeRegulation()
        {
            foreach (var item in tabcontrol.Items)
            {
                var i = (TabItem)item;
                i.Width = window.Width / tabcontrol.Items.Count - 5;
            }
        }

        private async void Updatebookboxes()
        {
            var converter = new Converter();
            var res = converter.ConvertToBookView(repo.ChooseUserBooksOfCategory(User.Login, UserToBook.Categories.PastRead));
            PastBookList.ItemsSource = res;

            res = converter.ConvertToBookView(repo.ChooseUserBooksOfCategory(User.Login, UserToBook.Categories.FutureRead));
            FutureBookList.ItemsSource = res;

            res = converter.ConvertToBookView(repo.ChooseUserBooksOfCategory(User.Login, UserToBook.Categories.Favourite));
            FavouriteBookList.ItemsSource = res;

            res = repo.ChooseUserScoresToShow(User);
            ScoreList.ItemsSource = res;

            var lst = Task.Run(() => repo.ShowRecommendations(User.Login));
            while (!lst.IsCompleted)
            {
                await Task.Delay(1000);
            }
            res = converter.ConvertToBookView(lst.Result);
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var a = author.Text;
            var b = bookname.Text;
            BookList.ItemsSource = repo.SearchABook(b, a);
        }

        private void BookList_Selected(object sender, RoutedEventArgs e)
        {
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
                var b = converter.ConvertToBook((BookView)PastBookList.SelectedItem);
                repo.DeleteUserToBook(User, b, UserToBook.Categories.PastRead);
                Updatebookboxes();
            }
        }

        private void DeleteFutureBook_Click(object sender, RoutedEventArgs e)
        {
            if (FutureBookList.SelectedItem != null)
            {
                var b = converter.ConvertToBook((BookView)FutureBookList.SelectedItem);
                repo.DeleteUserToBook(User, b, UserToBook.Categories.FutureRead);
                Updatebookboxes();
            }
        }

        private void DeleteFavourite_Click(object sender, RoutedEventArgs e)
        {
            if (FavouriteBookList.SelectedItem != null)
            {
                var b = converter.ConvertToBook((BookView)FavouriteBookList.SelectedItem);
                repo.DeleteUserToBook(User, b, UserToBook.Categories.Favourite);
                Updatebookboxes();
            }
        }

        private void MarkAsFavourite_Click(object sender, RoutedEventArgs e)
        {
            if (PastBookList.SelectedItem != null)
            {
                var b = converter.ConvertToBook((BookView)PastBookList.SelectedItem);
                if (repo.GetBookFromAddingForm(User, b, UserToBook.Categories.Favourite))
                {
                    Updatebookboxes();
                    MessageBox.Show("Книга успешно добавлена");
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            }
        }

        private void MoveToFutureBooks_Click(object sender, RoutedEventArgs e)
        {
            if (RecomendationList.SelectedItem != null)
            {
                var b = converter.ConvertToBook((BookView)RecomendationList.SelectedItem);
                if (repo.GetBookFromAddingForm(User, b, UserToBook.Categories.FutureRead))
                {
                    Updatebookboxes();
                    MessageBox.Show("Книга успешно добавлена");
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            }
        }

        private void ShowBookInfo(Books b)
        {
            var w = new BookInfo(b, User);
            w.Show();
            w.Closing += (a, a1) => { Updatebookboxes(); };
        }

        private void PastBookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PastBookList.SelectedItem != null)
            {
                var b = converter.ConvertToBook((BookView)PastBookList.SelectedItem);
                ShowBookInfo(b);
            }
        }

        private void FutureBookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var b = converter.ConvertToBook((BookView)FutureBookList.SelectedItem);
            ShowBookInfo(b);
        }

        private void FavouriteBookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var b = converter.ConvertToBook((BookView)FavouriteBookList.SelectedItem);
            ShowBookInfo(b);
        }

        private void ScoreList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var b = converter.ConvertToBook((BookView)ScoreList.SelectedItem);
            ShowBookInfo(b);
        }

        private void RecomendationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var b = converter.ConvertToBook((BookView)RecomendationList.SelectedItem);
            ShowBookInfo(b);
        }
    }
}
