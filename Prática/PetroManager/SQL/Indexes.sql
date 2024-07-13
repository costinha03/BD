
CREATE INDEX idx_Cliente_NIF ON PM.Cliente(NIF);
CREATE INDEX idx_Cliente_Nome ON PM.Cliente(Nome);
CREATE INDEX idx_Cliente_Contacto ON PM.Cliente(Contacto);
CREATE INDEX idx_Cliente_Idade ON PM.Cliente(Idade);


CREATE UNIQUE INDEX idx_Combustivel_ID ON PM.Combustivel(ID);
CREATE INDEX idx_Combustivel_Nome ON PM.Combustivel(Nome);


CREATE UNIQUE INDEX idx_Deposito_ID ON PM.Deposito(ID);
CREATE INDEX idx_Deposito_ID_Posto ON PM.Deposito(ID_Posto);
CREATE INDEX idx_Deposito_CapacidadeMax ON PM.Deposito(CapacidadeMax);
CREATE INDEX idx_Deposito_CapacidadeAtual ON PM.Deposito(CapacidadeAtual);


CREATE UNIQUE INDEX idx_Bomba_ID ON PM.Bomba(ID);
CREATE INDEX idx_Bomba_IDCombustivel ON PM.Bomba(IDCombustivel);
CREATE INDEX idx_Bomba_IDDeposito ON PM.Bomba(IDDeposito);
CREATE INDEX idx_Bomba_PostoID ON PM.Bomba(PostoID);


CREATE INDEX idx_Precario_CombustivelID ON PM.Precario(CombustivelID);
CREATE INDEX idx_Precario_DataInicio ON PM.Precario(DataInicio);
CREATE INDEX idx_Precario_Preco ON PM.Precario(Preco);
CREATE INDEX idx_Precario_DataFim ON PM.Precario(DataFim);


CREATE INDEX idx_Transacao_TransData ON PM.Transacao(TransData);
CREATE INDEX idx_Transacao_IDCombustivel ON PM.Transacao(IDCombustivel);
CREATE INDEX idx_Transacao_NIFCliente ON PM.Transacao(NIFCliente);
CREATE INDEX idx_Transacao_IDFunc ON PM.Transacao(IDFunc);
CREATE INDEX idx_Transacao_Litros ON PM.Transacao(Litros);
CREATE INDEX idx_Transacao_Quantia ON PM.Transacao(Quantia);



CREATE INDEX idx_Posto_ID ON PM.Posto(ID);
CREATE INDEX idx_Posto_Cidade ON PM.Posto(Cidade);
CREATE INDEX idx_Posto_Contacto ON PM.Posto(Contacto);
CREATE INDEX idx_Posto_Hora_Abertura ON PM.Posto(Hora_Abertura);
CREATE INDEX idx_Posto_Hora_Fecho ON PM.Posto(Hora_Fecho);
CREATE INDEX idx_Posto_MgrID ON PM.Posto(MgrID);


CREATE UNIQUE INDEX idx_Funcionario_ID ON PM.Funcionario(ID);
CREATE INDEX idx_Funcionario_ID_Posto ON PM.Funcionario(ID_Posto);
CREATE INDEX idx_Funcionario_Nome ON PM.Funcionario(Nome);
CREATE INDEX idx_Funcionario_Salario ON PM.Funcionario(Salario);
CREATE INDEX idx_Funcionario_Contacto ON PM.Funcionario(Contacto);
CREATE INDEX idx_Funcionario_Email ON PM.Funcionario(Email);


