drop function if exists PM.FilterFuncByCity
drop function if exists PM.FilterFuncByName

CREATE FUNCTION PM.FilterFuncByCity
(
    @Cidade NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM PM.FuncDisplay
    WHERE PCidade LIKE '%' + @Cidade + '%'
);


CREATE FUNCTION PM.FilterFuncByName
(
    @Nome NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM PM.FuncDisplay
    WHERE Nome LIKE '%' + @Nome + '%'
);

CREATE FUNCTION PM.FilterClienteByName
(
    @Nome NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM PM.Cliente
    WHERE Nome LIKE '%' + @Nome + '%'
);

CREATE FUNCTION PM.FilterTransacaoByFuncName
(
    @Nome NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM PM.TransacaoDetalhada
    WHERE Funcionario_Nome LIKE '%' + @Nome + '%'
);

CREATE FUNCTION PM.FilterTransacaoByClienteName
(
    @Nome NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM PM.TransacaoDetalhada
    WHERE Cliente_Nome LIKE '%' + @Nome + '%'
);
