CREATE TRIGGER trg_DeleteFuncBeforeDelete
ON PM.Funcionario
FOR DELETE
AS
BEGIN
    DECLARE @funcID INT;

    SELECT @funcID = ID FROM deleted; 

    IF @funcID IS NOT NULL
    BEGIN
        DELETE FROM PM.Transacao
        WHERE IDFunc = @funcID;
    END
END;


