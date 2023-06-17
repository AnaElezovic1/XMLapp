INSERT INTO role(id, name) VALUES (1, 'GOST');
INSERT INTO role(id, name) VALUES (2, 'HOST');
--INSERT INTO role(id, name) VALUES (3, 'NEREGISTROVANI_KORISNIK');


-- PASSWORD: password123

INSERT INTO user_app(id, username, email, password, active, role_id, password_salt,address) VALUES (nextval('user_app_seq'), 'gost', 'gost@email.com', '$2a$10$SFp508WvAPKDbemvKcYdd.wLahgUcoBDOPjBRXgNMzDBe3ot/ElwG', true, 1, '', 'adresa');
INSERT INTO gost(id) values (1);

INSERT INTO user_app(id, username, email, password, active, role_id, password_salt,address) VALUES (nextval('user_app_seq'), 'host', 'host@email.com', '$2a$10$SFp508WvAPKDbemvKcYdd.wLahgUcoBDOPjBRXgNMzDBe3ot/ElwG', false, 2, '', 'adresa');
INSERT INTO host(id) values (2);