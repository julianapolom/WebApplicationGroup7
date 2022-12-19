# Prueba Developer Intcomex
### Introducción
### Instalación
### Funcionamiento


## Introducción
Se desarrolla proyecto web con persistencia de datos (SQL server), la finalidad crear, actualizar, listar y eliminar registros de clientes.
La finalidad de este CRUD es demostrar el uso de:
* Distintas practicas de desarrollo.
* Principios SOLID
* Arquitectura de software.
* Patrones de diseño (Repositori, Unitofwork, inyección de dependencias).
* Programación funcional.
* Entity framework core.
* Pruebas unitarias.
* Y buenas practicas de desarrollo.

## Instalación

Verifique que cuenta con las siguientes herramientas para la ejecuciuón del proyecto local:
* [Visual Studio](https://visualstudio.microsoft.com/es/vs/community/)
* [Sql Server](https://www.microsoft.com/es-co/download/details.aspx?id=101064)
* [Sql Management](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
* [Git](https://gitforwindows.org/)

Descargar proyecto:
* `git clole https://github.com/EstebanGit215/EC_PruebaIntcomex.git`

Crear base de datos:
* En la carpeta 1.database `EC_PruebaIntcomex\1. DataBase\Intcomex.bak` encontrará un archivo .bak que contiene el backup de la base de datos.
* Restaure la base de datos `EC_PruebaIntcomex\1. DataBase\Intcomex.bak` desde el [Sql Management](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
![image](https://user-images.githubusercontent.com/52761564/208332971-4c748ba5-ad35-40f5-9ac4-c284d3b19cd7.png)


Preparar solución:
* Desde la carpeta del proyesto WebApplicationIntcomex, ejecute desde el [Visual Studio](https://visualstudio.microsoft.com/es/vs/community/) el archivo .sln `EC_PruebaIntcomex\WebApplicationIntcomex\WebApplicationIntcomex.sln`

Configuración de conexión:
* Abra el archivo `EC_PruebaIntcomex\WebApplicationIntcomex\appsettings.json`
* Modifique la cadena de conexión con los datos de su maquina local: 
>`"ConnectionStrings": {
    "Connection": "Server=<YOUR_SERVER>;DataBase=Intcomex;Trusted_Connection=yes;Encrypt=false;"
  }`

Ejecutar solución:
* Establesca el pryecto WebApplicationIntcomex como proyecto de inicio y ejecute la solución

Ejecutar pruebas:
* En el menú principal del [Visual Studio](https://visualstudio.microsoft.com/es/vs/community/) habilite la consola de exploración de pruebas:
![image](https://user-images.githubusercontent.com/52761564/208333808-0f1fb824-37e9-403c-b5b2-0b6ecf767496.png)
* En el menú de la consola de pruebas (Test explorer) se habilitará un menú con la opción de Run, el cual podra en ejecución test creados.
![image](https://user-images.githubusercontent.com/52761564/208333986-810a97e1-e248-477d-9ca4-b8a5b3c3ea73.png)

## Funcionamiento

* Administración de clientes

![image](https://user-images.githubusercontent.com/52761564/208305165-560bf3ea-4dd3-4233-af07-1fa68208103a.png)

* Creación

![image](https://user-images.githubusercontent.com/52761564/208305546-17e6d2ee-f901-42a6-b7ff-542041eec436.png)
![image](https://user-images.githubusercontent.com/52761564/208305628-37765107-1ebd-40f9-b3c5-0a55523e7382.png)

* Consulta

![image](https://user-images.githubusercontent.com/52761564/208305648-c0be257e-8f52-44a2-acce-89f9ee599be0.png)

* Actualización

![image](https://user-images.githubusercontent.com/52761564/208305677-6ea5a6ae-45af-436f-94cc-e386ea805e5e.png)
![image](https://user-images.githubusercontent.com/52761564/208305687-5e5f5087-7468-45b0-86ec-6baa44151c84.png)

* Eliminación

![image](https://user-images.githubusercontent.com/52761564/208305718-abc68019-94cb-4855-9c0d-75dbaeb1097f.png)
![image](https://user-images.githubusercontent.com/52761564/208305729-de30c32b-14a3-4a58-b3ab-fa98271ab862.png)




