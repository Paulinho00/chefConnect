package com.chefconnect.reservationservice.repository;

import java.time.LocalDateTime;
import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.chefconnect.reservationservice.domain.TableReservation;

public interface TableReservationRepository extends JpaRepository<TableReservation, Long> {
    @Query("SELECT COUNT(tr) FROM TableReservation tr " +
       "JOIN tr.reservations r " +
       "JOIN r.tableReservations trr " +
       "WHERE trr.restaurantId = :restaurantId " +
       "AND r.isDeleted = false " +
       "AND r.date >= :startTime " +
       "AND r.date < :endTime " +
       "AND (r.approvingWorkerId IS NOT NULL)")
    long countReservedTables(@Param("restaurantId") UUID restaurantId,
                            @Param("startTime") LocalDateTime startTime,
                            @Param("endTime") LocalDateTime endTime);
}
