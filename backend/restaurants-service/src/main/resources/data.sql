INSERT INTO address (id, street, street_number, flat_number, postal_code, city)
VALUES ('f47ac10b-58cc-4372-a567-0e02b2c3d479', 'ul. Kwiatowa', 12, NULL, '00-001', 'Warszawa'),
       ('f1ee5e84-3b95-4879-ba45-92a2b586b012', 'ul. Radosna', 45, 7, '01-003', 'Wrocław'),
       ('aab23c4e-1234-5678-9abc-12d34e5678f9', 'ul. Wesoła', 56, 3, '02-002', 'Kraków');

INSERT INTO restaurant (id, number_of_tables, address_id, name, open_time, close_time)
VALUES ('1e6101b4-4ae2-4c8c-92e2-0a62a3794877', 5, 'f47ac10b-58cc-4372-a567-0e02b2c3d479', 'The Fancy Fork', '08:00',
        '22:00'),
       ('26436fe8-e03c-4e2b-b763-897601033761', 7, 'f1ee5e84-3b95-4879-ba45-92a2b586b012', 'Savor & Spice', '08:00',
        '22:00'),
       ('cc9a850e-6b1d-43b7-a6ea-3e7f75927b0f', 3, 'aab23c4e-1234-5678-9abc-12d34e5678f9', 'The Rustic Spoon', '10:00', '20:00');

INSERT INTO restaurant_table (id, number_of_seats, restaurant_id)
VALUES ('d43c5708-62d7-45db-b60d-ff6b6dbca432', 4, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877'), -- Table for 4 at "The Fancy Fork"
       ('ab2c7610-12de-41db-bbc1-1b8a2b234567', 2, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877'), -- Table for 2 at "The Fancy Fork"
       ('b15a1234-5678-1234-5678-12a34b56789c', 6, '1e6101b4-4ae2-4c8c-92e2-0a62a3794877'), -- Table for 6 at "The Fancy Fork"
       ('c23ab123-9cde-4567-bbc1-1a23b4c56d78', 4, 'cc9a850e-6b1d-43b7-a6ea-3e7f75927b0f'), -- Table for 4 at "The Rustic Spoon"
       ('af12b345-9abc-4cde-1234-5a6bc78d9012', 3, 'cc9a850e-6b1d-43b7-a6ea-3e7f75927b0f'), -- Table for 3 at "The Rustic Spoon"
       ('af12b345-9abc-4cde-1234-5a6bc78d9012', 5, '26436fe8-e03c-4e2b-b763-897601033761'); -- Table for 5 at "Savor & Spice"