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
    /// Логика взаимодействия для AddOwnBook.xaml
    /// </summary>
    public partial class AddOwnBook : Window
    {
        public Action<Books> AddBook { get; set; }

        public AddOwnBook()
        {
            InitializeComponent();
            Genre.ItemsSource = Enum.GetNames(typeof(Books.Genres));
            Genre.SelectedIndex = 0;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var author = AuthorName.Text;
            var bookname = BookName.Text;
            var genre = (Books.Genres)Genre.SelectedItem;
            var desc = Description.Text;
            if (author=="")
            {
                MessageBox.Show("Укажите автора");
                return;
            }
            if (bookname=="")
            {
                MessageBox.Show("Укажите название книги");
            }
        }
    }
}
