using EdaVPuti.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
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
    public partial class FoodPage : ContentPage
    {
        ObservableCollection<Food> foods = new ObservableCollection<Food>();
        public ObservableCollection<Food> Foods { get { return foods; } }
        private static ISettings AppSettings => CrossSettings.Current;
        Food _this;
        public FoodPage(Food model)
        {
            InitializeComponent();
            FoodTitle.Text = model.title;
            FoodLogo.Source = "eda" + model.id + ".jpg";
            FoodPrice.Text = model.price.ToString();
            FoodDescription.Text = model.description;
            _this = model;
        }

        public void AddToCart(object sender, EventArgs e)
        {

            ObservableCollection<Food> card_foods = new ObservableCollection<Food>();
            var _foods = AppSettings.GetValueOrDefault("Cart", String.Empty);
            if (_foods != "")
            {
                var json_foods = JsonConvert.DeserializeObject<List<Food>>(_foods);
                foreach (Food _food in json_foods)
                {
                    card_foods.Add(_food);
                }
            }
            card_foods.Add(_this);
            string listValue = JsonConvert.SerializeObject(card_foods);
            AppSettings.AddOrUpdateValue("Cart", listValue);
        }
    }
}