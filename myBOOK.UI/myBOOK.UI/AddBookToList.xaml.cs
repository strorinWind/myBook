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
    /// Логика взаимодействия для AddBookToList.xaml
    /// </summary>
    public partial class AddBookToList : Window
    {
        public Users User { get; set; }
        public Action<Books> AddFoundBook { get; set; }

        private void AddBookToDatabase(Books book)
        {
            using (Context c = new Context())
            {
                var repo = new Repository();
                if (!repo.DoesBookExists(book.BookName,book.Author))
                {
                    c._Book.Add(book);
                    c.SaveChanges();
                    //MessageBox.Show("Книга успешно добавлена");
                    AddFoundBook?.Invoke(book);
                    Close();
                }
                else
                {
                    MessageBox.Show("Такая книга этого автора уже есть в базе, воспользуйтесь поиском, чтобы найти ее.");
                    return;
                }
            }
        }

        public AddBookToList(Users user)
        {
            InitializeComponent();
            User = user;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var repo = new Repository();
            BookList.ItemsSource = repo.SearchABook(bookname.Text,author.Text);
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (BookList.SelectedItem != null)
            {
                AddFoundBook?.Invoke((Books)BookList.SelectedItem);
                Close();
            }
        }

        private void AddOwnBook_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddOwnBook();
            w.AddBook += AddBookToDatabase;
            w.Show();
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
