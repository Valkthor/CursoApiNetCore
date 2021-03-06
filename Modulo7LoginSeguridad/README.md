# Notas de aplicacion

los otros tipos de seguridad:

- Anonimo, cualquiera la puede utilizar
- Basico, con usuario y password. se recomienda utilizar con SSL
- Bearer, es una manera estandarizada de utilizar la seguridad y en base a Tokens

se utilizara el sistema Bearer, que contiene un conjunto de librerias y tablas ya predefinidas por .net.

## Aplicando la estructura base

- Se crea la carpeta Models y se agrega la clase ApplicationUser y hereda ApplicationIdentity
- Se crea carpeta de contextos (Contexts) y dentro se crea una clase ApplicationDBContext. Hereda de IdentityDbContext < ApplicationUser >
    esta ultima hace que despues net cree la estructura de tablas por defecto.
- Se agrega a startup la inicializacion de los elementos por defecto:
              services.AddIdentity< ApplicationUser, IdentityRole > ()
                .AddEntityFrameworkStores< ApplicationDbContext >()
                .AddDefaultTokenProviders();

- Se configura tambien el string de conexion.
* un DB context es un elemento que pertenece a entity framework que permite consultar tablas a manera de objeto.


### Base de datos

- luego de todo lo anterior,se va a la consola administrador de paquetes y se ejecuta:
    Add-Migration nombreBD
    Add-Migracion sistema_login
- la bd sistema_login debe estar previamente creada, y tiene que estar seteada en el string de conexion.
- ejecutar la creacion de las tablas:
    update-database

## Programando la creacion del token

- Se crean dos clases en la carpeta models
  - UserInfo
    - Email
    - Password
  - UserToken
    - Token
    - Expiration (los token tienen fecha de expiracion)
- Se crea Controlador CuentaControllers
- agregar using System.IdentityModel.Tokens.Jwt;
- se crea la funcion MY build, anotaciones en el codigo que se encuentra en cuentasController
- se aplica la funcion al controlador y al metodo que se requiera, por ejemplo al metodo post de crear, despues de crear el usuario se agrega lo siguiente:
    objBuildToken.MyBuildToken(UserInfoModel, new List< string >());

una ves que se corre la aplicacion lo que ara es:

- crear el usuario en las tablas de microsoft.
- crear un token par ala conexion

### Ingenieria inversa

para traer tablas de bd ya existentes, se tiene que ejecutar el siguiente comando:

Scaffold-DbContext 'Data Source=VALK2-PC\SQLEXPRESS01; Initial Catalog=sistemaLogin;Integrated Security=True;' Microsoft.EntityFrameworkCore.SqlServer -Tables TablaUsuarios -UseDatabaseNames -OutputDir Models


## Aplicando la validacion del token

- se tiene que ir al startup.cs y instalar el nuget para este using:
  - using Microsoft.AspNetCore.Authentication.JwtBearer;
- se agrega el services de autenticacion, la explicacion esta en el codigo de startup.cs
- se configura el middleware para activar la configuracion de validacion del token:
  - app.UseAuthorization();

una vez lista esta parte se va al controlador que se quiere aplicar la validacion. Yo usare la aplicacion por defecto nomas.

- se elige el controlador y se agrega el esquema de autenticacion que se va a utilizar
  - [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

### aplicando validacion de token con roles

- se agrega un rol en la bd por defecto:
  insert into AspNetRoles (id, ConcurrencyStamp, name, NormalizedName) 
        values ( 'd77f8e79-ef65-4ee0-92bb-a87c045689d4', 'BSQ3CQBHVMTHMLXYAOZ2Q4UVG5RC6ASO', N'admin', N'admin')
- se crea un controlador  para los usuarios con un constructor (ver UsuariosController.cs)
- se crea un DTO en Models que se llama MyEditarRolDTO, con dos campos:
  - UserID
  - RoleName
- se vuelve al controlador recien creado y se crea el parametro post el cual recibe un parametro del estilo MyEditarDTO
- dentro de la funcion:
  - se obtiene el usuario por su ID.
  - se agrega al user manager el claim del role de forma tradicional y con JW
- se crea la funcion para remover, cambian algunas sintaxis minimas, mirar el archivo.

luego se modifica la funcion para crear el token agregando los roles dentro del token

## Probando el login

Ejecutar el metodo del login con post, escribiendo email y password que se crean en el metodo api/Cuentas/crear

Esto retornara un token, ese token se consulta al metodo de pruebas weatherforecast. Como el usuario no tiene el rol de usuario asignado no deberia entregar ningun resultado.

Despues se agrega al usuario con el rol de administrador, esto es en api/usuarios/AsignarUsuarioRol con metodo POST

{
	"userID": "6bb42f73-8986-432f-aace-de3497488918",
	"roleName": "admin"
}

se saca nuevo token en el login, y finalmente se trata de entrar al metodo prohibido.

ademas sde puede ver el token en JWT.IO, donde se puede pegar el token y ver la informacion.

funcionando!
