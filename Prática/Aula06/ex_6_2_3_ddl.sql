create table Prescricao.medico(
    numsns              int         primary key,
    nome                varchar(45) not null,
    especialidade       varchar(45),                      
);

create table Prescricao.paciente(
    numutente           int         primary key,
    nome                varchar(45) not null,
    datanasc            date        not null,
    endereco            text,
);

create table Prescricao.farmacia(
    nome                varchar(45) primary key,
    telefone            int         unique,
    endereco            text,                      
);

create table Prescricao.farmaceutica(
    numreg              int         primary key,
    nome                varchar(45),
    endereco            text,                      
);

create table Prescricao.farmaco(
    numregfarm          int,
    nome                varchar(45),
    formula             text,                      
    primary key (numregfarm, nome),
    foreign key (numregfarm) references prescricao.farmaceutica(numreg) on delete no action on update cascade
);

create table Prescricao.prescricao(
    numpresc            int         primary key,
    numutente           int         not null references prescricao.paciente(numutente) on delete no action on update cascade,
    nummedico           int         not null references prescricao.medico(numsns) on delete no action on update cascade,
    farmacia            varchar(45) references prescricao.farmacia(nome) on delete no action on update cascade,
    dataproc            date,
);

create table Prescricao.presc_farmaco(
    numpresc            int references prescricao.prescricao(numpresc) on delete no action on update cascade,
    numregfarm          int,
    nomefarmaco         varchar(45),
    foreign key (numregfarm, nomefarmaco) references prescricao.farmaco(numregfarm, nome) on delete no action on update cascade,
    primary key (numpresc, numregfarm, nomefarmaco)
);
