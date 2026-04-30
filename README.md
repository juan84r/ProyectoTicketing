# ProyectoTicketing

Primera entrega

# Sistema de Ticketing - Primera Entrega

Este proyecto es un sistema de gestion de eventos y reserva de asientos, desarrollado con una arquitectura de capas en **.NET** para el Backend y **React** para el Frontend.

---

## Como ejecutar el proyecto

### 1. Requisitos previos
* **PostgreSQL** instalado y corriendo (Puerto 5433).
* **.NET 8 SDK**.
* **Node.js** y **npm**.
* Configurar la cadena de conexión en: API/appsettings.json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=Ticket_db;Username=postgres;Password=1234"
        }
### 2. Configuración del Backend (.NET)
Desde la terminal en la carpeta raiz del proyecto:

1. **Restaurar dependencias:**
   ```bash
   dotnet restore

### MIGRACIONES
   dotnet ef database update --project Infrastructure --startup-project API

### Ejecutar el servidor
   dotnet run --project API

### Instalar dependencias
   npm install

### Ejecutar en modo desarrollo
   cd Frontend
   npm run dev

### Uso del Sistema
   Registrarse con un email y contraseña,
   Iniciar sesión
   ,Seleccionar un sector (Platea Baja / Alta)
   ,Elegir asientos disponibles
   ,Confirmar la reserva

Tecnologías utilizadas

    Backend: .NET 8 (C#) con Entity Framework Core.

    Frontend: React + Vite (JavaScript).

    Base de Datos: PostgreSQL.

    Documentacion: Swagger / OpenAPI.

Funcionalidades - Entrega 1

   Persistencia de datos con Entity Framework.

   Data Seeding: Carga automatica de 100 asientos y un evento inicial.

   Manejo de Errores: Respuestas 404 controladas para recursos no encontrados.

   Frontend Interactivo: Visualizacion de sectores y persistencia de seleccion local.

   CORS: Configurado para comunicacion entre el puerto 5173 y 5171.