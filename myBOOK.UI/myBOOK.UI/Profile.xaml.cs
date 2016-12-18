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
        Task<List<Books>> lst;
        Dictionary<ListBox,UserToBook.Categories> dict = new Dictionary<ListBox, UserToBook.Categories>();

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

            lst = Task.Run(() => Recomendations.Show(User.Login));
            LoadingInfo.Visibility = Visibility.Visible;
            while (!lst.IsCompleted)
            {
                await Task.Delay(1000);
            }
            res = converter.ConvertToBookView(lst.Result);
            RecomendationList.ItemsSource = res;
            LoadingInfo.Visibility = Visibility.Hidden;
        }

        public Profile(Users user)
        {
            InitializeComponent();
            User = user;
            dict.Add(PastBookList, UserToBook.Categories.PastRead);
            dict.Add(FutureBookList, UserToBook.Categories.FutureRead);
            dict.Add(FavouriteBookList, UserToBook.Categories.Favourite);
            Updatebookboxes();
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TabItemSizeRegulation();
        }

        private void AddBook(UserToBook.Categories category)
        {
            var w = new AddBookToList(User);
            w.AddFoundBook += (b) =>
            {
                if (repo.GetBookFromAddingForm(User, b, category))
                {
                    MessageBox.Show("Книга успешно добавлена");
                }
                else
                {
                    MessageBox.Show("Эта книга уже есть в вашем списке");
                }
            };
            w.Show();
            w.Closing += (a,b) => Updatebookboxes();
        }

        private void AddPastRead_Click(object sender, RoutedEventArgs e)
        {
            AddBook(UserToBook.Categories.PastRead);
        }

        private void AddFavourite_Click(object sender, RoutedEventArgs e)
        {
            AddBook(UserToBook.Categories.Favourite);
        }

        private void AddFutureRead_Click(object sender, RoutedEventArgs e)
        {
            AddBook(UserToBook.Categories.FutureRead);
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

        private void ListBox__MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var box = (ListBox)sender;
            if (box.SelectedItem != null)
            {
                var b = converter.ConvertToBook((BookView)box.SelectedItem);
                ShowBookInfo(b);
            }
        }

        private void MenuHelp_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
        private void MenuAuthors_Click(object sender, RoutedEventArgs e)
        {
            Authors authors = new Authors();
            authors.ShowDialog();
        }
        
        private void tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var t = (TabControl)sender;
            foreach (var item in t.Items)
            {
                ((TabItem)item).Foreground = new SolidColorBrush(Colors.White);
            }
            ((TabItem)t.SelectedItem).Foreground = new SolidColorBrush(Colors.Black);
        }

        private void DeleteButton_Click(ListBox listbox, UserToBook.Categories category)
        {
            if (listbox.SelectedItem != null)
            {
                var b = converter.ConvertToBook((BookView)listbox.SelectedItem);
                repo.DeleteUserToBook(User, b, category);
                Updatebookboxes();
            }
        }

        private void DeletePastRead_Click(object sender, RoutedEventArgs e)
        {
            DeleteButton_Click(PastBookList, UserToBook.Categories.PastRead);
        }

        private void DeleteFavourite_Click(object sender, RoutedEventArgs e)
        {
            DeleteButton_Click(FavouriteBookList,UserToBook.Categories.Favourite);
        }

        private void DeleteFutureRead_Click(object sender, RoutedEventArgs e)
        {
            DeleteButton_Click(FutureBookList,UserToBook.Categories.Favourite);
        }
    }
}
