create table StockManagement.tipo_fornecedor(
	codigo int not null check(codigo > 0),
	designacao varchar(20),
	primary key(codigo),
	unique(codigo)
);

create table StockManagement.fornecedor(
	nif int not null check(nif > 0),	
	nome varchar(15),
	fax int,	
	endereco varchar(40),
	condpag varchar(20),
	tipo int,
	primary key(nif),
	unique(nif),
	foreign key(tipo) references StockManagement.tipo_fornecedor(codigo)
);

create table StockManagement.produto(
	codigo int not null check(codigo > 0),	
	nome varchar(30) not null,
	preco smallmoney not null check(preco > 0),	
	iva int default 23,
	unidades int check(unidades > 0),
	primary key(codigo),
	unique(codigo)
);

create table StockManagement.encomenda(
	numero int not null check(numero > 0),	
	[data] date,
	fornecedor int,
	primary key(numero),
	unique(numero),
	foreign key(fornecedor) references StockManagement.fornecedor(nif)
);

create table StockManagement.item(
	numEnc int,	
	codProd int,
	unidades int,
	foreign key(numEnc) references StockManagement.encomenda(numero),
	foreign key(codProd) references StockManagement.produto(codigo)
);
