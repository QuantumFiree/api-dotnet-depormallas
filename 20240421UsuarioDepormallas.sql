-- modelada
create table usuario (
	id serial not null primary key,
	clave varchar(100) not null,
	correo varchar(100) not null,
	nombres varchar(100) not null,
	apellidos varchar(100) not null,
	nombreempresa varchar(100) not null,
	Telefono int not null,
	rol int
);
-- modelada
create table archivoservicio(
	id serial not null primary key,
	descripcion varchar(256),
	url varchar(400),
	fechacargue timestamp
);
alter table archivoservicio 
add column idservicio int references servicio(id);
-- modelada
create table tiposervicioprestado(
	id serial not null primary key,
	nombre varchar(128),
	descripcion varchar(256)
);
--modelado
create table clienteservicioprestado(
	id serial not null primary key,
	nombrecompleto varchar(256),
	telefono int,
	correo varchar(128),
	direccion varchar(128)
);
-- modelada
create table tipodeestado(
	id serial not null primary key,
	nombre varchar(128),
	descripcion varchar(128) 
);
-- modelada
create table servicioprestado(
	id serial not null primary key,
	idtiposervicio int not null references tiposervicioprestado(id),
	idclienteservicio int not null references clienteservicioprestado(id),
	valortotal int not null,
	numerodepagos int not null,
	valorpendiente int not null,
	fechainicio timestamp,
	fechafin timestamp,
	fechafinestimada timestamp,
	idestado int not null references tipodeestado(id)
);

create table pagoservicio(
	id serial not null primary key,
	idservicio int not null references servicioprestado(id),
	idarchivo int references archivoservicio(id),
	valorpago int not null,
	fechapago timestamp not null
);
create table conceptogastoservicio(
	id serial not null primary key,
	idservicio int not null references servicioprestado(id),
	idarchivo int references archivoservicio(id),
	descripcion varchar(256),
	fechagasto timestamp not null,
	valorgasto int not null
);

--PRIMERA EJECUCIÓN HASTA AQUÍ

create table producto(
	id serial not null primary key,
	nombre varchar(200),
	descripcion varchar(200),
	esmetroslineales bool not null, 
	esmetroscuadrados bool not null,
	esgramos bool not null,
	eskilogramos bool not null,
	estoneladas bool not null,
	valorporunidad decimal not null		
);

insert into producto (nombre, descripcion, esmetroslineales, esmetroscuadrados, esgramos, eskilogramos, 
					 estoneladas, valorporunidad)
values ('Malla', 'malla deportiva impermeabilizada', false, true, false, false, false, 7000)
--===================================================================================

create table servicio (
	id serial not null primary key,
	nombre varchar(200),
	descripcion varchar(200),
	esinstalacion bool not null,
	esmantenimiento bool not null
);

create table archivoproducto (
	id serial not null primary key,
	idproducto int not null references producto(id),
	descripcion varchar(256),
	url varchar(400),
	fechacargue timestamp not null
);

select * from archivoservicio