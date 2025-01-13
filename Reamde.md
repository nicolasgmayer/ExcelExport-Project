# Task Automation Tool

## Descripción

Task Automation Tool es una aplicación diseñada para automatizar tareas comunes como la exportación de datos a un archivo Excel y el envío de correos electrónicos con archivos adjuntos. Ahora cuenta con una interfaz de usuario simple desarrollada en WPF para facilitar su uso.

## Funcionalidades

1. **Exportar tareas a Excel**: Exporta los datos de tareas pendientes desde una base de datos a un archivo Excel.
2. **Enviar correos electrónicos**: Envía el archivo Excel generado como adjunto a direcciones de correo almacenadas en la base de datos.
3. **Interfaz gráfica en WPF**: Una ventana de usuario sencilla que permite ejecutar las funcionalidades mediante botones.

## Requisitos del sistema

- .NET 6 o superior.
- SQL Server (o cualquier base de datos compatible con los controladores de Microsoft).
- Configuración de una base de datos llamada `TaskAutomationDB`.

## Instalación

### 1. Configurar la base de datos

1. Crea una base de datos llamada `TaskAutomationDB`.
2. Crea las siguientes tablas:

   **Tabla `Tasks`:**
   - `TaskId` (Primary Key, int)
   - `TaskName` (varchar)
   - `IsCompleted` (bit)
   - `LastRun` (datetime, puede ser nulo)

   **Tabla `MailTest`:**
   - `MailId` (Primary Key, int)
   - `Description` (varchar, dirección de correo electrónico)
   - `Name` (varchar, nombre del destinatario)
   - `LastName` (varchar, apellido del destinatario)

3. Inserta datos de prueba en ambas tablas.

### 2. Configurar el proyecto

1. Clona este repositorio:
   ```bash
   git clone https://github.com/usuario/TaskAutomationTool.git
   ```

2. Abre la solución en Visual Studio.
3. Restaura los paquetes NuGet requeridos.
4. Verifica y ajusta la cadena de conexión en `DatabaseService.cs`:
   ```csharp
   private readonly string connectionString = "Data Source=localhost;Initial Catalog=TaskAutomationDB;Integrated Security=True;";
   ```

### 3. Ejecutar la aplicación

1. Configura `TaskAutomationWPF` como el proyecto de inicio.
2. Compila y ejecuta la aplicación.

## Uso

1. **Exportar tareas a Excel**:
   - Haz clic en el botón "Exportar Tareas a Excel".
   - El archivo `tasks.xlsx` se generará en el directorio de la aplicación.

2. **Enviar correos electrónicos**:
   - Haz clic en el botón "Enviar Correo".
   - La aplicación obtendrá los correos desde la tabla `MailTest` y enviará un correo electrónico con el archivo Excel adjunto.

3. **Salir**:
   - Haz clic en el botón "Salir" para cerrar la aplicación.

## Estructura del proyecto

- **TaskAutomationTool**: Contiene la lógica de negocio principal.
- **TaskAutomationWPF**: Proporciona la interfaz de usuario.
- **Base de datos**: La base `TaskAutomationDB` almacena las tareas y las direcciones de correo electrónico.

## Detalles adicionales

1. **Correo electrónico emisor**:
   - El correo emisor está configurado en `EmailService.cs`:
     ```csharp
     var smtpClient = new SmtpClient("smtp.gmail.com")
     {
         Port = 587,
         Credentials = new NetworkCredential("example@example.com", "tu_contraseña_de_aplicación"),
         EnableSsl = true,
     };
     ```

2. **Archivo Excel**:
   - El archivo se genera usando ClosedXML y se guarda como `tasks.xlsx`.

## Contribuciones

1. Realiza un fork del repositorio.
2. Crea una rama para tu nueva funcionalidad:
   ```bash
   git checkout -b nueva-funcionalidad
   ```
3. Realiza un pull request describiendo tus cambios.

## Licencia

Este proyecto está bajo la licencia MIT. Puedes usarlo, modificarlo y distribuirlo libremente.

## Autor

Creado por Nicolas Mayer.

