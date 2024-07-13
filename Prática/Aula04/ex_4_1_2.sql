create table airportmanager.Flight (
    airline     varchar(50) not null,
    number      int primary key not null,
    weekdays    VARCHAR(25) not null
);

create table airportmanager.Fare (
    code            varchar(10) not null,
    amount          decimal not null,
    restrictions    varchar(1000) not null,
    flight_number   int not null,
    foreign key (flight_number) references airportmanager.Flight(number)
);

create table airportmanager.Airport(
    code            int primary key not null,
    city            varchar(100) not null,
    airport_name    varchar(100) not null,
    airport_state   varchar(100) not null
);

create table airportmanager.FlightLeg(
    leg_no              int primary key not null,
    dep_time            datetime not null,
    arr_time            datetime not null,
    flight_no           int references airportmanager.Flight(number) not null,
    dep_airport_code    int references airportmanager.Airport(code) not null,
    arr_airport_code    int references airportmanager.Airport(code) not null
);

create table airportmanager.AirplaneType(
    typeName    varchar(25) primary key not null,
    maxSeats    int not null,
    company     varchar(50) not null
);

create table airportmanager.Airplane(
    ID                  int primary key not null,
    noSeats             int not null,
    typeName            varchar(25) not null,
    foreign key(typeName) references airportManager.AirplaneType(typeName)
);


create table airportmanager.LegInstance(
    legDate         datetime primary key not null,
    availableSeats  int not null, 
    depTime         datetime not null,
    arrTime         datetime not null,
    depAirportCode  int not null,
    arrAirportCode  int not null,
    flightNo        int not null,
    legNo           int not null,
    airplaneID      int not null,
    foreign key (depAirportCode) references airportManager.Airport(code),
    foreign key (arrAirportCode) references airportManager.Airport(code),
    foreign key (flightNo) references airportManager.Flight(number),
    foreign key (legNo) references airportManager.FlightLeg(leg_no),
    foreign key (airplaneID) references airportManager.Airplane(ID)
);

create table airportManager.Seat(
    seatNo      int primary key not null,
    costName    varchar(25) not null,
    costPhone   int not null,
    legDate     datetime not null,
    foreign key (legDate) references airportManager.LegInstance(legDate)
);
