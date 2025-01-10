-- Przykładowe dane do tabeli RESERVATION
INSERT INTO RESERVATION (id, approving_worker_id, date, is_approved, reservation_status, is_deleted, number_of_people, restaurant_id, user_id)
VALUES
  ('eab4b9e5-5c44-438f-bb23-36f3b24a9284', '0e674df3-a8c0-44d4-8d9a-4424b3f5746d', '2025-01-05T12:00:00', true, 'CONFIRMED', false, 4, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),


-- Dodajemy przykładowe zarezerwowane stoliki o restauracji z danym UUID do tabeli TableReservation
INSERT INTO TABLE_RESERVATION (id, table_id, restaurant_id, is_deleted)
VALUES
  ('90e89efd-741c-4f39-aa2f-785c11650318', 'd43c5708-62d7-45db-b60d-ff6b6dbca432', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),

-- Teraz przypisujemy stoliki do wybranej rezerwacji o danym UUID w tabeli łączącej "reservation_table" (TABELA ŁĄCZĄCA DLA RELACJI WIELE DO WIELU)
INSERT INTO RESERVATION_TABLE (reservation_id, table_id)
VALUES
  ('eab4b9e5-5c44-438f-bb23-36f3b24a9284', '90e89efd-741c-4f39-aa2f-785c11650318'),
