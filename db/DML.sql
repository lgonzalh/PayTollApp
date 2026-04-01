------DML------
`
USE PayTollApp;
GO

-- CATEGORIAS_VEHICULOS (semillas básicas)
INSERT INTO SERVICIO.CATEGORIAS_VEHICULOS (NOMBRE_CATEGORIA, DESCRIPCION, PRECIO) VALUES
(N'Liviano',    N'Automóvil liviano',       9000.00),
(N'Camioneta',  N'Camioneta y microbús',    12000.00),
(N'Bus',        N'Bus y camión 2 ejes',     15000.00),
(N'Camión 3 ejes', N'Carga 3 ejes',         20000.00),
(N'Camión 4+ ejes', N'Carga pesada',        25000.00);
GO

-- PEAJES (ejemplos)
INSERT INTO SERVICIO.PEAJES (NOMBRE, CIUDAD, DEPARTAMENTO) VALUES
(N'Peaje La Paz',     N'La Paz',       N'Santander'),
(N'Peaje Andes',      N'Andes',        N'Antioquia'),
(N'Peaje Caribe',     N'Cartagena',    N'Bolívar');
GO

-- Insertar usuario: LUIS GONZALEZ (evita duplicados por cédula o correo)
IF NOT EXISTS (
SELECT 1
FROM USUARIO.USUARIOS
WHERE CEDULA = '79862885'
OR CORREO_ELECTRONICO = N' lantonium@gmail.com '
)
BEGIN
INSERT INTO USUARIO.USUARIOS (CEDULA, NOMBRE, CORREO_ELECTRONICO, CONTRASENA, FECHA_CREACION)
VALUES (
'79862885',
N'LUIS GONZALEZ',
N' lantonium@gmail.com ',
N'Alfa#1011',
CAST('2025-11-06' AS DATETIME2)  -- fecha hoy: 06/11/2025 (formato ISO)
);
END
GO
`