create table conferencias.instituicao (
nome varchar(50),
endereco varchar(50),

primary key(nome),
);

create table conferencias.autor (
email varchar(50),
nome varchar(50),
nome_instituicao varchar(50),

primary key(nome),
foreign key(nome_instituicao) references conferencias.instituicao(nome)
);

create table conferencias.artigo (
    
numero_registo int,
titulo varchar(50),

primary key(numero_registo),

);

create table conferencias.participante (
email varchar(50),
nome varchar(50),
data_inscricao date,
morada varchar(50),
nome_instituicao varchar(50),

primary key(nome),
foreign key(nome_instituicao) references conferencias.instituicao(nome)
);

create table conferencias.naoestudante (
nome varchar(50),
nomeinstituicao varchar(50),
referencias int,

primary key(nome),
foreign key(nomeinstituicao) references conferencias.instituicao(nome)
);

create table conferencias.estudante (
nome varchar(50),
nome_instituicao varchar(50),

foreign key(nome_instituicao) references conferencias.instituicao(nome)
);

create table conferencias.tem(
nome varchar(50),
numero_registo int,
primary key (nome, numero_registo),
foreign key (nome) references conferencias.autor(nome),
foreign key (numero_registo) references conferencias.artigo(numero_registo)
);
