package com.chefconnect.reservationservice.repository;

import java.time.LocalDateTime;
import java.util.List;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.chefconnect.reservationservice.domain.ReservationStatus;
import com.chefconnect.reservationservice.domain.TableReservation;

public interface TableReservationRepository extends JpaRepository<TableReservation, UUID> {
   @Query("SELECT COUNT(DISTINCT tr) FROM TableReservation tr " +
      "JOIN tr.reservations r " +
      "WHERE tr.restaurantId = :restaurantId " +
      "AND r.isDeleted = false " +
      "AND r.date = :startTime " +
      "AND (r.approvingWorkerId IS NOT NULL)")
    long countReservedTables(@Param("restaurantId") UUID restaurantId,
                            @Param("startTime") LocalDateTime startTime);

   @Query("SELECT COUNT(tr) > 0 FROM TableReservation tr " +
      "JOIN tr.reservations r " +
      "WHERE tr.tableId = :tableId " +
      "AND tr.restaurantId = :restaurantId " +
      "AND tr.isDeleted = false " +
      "AND r.date = :reservationDate")
   boolean existsByTableIdRestaurantIdAndReservationDate(@Param("tableId") UUID tableId, 
                                                         @Param("restaurantId") UUID restaurantId,
                                                         @Param("reservationDate") LocalDateTime reservationDate);
}
