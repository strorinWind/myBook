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

        public AddBookToList(Users user)
        {
            InitializeComponent();
            User = user;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //var p = new Profile(User);
            //p.Show();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var repo = new Repository();
            BookList.ItemsSource = repo.SearchABook(bookname.Text,author.Text);
        }
    }
}
