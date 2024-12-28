using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChefConnectMobileApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChefConnectMobileApp.UIComponents.RestaurantListElement;

public partial class RestaurantListElementView : ContentView
{
    
    public RestaurantListElementView()
    {
        InitializeComponent();
    }

    private async void CheckBox_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        var viewModel = (RestaurantListElementViewModel)BindingContext;
        if (e.Value)
            await viewModel.AddNewFavourite();
        else
            await viewModel.RemoveFavourite();
    }
}