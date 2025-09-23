-- Active: 1700068523370@@127.0.0.1@3306@Studio
-- Consultas
use studio;

-- 1.-mostrar todos los personajes de la pelicula haru, con sus atores de voz
select P.Nombre 'Personajes', concat(AV.nombre,' ',AV.Apellido) 'Actores'
from Personajes P
inner join personaje_voz PV on P.id_personaje=PV.id_personaje
INNER JOIN Actor_voz AV on PV.id_actor=AV.id_actor
where P.id_pelicula=24;

-- 2.- mostrar todas las peliculas que tiene gibli antes de los 2000
select *
from peliculas
order by year(fecha_estreno) asc;

-- 3.- mostrar todas las peliculas que tiene gibli deespues de los 2000
select *
from peliculas
where year(fecha_estreno)>2000
order by year(fecha_estreno) asc;

-- 4.- mostrar el nombre de las peliculas dirijidas por hayao miyazaki
select nombre 'Peliculas'
from peliculas
where id_director=1;
-- 5.- mostrar cuantas peliculas dirigio cada director
select concat(D.nombre,' ',D.Apellido) 'Director', count(id_pelicula) 'Cantidad de Peliculas'
from Director D
inner join peliculas P on P.id_Director=D.id_Director
group by D.id_Director
order by count(id_pelicula);

-- mostrar la cantidad de personajes que hay en cada peli
select PE.nombre 'Peliculas', count(id_personaje) 'Cantidad de Personajes'
from peliculas PE
inner join Personajes P on PE.id_pelicula=P.id_pelicula
group by P.id_pelicula;

-- mostrar presupuesto y calificaion de cada peli
select PE.nombre 'Peliculas', PE.presupuesto, PE.calificacion
from peliculas PE;



SELECT P.id_personaje AS idPersonaje, P.Nombre, P.id_pelicula AS idPelicula
        FROM    Personajes P
        inner join personaje_voz PV ON P.id_personaje = PV.id_personaje
        WHERE PV.id_actor =1;