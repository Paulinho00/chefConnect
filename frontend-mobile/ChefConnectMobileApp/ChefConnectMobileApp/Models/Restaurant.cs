using CommunityToolkit.Mvvm.ComponentModel;

namespace ChefConnectMobileApp.Models;

public partial class Restaurant
{
    
    public int Id { get; init; }
    public int NumberOfTables { get; init; }
    public string Address { get; init; }
    public string Name { get; set; }
};