-- Przykładowe dane do tabeli RESERVATION
INSERT INTO RESERVATION (id, approving_worker_id, date, is_approved, reservation_status, is_deleted, number_of_people, restaurant_id, user_id)
VALUES
  ('eab4b9e5-5c44-438f-bb23-36f3b24a9284', '0e674df3-a8c0-44d4-8d9a-4424b3f5746d', '2025-01-05T12:00:00', true, 'CONFIRMED', false, 4, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('4e3c2f88-7b3a-4d1f-95cf-ff18a1cc3eab', 'a90aa581-3579-4cc4-9f00-67557371714b', '2025-01-05T14:00:00', true, 'CONFIRMED', false, 6, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('08be0d57-2276-4b47-9c81-c82615e62309', 'bc4189d9-afb7-47d5-8401-588eb273f639', '2025-01-06T10:00:00', true, 'CONFIRMED', false, 2, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('e438fd77-9258-4ed3-9f4a-2d7c6d04cd83', '40c7fbb3-5c64-4cce-aed1-c0eea909a4da', '2025-01-06T12:00:00', true, 'CONFIRMED', false, 8, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('10ab2f3a-cc9e-4b82-bc88-bb39413f59a5', '38c14b6a-aac8-4658-bf9e-2c8f970a5ea3', '2025-01-07T12:00:00', true, 'CONFIRMED', false, 3, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('ad7e9fd8-2b64-4e9d-9c89-dfa89a663f76', NULL, '2025-01-07T15:00:00', false, 'UNCONFIRMED', false, 5, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('55e5a9a2-c70d-4964-a319-cf6f9ea568d0', 'b9c894d2-47b6-40cf-8b9a-50e3f7b189d0', '2025-01-08T10:00:00', true, 'CONFIRMED', false, 10, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('8a653f1c-bf97-49a1-8e35-9ffcb7f8d358', '3deb69d8-a316-4893-9217-cdd790be700e', '2025-01-08T13:00:00', true, 'CONFIRMED', false, 7, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('442b61c8-e35f-4082-8d6c-f8595c18fa9c', '48b41b26-427e-44a1-a448-21243e327c65', '2025-01-09T11:00:00', true, 'CONFIRMED', false, 9, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245'),
  ('c1b674e2-d7c5-4861-a106-580988d515a2', NULL, '2025-01-09T16:00:00', false, 'UNCONFIRMED', false, 4, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', '071a66c5-9f85-465d-8430-e8b89fcf6245');


-- Dodajemy przykładowe zarezerwowane stoliki o restauracji z danym UUID do tabeli TableReservation
INSERT INTO TABLE_RESERVATION (id, table_id, restaurant_id, is_deleted)
VALUES
  ('90e89efd-741c-4f39-aa2f-785c11650318', 'd43c5708-62d7-45db-b60d-ff6b6dbca432', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('d5a3e0a4-31be-4c84-9c61-8e1eaecc8c4f', 'd43c5708-62d7-45db-b60d-ff6b6dbca432', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('e0c21d42-47ba-4ca9-94e3-f51a17d2b9ff', 'd43c5708-62d7-45db-b60d-ff6b6dbca432', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('baf5fc51-e990-4b91-80d3-d5d7e0a6f97f', 'ab2c7610-12de-41db-bbc1-1b8a2b234567', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('27f18f3b-76c6-48ea-a3eb-9c37968854cc', 'ab2c7610-12de-41db-bbc1-1b8a2b234567', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('684cf24f-055b-4c11-8cfb-42a4455bb8b9', 'ab2c7610-12de-41db-bbc1-1b8a2b234567', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('b9d5bc92-1c9b-44d1-b2fa-b2db2b12651e', 'b15a1234-5678-1234-5678-12a34b56789c', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('8b37fc51-7dfd-4d5f-a264-89d2c7f7099d', 'b15a1234-5678-1234-5678-12a34b56789c', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('4bdfcde5-f995-459f-bba3-5fe074cb7464', 'b15a1234-5678-1234-5678-12a34b56789c', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false),
  ('0f8ecb7b-16b9-44ff-b34c-bccfd72c8482', 'b15a1234-5678-1234-5678-12a34b56789c', '1e6101b4-4ae2-4c8c-92e2-0a62a3794877', false);

-- Teraz przypisujemy stoliki do wybranej rezerwacji o danym UUID w tabeli łączącej "reservation_table"
INSERT INTO RESERVATION_TABLE (reservation_id, table_id)
VALUES
  ('eab4b9e5-5c44-438f-bb23-36f3b24a9284', '90e89efd-741c-4f39-aa2f-785c11650318'),
  ('4e3c2f88-7b3a-4d1f-95cf-ff18a1cc3eab', 'd5a3e0a4-31be-4c84-9c61-8e1eaecc8c4f'),
  ('08be0d57-2276-4b47-9c81-c82615e62309', 'e0c21d42-47ba-4ca9-94e3-f51a17d2b9ff'),
  ('e438fd77-9258-4ed3-9f4a-2d7c6d04cd83', 'baf5fc51-e990-4b91-80d3-d5d7e0a6f97f'),
  ('10ab2f3a-cc9e-4b82-bc88-bb39413f59a5', '27f18f3b-76c6-48ea-a3eb-9c37968854cc'),
  ('ad7e9fd8-2b64-4e9d-9c89-dfa89a663f76', '684cf24f-055b-4c11-8cfb-42a4455bb8b9'),
  ('55e5a9a2-c70d-4964-a319-cf6f9ea568d0', 'b9d5bc92-1c9b-44d1-b2fa-b2db2b12651e'),
  ('8a653f1c-bf97-49a1-8e35-9ffcb7f8d358', '8b37fc51-7dfd-4d5f-a264-89d2c7f7099d'),
  ('442b61c8-e35f-4082-8d6c-f8595c18fa9c', '4bdfcde5-f995-459f-bba3-5fe074cb7464'),
  ('c1b674e2-d7c5-4861-a106-580988d515a2', '0f8ecb7b-16b9-44ff-b34c-bccfd72c8482');
