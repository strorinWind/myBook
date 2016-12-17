using myBOOK.data;
using myBOOK.data.Interfaces;
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
    /// Логика взаимодействия для WriteReview.xaml
    /// </summary>
    public partial class WriteReview : Window
    {
        public Users User { get; set; }
        public Books Book { get; set; }
        IRepository repo = Factory.Default.GetRepository();

        public WriteReview(Users user, Books book)
        {
            InitializeComponent();
            User = user;
            Book = book;
            Author.Text = Book.Author;
            Bookname.Text = Book.BookName;
            if (repo.ExistsReview(user,book) != null)
            {
                Review.Text = repo.ExistsReview(user, book).ReviewText;
            }
        }

        private void SendReview_Click(object sender, RoutedEventArgs e)
        {
            repo.AddOrChangeReview(User, Book, Review.Text);
            MessageBox.Show("Ваш отзыв сохранен");
            Close();
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
    }
}
