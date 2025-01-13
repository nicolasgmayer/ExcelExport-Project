using TaskAutomationTool.Services;

var dbService = new DatabaseService();
var excelService = new ExcelService();
var emailService = new EmailService();

// Exportar tareas pendientes a Excel
var tasks = dbService.GetPendingTasks();
excelService.ExportTasksToExcel(tasks, "tasks.xlsx");
Console.WriteLine("Tareas exportadas a Excel.");

// Obtener direcciones de correo desde la base de datos
var emailAddresses = dbService.GetEmailAddresses();
if (emailAddresses.Any())
{
    // Enviar notificación por correo a los destinatarios obtenidos
    emailService.SendEmail(emailAddresses, "Reporte de Tareas", "Se han exportado las tareas pendientes.", "tasks.xlsx");
    Console.WriteLine("Correo enviado.");
}
else
{
    Console.WriteLine("No se encontraron direcciones de correo en la base de datos.");
}
