using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Dtos;
using TodoApp.Application.Services;
using TodoApp.Application.Validation;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateTaskRequest request,
            CancellationToken cancellationToken)
        {
            var id = await _taskService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateTaskRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _taskService.UpdateAsync(id, request, cancellationToken);
                return NoContent();
            }
            catch (DomainRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/tasks/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(new { id });
        }
    }
}
