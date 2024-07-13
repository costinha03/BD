create table medicamentos.Medico(
    ID_SNS             int not null primary key,
    nome               varchar(100) not null,
    especialidade      varchar(100) not null
);

create table medicamentos.Paciente(
    numUtente           int not null primary key,
    nome                varchar(100) not null,
    dataNascimento      date not null,
    endereco            varchar(100) not null        
);

create table medicamentos.Farmacia(
    NIF                 int not null primary key,
    nome                varchar(100) not null,
    endereco            varchar(100) not null,
    telefone            varchar(20) not null
);

create table medicamentos.Farmaceutica(
    numRegisto          int not null primary key,
    nome                varchar(100) not null,
    morada              varchar(100) not null,
    telefone            varchar(20) not null
);

create table medicamentos.Farmaco(
    nomeComercial               varchar(100) not null primary key,
    formula                     varchar(100) not null,
    numRegistoFarmaceutica      int not null,
    foreign key (numRegistoFarmaceutica) references medicamentos.Farmaceutica(numRegisto)
);

create table medicamentos.Prescricao(
    ID                 int not null primary key,
    NIFfarmacia        int not null,
    ID_SNSmedico       int not null,
    numUtentepaciente  int not null,
    foreign key (NIFfarmacia) references medicamentos.Farmacia(NIF),
    foreign key (ID_SNSmedico) references medicamentos.Medico(ID_SNS),
    foreign key (numUtentepaciente) references medicamentos.Paciente(numUtente)
);

create table medicamentos.Contem(
    nomeComercialFarmaco    varchar(100) not null,
    ID_Prescricao           int not null,
    foreign key (nomeComercialFarmaco) references medicamentos.Farmaco(nomeComercial),
    foreign key (ID_Prescricao) references medicamentos.Prescricao(ID)
);
