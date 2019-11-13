CREATE TABLE Client
(client_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
  login varchar(30) NOT NULL UNIQUE,
  password_hash varchar(100) NOT NULL,
  lastname varchar(30) NOT NULL,
  firstname varchar(30) NOT NULL,
  patronimic varchar(30),
  telephone varchar(20),
  bday date NOT NULL,
  CONSTRAINT client_pk PRIMARY KEY (client_id)
);

insert into client(login, password_hash, lastname, firstname, patronimic, bday) values ('pomazafa', '$MYHASH$V1$1000$1s03xYaRhcDoXBOn/QRPuQWsILnfbkrBnYKh2eX4IRvaQJWp', 'Duben', 'Polina', 'Vasilievna', '12-05-1999');


CREATE TABLE Train
(
    train_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    train_number varchar(30) not null unique,
    count_seats number not null,
    cost_per_station real not null,
    CONSTRAINT train_pk PRIMARY KEY (train_id)
);


create table Address
(
    address_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    town varchar(30),
    street varchar(30),
    house varchar(10),
    flat varchar(30),
    CONSTRAINT address_pk PRIMARY KEY (address_id)
)


create table Station
(
    station_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    station_name varchar(30) not null unique,
    address_id number not null,
    CONSTRAINT fk_address
    FOREIGN KEY (address_id)
    REFERENCES Address (address_id),
    CONSTRAINT station_pk PRIMARY KEY (station_id)
)



create table Train_stop
(
    train_id number not null,
    arrival_date date not null,
    departure_date date not null,
    station_id number not null,
    FOREIGN KEY (train_id)
    REFERENCES Train (train_id),
    FOREIGN KEY (station_id)
    REFERENCES Station (station_id),
    CONSTRAINT train_stop_pk PRIMARY KEY(train_id, station_id)
)



create table Trip
(
    trip_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    train_id number not null,
    start_station_id number not null,
    end_station_id number not null,
    FOREIGN KEY (end_station_id)
    REFERENCES Station (station_id),
    FOREIGN KEY (start_station_id)
    REFERENCES Station (station_id),
    FOREIGN KEY (train_id)
    REFERENCES Train (train_id),
    CONSTRAINT trip_pk PRIMARY KEY (trip_id)
)


create table Ticket
(
    ticket_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    date_of_purchase date not null,
    client_id number not null,
    trip_id number not null,
    FOREIGN KEY (client_id)
    REFERENCES Client (client_id),
    FOREIGN KEY (trip_id)
    REFERENCES Trip (trip_id),
    CONSTRAINT ticket_pk PRIMARY KEY (ticket_id)
)


create table Card
(
    card_id  NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    card_number number check(LENGTH(card_number) = 16),
    espiration_date number check(LENGTH(espiration_date) = 4),
    card_owner varchar(30),
    CONSTRAINT card_pk PRIMARY KEY (card_id)
)


create table Occupation
(
    occupation_id  NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    name varchar(40) not null,
    cost_per_hour real not null,
    hours_per_week number not null,
    CONSTRAINT position_pk PRIMARY KEY (occupation_id)
)


create table Employee
(
    employee_id  NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    card_id number not null,
    position_id number not null,
    client_id number not null,
    is_admin number(1) DEFAULT 0 NOT NULL
    CONSTRAINT is_admin_check CHECK (is_admin in (1,0)),
    FOREIGN KEY (client_id)
    REFERENCES Client (client_id),
    FOREIGN KEY (position_id)
    REFERENCES Occupation (occupation_id),
    FOREIGN KEY (card_id)
    REFERENCES Card (card_id),
    CONSTRAINT employee_pk PRIMARY KEY (employee_id)
)

commit;
drop table Employee;
drop table Occupation;
drop table Card;
drop table ticket;
drop table trip;
drop table train_stop;
drop table Station;
drop table Address;
drop table Train;
drop table client;


select * from Client;

alter session set "_ORACLE_SCRIPT"=true;

CREATE USER kuser IDENTIFIED BY qwe123qwe;

GRANT CONNECT TO kuser;

GRANT SELECT ANY TABLE TO kuser;

GRANT INSERT ANY TABLE TO kuser;

GRANT UPDATE on client TO kuser;

grant execute on system.getUserByLogin to kuser;

CREATE OR REPLACE PROCEDURE getUserByLogin(
       p__userlogin IN system.client.login%TYPE,
       o__user_id OUT system.client.client_id%TYPE,
       o__user_passwordhash OUT system.client.password_hash%TYPE,
       o__user_lastname OUT system.client.lastname%TYPE,
       o__user_firstname OUT system.client.firstname%TYPE,
       o__user_patronimic OUT system.client.patronimic%TYPE,
       o__user_telephone OUT system.client.telephone%TYPE,
       o__bday OUT system.client. bday%TYPE
       )
IS
BEGIN
  SELECT client_id, password_hash , lastname, firstname, patronimic, telephone, bday
  INTO o__user_id, o__user_passwordhash, o__user_lastname, o__user_firstname,  o__user_patronimic, o__user_telephone , o__bday
  from  system.client WHERE login = p__userlogin;

END;
commit;
--drop procedure getuserbylogin;
exec getUserByLogin('Login');