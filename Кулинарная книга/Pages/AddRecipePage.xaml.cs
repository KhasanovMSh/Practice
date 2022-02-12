using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;


namespace Кулинарная_книга
{
    /// <summary>
    /// Логика взаимодействия для AddRecipePage.xaml
    /// </summary>
    public partial class AddRecipePage : Page
    {
        string path;
        BitmapImage bitmapImg;

        public AddRecipePage()
        {
            InitializeComponent();
        }

        private void BtnAddIngridient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {           
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images |*.png;*.jpg";
            
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
                bitmapImg = new BitmapImage(new Uri(path));
                ImageRecipe.Source = bitmapImg;
            }    
        }

        private void BtnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text == "" || TbType.Text == "" || TbIngridients.Text == "" || TbSteps.Text == "" || ImageRecipe.Source == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!");
            }
            else
            {
                try
                {
                    string ingridients = TbIngridients.Text.Replace(Environment.NewLine, "@")+"|"+ TbSteps.Text.Replace(Environment.NewLine, "@");
                    Recipe currentRecipe = new Recipe() { Name = TbName.Text, Type = TbType.Text, Ingridients = ingridients, ImagePreview = File.ReadAllBytes(path) };

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

                    CookBookEntities.GetContext().Recipes.Add(currentRecipe);
                    CookBookEntities.GetContext().SaveChanges();
                    MessageBox.Show("Рецепт успешно сохранен");
                    PageManager.MainFrame.GoBack();
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить рецепт");
                }
            }                    
        }
    }
}
