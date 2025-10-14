use studio;
delimiter &&
-- PROCEDURE
-- 1.- 
drop PROCEDURE if exists personajesP&&
create PROCEDURE personajesP (peli int)
BEGIN
    select Nombre
    from personajes
    where id_pelicula = peli;
end &&


drop PROCEDURE if EXISTS directorAG&&
CREATE PROCEDURE directorAG(out unidDirector int, unnombre VARCHAR(20),unapellido VARCHAR(20), unanacionalidad VARCHAR(20), unaFecha DATETIME, unadescripcion VARCHAR(400),unIMG varchar(400))
Begin
	INSERT into Director (id_Director, nombre, Apellido, nacionalidad, Fecha_nacimiento, descripcion, IMG)
	VALUES (unidDirector , unnombre , unapellido , unanacionalidad , unaFecha, unadescripcion, unIMG);
	SET unidDirector = LAST_INSERT_ID();

end&&

drop PROCEDURE if EXISTS directorUP&&
CREATE PROCEDURE directorUP(unidDirector int, unnombre VARCHAR(20),unapellido VARCHAR(20), unanacionalidad VARCHAR(20), unaFecha DATETIME, unadescripcion VARCHAR(400),unIMG varchar(400))
Begin
	update director
	set	nombre= unnombre,
		Apellido= unapellido,
        Fecha_nacimiento= unaFecha,
        nacionalidad= unanacionalidad,
        descripcion= unadescripcion,
        IMG = unIMG
	where id_Director= unidDirector;
end&&


-- 2.- agregarO pelicula
drop procedure if exists agregarP&&
CREATE PROCEDURE agregarP	(out unidpelicula int, unidestudio int, unidirector int, unnombre varchar(100), unfechaestreno date, unDuracion varchar(20), ungenero varchar(20), unpresupuesto double, uncalificacion varchar(20), unTrailer varchar(100),  unIMG varchar(400))
BEGIN
	INSERT INTO peliculas	(id_pelicula, id_estudio, id_director, nombre, fecha_estreno, Duracion, genero, presupuesto, calificacion, Trailer, IMG)
					VALUES (unidpelicula, unidestudio, unidirector, unnombre, unfechaestreno, unDuracion, ungenero, unpresupuesto, uncalificacion, unTrailer, unIMG);
					SET unidpelicula = LAST_INSERT_ID();
END&&

drop procedure if exists agregarPL&&
CREATE PROCEDURE agregarPL	(unidpelicula int, unidestudio int, unidirector int, unnombre varchar(100), unfechaestreno date, unDuracion varchar(20), ungenero varchar(20), unpresupuesto double, uncalificacion varchar(20), unTrailer varchar(100), unIMG varchar(400))
BEGIN
	INSERT INTO peliculas	(id_pelicula, id_estudio, id_director, nombre, fecha_estreno, Duracion, genero, presupuesto, calificacion, Trailer, IMG)
					VALUES (unidpelicula, unidestudio, unidirector, unnombre, unfechaestreno, unDuracion, ungenero, unpresupuesto, uncalificacion, unTrailer, unIMG);
					
END&&

drop procedure if exists actualizarPL&&
CREATE PROCEDURE actualizarPL	(unidpelicula int, unidestudio int, unidirector int, unnombre varchar(100), unfechaestreno date, unDuracion varchar(20), ungenero varchar(20), unpresupuesto double, uncalificacion varchar(20), unTrailer varchar(100), unIMG varchar(400))
BEGIN
	update peliculas	
	set id_estudio=unidestudio, 
		id_director=unidirector, 
		nombre=unnombre, 
		fecha_estreno=unfechaestreno, 
		Duracion=unDuracion, 
		genero=ungenero, 
		presupuesto=unpresupuesto, 
		calificacion=uncalificacion,
        Trailer= unTrailer,
		IMG = unIMG
	where id_pelicula = unidpelicula;
END&&

drop procedure if exists eliminarP&&
CREATE PROCEDURE eliminarP(unidpelicula int)
BEGIN

	delete from personaje_voz PV
    where exists(select *
    from personajes P
    where P.id_personaje=PV.id_personaje
    and P.id_pelicula=unidpelicula);
	
    
	delete from personajes
    where id_pelicula=unidpelicula;

	delete from peliculas
    where id_pelicula=unidpelicula;
END&&


drop procedure if exists agregarPer&&
CREATE procedure agregarPer(out unidpersonaje int, unidpelicula int, unnombre varchar(40), unIMG varchar(400))
begin
	insert INTO Personajes (id_personaje, id_pelicula, nombre, IMG)
					values (unidpersonaje, unidpelicula, unnombre, unIMG);
					SET unidpersonaje = LAST_INSERT_ID();
end&&

drop procedure if exists actualizarPer&&
CREATE procedure actualizarPer(out unidpersonaje int, unidpelicula int, unnombre varchar(40), unIMG varchar(400))
begin
	insert INTO Personajes (id_personaje, id_pelicula, nombre, IMG)
					values (unidpersonaje, unidpelicula, unnombre, unIMG);
					SET unidpersonaje = LAST_INSERT_ID();
end&&

-- 3.-Asignar personajes con actores
drop procedure if exists asignarAP&&
create procedure asignarAP (actor int, personaje int)
begin
	insert into personaje_voz(id_actor, id_personaje)
    values	(actor, personaje);
end &&

drop procedure if exists nuevoActor&&
create procedure nuevoActor (unIMG varchar(400) ,unnombre varchar(20), unapellido varchar(20), out unidactor int)
begin
	insert into Actor_voz (nombre, apellido, id_actor, IMG)
    values	(unnombre, unapellido, unidactor, unIMG);
	SET unidactor = LAST_INSERT_ID();
end &&

drop procedure if exists ActualizarActor&&
create procedure ActualizarActor (unIMG varchar(400) ,unnombre varchar(20), unapellido varchar(20), unidactor int)
begin
	update actor_voz
    set nombre= unnombre,
		Apellido=unapellido,
        IMG= unIMG
        where id_actor=unidactor;
end &&

-- nuevo no preguntes por que pero es necesario... creo.
DROP PROCEDURE if EXISTS NStudio&&
CREATE PROCEDURE NStudio(out unidstudio int, unnombre VARCHAR(20), unfechafundacion date, unubicacion VARCHAR(20))
begin
	INSERT INTO `Estudio`(id_estudio,nombre,fecha_fundacion,ubicacion)
	VALUES	(unidstudio, unnombre, unfechafundacion, unubicacion);
	SET unidstudio = LAST_INSERT_ID();
end &&
-- FUNCTIONS
-- 1.- 

	DROP FUNCTION if EXISTS peliculasDiferentesDirectores &&
	CREATE function peliculasDiferentesDirectores (xid_director int)
RETURNS INT READS Sql DATA 
Begin 
declare cantidad int;

select COUNT(Distinct id_pelicula ) INTO cantidad 
from peliculas
where id_director = xid_director;

Return cantidad;	
end &&  



-- 2.-



DROP FUNCTION IF EXISTS PrimerasPeliculas &&
CREATE FUNCTION PrimerasPeliculas(fecha DATE)
  RETURNS INT READS Sql DATA
  
BEGIN
  DECLARE total INT;
  
  SELECT COUNT(*)
  INTO total
  FROM peliculas
  WHERE fecha_estreno < fecha;

  RETURN total;
END &&




-- 3.-  SF, para saber cuantos actores de voz participaron en diferentes películas 

	
	DROP FUNCTION if EXISTS Actores_vozPeliculas &&
	CREATE function Actores_vozPeliculas (peli int)
RETURNS INT READS Sql DATA 
BEGIN 
declare Voces_actores int;

select COUNT(Distinct id_actor) INTO Voces_actores 
from personaje_voz Pv
inner join personajes Pe on Pe.id_personaje= Pv.id_personaje
where Pe.id_pelicula = peli;

RETURN Voces_actores;
	
END&&   



-- TRIGGRES
-- 1.-Comprobar la asignacion
drop trigger if exists CasignacionAP&& 
create trigger CasignacionAP before insert on personaje_voz
for each row
begin
	if (not exists(select *
					from Personajes
                    where id_personaje= NEW.id_personaje)) then
                    SIGNAL SQLSTATE '45000'
					SET MESSAGE_TEXT = 'No existe el personaje';
    END IF;
    if (not exists(select *
					from Actor_voz
                    where id_actor= NEW.id_actor)) then
                    SIGNAL SQLSTATE '45000'
					SET MESSAGE_TEXT = new.id_personaje;
                    -- 'No existe el actor'
    END IF;
end &&
-- 2.-si ya existe ese actor


drop trigger if exists CasignacionA && 
create trigger CasignacionA before insert on Actor_voz
for each row
begin
	IF(exists(select *
			from Actor_voz
			where nombre= new.nombre
			and Apellido= new.Apellido))then 
			SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Ya existe el actor';
	END IF;
    IF(exists(select *
			from `Actor_voz`
			where id_actor=new.id_actor))then 
			SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Ya existe ese id';
	END IF;
END &&


-- 3.-
drop trigger if exists pverificar&&
CREATE trigger pverificar before INSERT ON peliculas
for EACH row
begin
	IF (not EXISTS(SELECT *
	FROM Director d
	WHERE id_Director = new.id_Director)) THEN
       SIGNAL SQLSTATE '45000'
       SET MESSAGE_TEXT = 'no existe el director';
	end if;
     
   IF (not EXISTS(SELECT *
   FROM Estudio e
   WHERE id_estudio = new.id_estudio)) THEN
       SIGNAL SQLSTATE '45000'
       SET MESSAGE_TEXT = 'no existe el estudio';
	end if;
end &&







