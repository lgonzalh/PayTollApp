# PayTollApp

[English](#english) | [Español Latinoamericano](#espanol-latinoamericano)

## English

### Overview

PayTollApp is an ASP.NET Core 8 web application for managing electronic toll-card operations. The solution combines a REST API, PostgreSQL persistence on Supabase, static web pages, and Swagger-based API exploration for development environments.

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
- Entity Framework Core with PostgreSQL via Npgsql
- Swagger / OpenAPI for local API documentation
- Static HTML, CSS, JavaScript frontend served via Firebase Hosting
- Supabase PostgreSQL database
- Serverless backend deployment on Google Cloud Run via Docker
- CORS configuration supporting cross-origin requests from multiple environments

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
|   |-- DDL_POSTGRESQL
|   |-- DML_POSTGRESQL.txt
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
| Diagnostics | `POST /api/Sql/execute` | Execute read-only diagnostic queries |
| Administration | `GET/POST/PUT/DELETE /api/Administracion/...` | Administrative operations for users, cards, payments, top-ups, and requests |

### Database

The application is currently configured to run against Supabase PostgreSQL. The repository also includes database scripts under `db/`:

- `db/DDL_POSTGRESQL`: PostgreSQL schema creation
- `db/DML_POSTGRESQL.txt`: PostgreSQL seed data
- `db/Selects.sql`: query samples

The live application configuration uses a Supabase session-pooler PostgreSQL connection string in `appsettings.json` and `appsettings.Development.json`.

### Local Setup

#### Prerequisites

- .NET 8 SDK
- PostgreSQL access through Supabase
- A SQL client such as Supabase SQL Editor, DBeaver, or Azure Data Studio

#### 1. Provision the database

Run the scripts in this order:

1. `db/DDL_POSTGRESQL`
2. `db/DML_POSTGRESQL.txt`

#### 2. Configure connection strings

The repository already includes a working Supabase session-pooler connection string for demo and portfolio deployment. If you need to change it, update `appsettings.json` and `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=aws-1-us-east-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.vjtaejjmklslqqohcukw;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;Timeout=300;Command Timeout=300;",
  "UsuariosDB": "Host=aws-1-us-east-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.vjtaejjmklslqqohcukw;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;Timeout=300;Command Timeout=300;"
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

- The application uses Docker for deployment to serverless platforms like Google Cloud Run.
- The static frontend (`wwwroot`) is designed to be decoupled and is hosted on Firebase Hosting, communicating with the API via dynamic `localStorage` configuration.
- CORS policies are specifically configured to allow requests from the Firebase production domain and localhost.
- The application handles forwarded headers for reverse proxies and automatically binds to dynamic ports (`PORT` env variable) provided by cloud environments.
- The application is configured to work against a Supabase PostgreSQL database without additional post-deployment code changes.

### Current Implementation Notes

- Authentication validates credentials against stored user data and dynamically updates the UI state (e.g., hiding login/showing logout buttons).
- Custom HTTP error handling avoids unhandled exceptions on empty queries or missing records.
- Swagger is enabled only in development mode.
- The repository currently does not include automated tests.
- Password handling appears database-driven and should be reviewed before production use.

---

## Espanol Latinoamericano

### Resumen

PayTollApp es una aplicacion web construida con ASP.NET Core 8 para gestionar operaciones de telepeaje mediante tarjetas. La solucion integra una API REST, persistencia sobre PostgreSQL en Supabase, paginas web estaticas y documentacion Swagger disponible en entornos de desarrollo.

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
- Entity Framework Core con PostgreSQL mediante Npgsql
- Swagger / OpenAPI para exploracion local de la API
- Frontend estatico (HTML, CSS y JavaScript) servido mediante Firebase Hosting
- Base de datos PostgreSQL en Supabase
- Despliegue de backend en arquitectura serverless mediante Docker en Google Cloud Run
- Politicas de CORS habilitadas para comunicaciones seguras desde dominios cruzados

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
|   |-- DDL_POSTGRESQL
|   |-- DML_POSTGRESQL.txt
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
| Diagnostico | `POST /api/Sql/execute` | Ejecutar consultas diagnosticas de solo lectura |
| Administracion | `GET/POST/PUT/DELETE /api/Administracion/...` | Operaciones administrativas sobre usuarios, tarjetas, pagos, recargas y solicitudes |

### Base de Datos

La aplicacion esta configurada actualmente para ejecutarse sobre PostgreSQL en Supabase. El repositorio tambien incluye scripts en `db/`:

- `db/DDL_POSTGRESQL`: creacion del esquema PostgreSQL
- `db/DML_POSTGRESQL.txt`: datos iniciales para PostgreSQL
- `db/Selects.sql`: consultas de referencia

La configuracion activa usa una cadena de conexion PostgreSQL tipo session-pooler de Supabase en `appsettings.json` y `appsettings.Development.json`.

### Ejecucion Local

#### Prerrequisitos

- .NET 8 SDK
- Acceso a PostgreSQL mediante Supabase
- Un cliente SQL como Supabase SQL Editor, DBeaver o Azure Data Studio

#### 1. Aprovisionar la base de datos

Ejecuta los scripts en este orden:

1. `db/DDL_POSTGRESQL`
2. `db/DML_POSTGRESQL.txt`

#### 2. Configurar cadenas de conexion

El repositorio ya incluye una cadena de conexion funcional de Supabase session-pooler para despliegues de demo o portfolio. Si necesitas cambiarla, ajusta `appsettings.json` y `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=aws-1-us-east-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.vjtaejjmklslqqohcukw;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;Timeout=300;Command Timeout=300;",
  "UsuariosDB": "Host=aws-1-us-east-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.vjtaejjmklslqqohcukw;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;Timeout=300;Command Timeout=300;"
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

- La aplicacion se conteneriza con Docker para ser desplegada en plataformas serverless como Google Cloud Run.
- El frontend estatico (`wwwroot`) esta desacoplado y alojado en Firebase Hosting, conectandose dinamicamente a la API a traves de la configuracion en `localStorage`.
- Las politicas de CORS se configuraron especificamente para permitir el trafico seguro desde el dominio en produccion de Firebase.
- Soporta procesamiento de encabezados reenviados y adaptacion automatica de puertos para entornos de despliegue en la nube.
- La aplicacion trabaja con PostgreSQL en Supabase de forma automatica, leyendo la cadena de conexion desde las variables de entorno.

### Notas Actuales de Implementacion

- La interfaz de usuario refleja el estado de autenticacion de manera dinamica (ocultando botones de inicio de sesion y habilitando el cierre de sesion).
- Mejoras en el manejo de errores HTTP, como mensajes controlados (404) al consultar extractos vacios o realizar peticiones no encontradas.
- Swagger solo se habilita en modo desarrollo.
- Los archivos JavaScript aseguran una codificacion correcta en UTF-8.
- El manejo de contrasenas debe revisarse exhaustivamente para estandares productivos.
