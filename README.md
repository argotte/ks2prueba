# Prueba tecnica para ks2


Para este proyecto utilice el Patrón de diseño: Repository Pattern

El Patrón Repository 📦 combinado con la Inyección de Dependencias 💉 nos permite cambiar de base de datos a nuestro antojo, ya que no depende de una implementación concreta sino de una interfaz.

JSON Web Token está implementado para la validación/autenticación de cada usuario. A efectos de este proyecto, únicamente te autoriza para usar todos los endpoints que no sean register o login. Sin embargo, esta implementación puede servir como base para un sistema de autorización más complejo, donde diferentes usuarios tienen diferentes niveles de acceso a varios endpoints.


## Instalación

Describe los pasos para instalar tu proyecto. Por ejemplo:

1. Clona el repositorio

2. Instala las dependencias: 
Si usas Visual Studio Code:
`dotnet restore` 
Si usas Visual Studio:
En el Explorador de soluciones, haz clic con el botón derecho en la solución y selecciona "Restaurar paquetes NuGet". Esto instalará todas las dependencias especificadas en los archivos de proyecto de tu solución.

## Uso

Dependiendo del entorno de desarrollo que estés utilizando, los pasos para ejecutar la aplicación pueden variar:

Si estás utilizando Visual Studio Code, ejecuta el comando:dotnet watch run en la terminal.
Si estás utilizando Visual Studio, simplemente presiona el botón de ejecución.
Una vez que la aplicación esté en ejecución, se abrirá el navegador con la interfaz de Swagger.

Para empezar a interactuar con los endpoints protegidos, primero debes registrarte. Utiliza el endpoint /api/Users/RegisterJWT para crear un nuevo usuario. Una vez que hayas creado un usuario, puedes iniciar sesión utilizando el endpoint /api/Users/Login. Este endpoint te proporcionará un token que debes copiar y pegar en Swagger. Una vez que hayas proporcionado el token, estarás autorizado para interactuar con el resto de los endpoints.


GET /api/Users: Este endpoint devuelve una lista de usuarios. Acepta dos parámetros opcionales de consulta: pageNumber y pageSize que determinan la paginación de los resultados. Por defecto, devuelve la primera página con 10 usuarios.


GET /api/Users/{id}: Este endpoint devuelve el usuario con el ID especificado. Si no se encuentra ningún usuario con ese ID, devuelve un error 404.


PUT /api/Users/{id}: Este endpoint actualiza el usuario con el ID especificado. Acepta un cuerpo de solicitud que debe contener los nuevos datos del usuario. Si no se encuentra ningún usuario con ese ID, devuelve un error 404.


DELETE /api/Users/{id}: Este endpoint elimina el usuario con el ID especificado. Si no se encuentra ningún usuario con ese ID, devuelve un error 404.


POST /api/Users/RegisterJWT: Este endpoint registra un nuevo usuario. Acepta un cuerpo de solicitud que debe contener el email y la contraseña del nuevo usuario. Si la registración es exitosa, devuelve un token JWT.


POST /api/Users/Login: Este endpoint autentica a un usuario. Acepta un cuerpo de solicitud que debe contener el email y la contraseña del usuario. Si la autenticación es exitosa, devuelve un token JWT.


## 
