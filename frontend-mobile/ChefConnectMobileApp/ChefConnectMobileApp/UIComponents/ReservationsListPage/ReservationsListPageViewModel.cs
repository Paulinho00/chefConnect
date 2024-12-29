using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.ReservationService;
using ChefConnectMobileApp.UIComponents.ReservationListElementView;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChefConnectMobileApp.UIComponents.ReservationsListPage;

public partial class ReservationsListPageViewModel : ObservableObject
{
    private IReservationService _reservationService = ServiceHelper.GetService<IReservationService>();

    [ObservableProperty]
    private List<ReservationListElementViewModel> _reservations;

    public ReservationsListPageViewModel()
    {
        Init().Wait();
    }

    public async Task Init()
    {
        var reservations = await _reservationService.GetReservations();
        var reservationsViewModels = new List<ReservationListElementViewModel>();
        foreach (var reservation in reservations)
        {
            var reservationViewModel = new ReservationListElementViewModel(reservation);
            reservationsViewModels.Add(reservationViewModel);
        }
        Reservations = reservationsViewModels;
    }
}