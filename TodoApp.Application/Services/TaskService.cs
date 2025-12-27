using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Dtos;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Validation;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{

    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly TaskRules _taskRules;
        public TaskService(
            ITaskRepository taskRepository,
            TaskRules taskRules)
        {
            _taskRepository = taskRepository;
            _taskRules = taskRules;
        }

        public async Task<Guid> CreateAsync(CreateTaskRequest request,
            CancellationToken cancellationToken)
        {
            _taskRules.ValidateDueDate(request.DueDate);
            if (request.Priority == Domain.Enums.Priority.High &&
                request.Status != Domain.Enums.TaskStatus.Finished)
            {
                var count =
                    await _taskRepository.CountHighPriorityNotFinishedByDueDateAsync(request.DueDate, cancellationToken);
                if (count >= 100)
                {
                    throw new DomainRuleException(
                        "Cannot have more than 100 High Priority tasks " +
                        "with the same due date that are not finished."
                        );
                }
            }
            var task = new TaskItem
            {
                Name = request.Name,
                DueDate = request.DueDate,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Priority = request.Priority,
                Status = request.Status,
            };
            await _taskRepository.AddAsync(task, cancellationToken);
            await _taskRepository.SaveChangesAsync(cancellationToken);

            return task.Id;

        }

        public async Task UpdateAsync(
            Guid id,
            UpdateTaskRequest request,
            CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);

            if(task == null)
            {
                throw new DomainRuleException("Task not found.");


            }
            _taskRules.ValidateDueDate(request.DueDate);

            if(request.Priority == Domain.Enums.Priority.High &&
                request.Status != Domain.Enums.TaskStatus.Finished)
            {
                var count =
                    await _taskRepository.CountHighPriorityNotFinishedByDueDateAsync(
                        request.DueDate,
                        cancellationToken);
                var alreadyCounted = 
                    task.Priority == Domain.Enums.Priority.High &&
                    task.Status == Domain.Enums.TaskStatus.Finished &&
                    task.DueDate == request.DueDate;

                if (!alreadyCounted && count >= 100)
                {
                    throw new DomainRuleException(
                        "Cannot have more than 100 High Priority tasks " +
                        "with the same due date that are not finished.");
                }

            }

            task.Name = request.Name;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.StartDate = request.StartDate;
            task.EndDate = request.EndDate;
            task.Priority = request.Priority;
            task.Status = request.Status;

            await _taskRepository.SaveChangesAsync(cancellationToken);

        }

        public async Task<TaskResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);

            if (task == null)
                return null;

            return new TaskResponse
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                DueDate = task.DueDate,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Priority = task.Priority,
                Status = task.Status
            };
        }

    }
}
