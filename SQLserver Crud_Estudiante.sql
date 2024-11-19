--crear Base de Datos
create database Crud_Estudiante;

--usar base de Datos 
use Crud_Estudiante

--tabla estudiante 
CREATE TABLE Estudiante(
    EstudianteID INT PRIMARY KEY IDENTITY,
    NombreCompleto VARCHAR(50),
    FechaNacimiento DATE,
    Genero CHAR(1),
    CorreoElectronico VARCHAR(100),
    Telefono VARCHAR(15),
    Direccion VARCHAR(100),
    FechaIngreso DATE
);

go

--insertar Estudiante
insert into Estudiante(
NombreCompleto,
FechaNacimiento,
Genero,
CorreoElectronico,
Telefono,
Direccion,
FechaIngreso
)

values
('Daniel camilo Gonzalez','2001-03-10','M','camolo.777@gmail.com','3118397632','Cra 16 # 17-32','2022-02-25')

select * from Estudiante;

go 


--procedure para listar los estudiantes
CREATE PROCEDURE sp_ListaEstudiante
AS
BEGIN 
    SELECT 
        EstudianteID,
        NombreCompleto,
        CONVERT(CHAR(10), FechaNacimiento, 103) AS FechaNacimiento,
        Genero,
        CorreoElectronico,
        Telefono,
        Direccion,
        CONVERT(CHAR(10), FechaIngreso, 103) AS FechaIngreso
    FROM Estudiante
end

go


--procedure para obtener estudiante por id
create procedure sp_ObtenerEstudiante(
@estudianteID int
)
as
begin 
	select 
	EstudianteID,  
	NombreCompleto,
	CONVERT(char(10),FechaNacimiento,103)[FechaNacimiento],
	Genero,
	CorreoElectronico,
	Telefono,
	Direccion,
	CONVERT(char(10),FechaIngreso,103)FechaIngreso
	from Estudiante where EstudianteID = @estudianteID

end

go


--procedure CrearEstudiante 
CREATE PROCEDURE sp_CrearEstudiante(
    @NombreCompleto VARCHAR(50),
    @FechaNacimiento VARCHAR(10),
    @Genero CHAR(1),
    @CorreoElectronico VARCHAR(100),
    @Telefono VARCHAR(15),
    @Direccion VARCHAR(100),
    @FechaIngreso VARCHAR(10)
)
AS
BEGIN
    SET DATEFORMAT dmy;

    INSERT INTO Estudiante (
        NombreCompleto,
        FechaNacimiento,
        Genero,
        CorreoElectronico,
        Telefono,
        Direccion,
        FechaIngreso
    )
    VALUES (
        @NombreCompleto,
        CONVERT(DATE, @FechaNacimiento),
        @Genero,
        @CorreoElectronico,
        @Telefono,
        @Direccion,
        CONVERT(DATE, @FechaIngreso)
    );

-- Devolver el EstudianteID generado
    SELECT SCOPE_IDENTITY() AS EstudianteID;
END;

go

--procedure editar estudiante
create procedure sp_EditarEstudiante(
@estudianteID int,
@NombreCompleto varchar (50),
@FechaNacimiento varchar (10),
@Genero varchar (1),
@CorreoElectronico varchar (50),
@Telefono varchar (15),
@Direccion varchar (50),
@FechaIngreso varchar (10)
)

as
begin

set dateformat dmy
	update Estudiante 
	set
	NombreCompleto = @NombreCompleto,
	FechaNacimiento = convert(date,@FechaNacimiento),
	Genero = @Genero,
	CorreoElectronico = @CorreoElectronico,
	Telefono = @Telefono,
	Direccion = @Direccion,
	FechaIngreso = convert(date,@FechaIngreso)
	where EstudianteID = @estudianteID
end


go


--procedure eliminar Estudiante
create procedure sp_EliminarEstudiante(
@estudianteID int
)
as
begin
	delete from Estudiante where EstudianteID = @estudianteID
end


--ejemplo para eliminar estudiante desde el procedure 
EXEC sp_EliminarEstudiante @estudianteID = 2;
