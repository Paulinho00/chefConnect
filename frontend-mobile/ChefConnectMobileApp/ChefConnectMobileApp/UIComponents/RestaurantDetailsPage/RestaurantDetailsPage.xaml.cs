using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChefConnectMobileApp.Models;

namespace ChefConnectMobileApp.UIComponents.RestaurantDetailsPage;

public partial class RestaurantDetailsPage : ContentPage
{
    public RestaurantDetailsPage(Restaurant restaurant)
    {
        InitializeComponent();
        var viewModel = BindingContext as RestaurantDetailsPageViewModel;
        viewModel.Restaurant = restaurant;
    }
}