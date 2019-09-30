using EdaVPuti.Models;
using EdaVPuti.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EdaVPuti.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodsPage : ContentPage
    {
        ObservableCollection<Food> foods = new ObservableCollection<Food>();
        public ObservableCollection<Food> Foods { get { return foods; } }
        public FoodsPage(FoodModel model, string _RouteTitle, string _RestTitle)
        {
            InitializeComponent();
            foods.Clear();
            foreach(Food food in model.food)
            {
                foods.Add(food);
            }
            FoodList.ItemsSource = foods;
            Title.Title = _RouteTitle + ", " + _RestTitle;
        }

        public void OpenCart(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cart());
        }
        public async void OpenFood(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Food;
            if (item == null)
                return;
            await Navigation.PushAsync(new FoodPage(item));
            ((ListView)sender).SelectedItem = null;
        }
    }
}