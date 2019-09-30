using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EdaVPuti.Models;
using EdaVPuti.ViewModels;

namespace EdaVPuti.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        public ItemDetailPage(string id)
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1488",
                Description = "This is an item description234234."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }


        async void OnRestSelected(object sender, SelectedItemChangedEventArgs args)
        {

        }
    }
}