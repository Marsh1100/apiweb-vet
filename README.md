# Proyecto Administración de Veterinaria
Proyecto webapi de cuatro capas usando NetCore7.0 para la administración de una veterinaria. Empleando como gestor de base de datos MySql.
### ¿Qué se va obtener?
  - Autenticación y autorización
  - CRUD de cada una de las tablas
  - Restricción de peticiones consecutivas
  - Paginación de los controladores Get
  - Endpoints específicos:
    1. Visualización de los veterinarios cuya especialidad sea cirugía vascular.
    2. Lista de los medicamentos que pertenezcan a el laboratorio Genfar.
    3. Mascotas que se encuentren registradas cuya especie sea felina.
    4. Lista de los propietarios y sus mascotas.
    5. Lista de los medicamentos que tenga un precio de venta mayor a 50000.
    6. Lista de las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023.
    7. Lista de todas las mascotas agrupadas por especie.
    8. Lista de todos los movimientos de medicamentos y el valor total de cada movimiento.
    9. Lista de las mascotas que fueron atendidas por un determinado veterinario.
    10. Lista de los proveedores que me venden un determinado medicamento.
    11. Lista de las mascotas y sus propietarios cuya raza sea Golden Retriver.
    12. Lista de la cantidad de mascotas que pertenecen a una raza.
### Pre-requisitos 📋
MySQL<br>
NetCore 7.0
### Base de datos
![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/da65121c-fa56-4ac4-937c-42ebe91220f2)
### Ejecutar proyecto 🔧
1. Clone el repositorio en la carpeta que desee abriendo la terminal y ejecute el siguiente code
   ```
   git clone https://github.com/Marsh1100/apiweb-vet.git
   ```
2. Acceda al la carpeta que se acaba de generar
   ```cd apiweb-vet ```
3. Ahora ejecute el comando ```. code``` para abrir el proyecto en Visual Studio Code
4. En la carpeta API diríjase al archivo appsettings.Development.json
     Llene los campos según sea su caso en los valores server, user y password reemplazando las comillas simples.

     <b>Nota:</b> Puede cambiar el nombre de la base de datos (database) si así lo prefiere.
     ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/b20d2a13-52d6-4e23-a95d-a1b85efbe02e)
6. Ahora abra una nueva terminal en Visual Studio Code
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/7844a7c0-fa95-4952-a94a-18fff4601fb2)
7. Ejecute las siguientes líneas de código para migrar la Base de Datos a su servidor. <br>
     ```dotnet ef migrations add FirstMigration --project ./Persistence/ --startup-project ./API/ --output-dir ./Data/Migrations ```<br><br>
     ```dotnet ef database update --project ./Persistence --startup-project ./API```
8. Accede a la carpeta API ```cd API ``` y ejecuta el comando    ```dotnet run ```<br>
  Le aparecerá algo como esto:<br>
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/23015abf-e37f-417a-a298-b4bbad44c030)<br>
<b>Nota:</b>Tenga en cuenta que el servidor es local y el puerto puede cambiar.<br>
En este punto las tablas de la BD se ha llenado con datos semilla 🌱🌱

¡Listo! Ahora podrá ejecutar los endpoints sin problema.<br>
## Autenticación y autorización 
* Autenticación de un usuario registrado.
  ``` http://localhost:5076/api/User/token ```
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/fd7327c6-b2cd-4ebd-a5d2-f73df1557493)<br>
* RefreshToken
  ``` http://localhost:5076/api/User/refresh-token ```
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/5d5b8842-dc76-4ed4-a4c1-e0da17dbd922)<br>
* Autorización<br>
  En el siguiente enpoint es para eliminar un veterinario de la base de datos donde solo esta autorizado el Administrador
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/d35615ba-df82-4333-a292-40d26876a0ef)<br><br>
  Endpoint:  ``` http://localhost:5076/api/Vet/{id} ``` <b>Nota</b>: Reemplazar {id}.<br>
  <br>Token de un usuario autorizado ✅.
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/a9173043-d6ec-4565-95cf-341eee9bfafa)
  Token de un usuario no autorizado  ❌.
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/f0f7d7f6-afcb-484b-a616-954f53a43d2b)<br>
## CRUD
En cada uno de los controladores se realizó el CRUD correspondiente de las tablas. En el siguiente link [Peticiones](https://github.com/Marsh1100/apiweb-vet/blob/main/vet.postman_collection.json) es un archivo contenido en el proyecto que puede importarse a Postman o Insomia para visualizar cada una de las peticiones realizadas-
## Restricción de peticiones consecutivas






## Ejecutando los Endpoints ⚙️📚
