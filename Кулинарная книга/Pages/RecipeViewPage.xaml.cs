using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Diagnostics;

namespace Кулинарная_книга
{
    /// <summary>
    /// Логика взаимодействия для RecipeViewPage.xaml
    /// </summary>
    public partial class RecipeViewPage : Page
    {
        public List<Recipe> currentRecipes;

        public RecipeViewPage()
        {
            InitializeComponent();
            var sw = Stopwatch.StartNew();
            UpdateRecipe();
            sw.Stop();
            //MessageBox.Show(sw.Elapsed.ToString());
        }
        private void UpdateRecipe()
        {
            currentRecipes = CookBookEntities.GetContext().Recipes.ToList();
            LViewRecipes.ItemsSource = currentRecipes;
        }

        /*private void UpdateRecipe()
        {
            Thread trd = new Thread((obj)=>
            {
                currentRecipes = CookBookEntities.GetContext().Recipes.ToList();
               
            });
            trd.Start();
            trd.Join();
            LViewRecipes.ItemsSource = currentRecipes;
        }*/


        private void LViewRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentObject= LViewRecipes.SelectedIndex;
            Recipe currentRecipe = currentRecipes[currentObject];
            TbName.Text = currentRecipe.Name;
            TbType.Text = currentRecipe.Type;
            string ingridients = "Ингридиенты:"+Environment.NewLine+currentRecipe.Ingridients.Replace("@", Environment.NewLine).Replace("|",Environment.NewLine+ Environment.NewLine + "Шаги приготовления:" + Environment.NewLine) ;
            TbIngridients.Text = ingridients;          
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CookBookEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                UpdateRecipe();
            }
        }
    }
}
