using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using Dapper;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=TaskAutomationDB;Trusted_Connection=True;";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Obtener todas las tareas de la base de datos
            var tasks = connection.Query("SELECT * FROM Tasks");

            // Crear un archivo Excel
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tasks");
            worksheet.Cell(1, 1).Value = "TaskId";
            worksheet.Cell(1, 2).Value = "TaskName";
            worksheet.Cell(1, 3).Value = "IsCompleted";
            worksheet.Cell(1, 4).Value = "LastRun";

            int row = 2;
            foreach (var task in tasks)
            {
                worksheet.Cell(row, 1).Value = task.TaskId;
                worksheet.Cell(row, 2).Value = task.TaskName;
                worksheet.Cell(row, 3).Value = task.IsCompleted;
                worksheet.Cell(row, 4).Value = task.LastRun?.ToString("yyyy-MM-dd") ?? "N/A";
                row++;
            }

            // Guardar el archivo Excel
            workbook.SaveAs("Tasks.xlsx");
            Console.WriteLine("Tareas exportadas a Tasks.xlsx");
        }
    }
}
