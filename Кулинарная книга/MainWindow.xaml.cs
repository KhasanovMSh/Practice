using System;
using System.Windows;
using System.Windows.Input;


namespace Кулинарная_книга
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            PageManager.MainFrame = MainFrame;
            PageManager.MainFrame.Navigate(new RecipeViewPage());
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.GoBack();
            BtnAccount.Visibility = Visibility.Visible;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnMenu.Visibility = Visibility.Hidden;
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnMenu.Visibility = Visibility.Visible;
                BtnBack.Visibility = Visibility.Hidden;
            }
        }

        private void BtnAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            PopupAccount.IsOpen = true;
        }

        private void BtnAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            PopupAccount.IsOpen = false;
        }

        private void PopupAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            PopupAccount.IsOpen = true;
        }

        private void PopupAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            PopupAccount.IsOpen = false;
        }

        private void BtnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            BtnAccount.Visibility = Visibility.Hidden;
            PopupAccount.IsOpen = false;
            PageManager.MainFrame.Navigate(new AddRecipePage());
        }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            BtnAccount.Visibility = Visibility.Hidden;
            PopupAccount.IsOpen = false;
            PageManager.MainFrame.Navigate(new AuthRegPage(BtnAccount, TbUser, BtnAddRecipe, BtnLogout, BtnLogin));
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            TbUser.Text = string.Empty;
            BtnLogout.Visibility = Visibility.Collapsed;
            BtnAddRecipe.Visibility = Visibility.Collapsed;
            BtnLogin.Visibility = Visibility.Visible;
            CurrentUser._currentUser = null;
        }

        private void BtnAddAdmin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
