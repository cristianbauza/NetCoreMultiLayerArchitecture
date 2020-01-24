# Requerimientos
Net Core 3.1
Vistual Studio 2019

El proyecto está pensado para almacenar datos en un motor MariaDB 10.4.11, el mismo es compatible con MySQL.
Para cambiar el motor de base de datos hay que instalar el paquete NuGet correspondiente e indicar en la clase
DBContextFactory el nuevo motor seleccionado. También es necesario hacer lo mismo en el Startup.cs

# Iniciar el proyecto

1. Asegurarse de tener correctamente la connection string bien configurada en el DBContextFactory y en el archivo de configuración appsettings.json del proyecto WebAPI
2. Seleccionar como proyecto de inicio el proyecto DataAccessLayer
3. Desde la consola de paquetes nugget ejecutar: Update-Database
4. Seleccionar como proyecto de inicio el proyecto WebAPI
5. Ejecutar

# NetCoreMultiLayerArchitecture
Template proyecto NET Core para Web API.

# Links
## EF Core Migrations
https://www.learnentityframeworkcore.com/migrations

# Swagger
Para usar la interface de swagger, en el caso de los recursos que requieren autenticación se debe usar el Loggin del account controller y luego el la parte superior derecha  de la interface de swagger esta el botón de login, en el mismo se debe ingresar: Bearer {token}

