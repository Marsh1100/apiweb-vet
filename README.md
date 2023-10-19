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
8. Acceda a la carpeta API ```cd API ``` y ejecute el comando    ```dotnet run ```<br>
  Le aparecerá algo como esto:<br>
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/23015abf-e37f-417a-a298-b4bbad44c030)<br>
<b>Nota:</b>Tenga en cuenta que el servidor es local y el puerto puede cambiar.<br>
En este punto las tablas de la BD se ha llenado con datos semilla 🌱🌱.

¡Listo! Ahora podrá ejecutar los endpoints sin problema.<br>
## Autenticación y autorización 
* Autenticación de un usuario registrado.<br>
  ```
  http://localhost:5076/api/User/token
  ```
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/fd7327c6-b2cd-4ebd-a5d2-f73df1557493)<br><br>
* RefreshToken<br>
  ```
  http://localhost:5076/api/User/refresh-token
  ```
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/5d5b8842-dc76-4ed4-a4c1-e0da17dbd922)<br>
* Autorización<br>
  En el siguiente enpoint es para eliminar un veterinario de la base de datos donde solo esta autorizado el Administrador
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/d35615ba-df82-4333-a292-40d26876a0ef)<br><br>
  ```
  http://localhost:5076/api/Vet/{id}
  ```
  <b>Nota</b>: Reemplazar {id}.<br>
  <br>Token de un usuario autorizado ✅.
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/a9173043-d6ec-4565-95cf-341eee9bfafa)
  Token de un usuario no autorizado  ❌.
  ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/f0f7d7f6-afcb-484b-a616-954f53a43d2b)<br><br>
## CRUD
En cada uno de los controladores se realizó el CRUD correspondiente de las tablas. En el siguiente link [Peticiones](https://github.com/Marsh1100/apiweb-vet/blob/main/vet.postman_collection.json), es un archivo contenido en el proyecto, puede importarse a Postman o Insomia para visualizar cada una de las peticiones realizadas.
## Restricción de peticiones consecutivas
Limitación de peticiones desde una Ip a la webapi.<br>
Ejemplo al intentar acceder más de 3 veces en 10 segundos a la lista de propietarios de mascotas<br>
![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/ddd1f318-7ab2-4882-b66b-88702fb5c605)<br><br>
## Paginación de peteciones get y Versionado
Se realizó la paginación en las peticiones get de los controladores en la version 1.1.<br>
Para el versionado se puede realizar mediante la query.  
```
http://localhost:5076/api/Owner?ver=1.1 
```
![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/46c52227-a620-4c68-8818-736a3be8c585)<br>
O mediante los headers. 
```
http://localhost:5076/api/Owner
```
![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/db0c55dc-0c32-489a-8b37-0b02dd83a1f7) <br><br>
Ejemplo de paginación implementando los parámetros index y cantidad de registros por página.  
```
http://localhost:5076/api/Laboratory?ver=1.1&pageSize=2&pageIndex=3
```
![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/e170880f-414a-46d3-bd0b-a00343f264f5)<br><br>
Ejemplo de paginación implementando el párametro de busqueda.
``` 
http://localhost:5076/api/Laboratory?ver=1.1&search=Genfar
```
![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/36c35ca7-90de-4e2d-9006-6d75e4f0da0a)<br><br>

## Ejecutando las consultas ⚙️📚
1. Visualización de los veterinarios cuya especialidad sea cirugía vascular.
    ```
      http://localhost:5076/api/Vet/veterinariansBySpeciality
    ```
   ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/52e48842-66f9-47a2-bb30-48968deabad6)
   Endpoint para cualquier especialidad: ```http://localhost:5076/api/Vet/veterinariansBySpeciality/{id}``` <br><br>
3. Lista de los medicamentos que pertenezcan a el laboratorio Genfar.
    ```
      http://localhost:5076/api/Medicine/medicinesByLaboratory
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/4223e8c9-de8e-44a1-8242-8a4692f38511)
    Endpoint para cualquier laboratorio: ```http://localhost:5076/api/Medicine/medicinesByLaboratory/{id}``` <br><br>
3. Mascotas que se encuentren registradas cuya especie sea felina.
    ```
      http://localhost:5076/api/Pet/petBySpecie
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/c4ba6e96-4c28-4d58-a682-2775c098030c)
    Endpoint para cualquier especie: ```http://localhost:5076/api/Pet/petBySpecie/{id}``` <br><br>
4. Lista de los propietarios y sus mascotas.
    ```
      http://localhost:5076/api/Owner/ownerPets
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/b20c64cc-0fe7-4d87-8cf3-e7ed66c1fbef) <br><br>
5. Lista de los medicamentos que tenga un precio de venta mayor a 50000.
    ```
      http://localhost:5076/api/Medicine/medicinesPrice
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/05422b2c-45c0-45d6-9fca-0fd3a2ec11aa)
    Endpoint para cualquier precio de venta: ```http://localhost:5076/api/Medicine/medicinesPrice/{price}``` <br><br>
6. Lista de las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023.
     ```
        http://localhost:5076/api/Pet/petsByAppoiment
     ```
     ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/540431be-a6f1-468c-81da-f8bafb87a30c)
     Endpoint para cualquier trimestre del 2023: ```http://localhost:5076/api/Pet/petsByAppoiment/{quarter}``` <br><br>
7. Lista de todas las mascotas agrupadas por especie.
     ```
        http://localhost:5076/api/Pet/petsBySpecies
     ```
     ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/c803f3ae-3e14-47d5-8688-795977cb2ac2)  <br><br>
8. Lista de todos los movimientos de medicamentos y el valor total de cada movimiento.  
    ```
        http://localhost:5076/api/MovementMedicine/movementMedicines
    ```
     ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/cd346358-00a0-45f3-b2fa-986edd28da77) <br><br>
9. Lista de las mascotas que fueron atendidas por un determinado veterinario.
    ```
      http://localhost:5076/api/Pet/petsByVeterinarian/{id}
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/eae6a080-82f6-4b27-9133-9d8856322cc7) <br><br>
10. Lista de los proveedores que me venden un determinado medicamento.
    ```
      http://localhost:5076/api/Provider/providersByMedicine/{id}
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/c3699845-0ea0-4e58-8bb2-dbdc2e00fef6) <br><br>
11. Lista de las mascotas y sus propietarios cuya raza sea Golden Retriver.
    ```
      http://localhost:5076/api/Pet/OwnerPetsByBreed
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/9834f2d0-97f7-40d5-9128-f3b519622063)
    Endpoint para cualquier raza: ```http://localhost:5076/api/Pet/OwnerPetsByBreed/{id}``` <br><br>
12. Lista de la cantidad de mascotas que pertenecen a una raza.
     ```
      http://localhost:5076/api/Pet/quantityPets
    ```
    ![image](https://github.com/Marsh1100/apiweb-vet/assets/131481951/da24413b-a1d0-49fb-a69d-71abdbcb94f5)


