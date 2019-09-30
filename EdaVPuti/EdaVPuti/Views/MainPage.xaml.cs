using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EdaVPuti.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace EdaVPuti.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private static ISettings AppSettings => CrossSettings.Current;
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            if (AppSettings.GetValueOrDefault("UserNumber", "") == "")
            {
                //Navigation.PushModalAsync(new MainPage());
                Navigation.PushModalAsync(new LoginPage());
            }
            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.EnterTicketPage, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.EnterTicketPage:
                        MenuPages.Add(id, new NavigationPage(new EnterTicketPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Cart:
                        MenuPages.Add(id, new NavigationPage(new Cart()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}