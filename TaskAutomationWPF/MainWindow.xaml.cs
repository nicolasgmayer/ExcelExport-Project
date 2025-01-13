using System;
using System.Linq;
using System.Windows;
using TaskAutomationTool.Services;

namespace TaskAutomationWPF
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseService _dbService;
        private readonly ExcelService _excelService;
        private readonly EmailService _emailService;

        public MainWindow()
        {
            InitializeComponent();

            // Instanciar los servicios
            _dbService = new DatabaseService();
            _excelService = new ExcelService();
            _emailService = new EmailService();
        }

        private void ExportTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Exportar tareas pendientes a Excel
                var tasks = _dbService.GetPendingTasks();
                if (tasks.Any())
                {
                    _excelService.ExportTasksToExcel(tasks, "tasks.xlsx");
                    MessageBox.Show("Tareas exportadas a Excel con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No hay tareas pendientes para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al exportar tareas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener direcciones de correo desde la base de datos
                var emailAddresses = _dbService.GetEmailAddresses();
                if (emailAddresses.Any())
                {
                    // Enviar correo con el archivo adjunto
                    _emailService.SendEmail(emailAddresses, "Reporte de Tareas", "Se han exportado las tareas pendientes.", "tasks.xlsx");
                    MessageBox.Show("Correo enviado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron direcciones de correo en la base de datos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al enviar el correo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Salir de la aplicación
            Close();
        }
    }
}
