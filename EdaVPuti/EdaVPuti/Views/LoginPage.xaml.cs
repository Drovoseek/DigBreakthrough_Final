using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EdaVPuti.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private static ISettings AppSettings => CrossSettings.Current;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        public async void Login(object ender, EventArgs e)
        {
            Error.TextColor = Color.Transparent;
            var formContent = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("login", UserNumber.Text),
                    new KeyValuePair<string, string>("pass", Pass.Text ),
                }
            );

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://192.168.137.187:1489/api/auth", formContent);
            var _response = await response.Content.ReadAsStringAsync();
            try
            {
                Response result = JsonConvert.DeserializeObject<Response>(_response);
                if (result.field_is_correct == "true")
                {
                    AppSettings.AddOrUpdateValue("UserNumber", UserNumber.Text);
                    Navigation.PopModalAsync();

                }
                else
                {
                    Error.TextColor = Color.Red;
                }
            }
            catch { }
        }
        public ICommand TapCommand => new Command<string>(OpenBrowser);

        void OpenBrowser(string url)
        {
            Device.OpenUri(new Uri(url));
        }
    }

    public class Response
    {
        public string field_is_correct { get; set; }
    }
}