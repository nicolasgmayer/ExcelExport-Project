using System.Data;
using Microsoft.Data.SqlClient;
using TaskAutomationTool.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TaskAutomationTool.Services
{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;

        // Constructor que inyecta la configuración
        public DatabaseService()
        {
            // Carga la configuración desde appsettings.json
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        // Método para obtener la cadena de conexión
        private string GetConnectionString()
        {
            // Obtiene las credenciales del archivo de configuración
            string user = _configuration["DBSetting:User"];
            string password = _configuration["DBSetting:Pass"];

            // Obtiene la cadena de conexión básica desde ConnectionStrings
            string connectionStringTemplate = _configuration.GetConnectionString("TaskAutomationDB");

            // Reemplaza el lugar de los credenciales en la cadena de conexión
            return $"{connectionStringTemplate};User ID={user};Password={password}";
        }

        // Método existente para obtener tareas pendientes
        public IEnumerable<TaskModel> GetPendingTasks()
        {
            string connectionString = GetConnectionString();  // Obtén la cadena de conexión

            var tasks = new List<TaskModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT TaskId, TaskName, IsCompleted, LastRun FROM Tasks WHERE IsCompleted = 0";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TaskModel
                        {
                            TaskId = reader.GetInt32(0),
                            TaskName = reader.GetString(1),
                            IsCompleted = reader.GetBoolean(2),
                            LastRun = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                        });
                    }
                }
            }

            return tasks;
        }

        // Método para obtener correos electrónicos
        public IEnumerable<string> GetEmailAddresses()
        {
            string connectionString = GetConnectionString();  // Obtén la cadena de conexión

            var emailAddresses = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT Description FROM MailTest";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emailAddresses.Add(reader.GetString(0)); // Agrega la dirección de correo al listado
                    }
                }
            }

            return emailAddresses;
        }
    }
}
