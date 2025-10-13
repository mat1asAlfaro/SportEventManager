# 🏁 Sistema de Gestión Integral de Eventos Deportivos (Monolito Blazor Server)

## 📝 Descripción del Proyecto

Este repositorio contiene la arquitectura base de un **Sistema de Gestión Integral para Eventos Deportivos**, diseñado específicamente para manejar la creación y administración de múltiples eventos, la gestión completa de corredores e inscripciones, y el registro y seguimiento de tiempos en vivo (Live Tracking).

La solución se implementa bajo una **Arquitectura Monolítica** utilizando **ASP.NET Core Blazor Server** como tecnología web principal, con integración de **SignalR** para ofrecer funcionalidades en tiempo real críticas.

---

## 🏗️ Estructura Monolítica Sugerida

El proyecto se organiza en componentes lógicos dentro del monolito para garantizar una alta **rapidez de desarrollo, mantenibilidad** y **soporte natural** a las funcionalidades clave:

| Componente Lógico              | Funcionalidades Clave                                                                                          |
| :----------------------------- | :------------------------------------------------------------------------------------------------------------- |
| **Eventos**                    | Creación, configuración y gestión del ciclo de vida de múltiples eventos.                                      |
| **Corredores e Inscripciones** | Administración de inscripciones, asignación de dorsales y gestión de kits.                                     |
| **Tiempos**                    | API de recepción de datos de chips RFID y registro de tiempos. Gestión de seguimiento en vivo (Live Tracking). |
| **UI**                         | Interfaz de usuario para pantallas de inscripción, dashboards de gestión y monitores de visualización en meta. |

---

## 🛠️ Tecnologías Clave y Características de Diseño

El diseño de la aplicación se centra en la eficiencia, el rendimiento en tiempo real y la uniformidad tecnológica.

### Tecnologías Principales

- **Tecnología Web:** ASP.NET Core Blazor Server 🚀
- **Comunicaciones en Tiempo Real:** SignalR (Integrado con Blazor Server)
- **Persistencia:** Entity Framework Core
- **Base de Datos:** SQL Server (Relacional)
- **Lenguaje de Programación:** C#

### Capas y Responsabilidades

| Capa                         | Descripción                                                                                                                                                               |
| :--------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **UI** (Interfaz de Usuario) | Desarrollada con **Blazor Server**. Incluye formularios de inscripción, dashboards de administración y **monitores en tiempo real** para resultados en meta.              |
| **Capa de Aplicación**       | Contiene los **servicios de negocio**, orquestando las operaciones. Incluye el **SignalR Hub** para manejar las comunicaciones en vivo (p.ej., actualización de tiempos). |
| **Dominio**                  | El corazón del sistema. Define las entidades y reglas de negocio principales (p.ej., `Evento`, `Corredor`, `Inscripcion`, `Tiempo`).                                      |
| **Infraestructura**          | Maneja la persistencia de datos (con EF Core) y contiene la **API de Tiempos** para la recepción y procesamiento de lecturas de chips RFID.                               |

---

## 💡 Justificación de la Arquitectura Monolítica

Se eligió la arquitectura **monolítica** implementada con ASP.NET Core Blazor Server por tres pilares fundamentales: **simplicidad, mantenibilidad y soporte en tiempo real**.

1.  **Simplicidad y Agilidad:** Ante un cronograma acotado, el monolito permite desarrollar y desplegar una **única aplicación unificada**. Esto reduce drásticamente la complejidad operativa y de despliegue, facilitando la coordinación del equipo al integrar toda la lógica de negocio, UI y persistencia en un mismo entorno.
2.  **Mantenibilidad y Uniformidad Tecnológica:** El uso de **Blazor Server** permite construir una interfaz moderna e interactiva completamente en **C#** (tanto frontend como backend). Esto elimina la necesidad de depender de _frameworks_ de frontend adicionales (como Angular o React), simplificando el _stack_ tecnológico y el mantenimiento a largo plazo.
3.  **Rendimiento en Tiempo Real:** La integración nativa de **SignalR** con Blazor Server es la clave para la funcionalidad _Live Tracking_. Garantiza una comunicación bidireccional de baja latencia necesaria para el seguimiento en vivo de los corredores y la visualización **automática e instantánea** de los resultados en los monitores de meta.

---

## 🚀 Puesta en Marcha (Setup)

### 1. Configuración de la Base de Datos (SQL Server con Docker)

Para facilitar la configuración local, se recomienda utilizar **Docker Compose** para levantar la instancia de **SQL Server**.

**`docker-compose.yml`** (Sugerido):

```yaml
version: "3.8"

services:
  db:
    image: [mcr.microsoft.com/mssql/server:2022-latest]
    container_name: sqlserver_dev
    environment:
      SA_PASSWORD: "YourStrong!Password" # ¡Cambia esta contraseña!
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql # Persistencia de datos
    healthcheck:
      test:
        [
          "CMD-SHELL",
          "/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong!Password -Q 'SELECT 1'",
        ]
      interval: 10s
      retries: 10
      start_period: 20s

volumes:
  sqlserver_data:
```

**Pasos:**

1.  Asegúrate de tener **Docker** instalado y ejecutándose.
2.  Guarda el archivo anterior como **`docker-compose.yml`** en la raíz del proyecto.
3.  Inicia el contenedor de la base de datos:
    ```bash
    docker-compose up -d
    ```

### 2. Configuración del Proyecto ASP.NET Core

1.  **Clonar el repositorio:**
    ```bash
    git clone https://github.com/mat1asAlfaro/SportEventManager.git
    cd SportEventManager
    ```
2.  **Ajustar la Cadena de Conexión:**
    Actualiza el `ConnectionString` en **`appsettings.json`** para que apunte al contenedor de Docker.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost,1433;Database=EventosDB;User Id=sa;Password=YourStrong!Password;"
      // NOTA: Si usas el contenedor 'db' desde otro contenedor de Docker, usa 'db' en lugar de 'localhost'
    }
    ```

3.  **Ejecutar Migraciones de Entity Framework Core:**
    Aplica las migraciones para crear el esquema de la base de datos:
    ```bash
    dotnet ef database update
    ```
4.  **Ejecutar la Aplicación:**
    ```bash
    dotnet run
    ```
    La aplicación estará disponible en la URL indicada en la consola (usualmente `https://localhost:XXXX`).

---

## 🤝 Contribución

¡Las **contribuciones son bienvenidas**! Si deseas mejorar el proyecto, sigue estos pasos:

1.  Haz un _fork_ del repositorio.
2.  Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3.  Realiza tus cambios y _commits_ (`git commit -m 'Agrega nueva funcionalidad X'`).
4.  Sube tu rama (`git push origin feature/nueva-funcionalidad`).
5.  Abre un _Pull Request_ detallando los cambios.
