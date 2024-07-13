create table RentACar.cliente(
NIF int not null primary key,
nome varchar(40),
endereco varchar(40),
num_carta int,
);
create table RentACar.aluguer(
numero int,
nif_cliente int,
numero_balcao int,
matricula_veiculo varchar(40) references RentACar.veiculo(matricula),
primary key (numero) not null,
foreign key	(nif_cliente) references RentACar.cliente(NIF),
foreign key (numero_balcao) references RentACar.balcao(numero)
);
drop table RentACar.veiculo
create table RentACar.balcao(
numero int not null primary key,
nome varchar(40),
endereco varchar(40),
);
create table RentACar.veiculo(
matricula varchar(40) not null primary key,
marca varchar(40),
ano int,
codigo_veiculo int references RentACar.tipoveiculo(codigo)
);



create table RentACar.tipoveiculo(
codigo int not null primary key,
ar_condicionado bit,
designacao varchar(50),

);

create table RentACar.similaridade(
similaridade1 int foreign key references RentACar.tipoveiculo(codigo),
similaridade2 int foreign key references RentACar.tipoveiculo(codigo),
primary key(similaridade1, similaridade2),

);

create table RentACar.ligeiro(
codigo_veiculo int not null primary key references RentACar.tipoveiculo(codigo),
combustivel varchar(50),
portas int,
numlugares int,
);

create table RentACar.pesados(
codigo_veiculo int not null primary key references RentACar.tipoveiculo(codigo),
peso int not null,
passageiros int not null,
);