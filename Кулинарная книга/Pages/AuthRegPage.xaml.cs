using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Кулинарная_книга
{
    /// <summary>
    /// Логика взаимодействия для AuthRegPage.xaml
    /// </summary>
    public partial class AuthRegPage : Page
    {
        bool _auth = true;
        Button _buttonAccount;
        Button _buttonAddRecipe;
        Button _buttonLogin;
        Button _buttonLogout;
        TextBlock _tbUser;

        public AuthRegPage(Button buttonAcc, TextBlock tbUs,Button buttonAddRecipe, Button buttonLogout, Button buttonLogin)
        {
            InitializeComponent();
            _buttonAccount = buttonAcc;
            _buttonAddRecipe = buttonAddRecipe;
            _buttonLogin = buttonLogin;
            _buttonLogout = buttonLogout;
            _tbUser = tbUs;
        }

        private void BtnAuthReg_Click(object sender, RoutedEventArgs e)
        {
            if (_auth == true)
            {
                Authorization();
            }
            else
            {
                Registration();
            }
        }
        private void Registration()
        {
            if (TbMail.Text == "" || TbLogin.Text == "" || TbPassword.Text == "" || TbFirstName.Text == "" || TbSecondName.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!");
            }
            else
            {
                try
                {
                    List<User> users = CookBookEntities.GetContext().Users.ToList();
                    if(users.Where(p => p.Login.Contains(TbLogin.Text)).FirstOrDefault() != null)
                    {
                        MessageBox.Show("Такой пользователь уже существует");
                    }
                    else
                    {
                        User newUser = new User() {User_type="user", Name=TbFirstName.Text, Secondname=TbSecondName.Text, Login=TbLogin.Text, Password=TbPassword.Text, Email=TbMail.Text};
                        CookBookEntities.GetContext().Users.Add(newUser);
                        CookBookEntities.GetContext().SaveChanges();
                        users = CookBookEntities.GetContext().Users.ToList();
                        CurrentUser._currentUser = users.Where(p => p.Login.Contains(TbLogin.Text) && p.Password.Contains(TbPassword.Text)).First();                      
                        _buttonAccount.Visibility = Visibility.Visible;
                        _buttonAddRecipe.Visibility = Visibility.Visible;
                        _buttonLogin.Visibility = Visibility.Collapsed;
                        _buttonLogout.Visibility = Visibility.Visible;
                        _tbUser.Text = CurrentUser._currentUser.Name.Replace(" ", string.Empty) + " " + CurrentUser._currentUser.Secondname.Replace(" ", string.Empty);
                        MessageBox.Show("Пользователь добавлен!", "Успешно!");
                        PageManager.MainFrame.GoBack();
                    }                   
                }
                catch
                {
                   MessageBox.Show("Невозможно добавить пользователя!", "Ошибка", MessageBoxButton.OK);
                }
            }
        }
        private void Authorization()
        {
            if (TbLogin.Text == "" || TbPassword.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!");
            }
            else
            {
                try
                {
                    List<User> users = CookBookEntities.GetContext().Users.ToList();
                    CurrentUser._currentUser= users.Where(p => p.Login.Contains(TbLogin.Text) && p.Password.Contains(TbPassword.Text)).First();
                    PageManager.MainFrame.GoBack();
                    _buttonAccount.Visibility = Visibility.Visible;
                    _buttonAddRecipe.Visibility = Visibility.Visible;
                    _buttonLogin.Visibility = Visibility.Collapsed;
                    _buttonLogout.Visibility = Visibility.Visible;
                    _tbUser.Text = CurrentUser._currentUser.Name.Replace(" ", string.Empty) +" "+CurrentUser._currentUser.Secondname.Replace(" ", string.Empty);
                }
                catch
                {
                    MessageBox.Show("Такого пользователя не существует!", "Ошибка", MessageBoxButton.OK);
                }
            }
        }

        private void BtnChangeAuth_Click(object sender, RoutedEventArgs e)
        {
            if (_auth == true)
            {
                _auth = false;
                TbAuth.Text = "Регистрация";
                BtnChangeAuth.Content = "Авторизация";
                GridMail.Visibility = Visibility.Visible;
                GridFirstName.Visibility = Visibility.Visible;
                GridSecondName.Visibility = Visibility.Visible;
                BtnAuthReg.Content = "Зарегистрироваться";
            }
            else
            {
                _auth = true;
                TbAuth.Text = "Авторизация";
                BtnChangeAuth.Content = "Регистрация";
                GridMail.Visibility = Visibility.Hidden;
                GridFirstName.Visibility = Visibility.Hidden;
                GridSecondName.Visibility = Visibility.Hidden;
                BtnAuthReg.Content = "Авторизоваться";
            }
        }
    }
}
