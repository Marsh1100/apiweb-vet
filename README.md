# Proyecto Administración de Veterinaria
Proyecto webapi de cuatro capas usando NetCore7.0 para la administración de una veterinaria. Empleando como gestor de base de datos MySql.
### ¿Qué se va obtener?
  - Autenticación y autorización
  - CRUD de cada una de las tablas
  - Restricción de peticiones consecutivas
  - Paginación de los controladores Get
  - Consultas:
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
![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/bd.png)
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
     ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/dbSettings.png)
6. Ahora abra una nueva terminal en Visual Studio Code
  ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/terminal.png)
7. Ejecute las siguientes líneas de código para migrar la Base de Datos a su servidor. <br>
     ```dotnet ef migrations add FirstMigration --project ./Persistence/ --startup-project ./API/ --output-dir ./Data/Migrations ```<br><br>
     ```dotnet ef database update --project ./Persistence --startup-project ./API```
8. Acceda a la carpeta API ```cd API ``` y ejecute el comando    ```dotnet run ```<br>
  Le aparecerá algo como esto:<br>
  ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/dotnetrun.png)<br>
<b>Nota:</b>Tenga en cuenta que el servidor es local y el puerto puede cambiar.<br>
En este punto las tablas de la BD se ha llenado con datos semilla 🌱🌱.

¡Listo! Ahora podrá ejecutar los endpoints sin problema.<br>
## Autenticación y autorización 
* Autenticación de un usuario registrado.<br>
  ```
  http://localhost:5076/api/User/token
  ```
  ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/token.png)<br><br>
* RefreshToken<br>
  ```
  http://localhost:5076/api/User/refresh-token
  ```
  ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/refresh.png)<br>
* Autorización<br>
  En el siguiente enpoint es para eliminar un veterinario de la base de datos donde solo esta autorizado el Administrador
  ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/admi.png)<br><br>
  ```
  http://localhost:5076/api/Vet/{id}
  ```
  <b>Nota</b>: Reemplazar {id}.<br>
  <br>Token de un usuario autorizado ✅.
  ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/tokenAutorizado.png)
  Token de un usuario no autorizado  ❌.
  ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/tokenNoAutorizado.png)<br><br>
## CRUD
En cada uno de los controladores se realizó el CRUD correspondiente de las tablas. En el siguiente link [Peticiones](https://github.com/Marsh1100/apiweb-vet/blob/main/vet.postman_collection.json), es un archivo contenido en el proyecto, puede importarse a Postman o Insomia para visualizar cada una de las peticiones realizadas.
## Restricción de peticiones consecutivas
Limitación de peticiones desde una Ip a la webapi.<br>
Ejemplo al intentar acceder más de 3 veces en 10 segundos a la lista de propietarios de mascotas<br>
![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/limitRate.png)<br><br>
## Paginación de peteciones get y Versionado
Se realizó la paginación en las peticiones get de los controladores en la version 1.1.<br>
Para el versionado se puede realizar mediante la query.  
```
http://localhost:5076/api/Owner?ver=1.1
```
![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/query.png)<br>
O mediante los headers. 
```
http://localhost:5076/api/Owner
```
![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/header.png) <br><br>
Ejemplo de paginación implementando los parámetros index y cantidad de registros por página.  
```
http://localhost:5076/api/Laboratory?ver=1.1&pageSize=2&pageIndex=3
```
![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/index.png)<br><br>
Ejemplo de paginación implementando el párametro de busqueda.
``` 
http://localhost:5076/api/Laboratory?ver=1.1&search=Genfar
```
![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/search.png)<br><br>

## Ejecutando las consultas ⚙️📚
1. Visualización de los veterinarios cuya especialidad sea cirugía vascular.
    ```
    http://localhost:5076/api/Vet/veterinariansBySpeciality
    ```
   ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end1.png)
   Endpoint para cualquier especialidad: ```http://localhost:5076/api/Vet/veterinariansBySpeciality/{id}``` <br><br>
3. Lista de los medicamentos que pertenezcan a el laboratorio Genfar.
    ```
    http://localhost:5076/api/Medicine/medicinesByLaboratory
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end2.png)
    Endpoint para cualquier laboratorio: ```http://localhost:5076/api/Medicine/medicinesByLaboratory/{id}``` <br><br>
3. Mascotas que se encuentren registradas cuya especie sea felina.
    ```
    http://localhost:5076/api/Pet/petBySpecie
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end3.png)
    Endpoint para cualquier especie: ```http://localhost:5076/api/Pet/petBySpecie/{id}``` <br><br>
4. Lista de los propietarios y sus mascotas.
    ```
    http://localhost:5076/api/Owner/ownerPets
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end4.png)
5. Lista de los medicamentos que tenga un precio de venta mayor a 50000.
    ```
    http://localhost:5076/api/Medicine/medicinesPrice
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end5.png)
    Endpoint para cualquier precio de venta: ```http://localhost:5076/api/Medicine/medicinesPrice/{price}``` <br><br>
6. Lista de las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023.
     ```
     http://localhost:5076/api/Pet/petsByAppoiment
     ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end6.png)
     Endpoint para cualquier trimestre del 2023: ```http://localhost:5076/api/Pet/petsByAppoiment/{quarter}``` <br><br>
7. Lista de todas las mascotas agrupadas por especie.
     ```
     http://localhost:5076/api/Pet/petsBySpecies
     ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end7.png)
8. Lista de todos los movimientos de medicamentos y el valor total de cada movimiento.  
    ```
    http://localhost:5076/api/MovementMedicine/movementMedicines
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end8.png)
9. Lista de las mascotas que fueron atendidas por un determinado veterinario.
    ```
    http://localhost:5076/api/Pet/petsByVeterinarian/{id}
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end9.png)
10. Lista de los proveedores que me venden un determinado medicamento.
    ```
    http://localhost:5076/api/Provider/providersByMedicine/{id}
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end10.png)
11. Lista de las mascotas y sus propietarios cuya raza sea Golden Retriver.
    ```
    http://localhost:5076/api/Pet/OwnerPetsByBreed
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end11.png)
    Endpoint para cualquier raza: ```http://localhost:5076/api/Pet/OwnerPetsByBreed/{id}``` <br><br>
12. Lista de la cantidad de mascotas que pertenecen a una raza.
     ```
     http://localhost:5076/api/Pet/quantityPets
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/blob/main/img/end12.png)


