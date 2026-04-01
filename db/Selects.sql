begin;

-- Use these statements only in development or controlled admin scenarios.
select * from servicio.categorias_vehiculos order by id_categoria;
select * from servicio.peajes order by id_peaje;
select * from servicio.movimientos order by id_movimiento asc;
select * from servicio.recargas order by id_recarga desc;
select * from servicio.pagos order by id_pago desc;
select * from servicio.tarjetas order by id_tarjeta;
select * from servicio.vehiculos order by id_vehiculo;
select * from usuario.usuarios order by id_usuario;
select * from usuario.solicitudes order by id_solicitud desc;
select * from usuario.contactos order by id_contacto desc;

rollback;
