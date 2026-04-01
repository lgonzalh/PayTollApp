# PayTollApp

Professional bilingual project documentation for the PayToll toll-card platform.

[English](#english) | [Español Latinoamericano](#espanol-latinoamericano)

## English

### Overview

PayTollApp is an ASP.NET Core 8 web application for managing electronic toll-card operations. The solution combines a REST API, SQL Server persistence, static web pages, and Swagger-based API exploration for development environments.

The platform currently supports:

- User registration and sign-in
- Vehicle registration linked to a user
- Toll-card creation and lookup
- Balance top-ups and toll payments
- Account statement generation
- Customer contact and support requests
- Administrative user and card management

### Architecture

The codebase follows a layered structure:

- `Core/`: domain entities, business services, and service contracts
- `Infrastructure/`: Entity Framework Core database contexts
- `Web/`: API controllers and request/response models
- `wwwroot/`: static frontend assets and HTML pages
- `db/`: SQL scripts for schema creation, seed data, and ad-hoc queries

This layout keeps business logic separated from HTTP concerns and persistence details, making the project easier to maintain and extend.

### Technology Stack

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core with SQL Server
- Swagger / OpenAPI for local API documentation
- Static HTML, CSS, JavaScript frontend served by ASP.NET Core
- SQL Server DDL/DML scripts for database provisioning
- Heroku-oriented runtime support through `Procfile` and forwarded headers handling

### Repository Structure

```text
PayTollApp/
|-- Core/
|   |-- Entities/
|   |-- Interfaces/
|   `-- Services/
|-- Infrastructure/
|   `-- Persistence/
|-- Web/
|   |-- Controllers/
|   `-- Models/
|-- db/
|   |-- DDL.sql
|   |-- DML.sql
|   |-- DescribeTabla.sql
|   `-- Selects.sql
|-- wwwroot/
|   |-- css/
|   |-- js/
|   `-- *.html
|-- Program.cs
|-- PayTollCardApi.csproj
`-- README.md
```

### Main API Surface

| Area | Endpoint | Purpose |
| --- | --- | --- |
| Users | `POST /api/Usuarios/register` | Register a new user |
| Users | `POST /api/Usuarios/login` | Basic credential validation |
| Users | `GET /api/Usuarios/perfil/{cedula}` | Get user profile by national ID |
| Users | `GET /api/Usuarios/categoria/{cedula}` | Get vehicle category associated with a card |
| Vehicles | `POST /api/Vehiculos/register` | Register a vehicle for an existing user |
| Vehicles | `GET /api/Vehiculos/getByCedula/{cedula}` | List vehicles owned by a user |
| Cards | `POST /api/Tarjetas/create` | Create a toll card |
| Cards | `GET /api/Tarjetas/getByCedula/{cedula}` | List cards by user national ID |
| Top-ups | `POST /api/Recargas/recargar` | Add balance to a card |
| Top-ups | `GET /api/Recargas/historial/{cedula}` | Retrieve top-up history |
| Payments | `POST /api/Pagos/pagar` | Execute a toll payment |
| Payments | `GET /api/Pagos/historial/{cedula}` | Retrieve payment history |
| Statements | `GET /api/Extracto/{cedula}` | Build a consolidated account statement |
| Requests | `POST /api/Solicitudes/crear` | Create a support or service request |
| Requests | `GET /api/Solicitudes/historial/{cedula}` | Retrieve request history |
| Contacts | `POST /api/Contactos/enviar` | Submit a contact message |
| Contacts | `GET /api/Contactos/historial/{cedula}` | Retrieve contact history |
| Administration | `GET/POST/PUT/DELETE /api/Administracion/...` | Administrative operations for users, cards, payments, top-ups, and requests |

### Database

The project uses SQL Server and includes database scripts under `db/`:

- `db/DDL.sql`: schema creation
- `db/DML.sql`: seed or initial data population
- `db/DescribeTabla.sql`: table inspection helpers
- `db/Selects.sql`: query samples

The default local connection strings point to a SQL Server database named `PayTollApp`.

### Local Setup

#### Prerequisites

- .NET 8 SDK
- SQL Server
- A SQL client such as SQL Server Management Studio or Azure Data Studio

#### 1. Create the database

Run the scripts in this order:

1. `db/DDL.sql`
2. `db/DML.sql`

#### 2. Configure connection strings

Update `appsettings.json` as needed:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PayTollApp;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;",
  "UsuariosDB": "Server=localhost;Database=PayTollApp;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;"
}
```

#### 3. Restore and build

```bash
dotnet restore
dotnet build
```

#### 4. Run the application

```bash
dotnet run
```

Behavior by environment:

- Development: Swagger UI is exposed at the application root
- Non-development: static files and HTML pages are served, and Swagger is disabled

### Frontend

The project also serves a static site from `wwwroot/`, including pages such as:

- `index.html`
- `register.html`
- `services.html`
- `about.html`
- `contact.html`
- `admin.html`
- `shortcut.html`

### Deployment Notes

- The repository includes a `Procfile` with `web: dotnet PayTollCardApi.dll`
- The application handles forwarded headers for reverse proxies
- HTTPS redirection is skipped when a `PORT` environment variable is present, which supports certain cloud-hosted environments
- A CORS policy currently allows the deployed Heroku frontend origin configured in `Program.cs`

### Current Implementation Notes

- Authentication is currently implemented as direct credential validation against stored user data
- Swagger is enabled only in development mode
- The repository currently does not include automated tests
- Password handling appears database-driven and should be reviewed before production use

---

## Espanol Latinoamericano

### Resumen

PayTollApp es una aplicacion web construida con ASP.NET Core 8 para gestionar operaciones de telepeaje mediante tarjetas. La solucion integra una API REST, persistencia sobre SQL Server, paginas web estaticas y documentacion Swagger disponible en entornos de desarrollo.

Actualmente la plataforma soporta:

- Registro e inicio de sesion de usuarios
- Registro de vehiculos asociados a un usuario
- Creacion y consulta de tarjetas de telepeaje
- Recargas de saldo y pagos en peajes
- Generacion de extractos
- Gestion de contactos y solicitudes de soporte
- Operaciones administrativas sobre usuarios y tarjetas

### Arquitectura

La base del proyecto sigue una estructura por capas:

- `Core/`: entidades de dominio, servicios y contratos
- `Infrastructure/`: contextos de base de datos con Entity Framework Core
- `Web/`: controladores HTTP y modelos de entrada/salida
- `wwwroot/`: frontend estatico y recursos publicos
- `db/`: scripts SQL para crear, poblar e inspeccionar la base de datos

Esta organizacion facilita el mantenimiento y separa la logica del negocio de la capa web y de persistencia.

### Stack Tecnologico

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core con SQL Server
- Swagger / OpenAPI para exploracion local de la API
- Frontend estatico en HTML, CSS y JavaScript
- Scripts DDL/DML para aprovisionamiento de base de datos
- Compatibilidad de despliegue tipo Heroku mediante `Procfile` y encabezados reenviados

### Estructura del Repositorio

```text
PayTollApp/
|-- Core/
|   |-- Entities/
|   |-- Interfaces/
|   `-- Services/
|-- Infrastructure/
|   `-- Persistence/
|-- Web/
|   |-- Controllers/
|   `-- Models/
|-- db/
|   |-- DDL.sql
|   |-- DML.sql
|   |-- DescribeTabla.sql
|   `-- Selects.sql
|-- wwwroot/
|   |-- css/
|   |-- js/
|   `-- *.html
|-- Program.cs
|-- PayTollCardApi.csproj
`-- README.md
```

### Superficie Principal de la API

| Area | Endpoint | Proposito |
| --- | --- | --- |
| Usuarios | `POST /api/Usuarios/register` | Registrar un nuevo usuario |
| Usuarios | `POST /api/Usuarios/login` | Validar credenciales basicas |
| Usuarios | `GET /api/Usuarios/perfil/{cedula}` | Consultar perfil por cedula |
| Usuarios | `GET /api/Usuarios/categoria/{cedula}` | Obtener la categoria del vehiculo asociada a la tarjeta |
| Vehiculos | `POST /api/Vehiculos/register` | Registrar un vehiculo para un usuario existente |
| Vehiculos | `GET /api/Vehiculos/getByCedula/{cedula}` | Listar vehiculos del usuario |
| Tarjetas | `POST /api/Tarjetas/create` | Crear una tarjeta de telepeaje |
| Tarjetas | `GET /api/Tarjetas/getByCedula/{cedula}` | Consultar tarjetas por cedula |
| Recargas | `POST /api/Recargas/recargar` | Agregar saldo a la tarjeta |
| Recargas | `GET /api/Recargas/historial/{cedula}` | Consultar historial de recargas |
| Pagos | `POST /api/Pagos/pagar` | Ejecutar un pago de peaje |
| Pagos | `GET /api/Pagos/historial/{cedula}` | Consultar historial de pagos |
| Extracto | `GET /api/Extracto/{cedula}` | Generar extracto consolidado |
| Solicitudes | `POST /api/Solicitudes/crear` | Crear una solicitud de servicio o soporte |
| Solicitudes | `GET /api/Solicitudes/historial/{cedula}` | Consultar historial de solicitudes |
| Contactos | `POST /api/Contactos/enviar` | Enviar mensaje de contacto |
| Contactos | `GET /api/Contactos/historial/{cedula}` | Consultar historial de contactos |
| Administracion | `GET/POST/PUT/DELETE /api/Administracion/...` | Operaciones administrativas sobre usuarios, tarjetas, pagos, recargas y solicitudes |

### Base de Datos

El proyecto usa SQL Server e incluye scripts en `db/`:

- `db/DDL.sql`: creacion del esquema
- `db/DML.sql`: datos iniciales o de prueba
- `db/DescribeTabla.sql`: apoyo para inspeccion de tablas
- `db/Selects.sql`: consultas de referencia

Las cadenas de conexion locales apuntan por defecto a una base de datos llamada `PayTollApp`.

### Ejecucion Local

#### Prerrequisitos

- .NET 8 SDK
- SQL Server
- Un cliente SQL como SQL Server Management Studio o Azure Data Studio

#### 1. Crear la base de datos

Ejecuta los scripts en este orden:

1. `db/DDL.sql`
2. `db/DML.sql`

#### 2. Configurar cadenas de conexion

Ajusta `appsettings.json` segun tu entorno:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PayTollApp;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;",
  "UsuariosDB": "Server=localhost;Database=PayTollApp;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;"
}
```

#### 3. Restaurar y compilar

```bash
dotnet restore
dotnet build
```

#### 4. Ejecutar la aplicacion

```bash
dotnet run
```

Comportamiento por entorno:

- Desarrollo: Swagger UI se expone en la raiz de la aplicacion
- No desarrollo: se sirven archivos estaticos y paginas HTML, y Swagger se deshabilita

### Frontend

La aplicacion tambien sirve un sitio estatico desde `wwwroot/`, con paginas como:

- `index.html`
- `register.html`
- `services.html`
- `about.html`
- `contact.html`
- `admin.html`
- `shortcut.html`

### Notas de Despliegue

- El repositorio incluye un `Procfile` con `web: dotnet PayTollCardApi.dll`
- La aplicacion procesa encabezados reenviados para funcionar detras de proxies
- La redireccion a HTTPS se omite cuando existe la variable de entorno `PORT`, lo que ayuda en ciertos despliegues cloud
- Existe una politica CORS configurada para el origen del frontend desplegado en Heroku definido en `Program.cs`

### Notas Actuales de Implementacion

- La autenticacion actual valida credenciales directamente contra los datos almacenados
- Swagger solo se habilita en modo desarrollo
- El repositorio no incluye pruebas automatizadas en este momento
- El manejo de contrasenas debe revisarse antes de un uso productivo
