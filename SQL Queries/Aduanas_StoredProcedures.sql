-------------- Stored Proc for INSERT
CREATE PROCEDURE spAduanas_Insert
--Parameters for Insert stored procedure
@AduanasID bigint,
@NombreAduana bigint,
@AbreviacionAduana bigint
AS
BEGIN
--SQL for Insert stored procedure
INSERT INTO Aduanas(AduanasID, NombreAduana, AbreviacionAduana) VALUES (@AduanasID, @NombreAduana, @AbreviacionAduana)
END

--------------  Stored Proc for SELECT (LIST, just first six fields but you can change in final code.)
CREATE PROCEDURE spAduanas_List
--No parameters required.
AS
BEGIN
--SQL for Select stored procedure.
SELECT * FROM Aduanas ORDER BY Id DESC
END

--------------  Stored Proc for SELECT (one)
CREATE PROCEDURE spAduanas_GetOne
-- Needs one parameter for primary key
@Id int
AS 
BEGIN
-- SQL Select for one table row
SELECT Id, AduanasID, NombreAduana, AbreviacionAduana FROM Aduanas WHERE Id= @Id
END


--------------  Stored Proc for UPDATE
CREATE PROCEDURE spAduanas_Update
-- Parameters for Update stored procedure.
@Id bigint,
@AduanasID bigint,
@NombreAduana bigint,
@AbreviacionAduana bigint
AS
BEGIN
-- SQL for Update stored procedure
UPDATE Aduanas SET AduanasID = @AduanasID, NombreAduana = @NombreAduana, AbreviacionAduana = @AbreviacionAduana WHERE Id = @Id
END