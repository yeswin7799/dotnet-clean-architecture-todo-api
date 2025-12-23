using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Enums;

namespace TodoApp.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateOnly DueDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public Priority Priority { get; set; }
        public TodoApp.Domain.Enums.TaskStatus Status { get; set; }
    }
}
