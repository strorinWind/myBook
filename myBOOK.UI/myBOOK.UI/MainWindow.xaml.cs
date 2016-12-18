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
using myBOOK.data.Interfaces;


namespace myBOOK.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository repository = Factory.Default.GetRepository();

        public MainWindow()
        {
            //first request to make faster others
            //repository.DoesBookExists("","");
            InitializeComponent();
        }

        //IUserBooks userbooks = Factory.Default.GetUserBooks(); понадобится в другом месте мб

        private async void Enter_Click(object sender, RoutedEventArgs e)
        {
            var login = Username.Text;
            var password = Password.Password;
          //  ((Button)sender).Background = new SolidColorBrush(Colors.Red); эта фигня не хочет красить кнопку(((((
                //проверка корректности    
            var user = await repository.IsUserDataCorrect(login, password);
            if (user!=null)
            {
                var p = new Profile(user);
                p.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Некорректные данные!");
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var r = new Registration();
            r.Show();
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
