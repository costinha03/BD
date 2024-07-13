create table ATL.Pessoa(
    CC             int not null primary key,
    nome           varchar(100) not null,
    dataNasc       datetime not null,
    morada         varchar(100) not null,
    telefone       int not null,
    email          varchar(100) not null
);

create table ATL.Responsavel(
    CC             int not null primary key,
    foreign key (CC) references ATL.Pessoa(CC)
);

create table ATL.Professor(
    CC                  int not null primary key,
    numFuncionario      varchar(100) not null,
    foreign key (CC) references ATL.Pessoa(CC)
);

create table ATL.EncarregadoEducação(
    CC             int not null primary key,
    parentesco     varchar(100) not null,
    email          varchar(100) not null,
    foreign key (CC) references ATL.Pessoa(CC)
);

create table ATL.Turma(
    ID              int not null primary key,
    designacao      varchar(100) not null,
    anoLetivo       int not null,
    professor       int not null,
    maxAlunos       int not null
);

create table ATL.Aluno(
    CC              int not null primary key,
    IDturma         int not null,
    nome            varchar(100) not null,
    dataNasc        datetime not null,
    morada          varchar(100) not null,
    foreign key (CC) references ATL.Pessoa(CC),
    foreign key (IDturma) references ATL.Turma(ID)
);

create table ATL.Atividade(
    ID             int not null primary key,
    designacao     varchar(100) not null,
    custo          int not null
);

create table ATL.Participa(
    CC              int not null,
    IDatividade     int not null,
    dataInscricao   datetime not null,
    foreign key (CC) references ATL.Aluno(CC),
    foreign key (IDatividade) references ATL.Atividade(ID)
);

create table ATL.Levanta(
    CCresponsavel   int not null,
    CCaluno        int not null,
    foreign key (CCresponsavel) references ATL.Responsavel(CC),
    foreign key (CCaluno) references ATL.Aluno(CC)
);

create table ATL.Realiza(
    IDatividade    int not null,
    IDturma        int not null,
    foreign key (IDatividade) references ATL.Atividade(ID),
    foreign key (IDturma) references ATL.Turma(ID)
);