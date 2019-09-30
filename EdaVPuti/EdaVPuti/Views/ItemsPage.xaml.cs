using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EdaVPuti.Models;
using EdaVPuti.Views;
using EdaVPuti.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace EdaVPuti.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ObservableCollection<RoutePoint> routepoints = new ObservableCollection<RoutePoint>();
        public ObservableCollection<RoutePoint> RoutePointView { get { return routepoints; } }

        public ItemsPage(RoutePoints model, string ticket)
        {
            InitializeComponent();
            routepoints.Clear();
            foreach(RoutePoint routePoint in model.route_points)
            {
                routepoints.Add(routePoint);
            }
            ItemsListView.ItemsSource = RoutePointView;
            Ticket.Text = ticket;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as RoutePoint;
            if (item == null)
                return;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://192.168.137.187:1489/api/get_restaurants/" + item.id);
            var _response = await response.Content.ReadAsStringAsync();
            try
            {
                RestsModel restsModel = JsonConvert.DeserializeObject<RestsModel>(_response);
                await Navigation.PushAsync(new Rests(restsModel, item.title));
            }
            catch { }


            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }

    public class RestsModel
    {
        public List<Rest> route_points { get; set; }
    }
}