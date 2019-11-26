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
    cost_per_station decimal(7,2) not null,
    CONSTRAINT train_pk PRIMARY KEY (train_id)
);

insert into train (train_number, count_seats, cost_per_station) values ('123', 32, 12.21);
insert into train (train_number, count_seats, cost_per_station) values ('731DB32', 10, 6.07);

commit;
create table Address
(
    address_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    town varchar(30),
    street varchar(30),
    house varchar(10),
    flat number,
    CONSTRAINT address_pk PRIMARY KEY (address_id)
)

insert into Address (town, street, house) values ('Minsk', 'Koshevogo', '13B');
insert into Address (town, street, house) values ('Minsk', 'Kuzapina', '33');
insert into Address (town, street, house) values ('Grodno', 'Talapieva', '11');

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
select * from system.trip;
select * from system.Trip inner join system.train on system.Trip.train_id = system.Train.train_id inner join system.station on system.Trip.start_station_id = system.station.station_id inner join system.station on system.Trip.end_station_id = system.station.station_id;

insert into Station (station_name, address_id) values ('Minsk South', 1);
insert into Station (station_name, address_id) values ('Minsk West', 2);
insert into Station (station_name, address_id) values ('Grodno Central', 3);
--select * from address;
select * from station;

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

--insert into trip(train_id, start_station_id, end_station_id) values (1, 1, 2);
insert into trip(train_id, start_station_id, end_station_id) values (1, 1, 2);
insert into trip(train_id, start_station_id, end_station_id) values (1, 2, 3);
insert into trip(train_id, start_station_id, end_station_id) values (1, 2, 1);

create table Train_stop
(
    train_stop_id  NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    --train_id number not null,
    arrival_time date not null,
    departure_time date not null,
    --stop_date date not null,
    station_id number not null,    
    trip_id number not null,            --����� ������� �� ��������
    --generated by default on null as identity
    FOREIGN KEY (trip_id)
    REFERENCES Trip (trip_id),
    FOREIGN KEY (station_id)
    REFERENCES Station (station_id),
    constraint train_stop_pk PRIMARY KEY(train_stop_id)
)
select * from system.trip;
insert into train_stop (arrival_time, departure_time, station_id, trip_id) values(TO_DATE('10-12-2019 15:45', 'DD-MM-YYYY HH24:MI'), TO_DATE('10-12-2019 15:54', 'DD-MM-YYYY HH24:MI'), 1, 5);
insert into train_stop (arrival_time, departure_time, station_id, trip_id) values(TO_DATE('10-12-2019 15:03', 'DD-MM-YYYY HH24:MI'), TO_DATE('10-12-2019 15:09', 'DD-MM-YYYY HH24:MI'), 2, 5);
insert into train_stop (arrival_time, departure_time, station_id, trip_id) values(TO_DATE('10-12-2019 12:01', 'DD-MM-YYYY HH24:MI'), TO_DATE('10-12-2019 12:05', 'DD-MM-YYYY HH24:MI'), 2, 4);
insert into train_stop (arrival_time, departure_time, station_id, trip_id) values(TO_DATE('10-12-2019 12:15', 'DD-MM-YYYY HH24:MI'), TO_DATE('10-12-2019 12:21', 'DD-MM-YYYY HH24:MI'), 1, 4);
insert into train_stop (arrival_time, departure_time, station_id, trip_id) values(TO_DATE('10-12-2019 16:14', 'DD-MM-YYYY HH24:MI'), TO_DATE('10-12-2019 16:45', 'DD-MM-YYYY HH24:MI'), 3, 4);
insert into train_stop (arrival_time, departure_time, station_id, trip_id) values(TO_DATE('30-11-2019 10:57', 'DD-MM-YYYY HH24:MI'), TO_DATE('30-11-2019 11:00', 'DD-MM-YYYY HH24:MI'), 1, 6);
insert into train_stop (arrival_time, departure_time, station_id, trip_id) values(TO_DATE('30-11-2019 10:02', 'DD-MM-YYYY HH24:MI'), TO_DATE('30-11-2019 10:05', 'DD-MM-YYYY HH24:MI'), 2, 6);

select * from trip;
drop table system.train_stop;
select * from train_stop inner join station on station.station_id = train_stop.station_id;

create table Ticket
(
    ticket_id NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    date_of_purchase date,
    date_of_trip date,
    client_id number not null,
    trip_id number not null,
    FOREIGN KEY (client_id)
    REFERENCES Client (client_id),
    FOREIGN KEY (trip_id)
    REFERENCES Trip (trip_id),
    CONSTRAINT ticket_pk PRIMARY KEY (ticket_id)
)
drop table ticket;
select * from system.Trip inner join system.Train on system.Trip.train_id = system.Train.train_id;
--select * from client;
insert into ticket(date_of_trip, client_id, trip_id) values (TO_DATE('30-11-2019 15:32', 'DD-MM-YYYY HH24:MI'), 6, 6);

create table Card
(
    card_id  NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    card_number varchar2(16) check(LENGTH(card_number) = 16),
    espiration_date varchar2(5) check(LENGTH(espiration_date) = 5),
    card_owner varchar2(30),
    CONSTRAINT card_pk PRIMARY KEY (card_id)
)
drop table card;
insert into Card (card_number, espiration_date, card_owner) values ('5351123412341234', '12/20', 'MASTERCARD PAYOKAY');

create table Occupation
(
    occupation_id  NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    name varchar(40) not null,
    cost_per_hour decimal(5, 2) not null,
    hours_per_week number not null,
    CONSTRAINT occupation_pk PRIMARY KEY (occupation_id)
)

insert into Occupation (name, cost_per_hour, hours_per_week) values ('Debugger', 12.43, 40);

create table Employee
(
    employee_id  NUMBER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
    card_id number not null,
    occupation_id number not null,
    client_id number not null,
    is_admin number(1) DEFAULT 0 NOT NULL
    CONSTRAINT is_admin_check CHECK (is_admin in (1,0)),
    FOREIGN KEY (client_id)
    REFERENCES Client (client_id),
    FOREIGN KEY (occupation_id)
    REFERENCES Occupation (occupation_id),
    FOREIGN KEY (card_id)
    REFERENCES Card (card_id),
    CONSTRAINT employee_pk PRIMARY KEY (employee_id)
)
--select * from client;

insert into Employee ( card_id, occupation_id, client_id, is_admin) values (1, 1, 2, 1);

commit;
drop table Employee;
drop table Occupation;
drop table Card;
drop table ticket;
drop table train_stop;
drop table trip;
drop table Station;
drop table Address;
drop table Train;
drop table client;


select * from Client;

alter session set "_ORACLE_SCRIPT"=true;

CREATE USER kuser IDENTIFIED BY qwe123qwe;

GRANT CONNECT TO kuser;

GRANT SELECT ANY TABLE TO c##admin_user;

GRANT INSERT ANY TABLE TO c##admin_user;

GRANT UPDATE on client TO c##admin_user;

GRANT UPDATE on system.client TO c##admin_user;
GRANT insert on system.client TO c##admin_user;
grant select on system.address to c##admin_user;
grant select on system.train_stop to c##admin_user;
grant select on system.train to c##admin_user;
grant select on system.trip to c##admin_user;
grant select on system.station to c##admin_user;
grant insert on system.ticket to c##admin_user;

grant execute on system.getUserByLogin to c##kuser;
grant execute on system.getTrip to c##kuser;

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

select * from station;

describe train

CREATE OR REPLACE PROCEDURE getTrip(
       in_st_name1 IN system.station.station_name%TYPE,
       in_st_name2 IN system.station.station_name%TYPE,
       out_trip_id OUT system.trip.trip_id%TYPE,
       out_station_id1 OUT system.station.station_id%TYPE,
       out_station_name1 OUT system.station.station_name%TYPE,
       out_station_id2 OUT system.station.station_id%TYPE,
       out_station_name2 OUT system.station.station_name%TYPE,
       out_train_id OUT system.train.train_id%TYPE,
       out_train_number OUT system.train.train_number%TYPE,
       out_count_seats OUT system.train.count_seats%TYPE,
       out_cost_per_station OUT system.train.cost_per_station%TYPE
       )
IS
BEGIN
select trip_id, s1.station_id, s1.station_name, s2.station_id, s2.station_name, t.train_id, train_number, count_seats, cost_per_station 
        into out_trip_id, out_station_id1, out_station_name1, out_station_id2, out_station_name2, out_train_id, out_train_number, out_count_seats, out_cost_per_station
        from system.Trip tr 
            inner join system.train t on tr.train_id = t.train_id inner join system.station s1 on tr.start_station_id = s1.station_id 
            inner join system.station s2 on tr.end_station_id = s2.station_id 
                WHERE EXISTS(SELECT * 
                  FROM system.train_stop
               WHERE tr.trip_id = system.train_stop.trip_id and system.train_stop.station_id = in_st_name1)
               and exists(SELECT *
                  FROM system.train_stop
               WHERE tr.trip_id = system.train_stop.trip_id and system.train_stop.station_id = in_st_name2);
END;


commit;
--drop procedure gettrip;
exec getUserByLogin('Login');
exec getTrip('Minsk South', 'Minsk West');

SELECT *
   FROM system.trip
WHERE EXISTS (SELECT *
                 FROM system.train_stop
              WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = 1)
              and exists (SELECT *
                 FROM system.train_stop
              WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = 2);
              
select * from system.Trip inner join system.train on system.Trip.train_id = system.Train.train_id inner join system.station on system.Trip.start_station_id = system.station.station_id inner join system.station on system.Trip.end_station_id = system.station.station_id WHERE EXISTS(SELECT * 
                  FROM system.train_stop
               WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = 1)
               and exists(SELECT *
                  FROM system.train_stop
               WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = 2)  ;

select * from system.trip;
select * from system.train_stop t1 inner join system.train_stop t2 on t1.trip_id = t2.trip_id where t1.station_id = 1 and t2.station_id = 2 and t1.departure_time < t2.departure_time;           
               
select * from system.train_stop where trip_id = 2;


SELECT parameter,value
FROM gv$OPTION
WHERE PARAMETER = 'Oracle Database Vault';


select trim(TO_CHAR(arrival_time, 'dd.mm.yyyy')) from system.train_stop inner join system.station on system.station.station_id = system.train_stop.station_id;


select * from system.train_stop inner join system.station on system.station.station_id = system.train_stop.station_id where '10-12-2019' = trim(TO_CHAR(arrival_time, 'dd-mm-yyyy')) and trip_id = 2;
select * from system.train_stop inner join system.station on system.station.station_id = system.train_stop.station_id where '10-12-2019' = ltrim(TO_CHAR(arrival_time,'dd-mm-yyyy')) and trip_id = 23;

select * from system.train_stop;
select * from system.trip where trip_id = (select trip_id from system.train_stop where station_id = 1) intersect select * from system.trip where trip_id = (select trip_id from system.train_stop where station_id = 2) ;
