using ClosedXML.Excel;

namespace TaskAutomationTool.Services
{
    public class ExcelService
    {
        public void ExportTasksToExcel(IEnumerable<dynamic> tasks, string filePath)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tasks");

            // Encabezados
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

                // Verificar si LastRun es nulo y asignar un valor adecuado
                if (task.LastRun != null)
                {
                    worksheet.Cell(row, 4).Value = task.LastRun.Value;  // Asignar el valor de LastRun si no es null
                }
                else
                {
                    worksheet.Cell(row, 4).Value = new XLCellValue();  // Asignar una celda vacía si LastRun es null
                }

                row++;
            }

            workbook.SaveAs(filePath);
        }



    }
}
