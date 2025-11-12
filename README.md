# LionStrategiesTest API

Este proyecto es una API web con .NET diseñada para gestionar usuarios, operaciones financieras (compras y ventas) y declaraciones de impuestos.

## Requisitos Previos

Antes de empezar, asegúrate de tener instalado lo siguiente:

*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [PostgreSQL](https://www.postgresql.org/download/)

## Puesta en Marcha

Sigue estos pasos para configurar y ejecutar el proyecto en tu entorno local.

1.  **Clonar el repositorio**
    ```sh
    git clone https://github.com/Edhezz/testLionStrategies.git
    cd LionStrategiesTest
    ```

2.  **Configurar la conexión a la base de datos**
    Abre el archivo `appsettings.json` y modifica la cadena de conexión `DefaultConnection` para que apunte a tu instancia de PostgreSQL.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=nombrebd;Username=usuario;Password=contraseña"
    }
    ```

3.  **Aplicar las migraciones**
    Este comando creará la base de datos (si no existe) y aplicará el esquema necesario.
    ```sh
    dotnet ef database update
    ```

4.  **Ejecutar la aplicación**
    ```sh
    dotnet run
    ```
    La API estará disponible en `https://localhost:5103`. El puerto se especifica en el archivo `Properties/launchSettings.json`. Una vez iniciada, puedes acceder a la documentación interactiva de Swagger en la ruta `/swagger` para probar los endpoints.

## Dependencias de Terceros

Este proyecto utiliza las siguientes librerías y herramientas externas:

*   **BCrypt.Net-Next**: Para el hash y la verificación de las contraseñas de los usuarios.
*   **EFCore.NamingConventions**: Para aplicar convenciones de nomenclatura consistentes al esquema de la base de datos gestionado por Entity Framework Core.
*   **Microsoft.AspNetCore.OpenApi**: Para generar especificaciones OpenAPI (Swagger) para los endpoints de la API.
*   **Microsoft.EntityFrameworkCore.Tools**: Proporciona herramientas de línea de comandos para gestionar las migraciones de Entity Framework Core.
*   **Npgsql.EntityFrameworkCore.PostgreSQL**: Es el proveedor de base de datos de PostgreSQL para Entity Framework Core.
*   **Swashbuckle.AspNetCore**: Para generar documentación interactiva de la API y una interfaz de usuario (Swagger UI) para probar los endpoints.