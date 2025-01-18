package com.chefconnect.reservationservice.services;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.chefconnect.reservationservice.domain.CustomerOpinion;
import com.chefconnect.reservationservice.domain.Reservation;
import com.chefconnect.reservationservice.exceptions.InvalidReservationStatusException;
import com.chefconnect.reservationservice.exceptions.OpinionAlreadyExistsException;
import com.chefconnect.reservationservice.repository.CustomerOpinionRepository;
import com.chefconnect.reservationservice.repository.ReservationRepository;
import com.chefconnect.reservationservice.services.Dto.CustomerOpinionRequestDto;
import com.chefconnect.reservationservice.services.Dto.MessageResponseDto;

@Service
public class CustomerOpinionService {

    private final ReservationRepository reservationRepository;
    private final CustomerOpinionRepository customerOpinionRepository;

    public CustomerOpinionService(ReservationRepository reservationRepository,
                                  CustomerOpinionRepository customerOpinionRepository) {
        this.reservationRepository = reservationRepository;
        this.customerOpinionRepository = customerOpinionRepository;
    }

    @Transactional
    public MessageResponseDto addOpinion(CustomerOpinionRequestDto request) {
        Optional<Reservation> optionalReservation = reservationRepository.findById(request.getReservationId());

        if (optionalReservation.isEmpty())
            throw new IllegalArgumentException("Nie znaleziono rezerwacji o id: " + request.getReservationId());
        
        Reservation reservation = optionalReservation.get();


        //// WARUNEK KONIECZNY DO DODANIA NA PÓŹNIEJ. W CELACH TESTOWYCH JEST ON NARAZIE ZAKOMENTOWANY ////

        // if (reservation.getReservationStatus() == ReservationStatus.UNCONFIRMED ||
        //     reservation.getReservationStatus() == ReservationStatus.CANCELLED ||
        //     reservation.getDate().isAfter(LocalDateTime.now())) {
        //     throw new InvalidReservationStatusException("Nie można dodać opinii dla rezerwacji o statusie " +
        //             reservation.getReservationStatus() + " albo dla przeszłej daty");
        // }

        if(reservation.getCustomerOpinion() != null)
            throw new OpinionAlreadyExistsException("Opinia już istnieje dla rezerwacji o id: " + request.getReservationId());

        CustomerOpinion opinion = new CustomerOpinion();
        opinion.setRate(request.getRate());
        opinion.setDescription(request.getDescription());
        opinion.setPublishDate(LocalDateTime.now().toString());
        opinion.setReservation(reservation);
        customerOpinionRepository.save(opinion);

        reservation.setCustomerOpinion(opinion);
        reservationRepository.save(reservation);

        return new MessageResponseDto("Dodano opinię");
    }

    public double calculateAverageRatingForRestaurant(UUID restaurantId) {
        List<Reservation> reservations = reservationRepository.findByRestaurantId(restaurantId);

        double totalRating = 0;
        int totalOpinions = 0;

        for (Reservation reservation : reservations) {
            if (reservation.getCustomerOpinion() != null) {
                totalRating += reservation.getCustomerOpinion().getRate();
                totalOpinions++;
            }
        }

        if (totalOpinions == 0)
            return 0;

        return totalRating / totalOpinions;
    }
}
