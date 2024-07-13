

ALTER TABLE PM.Transacao DROP CONSTRAINT IF EXISTS fk_Transacao_Cliente;
ALTER TABLE PM.Transacao DROP CONSTRAINT IF EXISTS fk_Transacao_Bomba;
ALTER TABLE PM.Transacao DROP CONSTRAINT IF EXISTS fk_Transacao_Funcionario;

ALTER TABLE PM.Precario DROP CONSTRAINT IF EXISTS fk_Precario_Combustivel;

ALTER TABLE PM.Bomba DROP CONSTRAINT IF EXISTS fk_Bomba_Combustivel;
ALTER TABLE PM.Bomba DROP CONSTRAINT IF EXISTS fk_Bomba_Deposito;

ALTER TABLE PM.Deposito DROP CONSTRAINT IF EXISTS fk_Deposito_Posto;

ALTER TABLE PM.Funcionario DROP CONSTRAINT IF EXISTS fk_Funcionario_Posto;

ALTER TABLE PM.Posto DROP CONSTRAINT IF EXISTS fk_Posto_Manager;

DROP TABLE IF EXISTS PM.Transacao;
DROP TABLE IF EXISTS PM.Precario;
DROP TABLE IF EXISTS PM.Bomba;
DROP TABLE IF EXISTS PM.Deposito;
DROP TABLE IF EXISTS PM.Combustivel;
DROP TABLE IF EXISTS PM.Cliente;
DROP TABLE IF EXISTS PM.Funcionario;
DROP TABLE IF EXISTS PM.Posto;

CREATE TABLE PM.Posto (
    ID INT PRIMARY KEY NOT NULL,
    Cidade VARCHAR(50),
    Contacto INT,
    Hora_Abertura TIME,
    Hora_Fecho TIME,
    MgrID INT
);

CREATE TABLE PM.Funcionario(
    ID INT PRIMARY KEY NOT NULL,
    Nome VARCHAR(25) NOT NULL,
    Salario FLOAT,
    Contacto INT,
    Email VARCHAR(50),
    ID_Posto INT
);

CREATE TABLE PM.Cliente(
    NIF INT PRIMARY KEY,
    Nome VARCHAR(25) NOT NULL,
    Contacto INT,
    Idade INT
);

CREATE TABLE PM.Combustivel(
    ID INT PRIMARY KEY NOT NULL,
    Nome VARCHAR(9) NOT NULL CHECK (Nome IN('Diesel','Gasolina','GPL'))
);

CREATE TABLE PM.Deposito(
    ID INT PRIMARY KEY NOT NULL,
    CapacidadeMax INT NOT NULL,
    CapacidadeAtual INT,
    ID_Posto INT
);

CREATE TABLE PM.Bomba(
    ID INT PRIMARY KEY NOT NULL,
    IDCombustivel INT,
    IDDeposito INT,
	PostoID int
);

CREATE TABLE PM.Precario(
    CombustivelID INT NOT NULL,
    Preco FLOAT,
    DataInicio DATE,
    DataFim DATE,
    PRIMARY KEY (CombustivelID, DataInicio)
);

CREATE TABLE PM.Transacao(
    ID INT  IDENTITY (1,1) PRIMARY KEY,
    TransData DATE,
    Litros INT,
    IDCombustivel INT,
    Quantia FLOAT,
    NIFCliente INT,
    IDFunc INT
);



ALTER TABLE PM.Deposito
    ADD CONSTRAINT fk_Deposito_Posto FOREIGN KEY (ID_Posto)
    REFERENCES PM.Posto(ID) ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE PM.Bomba
    ADD CONSTRAINT fk_Bomba_Combustivel FOREIGN KEY (IDCombustivel)
    REFERENCES PM.Combustivel(ID) ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE PM.Bomba
    ADD CONSTRAINT fk_Bomba_Deposito FOREIGN KEY (IDDeposito)
    REFERENCES PM.Deposito(ID) ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE PM.Bomba
	ADD CONSTRAINT fk_Bomba_PostoID FOREIGN KEY (PostoID) 
	REFERENCES PM.Posto(ID);

ALTER TABLE PM.Precario
    ADD CONSTRAINT fk_Precario_Combustivel FOREIGN KEY (CombustivelID)
    REFERENCES PM.Combustivel(ID) ON UPDATE CASCADE;

ALTER TABLE PM.Transacao
    ADD CONSTRAINT fk_Transacao_Cliente FOREIGN KEY (NIFCliente)
    REFERENCES PM.Cliente(NIF) ON UPDATE CASCADE;


ALTER TABLE PM.Transacao
    ADD CONSTRAINT fk_Transacao_Funcionario FOREIGN KEY (IDFunc)
    REFERENCES PM.Funcionario(ID) ON UPDATE CASCADE;


ALTER TABLE PM.Posto
    ADD CONSTRAINT fk_Posto_Manager FOREIGN KEY (MgrID)
    REFERENCES PM.Funcionario(ID) ON DELETE SET NULL ON UPDATE CASCADE;

