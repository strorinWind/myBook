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
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var login = Username.Text;
            var password = Password.Password;
            //проверка корректности
            var repo = new Repository();
            if (repo.IsUserDataCorrect(login,password)!=0)
            {
                var p = new Profile();
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
    }
}
