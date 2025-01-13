using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAutomationTool.Models
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? LastRun { get; set; } // Asegúrate de que sea nullable
    }

}
