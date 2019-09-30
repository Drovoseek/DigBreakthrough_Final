using EdaVPuti.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EdaVPuti.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rests : ContentPage
    {
        ObservableCollection<Rest> rests = new ObservableCollection<Rest>();
        public ObservableCollection<Rest> RestView { get { return rests; } }
        public Rests(RestsModel model, string _Title)
        {
            InitializeComponent();
            rests.Clear();
            foreach(Rest rest in model.route_points)
            {
                rests.Add(rest);
            }
            RestList.ItemsSource = RestView;
            Title.Title = _Title;
        }

        async void OpenRest(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Rest;
            if (item == null)
                return;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://192.168.137.187:1489/api/get_food/" + item.id);
            var _response = await response.Content.ReadAsStringAsync();
            try
            {
                FoodModel foodModel = JsonConvert.DeserializeObject<FoodModel>(_response);
                await Navigation.PushAsync(new FoodsPage(foodModel, Title.Title, item.title));
            }
            catch { }
            RestList.SelectedItem = null;
        }
    }

    public class FoodModel
    {
        public List<Food> food { get; set; }
    }
}