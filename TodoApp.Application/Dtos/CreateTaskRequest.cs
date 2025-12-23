using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Enums;

namespace TodoApp.Application.Dtos
{
    public class CreateTaskRequest
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DateOnly DueDate { get; init; }
        public DateOnly? StartDate { get; init; }
        public DateOnly? EndDate { get; init; }

        public Priority Priority { get; init; }
        public TodoApp.Domain.Enums.TaskStatus Status { get; init; }
    }
}
