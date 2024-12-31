namespace ChefConnectMobileApp.Models;

public record User
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}