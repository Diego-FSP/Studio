drop database if exists studio;
create database studio;
use studio;

create table Director(
 nombre varchar(20),
 Apellido varchar(20),
 id_Director int AUTO_INCREMENT,
 Fecha_nacimiento date,
 nacionalidad varchar(20),
 descripcion varchar(400),
 
 constraint pk_Director primary key(id_director)
 );
create table Actor_voz(
 nombre varchar(20),
 Apellido  varchar(20), 
 id_actor int AUTO_INCREMENT,
 constraint pk_Actor_voz primary key(id_actor)
);

create table Estudio(
id_estudio int not null AUTO_INCREMENT,
nombre varchar(20), 
fecha_fundacion date, 
ubicacion varchar(100),
constraint pk_Estudio primary key(id_estudio) 
);

create table peliculas(
id_pelicula    int unique not null AUTO_INCREMENT,
id_estudio    int, 
id_director  int,
nombre    varchar(100),
fecha_estreno    date,
fecha_creacion    date, 
Duracion    varchar(20), 
genero  varchar(20), 
presupuesto double, 
calificacion varchar(20),  
Programa_stilo varchar(20),
constraint PK_puliculas primary key(id_pelicula, genero),
constraint fk_peliculas_Estudio foreign key(id_estudio)
references  Estudio (id_estudio),
constraint fk_pelicula_director foreign key(id_director)
references Director (id_Director)
);

create table Personajes(
id_personaje int not null AUTO_INCREMENT,
id_pelicula int,
Nombre varchar(40),
constraint PK_personajes primary key(id_personaje),
 constraint fk_personajes_peliculas foreign key(id_pelicula)
 references  peliculas (id_pelicula)
);

create table personaje_voz(
 id_actor int,
 id_personaje int,
 constraint fk_personaje_voz_Actor_voz foreign key(id_actor)
 references  Actor_voz (id_actor),
 constraint fk_personaje_voz_personajes foreign key(id_personaje)
 references Personajes (id_personaje)
  );
