using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Enums;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Infrastructure.Repositories
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public EfTaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskItem?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Tasks
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task AddAsync(
            TaskItem task,
            CancellationToken cancellationToken)
        {
            await _dbContext.Tasks.AddAsync(task, cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CountHighPriorityNotFinishedByDueDateAsync(
            DateOnly dueDate,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Tasks.CountAsync(
                t => t.DueDate == dueDate &&
                     t.Priority == Priority.High &&
                     t.Status != Domain.Enums.TaskStatus.Finished,
                cancellationToken);
        }


    }
}
