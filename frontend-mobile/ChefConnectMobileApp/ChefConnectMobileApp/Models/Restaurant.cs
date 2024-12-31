
namespace ChefConnectMobileApp.Models;

public record Restaurant
{
    
    public int Id { get; init; }
    public int NumberOfTables { get; init; }
    public string Address { get; init; }
    public string Name { get; init; }
    public TimeSpan OpenTime { get; init; }
    public TimeSpan CloseTime { get; init; }
};