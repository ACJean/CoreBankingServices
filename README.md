## Prueba T�cnica - Desarrollador Backend .NET

### Descripci�n

La prueba t�cnica tiene como objetivo desarrollar y desplegar dos microservicios interdependientes que se comunican entre s�, utilizando Docker Compose para la orquestaci�n.

### Estructura de la Soluci�n

- __CustomerProfileService__: Este microservicio se encarga de gestionar datos del client, permitiendo operaciones como creaci�n, actualizaci�n y eliminaci�n. Utiliza una base de datos MSQL para almacenar la informaci�n de los clientes.
- __AccountTransactionService__: Este microservicio se encarga de la gesti�n de cuentas, permitiendo operaciones de movimientos. Este microservicio tambi�n puede comunicarse con *CustomerProfileService* para validar la existencia de clientes antes de realizar operaciones relacionadas con las cuentas.

### Implementaci�n

- __Dise�o de API__: Se definen las API REST para ambos microservicios, utilizando ASP.NET 6 para construir los endpoints.
- __Interacci�n entre Microservicios__: Los microservicios utilizan HTTP para comunicarse entre s�. Por ejemplo, al crear una cuenta en el *AcountTransactionService*, se verifica primero la existencia del cliente en el *CustomerProfileService*.
- __Dockerizaci�n__: Se crean archivos ```Dockerfile``` para cada microservicio, definiendo c�mo construir las im�genes de Docker necesarias para cada uno.
- __Orquestaci�n con Docker Compose__:
	- Se crea un archivo ```docker-compose.yml``` que define los servicios, redes y vol�menes necesarios. Se especifican las im�genes de los microservicios y se configuran las variables de entorno necesarias, como las cadenas de conexi�n a las bases de datos.
	- La configuraci�n de puertos se realiza para permitir el acceso a las APIs desde el exterior.

### Requisitos

- Docker
- SDK de .NET Core/6.0
- MSQL Engine

### Despliegue

- Clonar el repositorio

  ```git clone https://github.com/ACJean/CoreBankingServices.git```

- Ejecutar script de base de datos en MSQL

  ```BaseDatos.sql```

- Configurar las cadenas de conexi�n en el archivo ```docker-compose.yml``` de cada microservicio.

- Ubicarse en la ra�z del proyecto y ejecutar: 	

  ```
  cd CoreBankingServices
  docker-compose up --build
  ```