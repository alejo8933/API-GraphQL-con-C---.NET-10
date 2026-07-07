AdventureWorks GraphQL API con C# y .NET 10


API GraphQL desarrollada en C# con .NET 10 usando Hot Chocolate, Entity Framework Core y la base de datos AdventureWorks. Este proyecto fue realizado como parte de la actividad de formación ADSO del SENA.

Tecnologías usadas
C# 13.

.NET 10.

Hot Chocolate 14+.

Entity Framework Core 10.

SQL Server.

AdventureWorks.

Nitro / Banana Cake Pop.

Funcionalidades
Queries para consultar productos y otros datos.

Mutations para registrar o actualizar información.

Subscriptions para eventos en tiempo real.

Paginación, filtrado, ordenamiento y proyección.

Buenas prácticas de organización y rendimiento.

Escenarios implementados
Escenario A: catálogo para tienda en línea.

Escenario C: integración en tiempo real con notificación de cambios de precio.

Requisitos
Visual Studio 2022/2026.

SDK de .NET 10.

SQL Server con la base de datos AdventureWorks restaurada.

Ejecución del proyecto
Clonar el repositorio.

Abrir la solución en Visual Studio.

Configurar la cadena de conexión a AdventureWorks en appsettings.json.

Ejecutar el proyecto.

Abrir /graphql para usar Nitro o Banana Cake Pop.

Pruebas realizadas
Se verificó el funcionamiento mediante:

Query productCatalog.

Mutation updatePrice.

Subscription onPriceChanged.
