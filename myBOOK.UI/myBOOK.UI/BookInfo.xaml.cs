using myBOOK.data;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для BookInfo.xaml
    /// </summary>
    /// 

    public partial class BookInfo : Window
    {
        // false - form is not enabled; true - form is enabled
        bool correct = false;
        public Books Book { get; set; }
        Context c = new Context();
        public Users User { get; set; }


        public BookInfo(Books book, Users user)
        {
            InitializeComponent();
            Book = c._Book.Find(book.BookName, book.Author);
            User = c.User.Find(user.Login);
            Author.Text = Book.Author;
            Bookname.Text = Book.BookName;
            Description.Text = Book.Description;
            Genre.ItemsSource = Enum.GetNames(typeof(Books.Genres));
            Genre.SelectedIndex = (int)Book.Genre;
        }

        private void Correct_Click(object sender, RoutedEventArgs e)
        {
            if (!correct)
            {
                Correct.Content = "Сохранить";
                Description.IsEnabled = true;
                Genre.IsEnabled = true;
                Link.IsEnabled = true;
                correct = true;
            }
            else
            {
                //Book = c._Book.Find(Book.BookName, Book.Author);
                Book.Description = Description.Text;
                Book.Genre = (Books.Genres)Genre.SelectedIndex;
                Book.LoadingLink = Link.Text;
                c.SaveChanges();
                Correct.Content = "Редактировать";
                Description.IsEnabled = false;
                Genre.IsEnabled = false;
                Link.IsEnabled = false;
                correct = false;
            }
        }

        private void GiveScore_Click(object sender, RoutedEventArgs e)
        {
            int score = 0;
            if (b1.IsChecked.Value)
            {
                score = 1;
            }
            else if (b2.IsChecked.Value)
            {
                score = 2;
            }
            else if (b3.IsChecked.Value)
            {
                score = 3;
            }
            else if (b4.IsChecked.Value)
            {
                score = 4;
            }
            else if (b5.IsChecked.Value)
            {
                score = 5;
            }
            else if (b6.IsChecked.Value)
            {
                score = 6;
            }
            else if (b7.IsChecked.Value)
            {
                score = 7;
            }
            else if (b8.IsChecked.Value)
            {
                score = 8;
            }
            else if (b9.IsChecked.Value)
            {
                score = 9;
            }
            else if (b10.IsChecked.Value)
            {
                score = 10;
            }
            var repo = new Repository();
            repo.AddOrChangeScore(User,Book,score);
            MessageBox.Show("Спасибо, что оценили книгу!");
        }

        private void WriteReview_Click(object sender, RoutedEventArgs e)
        {
            var w = new WriteReview(User, Book);
            w.Show();
        }
    }
}
