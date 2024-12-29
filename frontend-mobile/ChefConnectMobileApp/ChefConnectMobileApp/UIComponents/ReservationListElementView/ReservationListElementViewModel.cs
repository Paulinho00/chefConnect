using ChefConnectMobileApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.ReservationListElementView;

public partial class ReservationListElementViewModel : ObservableObject
{
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
    private bool _isSendOpinionButtonEnabled;

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
}