﻿using myBOOK.data;
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
            using (Context c = new Context())
            {
                //c._User.Where(k => k.Login=="user").ToList();
                //c._FutureReadBooks.Add(new FutureReadBooks {Book = c._Book.First(), User = c.User.First() });
                //c._Favourite.Add(new Favourite { Book = c._Book.First(), User = c.User.First() });
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
    }
}
