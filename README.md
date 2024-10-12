## Prueba Técnica - Desarrollador Backend .NET

### Descripción

La prueba técnica tiene como objetivo desarrollar y desplegar dos microservicios interdependientes que se comunican entre sí, utilizando Docker Compose para la orquestación.

### Estructura de la Solución

- __CustomerProfileService__: Este microservicio se encarga de gestionar datos del client, permitiendo operaciones como creación, actualización y eliminación. Utiliza una base de datos MSQL para almacenar la información de los clientes.
- __AccountTransactionService__: Este microservicio se encarga de la gestión de cuentas, permitiendo operaciones de movimientos. Este microservicio también puede comunicarse con *CustomerProfileService* para validar la existencia de clientes antes de realizar operaciones relacionadas con las cuentas.

### Implementación

- __Diseño de API__: Se definen las API REST para ambos microservicios, utilizando ASP.NET 6 para construir los endpoints.
- __Interacción entre Microservicios__: Los microservicios utilizan HTTP para comunicarse entre sí. Por ejemplo, al crear una cuenta en el *AcountTransactionService*, se verifica primero la existencia del cliente en el *CustomerProfileService*.
- __Dockerización__: Se crean archivos ```Dockerfile``` para cada microservicio, definiendo cómo construir las imágenes de Docker necesarias para cada uno.
- __Orquestación con Docker Compose__:
	- Se crea un archivo ```docker-compose.yml``` que define los servicios, redes y volúmenes necesarios. Se especifican las imágenes de los microservicios y se configuran las variables de entorno necesarias, como las cadenas de conexión a las bases de datos.
	- La configuración de puertos se realiza para permitir el acceso a las APIs desde el exterior.

### Requisitos

- Docker
- SDK de .NET Core/6.0
- MSQL Engine

### Despliegue

- Clonar el repositorio

  ```git clone https://github.com/ACJean/CoreBankingServices.git```

- Ejecutar script de base de datos en MSQL

  ```BaseDatos.sql```

- Configurar las cadenas de conexión en el archivo ```docker-compose.yml``` de cada microservicio.

- Ubicarse en la raíz del proyecto y ejecutar: 	

  ```
  cd CoreBankingServices
  docker-compose up --build
  ```