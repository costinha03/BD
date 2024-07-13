create table Encomendas.TipoFornecedor(
    codigo          int primary key not null,
    designação      varchar(100) not null  

);

create table Encomendas.Fornecedor(
    nif                 int primary key not null,
    nome                varchar(100) not null,
    condicoesPagamento  varchar(100) not null,
    fax                 int not null,
    endereço            varchar(100) not null,
    codigo              int not null,
    foreign key (codigo) references Encomendas.TipoFornecedor(codigo)
);

create table Encomendas.Encomenda(
    numero                        int primary key not null,
    data                          datetime not null,
    nifFornecedor                 int not null,
    foreign key (nifFornecedor) references Encomendas.Fornecedor(nif)

);

create table Encomendas.Produto(
    codigo          int primary key not null,
    nome            varchar(100) not null,
    preco           int not null,
    stock           int not null,
    numero          int not null,
    foreign key (numero) references Encomendas.Encomenda(numero)
);