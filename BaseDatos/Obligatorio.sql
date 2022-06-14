
SET DATEFORMAT dmy

USE Master
GO

IF EXISTS (select * from sys.databases where name = 'Obligatorio')
begin
	DROP DATABASE Obligatorio
end
GO

CREATE DATABASE Obligatorio ON
(
	NAME=Obligatorio,
	FILENAME='D:\Descargas\Ob2\BaseDatos\Obligatorio.mdf'
)
GO

USE Obligatorio
GO

--TABLASs

CREATE TABLE Empleados
(
	NombreUsuario varchar(10) PRIMARY KEY CHECK(LEN(LTRIM(RTRIM(NombreUsuario))) = 10),
	Contraseña varchar(7) NOT NULL CHECK(Contraseña LIKE '[a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][0-9][0-9][0-9]' 
                                         OR Contraseña LIKE '[0-9][0-9][0-9][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z]')
)
GO

CREATE TABLE Secciones
(
	CodigoSecc varchar(5) PRIMARY KEY CHECK(LEN(LTRIM(RTRIM(CodigoSecc))) = 5),
	Nombre varchar(20)NOT NULL,
	Activo bit Not Null Default (1)
)
GO

CREATE TABLE Periodistas
(
	Cedula varchar(8) PRIMARY KEY,
	Nombre varchar(20) NOT NULL,
	-- regex extraIdo de https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/email#basic_validation
	Email varchar (20) NOT NULL CHECK(Email LIKE '%[a-zA-Z0-9][@][a-zA-Z0-9]%[.][a-zA-Z0-9]%'),
	Activo bit Not Null Default (1)
)
GO

CREATE TABLE Noticias
(
	Codigo varchar(20) PRIMARY KEY,
	NombreUsuario varchar(10) FOREIGN KEY References Empleados(NombreUsuario) NOT NULL,
    Titulo varchar(50) NOT NULL,
    Cuerpo varchar(8000) NOT NULL,
	Importancia int NOT NULL CHECK(Importancia >= 1 AND Importancia <= 5),
	FechaPublicacion date NOT NULL
)
GO

CREATE TABLE Internacionales
(
	Codigo varchar(20) PRIMARY KEY,
	Pais varchar(20) NOT NULL,
	FOREIGN KEY(Codigo) References Noticias(Codigo) 
)
GO

CREATE TABLE Nacionales
(
	Codigo varchar(20) PRIMARY KEY,
	CodigoSecc varchar(5) FOREIGN KEY References Secciones(CodigoSecc) NOT NULL,
	FOREIGN KEY(Codigo) References Noticias(Codigo) 
)
GO

CREATE TABLE Escriben
(
	Codigo varchar(20) FOREIGN KEY References Noticias(Codigo) NOT NULL,
	Cedula varchar(8) FOREIGN KEY References Periodistas(Cedula) NOT NULL,
	PRIMARY KEY(Codigo, Cedula)
)
GO

--INSERTS

INSERT Empleados(NombreUsuario, Contraseña) VALUES ('Empleado10', 'empl123')
INSERT Empleados(NombreUsuario, Contraseña) VALUES ('Empleado20', 'empl234')
INSERT Empleados(NombreUsuario, Contraseña) VALUES ('Empleado30', 'empl345')
INSERT Empleados(NombreUsuario, Contraseña) VALUES ('Empleado40', 'empl456')
INSERT Secciones(CodigoSecc,Nombre,Activo) VALUES ('polic','Policial', 1)
INSERT Secciones(CodigoSecc,Nombre,Activo) VALUES ('econo','Economia', 1)
INSERT Secciones(CodigoSecc,Nombre,Activo) VALUES ('depor','Deportes', 1)
INSERT Secciones(CodigoSecc,Nombre,Activo) VALUES ('cultu','Cultural', 1)
INSERT Periodistas(Cedula, Nombre, Email, Activo) VALUES ('44259772', 'Periodista 1', 'period1@gmail.com', 1)
INSERT Periodistas(Cedula, Nombre, Email, Activo) VALUES ('51237687', 'Periodista 2', 'period2@gmail.com', 1)
INSERT Periodistas(Cedula, Nombre, Email, Activo) VALUES ('39867291', 'Periodista 3', 'period3@gmail.com', 1)
INSERT Periodistas(Cedula, Nombre, Email, Activo) VALUES ('67839025', 'Periodista 4', 'period4@gmail.com', 1)
INSERT Noticias(Codigo,NombreUsuario,Titulo, Cuerpo, Importancia, FechaPublicacion) 
VALUES ('codnot1','Empleado10', 'Titulo Noticia 1','Crimen en..', 4,'20210717')
INSERT Noticias(Codigo,NombreUsuario,Titulo, Cuerpo, Importancia, FechaPublicacion) 
VALUES ('codnot5','Empleado10', 'Titulo Noticia 5','Asesinato en..', 4,'20210717')
INSERT Noticias(Codigo,NombreUsuario,Titulo, Cuerpo, Importancia, FechaPublicacion) 
VALUES ('codnot6','Empleado10', 'Titulo Noticia 6','Messi se va del barcelona en..', 5,'20210821')
INSERT Noticias(Codigo,NombreUsuario,Titulo, Cuerpo, Importancia, FechaPublicacion) 
VALUES ('codnot2','Empleado20', 'Titulo Noticia 2','Victoria de Uruguay.', 2,'20210820')
INSERT Noticias(Codigo,NombreUsuario,Titulo, Cuerpo, Importancia, FechaPublicacion) 
VALUES ('codnot3','Empleado10', 'Titulo Noticia 3','Mercosur', 5,'20210629')
INSERT Noticias(Codigo,NombreUsuario,Titulo, Cuerpo, Importancia, FechaPublicacion) 
VALUES ('codnot4','Empleado40', 'Titulo Noticia 4','Aumento de combustible...', 3,'20210712')
INSERT Internacionales(Codigo,Pais) VALUES ('codnot4', 'Argentina')
INSERT Internacionales(Codigo,Pais) VALUES ('codnot6', 'Argentina')
INSERT Internacionales(Codigo,Pais) VALUES ('codnot3', 'Brasil')
INSERT Nacionales(Codigo, CodigoSecc) VALUES ('codnot1', 'polic')
INSERT Nacionales(Codigo, CodigoSecc) VALUES ('codnot5', 'polic')
INSERT Nacionales(Codigo, CodigoSecc) VALUES ('codnot2', 'depor')
INSERT Escriben(Codigo,Cedula) VALUES ('codnot1', '44259772')
INSERT Escriben(Codigo,Cedula) VALUES ('codnot2', '51237687')
INSERT Escriben(Codigo,Cedula) VALUES ('codnot3', '39867291')
INSERT Escriben(Codigo,Cedula) VALUES ('codnot4', '67839025')
INSERT Escriben(Codigo,Cedula) VALUES ('codnot5', '67839025')
INSERT Escriben(Codigo,Cedula) VALUES ('codnot6', '67839025')
--SELECT * FROM Internacionales
--SELECT * FROM Empleados
--SELECT * FROM Nacionales
--SELECT * FROM Secciones
--SELECT * FROM Periodistas
--SELECT * FROM Noticias
GO

-- Procedimientos almacenados

-- ===============================================
CREATE PROCEDURE ListarNacionalesPublicadas
AS
BEGIN 
  SELECT N.*, NA.CodigoSecc
  FROM Nacionales NA
  INNER JOIN Noticias N ON NA.Codigo = N.Codigo
END
GO

-- ===============================================
CREATE PROCEDURE ListarInternacionalesPublicadas
AS
BEGIN 
  SELECT N.*, I.Pais
  FROM Internacionales I
  INNER JOIN Noticias N ON I.Codigo = N.Codigo
END
GO
-- ===============================================
CREATE PROCEDURE ListarNacionales5Dias
AS
BEGIN 
  SELECT N.*, NA.CodigoSecc
  FROM Nacionales NA
  INNER JOIN Noticias N ON NA.Codigo = N.Codigo
  WHERE N.FechaPublicacion BETWEEN DATEADD(DAY,-5,GETDATE()) AND GETDATE()
END
GO

-- ===============================================
CREATE PROCEDURE ListarInternacionales5Dias
AS
BEGIN 
  SELECT N.*, I.Pais
  FROM Internacionales I
  INNER JOIN Noticias N ON I.Codigo = N.Codigo
  WHERE N.FechaPublicacion BETWEEN DATEADD(DAY,-5,GETDATE()) AND GETDATE()
END
GO

-- ===============================================
Create Procedure ListadoNoticiasPeriodistas @Codigo int As
Begin
	Select  *
	From Noticias
	Where Codigo = @Codigo
End
go

-- ===============================================
CREATE PROCEDURE BuscarSeccionActiva
@CodigoSecc varchar(5)
AS
BEGIN 
  SELECT *
  FROM Secciones
  WHERE CodigoSecc = @CodigoSecc AND Activo = 1
END
GO
-- ===============================================
CREATE PROCEDURE BuscarSeccionActiva2
@Nombre varchar(5)
AS
BEGIN 
  SELECT *
  FROM Secciones
  WHERE Nombre = @Nombre AND Activo = 1
END
GO
-- ===============================================
CREATE PROCEDURE BuscarTodasSecciones
@CodigoSecc varchar(5)
AS
BEGIN 
  SELECT *
  FROM Secciones
  WHERE CodigoSecc = @CodigoSecc
END
GO

-- ===============================================
CREATE PROCEDURE BuscarPeriodistaActivo
@Cedula varchar(8)
AS
BEGIN 
  SELECT *
  FROM Periodistas
  WHERE Cedula = @Cedula AND Activo = 1
END
GO

-- ===============================================
Create View TodosLosPeriodistas As
	Select *
	From Periodistas
go
Create Procedure TodosLosPers As
Begin
	Select *
	From TodosLosPeriodistas
End
go

-- ===============================================
CREATE PROCEDURE ListadoSoloPeriodistas
AS
BEGIN 
  SELECT *
  FROM Periodistas
END
GO
-- ===============================================
CREATE PROCEDURE Logueo
@NombreUsuario VARCHAR(10),
@Contraseña VARCHAR(7)
AS
BEGIN
	SELECT * FROM Empleados WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña
END
GO

-- ===============================================
CREATE PROCEDURE CrearPeriodista
@Cedula varchar(8),
@Nombre varchar(20),
@Email varchar(20)
AS
BEGIN
	IF (EXISTS(SELECT * FROM Periodistas WHERE Cedula = @Cedula AND Activo = 0))	
	BEGIN
		UPDATE Periodistas
		SET Nombre = @Nombre, Email = @Email, Activo = 1
		WHERE Cedula = @Cedula
		
		RETURN 1
	END
	
	IF (EXISTS(SELECT * FROM Periodistas WHERE Cedula = @Cedula AND Activo = 1))
	BEGIN
		RETURN -1
	END
	
	INSERT Periodistas(Cedula,Nombre,Email) VALUES (@Cedula,@Nombre,@Email)
	
	RETURN 1
END
GO

-- ===============================================
CREATE PROCEDURE ModificarPeriodista
@Cedula varchar(8),
@Nombre varchar(20),
@Email varchar(20)
AS
BEGIN
	IF (NOT EXISTS(SELECT * FROM Periodistas WHERE Cedula = @Cedula AND Activo = 1))
		BEGIN
			RETURN -1 
		END
	ELSE
		BEGIN
			UPDATE Periodistas
			SET Nombre = @Nombre, Email = @Email
			WHERE Cedula = @Cedula
			
			IF (@@ERROR = 0)
				RETURN 1
			ELSE
				RETURN -2 
		END
END
GO

-- ===============================================
CREATE PROCEDURE EliminarPeriodista
@Cedula varchar(8)
AS
BEGIN
	IF (NOT EXISTS(SELECT * FROM Periodistas WHERE Cedula = @Cedula))
		RETURN -1 
	
	DECLARE @Error INT
	
	IF EXISTS(SELECT * FROM Escriben WHERE Cedula = @Cedula)
	BEGIN
		UPDATE Periodistas
		SET Activo = 0
		WHERE Cedula = @Cedula
		
		SET @Error = @@ERROR
	END
	ELSE
	BEGIN
		DELETE Periodistas WHERE Cedula = @Cedula
		
		SET @Error = @@ERROR
	END
	
	IF(@Error = 0)
		RETURN 1
	ELSE
		RETURN -2
END
GO
	
-- ===============================================
CREATE PROCEDURE CrearEmpleado
  @NombreUsuario VARCHAR(10),
  @Contraseña VARCHAR(7)
AS
BEGIN
	IF (EXISTS (SELECT NombreUsuario FROM Empleados WHERE NombreUsuario=@NombreUsuario))
		RETURN -1;
		
	INSERT INTO Empleados(NombreUsuario, Contraseña) 
	VALUES(@NombreUsuario, @Contraseña)
END
GO
-- ===============================================
CREATE PROCEDURE BuscarEmpleado
 @NombreUsuario VARCHAR(10)
AS
BEGIN
  SELECT * FROM Empleados WHERE NombreUsuario = @NombreUsuario
END
GO


-- ABMSecciones
-- ===============================================
CREATE PROCEDURE CrearSeccion
  @CodigoSecc VARCHAR(5),
  @Nombre VARCHAR(20)
AS
BEGIN
	IF (EXISTS(SELECT * FROM Secciones WHERE CodigoSecc = @CodigoSecc AND Activo = 0))	
	BEGIN
		UPDATE Secciones
		SET Nombre = @Nombre, Activo = 1
		WHERE CodigoSecc = @CodigoSecc
		
		RETURN 1
	END
	IF (EXISTS(SELECT * FROM Secciones WHERE CodigoSecc = @CodigoSecc AND Activo = 1))
	BEGIN
		RETURN -1
	END
	
	INSERT INTO Secciones(CodigoSecc, Nombre) VALUES (@CodigoSecc, @Nombre)
	
	RETURN 1
END
GO

-- ===============================================
CREATE PROCEDURE EliminarSeccion
	@CodigoSecc varchar(5)
AS
BEGIN 
	IF (NOT EXISTS (SELECT CodigoSecc FROM Secciones WHERE CodigoSecc = @CodigoSecc ))
		RETURN -1;
	
	DECLARE @Error INT
	
	IF (EXISTS (SELECT * FROM Nacionales WHERE CodigoSecc = @CodigoSecc ))
	BEGIN
		UPDATE Secciones
		SET Activo = 0
		WHERE CodigoSecc = @CodigoSecc
		
		SET @Error = @@ERROR
	END
	ELSE 
	BEGIN
		DELETE Secciones WHERE CodigoSecc = @CodigoSecc
		SET @Error = @@ERROR
	END
	
	IF(@Error = 0)
		RETURN 1
	ELSE
		RETURN -2 
END
GO

-- ===============================================
CREATE PROCEDURE ModificarSeccion
  @CodigoSecc VARCHAR(5),
  @Nombre VARCHAR(20)
AS
BEGIN
	IF (NOT EXISTS(SELECT * FROM Secciones WHERE CodigoSecc = @CodigoSecc AND Activo = 1))
	BEGIN
		RETURN -1
	END
	
	UPDATE Secciones 
	SET Nombre = @Nombre 
	WHERE CodigoSecc = @CodigoSecc
END
GO

-- ===============================================
CREATE PROCEDURE CrearNoticiaInternacional 
@Codigo VARCHAR(20),
@Titulo VARCHAR(50),
@Cuerpo VARCHAR(8000),
@Importancia INT,
@FechaPublicacion DATE,
@Pais VARCHAR(20),
@NombreUsuario VARCHAR(10)
AS
BEGIN
   IF EXISTS(SELECT * FROM Noticias WHERE Codigo = @Codigo)
   BEGIN
     RETURN -1 
   END
   
   IF NOT EXISTS(SELECT * FROM Empleados WHERE NombreUsuario = @NombreUsuario)
   BEGIN
     RETURN -2 
   END
   
   INSERT INTO Noticias(Codigo, Titulo, Cuerpo, Importancia, FechaPublicacion, NombreUsuario)
   VALUES(@Codigo, @Titulo, @Cuerpo, @Importancia, @FechaPublicacion, @NombreUsuario)
   
   IF @@ERROR != 0
   BEGIN
     RETURN -3 
   END
   
   INSERT INTO Internacionales(Codigo, Pais) VALUES(@Codigo, @Pais)
   
   IF @@ERROR != 0
   BEGIN
     RETURN -3 
   END
   
   RETURN 1
END
GO

-- ===============================================

CREATE PROCEDURE ModificarNoticiaInternacional 
  @Codigo VARCHAR(20),
  @Titulo VARCHAR(50),
  @Cuerpo VARCHAR(8000),
  @Importancia INT,
  @FechaPublicacion DATE,
  @Pais VARCHAR(20),
  @NombreUsuario VARCHAR(10)
AS
BEGIN
   
   IF NOT EXISTS(SELECT * FROM Internacionales WHERE Codigo = @Codigo)
   BEGIN
     RETURN -1
   END
   
   IF NOT EXISTS(SELECT * FROM Empleados WHERE NombreUsuario = @NombreUsuario)
   BEGIN
     RETURN -2
   END
      
   UPDATE Noticias
   SET Titulo = @Titulo, Cuerpo = @Cuerpo, Importancia = @Importancia, FechaPublicacion = @FechaPublicacion, NombreUsuario = @NombreUsuario
   WHERE Codigo = @Codigo
   
   IF @@ERROR != 0
   BEGIN
     RETURN -2
   END
   
   UPDATE Internacionales
   SET Pais = @Pais
   WHERE Codigo = @Codigo


   IF @@ERROR != 0
   BEGIN
     RETURN -2
   END
   
   RETURN 1
END
GO

-- ===============================================
CREATE PROCEDURE BuscarNoticiaInternacional
@Codigo VARCHAR(20)
AS
BEGIN
  SELECT *
  FROM Noticias I
  INNER JOIN Internacionales Inter ON I.Codigo = Inter.Codigo
  WHERE Inter.Codigo = @Codigo
END
GO


-- ===============================================
CREATE PROCEDURE CrearNoticiaNacional 
  @Codigo VARCHAR(20),
  @Titulo VARCHAR(50),
  @Cuerpo VARCHAR(8000),
  @Importancia INT,
  @FechaPublicacion DATE,
  @CodigoSecc VARCHAR(8),
  @NombreUsuario VARCHAR(10)
AS
BEGIN
   IF EXISTS(SELECT * FROM Noticias WHERE Codigo = @Codigo)
   BEGIN
     RETURN -1 
   END
   
   IF NOT EXISTS(SELECT * FROM Empleados WHERE NombreUsuario = @NombreUsuario)
   BEGIN
     RETURN -2 
   END
   
   IF NOT EXISTS(SELECT * FROM Secciones WHERE CodigoSecc = @CodigoSecc AND ACTIVO = 1)
   BEGIN
     RETURN -3
   END
   
   INSERT INTO Noticias(Codigo, Titulo, Cuerpo, Importancia, FechaPublicacion, NombreUsuario)
   VALUES(@Codigo, @Titulo, @Cuerpo,@Importancia, @FechaPublicacion, @NombreUsuario)
   
   IF @@ERROR != 0
   BEGIN
     RETURN -4 
   END
   
   INSERT INTO Nacionales(Codigo, CodigoSecc) VALUES (@Codigo, @CodigoSecc)
   
   IF @@ERROR != 0
   BEGIN
     RETURN -4 
   END
   
   RETURN 1
END
GO

--Create Procedure CrearNoticiaNacional  
--  @Codigo VARCHAR(20),
--  @Titulo VARCHAR(50),
--  @Cuerpo VARCHAR(8000),
--  @Importancia INT,
--  @FechaPublicacion DATE,
--  @CodigoSecc VARCHAR(8),
--  @NombreUsuario VARCHAR(10)
--AS
--BEGIN 
--	if (EXISTS (SELECT * FROM Noticias WHERE Codigo=@Codigo))
--		RETURN -1

--	if Not(EXISTS(Select * From Secciones Where CodigoSecc = @CodigoSecc))
--		return -2
		
--	--si llego aca puedo agregar
--	DECLARE @Error int
--	BEGIN TRAN

--	INSERT Noticias(Codigo, Titulo, Cuerpo, Importancia, FechaPublicacion, NombreUsuario) VALUES(@Codigo, @Titulo, @Cuerpo,@Importancia, @FechaPublicacion, @NombreUsuario) 
--	SET @Error=@@ERROR;

--	INSERT Nacionales(CodigoSecc) VALUES( @CodigoSecc)
--	SET @Error=@@ERROR+@Error;

--	IF(@Error=0)
--	BEGIN
--		COMMIT TRAN
--		RETURN 1
--	END
--	ELSE
--	BEGIN
--		ROLLBACK TRAN
--		RETURN -3
--	END	
--END
--go

-- ===============================================

CREATE PROCEDURE BuscarNoticiaNacional
@Codigo VARCHAR(20)
AS
BEGIN
  SELECT *
  FROM Noticias N
  INNER JOIN Nacionales Nac ON N.Codigo = Nac.Codigo
  WHERE Nac.Codigo = @Codigo
END
GO

-- ===============================================
CREATE PROCEDURE ModificarNoticiaNacional 
  @Codigo VARCHAR(20),
  @Titulo VARCHAR(50),
  @Cuerpo VARCHAR(8000),
  @Importancia INT,
  @FechaPublicacion DATE,
  @CodigoSecc VARCHAR(8),
  @NombreUsuario VARCHAR(10)
AS
  BEGIN
   IF NOT EXISTS(SELECT * FROM Nacionales WHERE Codigo = @Codigo)
   BEGIN
     RETURN -1
   END
   
   IF NOT EXISTS(SELECT * FROM Empleados WHERE NombreUsuario = @NombreUsuario)
   BEGIN
     RETURN -2
   END
   
   IF NOT EXISTS(SELECT * FROM Secciones WHERE CodigoSecc = @CodigoSecc AND ACTIVO = 1)
   BEGIN
     RETURN -3
   END
   
   UPDATE Noticias
   SET Titulo = @Titulo, Cuerpo = @Cuerpo, Importancia = @Importancia, FechaPublicacion = @FechaPublicacion,
    NombreUsuario = @NombreUSuario
   WHERE Codigo = @Codigo
   
   IF @@ERROR != 0
   BEGIN
     RETURN -2
   END
   
   UPDATE Nacionales
   SET CodigoSecc = @CodigoSecc
   WHERE Codigo = @Codigo

   IF @@ERROR != 0
   BEGIN
     RETURN -2 
   END
   
   RETURN 1
END
GO

-- ===============================================
CREATE PROCEDURE AsignarPeriodista
@Codigo varchar(20),
@Cedula varchar(8)
AS
BEGIN 
   IF (NOT EXISTS(SELECT * FROM Periodistas WHERE Cedula = @Cedula AND Activo = 1))
   BEGIN
     RETURN -1 
   END
   
   IF NOT EXISTS(SELECT * FROM Noticias WHERE Codigo = @Codigo)
   BEGIN
     RETURN -2 
   END

   IF EXISTS(SELECT * FROM Escriben WHERE Codigo = @Codigo AND Cedula = @Cedula)
   BEGIN
     RETURN -3 
   END
   
   
   INSERT INTO Escriben(Codigo,Cedula) VALUES (@Codigo,@Cedula)
   
   IF @@ERROR != 0
   BEGIN
     RETURN -4 
   END
   RETURN 1
END
GO

-- ===============================================
CREATE PROCEDURE EliminarPeriodistaAsignado
@Codigo varchar(20)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Escriben WHERE Codigo = @Codigo)
	BEGIN
		RETURN -1 
	END
	
	DECLARE @Error INT
	
	DELETE Escriben	WHERE Codigo = @Codigo
	
	SET @Error = @@ERROR
	
	IF(@Error = 0)
		RETURN 1
	ELSE
		RETURN -2 
END
GO

CREATE PROCEDURE CargarPeriodistasNoticia
@Codigo varchar (20)
AS
BEGIN
	SELECT P.*
	FROM Escriben as E INNER JOIN Periodistas as P
	ON E.Cedula = P.Cedula
	WHERE E.Codigo = @Codigo
END
GO 

 