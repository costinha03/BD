
use PetroManager;

drop view IF exists clientsID ;

CREATE VIEW PM.clientsID AS
SELECT 
    * 
FROM 
    Cliente;


drop view if exists PostosList	;


CREATE VIEW PM.PostosList AS
SELECT 
    *
	FROM 
    Posto;


DROP VIEW IF EXISTS TransacoesID;

CREATE VIEW PM.TransacoesID AS
SELECT 
    ID
	FROM
    Transacao;

drop view if exists PM.managerList;

CREATE VIEW PM.managerList AS
SELECT f.Nome AS 'Nome', f.Email AS 'Email',f.Contacto as 'Contacto',f.ID_Posto as 'PostoID', p.Cidade AS 'Cidade',p.Contacto as 'PostoContacto', p.Hora_Abertura as 'Abertura', p.Hora_Fecho as 'Fecho'
FROM Funcionario f
INNER JOIN Posto p ON f.ID = p.MgrID;


drop view if exists capacidadesDepositos

CREATE VIEW PM.capacidadesDepositos AS
SELECT
    p.ID AS Posto,
	d.ID AS IDDeposito,
    d.CapacidadeMax AS CapacidadeMaxima,
    d.CapacidadeAtual AS CapacidadeAtual,
    c.Nome AS Combustivel
FROM Posto p
LEFT JOIN Deposito d ON p.ID = d.id_posto
LEFT JOIN Bomba b ON d.ID = b.IDDeposito
LEFT JOIN Combustivel c ON b.IDCombustivel = c.ID;


CREATE VIEW PM.lastIDFunc AS 
SELECT
	MAX(ID) AS 'LastID' from PM.Funcionario


select * from PM.lastIDFunc


CREATE VIEW PM.lastIDPosto AS 
SELECT
	MAX(ID) AS 'LastID' from PM.Posto

CREATE VIEW PM.LastIDTransacao AS
SELECT
	MAX(ID) AS 'LastID' from PM.Transacao



CREATE VIEW PM.managersID AS
SELECT 
		p.ID as 'PID',
		f.ID as 'FID' 
	from Funcionario f
INNER JOIN Posto p ON f.ID = p.MgrID;



drop view if exists PM.FuncDisplay

CREATE VIEW PM.FuncDisplay AS
SELECT
  f.ID AS 'ID',
  f.Nome AS 'Nome',
  f.Contacto AS 'Contacto',
  f.Email AS 'Email',
  f.Salario AS 'Salario',
  p.ID AS 'PID',
  p.Cidade AS 'PCidade'
FROM PM.Funcionario f
INNER JOIN PM.Posto p ON f.ID_Posto = p.ID

drop view if exists PM.CityID;

CREATE VIEW PM.CityID AS

SELECT ID, Cidade from Posto


select * from PM.CityID


CREATE VIEW PM.CustomerTotalSpent AS
SELECT c.NIF AS Customer_NIF, SUM(t.Quantia) AS Total_Spent
FROM PM.Cliente c
JOIN PM.Transacao t ON c.NIF = t.NIFCliente
GROUP BY c.NIF, c.Nome;


DROP VIEW PM.TransacaoDetalhada

CREATE VIEW PM.TransacaoDetalhada AS
SELECT 
    t.ID AS Transacao_ID,
    t.TransData AS Data,
    t.IDCombustivel AS Combustivel_ID,
    t.Litros AS Quantidade,
    p.Preco AS Preco_Combustivel,
    t.Quantia AS Total_Compra,
    c.Nome AS Cliente_Nome,
    c.Contacto AS Cliente_Contacto,
    c.NIF AS Cliente_NIF,
    f.Nome AS Funcionario_Nome,
	f.ID AS Funcionario_ID
FROM 
    PM.Transacao t
JOIN 
    PM.Cliente c ON t.NIFCliente = c.NIF
JOIN 
    PM.Funcionario f ON t.IDFunc = f.ID
JOIN 
    PM.Precario p ON t.IDCombustivel = p.CombustivelID 
    AND t.TransData BETWEEN p.DataInicio AND p.DataFim;


