# PayTollApp

[English](#english) | [Español Latinoamericano](#espanol-latinoamericano)

## English

### Executive Summary

PayTollApp is a comprehensive cloud-based platform designed for automated toll payment management and electronic top-ups. The solution integrates a robust backend REST API with a clean, decoupled static frontend, providing a seamless experience for vehicle registration, toll card management, and transaction history. Designed with a modular architecture, it separates business logic from HTTP concerns and persistence details, ensuring high maintainability and scalability.

### Technology Stack

**Backend**
- .NET 8 / ASP.NET Core Web API
- Entity Framework Core (ORM)
- PostgreSQL (Supabase)
- Swagger / OpenAPI (API Documentation)

**Frontend**
- HTML5 & CSS3 (Bootstrap 5)
- Vanilla JavaScript
- Dynamic UI State Management & LocalStorage

**Cloud & DevOps**
- Docker (Containerization)
- Google Cloud Run (Serverless API deployment)
- Cross-Origin Resource Sharing (CORS) configured for decoupled architectures

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

---

## Español Latinoamericano

### Resumen Ejecutivo

PayTollApp es una plataforma integral en la nube diseñada para la gestión automatizada de pagos de peaje y recargas electrónicas. La solución integra un robusto backend API REST con un frontend estático desacoplado, brindando una experiencia fluida para el registro de vehículos, gestión de tarjetas de peaje e historial de transacciones. Construida sobre una arquitectura modular, separa la lógica de negocio de las operaciones HTTP y de persistencia, garantizando un alto grado de escalabilidad y mantenibilidad.

### Stack Tecnológico

**Backend**
- .NET 8 / ASP.NET Core Web API
- Entity Framework Core (ORM)
- PostgreSQL (Supabase)
- Swagger / OpenAPI (Documentación de API)

**Frontend**
- HTML5 y CSS3 (Bootstrap 5)
- Vanilla JavaScript
- Gestión dinámica del estado de la UI y LocalStorage

**Nube y DevOps**
- Docker (Contenerización)
- Google Cloud Run (Despliegue serverless de la API)
- Políticas CORS configuradas para arquitecturas web desacopladas

### Superficie Principal de la API

| Área | Endpoint | Propósito |
| --- | --- | --- |
| Usuarios | `POST /api/Usuarios/register` | Registrar un nuevo usuario |
| Usuarios | `POST /api/Usuarios/login` | Validar credenciales básicas |
| Usuarios | `GET /api/Usuarios/perfil/{cedula}` | Consultar perfil por cédula |
| Usuarios | `GET /api/Usuarios/categoria/{cedula}` | Obtener la categoría del vehículo asociada a la tarjeta |
| Vehículos | `POST /api/Vehiculos/register` | Registrar un vehículo para un usuario existente |
| Vehículos | `GET /api/Vehiculos/getByCedula/{cedula}` | Listar vehículos del usuario |
| Tarjetas | `POST /api/Tarjetas/create` | Crear una tarjeta de telepeaje |
| Tarjetas | `GET /api/Tarjetas/getByCedula/{cedula}` | Consultar tarjetas por cédula |
| Recargas | `POST /api/Recargas/recargar` | Agregar saldo a la tarjeta |
| Recargas | `GET /api/Recargas/historial/{cedula}` | Consultar historial de recargas |
| Pagos | `POST /api/Pagos/pagar` | Ejecutar un pago de peaje |
| Pagos | `GET /api/Pagos/historial/{cedula}` | Consultar historial de pagos |
| Extracto | `GET /api/Extracto/{cedula}` | Generar extracto consolidado |
| Solicitudes | `POST /api/Solicitudes/crear` | Crear una solicitud de servicio o soporte |
| Solicitudes | `GET /api/Solicitudes/historial/{cedula}` | Consultar historial de solicitudes |
| Contactos | `POST /api/Contactos/enviar` | Enviar mensaje de contacto |
| Contactos | `GET /api/Contactos/historial/{cedula}` | Consultar historial de contactos |
| Diagnóstico | `POST /api/Sql/execute` | Ejecutar consultas diagnósticas de solo lectura |