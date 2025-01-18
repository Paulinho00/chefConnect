using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.ReservationService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.ReservationListElementView;

public partial class ReservationListElementViewModel : ObservableObject
{
    private IReservationService _reservationService = ServiceHelper.GetService<IReservationService>();
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();

    [ObservableProperty]
    private Reservation _reservation;

    [ObservableProperty]
    private string _reservationStatus;

    [ObservableProperty] 
    private bool _isRateButtonVisible;

    [ObservableProperty]
    private bool _isCancelButtonVisible;

    [ObservableProperty]
    private bool _isOpinionSectionVisible;

    [ObservableProperty] 
    private string _opinionDescription;

    [ObservableProperty]
    private int _opinionRate = 1;

    [ObservableProperty] 
    private bool _isSendOpinionButtonEnabled = true;

    public ReservationListElementViewModel()
    {

    }

    public ReservationListElementViewModel(Reservation reservation)
    {
        Reservation = reservation;
    }

    partial void OnOpinionRateChanged(int value)
    {
        IsSendOpinionButtonEnabled = OpinionRate >= 1 && OpinionRate <= 5;
    }

    partial void OnReservationChanged(Reservation value)
    {
        switch (Reservation.Status)
        {
            case Models.ReservationStatus.Unconfirmed:
            {
                IsCancelButtonVisible = true;
                IsRateButtonVisible = false;
                ReservationStatus = "Niepotwierdzona";
            }
                break;
            case Models.ReservationStatus.Confirmed:
            {
                IsCancelButtonVisible = Reservation.Date > DateTime.Now;
                IsRateButtonVisible = Reservation.Date <= DateTime.Now;
                ReservationStatus = "Potwierdzona";
                }
                break;
            case Models.ReservationStatus.Cancelled:
            {
                IsCancelButtonVisible = false;
                IsRateButtonVisible = false;
                ReservationStatus = "Odwołana";
                }
                break;
            case Models.ReservationStatus.OpinionSaved:
            {
                IsCancelButtonVisible = false;
                IsRateButtonVisible = false;
                ReservationStatus = "Potwierdzona - opinia wystawiona";
            }
                break;
            default:
            {
                IsCancelButtonVisible = false;
                IsRateButtonVisible = false;
                ReservationStatus = "";
                }
                break;
        }
    }

    [RelayCommand]
    private void ChangeVisibilityOfOpinionSection()
    {
        IsOpinionSectionVisible = !IsOpinionSectionVisible;
    }

    [RelayCommand]
    private async Task CancelReservation()
    {
        var result = await _reservationService.CancelReservation(Reservation.Id).ConfigureAwait(false);
        if (result.IsFailure)
        {
            _alertService.ShowAlert("Błąd: Spróbuj jeszcze raz", result.Error);
        }
        else
        {
            _alertService.ShowAlert("Anulowano rezerwacje", "Rezerwacja została anulowana");
            Reservation.Status = Models.ReservationStatus.Cancelled;
            Reservation = new Reservation()
            {
                Address = Reservation.Address,
                Date = Reservation.Date,
                Id = Reservation.Id,
                NumberOfTable = Reservation.NumberOfTable,
                Status = Reservation.Status
            };
            
            IsCancelButtonVisible = false;
            IsRateButtonVisible = false;
        }
    }

    [RelayCommand]
    private async Task SendOpinion()
    {
        var opinionDto = new ReservationOpinionDTO()
        {
            Description = OpinionDescription,
            Rate = OpinionRate,
            ReservationId = Reservation.Id
        };

        var result = await _reservationService.SendOpinion(opinionDto).ConfigureAwait(false);
        if (result.IsFailure)
        {
            _alertService.ShowAlert("Błąd: Spróbuj jeszcze raz", result.Error);
        }
        else
        {
            _alertService.ShowAlert("Opinia zapisana", "Twoja opinia została pomyślnie zapisana. Dziękujemy!");
            IsCancelButtonVisible = false;
            IsRateButtonVisible = false;
            IsOpinionSectionVisible = false;
            ReservationStatus = "Potwierdzona / opinia wystawiona";
            Reservation.Status = Models.ReservationStatus.OpinionSaved;
        }
    }
}