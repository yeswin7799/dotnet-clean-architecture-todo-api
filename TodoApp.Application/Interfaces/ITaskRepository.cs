using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(TaskItem task, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> CountHighPriorityNotFinishedByDueDateAsync(
            DateOnly dueDate,
            CancellationToken cancellationToken);
    }
}
