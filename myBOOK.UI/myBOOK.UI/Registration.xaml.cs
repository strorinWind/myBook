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
using System.Windows.Shapes;

namespace myBOOK.UI
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Registrarion_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password == PasswordRepeat.Password)
            {
                var login = Username.Text;
                var password = Password.Password;
                var fullname = Fullname.Text;
                Gender gender;
                if (Male.IsChecked.Value)
                {
                    gender = Gender.Male;
                }
                else
                {
                    gender = Gender.Female;
                }
                Users user = new Users
                {
                    Login = login,
                    FullName = fullname,
                    Password = Encryption.GetHashString(password),
                    Gender = gender,
                    RegistrationDate = DateTime.Now,
                };
                var repo = new Repository();
                using (Context c = new Context())
                {
                    if (repo.IsLoginRepeated(login))
                    {
                        MessageBox.Show("Этот логин уже используется");
                    }
                    else
                    {
                        c.User.Add(user);
                        c.SaveChanges();
                        Close();
                    }       
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }

        private void Female_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
