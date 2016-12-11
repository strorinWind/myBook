using myBOOK.data;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace myBOOK.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (Context c = new Context())
            {
                //c._Book.Add(new Books {Author="Булгаков", BookName="Мастер и Маргарита", Genre=Books.Genres.Fiction, Description="крутая книга" });
                //c._User.Where(k => k.Login=="user").ToList();
                //c._FutureReadBooks.Add(new FutureReadBooks {Book = c._Book.First(), User = c.User.First() });
                //c._Book.Add(new Books {BookName= "Рассказы",Author= "Агата Кристи",Genre="детектив",Description="Здесь есть описание" });
                //c._PastReadBooks.Add(new PastReadBooks { Book = c._Book.First(), User = c.User.First() });
                //c.SaveChanges();
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var login = Username.Text;
            var password = Password.Password;
            //проверка корректности
            var repo = new Repository();
            var user = repo.IsUserDataCorrect(login, password);
            if (user!=null)
            {
                var p = new Profile(user);
                p.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var r = new Registration();
            r.Show();
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
