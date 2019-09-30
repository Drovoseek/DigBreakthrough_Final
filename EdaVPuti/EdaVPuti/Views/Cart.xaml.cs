using EdaVPuti.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EdaVPuti.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        ObservableCollection<Food> foods = new ObservableCollection<Food>();
        public ObservableCollection<Food> CartItems { get { return foods; } }
        private static ISettings AppSettings => CrossSettings.Current;
        public Cart()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var _foods = AppSettings.GetValueOrDefault("Cart", String.Empty);
            var json_foods = JsonConvert.DeserializeObject<List<Food>>(_foods);
            int summ = 0;
            foods.Clear();
            if (json_foods != null)
            {
                int inc = 0;
                foreach (Food _food in json_foods)
                {
                    inc++;
                    _food.cartId = inc;
                    summ += _food.price;
                    foods.Add(_food);
                }
            }
            CartView.ItemsSource = CartItems;
            Total.Text = summ.ToString();
        }
        public async void Pay(object sender, EventArgs e)
        {
            CartView.ItemsSource = null;
            AppSettings.AddOrUpdateValue("Cart", string.Empty);
            await DisplayAlert("Success!", "Payment successful. Await for your order in choosen location", "OK");
        }


        public void OpenFood(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Food;
            Navigation.PushAsync(new FoodPage(item));
        }

        public void deleteItem(object sender, EventArgs e)
        {
            int index = int.Parse(((Button)sender).CommandParameter.ToString());
            var _foods = AppSettings.GetValueOrDefault("Cart", String.Empty);
            var json_foods = JsonConvert.DeserializeObject<List<Food>>(_foods);
            ObservableCollection<Food> new_foods = new ObservableCollection<Food>();
            int summ = 0;
            int inc = 0;
            foreach (Food _food in json_foods)
            {
                inc++;
                if (inc != index)
                {
                    _food.cartId = inc;
                    summ += _food.price;
                    new_foods.Add(_food);
                }
            }

            CartView.ItemsSource = new_foods;
            Total.Text = summ.ToString();
            string listValue = JsonConvert.SerializeObject(new_foods);
            AppSettings.AddOrUpdateValue("Cart", listValue);
        }
    }

    public class itemToBuy
    {
        public int foodId { get; set; }
        public int count { get; set; }
    }
}