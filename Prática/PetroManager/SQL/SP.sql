drop procedure if exists  PM.RegistarNovoCliente;

CREATE PROCEDURE PM.RegistarNovoCliente (
    @NIF INT,
    @Nome VARCHAR(25),
    @Contacto INT,
    @Idade INT
)
AS
BEGIN
    -- Verifica se o NIF est� dentro do intervalo permitido
    IF @NIF < 99999999 OR @NIF > 999999999
    BEGIN
        RAISERROR('NIF fora do intervalo permitido (deve estar entre 99999999 e 999999999)', 16, 1);
        RETURN;
    END

	 IF @Idade < 18
    BEGIN
        RAISERROR('O cliente deve ser maior de idade', 16, 1);
        RETURN;
    END

    -- Verifica se o contato est� dentro do intervalo permitido
    IF @Contacto < 899999999 OR @Contacto > 999999999
    BEGIN
        RAISERROR('Contato fora do intervalo permitido (deve estar entre 899999999 e 999999999)', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Cliente WHERE NIF = @NIF)
    BEGIN
        RAISERROR('Cliente j� registado', 16, 1);
        RETURN;
    END

    INSERT INTO Cliente (NIF, Nome, Contacto, Idade)
    VALUES (@NIF, @Nome, @Contacto, @Idade);

    PRINT 'Cliente registado com sucesso';
END;

drop procedure PM.UpdateCliente

CREATE PROCEDURE PM.UpdateCliente (
    @NIF INT,
    @Nome VARCHAR(25),
    @Contacto INT,
    @Idade INT
)
AS
BEGIN
    -- Verifica se o NIF est� dentro do intervalo permitido
    IF @NIF < 99999999 OR @NIF > 999999999
    BEGIN
        RAISERROR('NIF fora do intervalo permitido (deve estar entre 99999999 e 999999999)', 16, 1);
        RETURN;
    END

    -- Verifica se o contato est� dentro do intervalo permitido
    IF @Contacto < 899999999 OR @Contacto > 999999999
    BEGIN
        RAISERROR('Contato fora do intervalo permitido (deve estar entre 899999999 e 999999999)', 16, 1);
        RETURN;
    END

	    IF @Idade < 18
    BEGIN
        RAISERROR('O cliente deve ser maior de idade', 16, 1);
        RETURN;
    END

    UPDATE Cliente
    SET Nome = @Nome, Contacto = @Contacto, Idade = @Idade
    WHERE NIF = @NIF;

    PRINT 'Cliente atualizado com sucesso';
END;

CREATE PROCEDURE PM.DeleteCliente
    @NIF INT
AS
BEGIN

    BEGIN TRANSACTION;

    BEGIN TRY

        DELETE FROM Transacao
        WHERE NIFCliente = @NIF;

       DELETE FROM Cliente
	   WHERE NIF = @NIF
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;


drop procedure if exists updatePrice;

GO
CREATE PROCEDURE PM.updatePrice (
    @CombustivelID INT,
    @NovoPreco FLOAT,
    @DataInicio DATE,
    @DataFim DATE
)
AS
BEGIN
    UPDATE Precario
    SET DataFim = GETDATE()
    WHERE CombustivelID = @CombustivelID AND DataFim IS NULL;

    INSERT INTO Precario (CombustivelID, Preco, DataInicio, DataFim)
    VALUES (@CombustivelID, @NovoPreco, @DataInicio, @DataFim);

    PRINT 'Pre�o do combust�vel atualizado com sucesso';
END;
GO

drop procedure if exists PM.AdicionarPosto;


CREATE PROCEDURE PM.AdicionarPosto (
    @p_ID INT,
    @p_Cidade VARCHAR(50),
    @p_Contacto INT,
    @p_Hora_Abertura TIME,
    @p_Hora_Fecho TIME,
    @p_MgrID INT
)
AS
BEGIN
    -- Verifica��es dos campos
    IF @p_ID IS NULL
    BEGIN
        THROW 50000, 'ID n�o pode ser nulo', 1;
    END;
    
    IF @p_Cidade IS NULL OR LEN(@p_Cidade) = 0
    BEGIN
        THROW 50000, 'Cidade n�o pode ser nula ou vazia', 1;
    END;

    IF @p_Contacto IS NULL OR @Contacto < 200000000 OR @Contacto > 299999999
    BEGIN
        THROW 50000, 'Contacto inv�lido', 1;
    END;

    IF @p_Hora_Abertura IS NULL
    BEGIN
        THROW 50000, 'Hora de Abertura n�o pode ser nula', 1;
    END;

    IF @p_Hora_Fecho IS NULL
    BEGIN
        THROW 50000, 'Hora de Fecho n�o pode ser nula', 1;
    END;

    IF @p_MgrID IS NULL
    BEGIN
        THROW 50000, 'MgrID n�o pode ser nulo', 1;
    END;

    -- Verifica��o de valores espec�ficos (exemplo: hor�rio de abertura e fechamento)
    IF @p_Hora_Abertura < '00:00:00' OR @p_Hora_Abertura > '23:59:59'
    BEGIN
        THROW 50000, 'Hora de Abertura inv�lida', 1;
    END;
    
    IF @p_Hora_Fecho < '00:00:00' OR @p_Hora_Fecho > '23:59:59'
    BEGIN
        THROW 50000, 'Hora de Fecho inv�lida', 1;
    END;

    -- Verificar se o ID do funcion�rio existe na tabela PM.Funcionario
    IF (SELECT COUNT(*) FROM PM.Funcionario WHERE ID = @p_MgrID) = 0
    BEGIN
        THROW 50000, 'MgrID n�o existe na tabela Funcionario', 1;
    END;

    -- Inserir o novo posto se todas as verifica��es passarem
    INSERT INTO PM.Posto (ID, Cidade, Contacto, Hora_Abertura, Hora_Fecho, MgrID)
    VALUES (@p_ID, @p_Cidade, @p_Contacto, @p_Hora_Abertura, @p_Hora_Fecho, @p_MgrID);

    -- Atualizar a tabela Funcionario com o novo ID do posto
    UPDATE PM.Funcionario
    SET PostoID = @p_ID
    WHERE ID = @p_MgrID;
END

CREATE PROCEDURE PM.UpdatePostoDetails 
    @p_ID INT,
    @p_Cidade NVARCHAR(50),
    @p_Contacto INT,
    @p_Hora_Abertura TIME,
    @p_Hora_Fecho TIME,
    @p_MgrID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Atualizar informa��es do posto na tabela Posto
        UPDATE PM.Posto 
        SET 
            Cidade = @p_Cidade, 
            Contacto = @p_Contacto, 
            Hora_Abertura = @p_Hora_Abertura, 
            Hora_Fecho = @p_Hora_Fecho,
            MgrID = @p_MgrID
        WHERE ID = @p_ID;

        COMMIT TRANSACTION;
        PRINT 'Update successful';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Error occurred during update';
    END CATCH
END;

drop procedure PM.DeletePosto
CREATE PROCEDURE PM.DeletePosto
    @PostoID INT
AS
BEGIN
    -- In�cio da transa��o
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar transa��es associadas ao posto
        DELETE FROM Transacao
        WHERE IDFunc IN (SELECT ID FROM Funcionario WHERE ID_Posto = @PostoID);

        -- Eliminar funcion�rios associados ao posto
        DELETE FROM Funcionario
        WHERE ID_Posto = @PostoID;

        -- Eliminar bombas associadas ao posto
        DELETE FROM Bomba
        WHERE PostoID = @PostoID;

        -- Eliminar dep�sitos associados ao posto
        DELETE FROM Deposito
        WHERE ID_Posto = @PostoID;

        -- Eliminar o pr�prio posto
        DELETE FROM Posto
        WHERE ID = @PostoID;

        -- Se tudo correr bem, commit na transa��o
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Em caso de erro, rollback na transa��o
        ROLLBACK TRANSACTION;
        -- Lan�ar erro
        THROW;
    END CATCH
END;




CREATE PROCEDURE PM.DeleteFunc
    @FuncID INT
AS
BEGIN
    -- In�cio da transa��o
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar funcion�rios associados ao posto
        DELETE FROM Transacao
        WHERE IDFunc = @FuncID;

       DELETE FROM Funcionario
	   WHERE ID = @FuncID
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Em caso de erro, rollback na transa��o
        ROLLBACK TRANSACTION;
        -- Lan�ar erro
        THROW;
    END CATCH
END;

CREATE PROCEDURE PM.AddFunc
	@ID int,
    @Nome VARCHAR(25),
    @Contacto INT,
    @Email VARCHAR(50),
    @Salario FLOAT,
    @PID INT
AS
BEGIN
    INSERT INTO Funcionario (ID, Nome, Salario, Contacto, Email, ID_Posto)
    VALUES (@ID, @Nome, @Salario, @Contacto, @Email, @PID);
END;


CREATE PROCEDURE PM.UpdateFunc
	@ID int,
    @Nome VARCHAR(25),
    @Contacto INT,
    @Email VARCHAR(50),
    @Salario FLOAT,
    @PID INT
AS
BEGIN
    UPDATE Funcionario
    SET Nome = @Nome,
        Contacto = @Contacto,
        Email = @Email,
        Salario = @Salario,
        ID_Posto = @PID
    WHERE ID = @ID;
END;


CREATE PROCEDURE PM.UpdateTransacao
    @TransacaoID INT,
    @TransData DATE,
    @Litros INT,
    @IDCombustivel INT,
    @Quantia FLOAT,
    @NIFCliente INT,
    @IDFunc INT
AS
BEGIN
    -- Validate NIFCliente
    IF NOT EXISTS (SELECT 1 FROM PM.Cliente WHERE NIF = @NIFCliente)
    BEGIN
        SELECT 'Erro: Cliente com o NIF especificado n�o existe.' AS Message;
        RETURN;
    END

    -- Validate IDFunc
    IF NOT EXISTS (SELECT 1 FROM PM.Funcionario WHERE ID = @IDFunc)
    BEGIN
        SELECT 'Erro: Funcion�rio com o ID especificado n�o existe.' AS Message;
        RETURN;
    END

    UPDATE PM.Transacao
    SET TransData = @TransData,
        Litros = @Litros,
        IDCombustivel = @IDCombustivel,
        Quantia = @Quantia,
        NIFCliente = @NIFCliente,
        IDFunc = @IDFunc
    WHERE ID = @TransacaoID;

    SELECT 'Transa��o atualizada com sucesso.' AS Message;
END;

drop Procedure PM.AddTransacao

CREATE PROCEDURE PM.AddTransacao
    @TransData DATE,
    @Litros INT,
    @IDCombustivel INT,
    @Quantia FLOAT,
    @NIFCliente INT,
    @IDFunc INT
AS
BEGIN
	SET @quantia = ROUND(@quantia, 2);
    -- Validate NIFCliente
    IF NOT EXISTS (SELECT 1 FROM PM.Cliente WHERE NIF = @NIFCliente)
    BEGIN
        SELECT 'Erro: Cliente com o NIF especificado n�o existe.' AS Message;
        RETURN;
    END

    -- Validate IDFunc
    IF NOT EXISTS (SELECT 1 FROM PM.Funcionario WHERE ID = @IDFunc)
    BEGIN
        SELECT 'Erro: Funcion�rio com o ID especificado n�o existe.' AS Message;
        RETURN;
    END

    INSERT INTO PM.Transacao (TransData, Litros, IDCombustivel, Quantia, NIFCliente, IDFunc)
    VALUES (@TransData, @Litros, @IDCombustivel, @Quantia, @NIFCliente, @IDFunc);

    SELECT 'Transa��o adicionada com sucesso.' AS Message;
END;

CREATE PROCEDURE PM.DeleteTransacao
    @TransacaoID INT
AS
BEGIN
    -- Check if the transaction exists
    IF NOT EXISTS (SELECT 1 FROM PM.Transacao WHERE ID = @TransacaoID)
    BEGIN
        RAISERROR ('Transa��o n�o encontrada', 16, 1);
        RETURN;
    END

    DELETE FROM PM.Transacao
    WHERE ID = @TransacaoID;
END;
