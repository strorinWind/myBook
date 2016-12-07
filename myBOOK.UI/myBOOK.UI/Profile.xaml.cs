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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Users User { get; set; }

        private void TabItemSizeRegulation()
        {
            foreach (var item in tabcontrol.Items)
            {
                var i = (TabItem)item;
                i.Width = window.Width / tabcontrol.Items.Count - 5;
            }
        }


        public Profile(Users user)
        {
            InitializeComponent();
            User = user;
            var repo = new Repository();
            PastBookList.ItemsSource = repo.ChooseUsersPastBooks(user.Login);
            FutureBookList.ItemsSource = repo.ChooseUsersFutureBooks(user.Login);
            FavouriteBookList.ItemsSource = repo.ChooseUsersFavouriteBooks(user.Login);
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TabItemSizeRegulation();
        }

        private void AddPastRead_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddBookToList(User);
            w.Show();
            
            //Close();
        }

        private void AddFavourite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFutureRead_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
