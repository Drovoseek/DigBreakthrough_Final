using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EdaVPuti.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterTicketPage : ContentPage
    {
        private static ISettings AppSettings => CrossSettings.Current;
        public EnterTicketPage()
        {
            InitializeComponent();
        }

        public async void EnterTicketNumber(object sender, EventArgs e)
        {
            string ticket = Ticket.Text;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://192.168.137.187:1489/api/get_points/" + ticket);
            var _response = await response.Content.ReadAsStringAsync();
            try
            {
                RoutePoints result = JsonConvert.DeserializeObject<RoutePoints>(_response);
                AppSettings.AddOrUpdateValue("ticketN", ticket);
                Navigation.PushAsync(new ItemsPage(result, ticket));
            }
            catch { }
        }
    }

    public class RoutePoint
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class RoutePoints
    {
        public List<RoutePoint> route_points { get; set; }
    }
}