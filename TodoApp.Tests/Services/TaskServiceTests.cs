using FluentAssertions;
using TodoApp.Application.Dtos;
using TodoApp.Application.Services;
using TodoApp.Application.Validation;
using TodoApp.Domain.Enums;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Tests.Fakes;

namespace TodoApp.Tests.Services
{
    public class TaskServiceTests
    {
        [Fact]
        public async Task Create_Should_Fail_When_DueDate_Is_In_Past()
        {
            var db = TestDbFactory.Create();
            var repo = new EfTaskRepository(db);

            var rules = new TaskRules(
                new FakeHolidayService(),
                new FakeDateTimeProvider(new DateOnly(2025, 1, 10)));

            var service = new TaskService(repo, rules);

            var request = new CreateTaskRequest
            {
                Name = "Past Task",
                DueDate = new DateOnly(2025, 1, 9),
                Priority = Priority.Low,
                Status = Domain.Enums.TaskStatus.New
            };

            Func<Task> act = async () =>
                await service.CreateAsync(request, CancellationToken.None);

            await act.Should().ThrowAsync<DomainRuleException>();
        }

        [Fact]
        public async Task Create_Should_Fail_When_DueDate_Is_On_Weekend()
        {
            var db = TestDbFactory.Create();
            var repo = new EfTaskRepository(db);

            var rules = new TaskRules(
                new FakeHolidayService(),
                new FakeDateTimeProvider(new DateOnly(2025, 1, 10)));

            var service = new TaskService(repo, rules);

            var request = new CreateTaskRequest
            {
                Name = "Weekend Task",
                DueDate = new DateOnly(2025, 1, 11), // Saturday
                Priority = Priority.Low,
                Status = Domain.Enums.TaskStatus.New
            };

            Func<Task> act = async () =>
                await service.CreateAsync(request, CancellationToken.None);

            await act.Should().ThrowAsync<DomainRuleException>();
        }

        [Fact]
        public async Task Create_Should_Fail_When_Over_100_HighPriority_Tasks()
        {
            var db = TestDbFactory.Create();
            var repo = new EfTaskRepository(db);

            var rules = new TaskRules(
                new FakeHolidayService(),
                new FakeDateTimeProvider(new DateOnly(2025, 1, 10)));

            var service = new TaskService(repo, rules);

            var dueDate = new DateOnly(2025, 1, 13);

            for (int i = 0; i < 100; i++)
            {
                await service.CreateAsync(
                    new CreateTaskRequest
                    {
                        Name = $"Task {i}",
                        DueDate = dueDate,
                        Priority = Priority.High,
                        Status = Domain.Enums.TaskStatus.InProgress
                    },
                    CancellationToken.None);
            }

            var request = new CreateTaskRequest
            {
                Name = "Overflow Task",
                DueDate = dueDate,
                Priority = Priority.High,
                Status = Domain.Enums.TaskStatus.New
            };

            Func<Task> act = async () =>
                await service.CreateAsync(request, CancellationToken.None);

            await act.Should().ThrowAsync<DomainRuleException>();
        }
    }
}
