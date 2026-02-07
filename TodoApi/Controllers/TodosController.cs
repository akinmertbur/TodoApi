using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService) {
            _todoService = todoService;
        }

        /// <summary>
        /// Returns a paginated list of todos with optional filtering and search.
        /// </summary>
        /// <param name="isCompleted">Filter by completion status.</param>
        /// <param name="search">Search term applied to the title.</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Items per page (max 100).</param>
        /// <response code="200">Returns the list of todos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoReadDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoReadDto>>> GetAll(
            [FromQuery] bool? isCompleted,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10) {
            var result = await _todoService.GetAllAsync(isCompleted, search, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Returns a single todo by id.
        /// </summary>
        /// <param name="id">Todo id.</param>
        /// <response code="200">Returns the todo.</response>
        /// <response code="404">Todo not found.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TodoReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoReadDto>> GetById(int id) {
            var item = await _todoService.GetByIdAsync(id);
            if (item is null) return NotFound();

            return Ok(item);
        }

        /// <summary>
        /// Creates a new todo.
        /// </summary>
        /// <response code="201">Todo created successfully.</response>
        /// <response code="400">Invalid input.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TodoReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoReadDto>> Create([FromBody] TodoCreateDto dto) {
            var created = await _todoService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        /// <summary>
        /// Updates an existing todo by id.
        /// </summary>
        /// <response code="204">Todo updated successfully.</response>
        /// <response code="400">Invalid input.</response>
        /// <response code="404">Todo not found.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDto dto) {
            var updated = await _todoService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a todo by id.
        /// </summary>
        /// <response code="204">Todo deleted successfully.</response>
        /// <response code="404">Todo not found.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) {
            var deleted = await _todoService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Marks a todo as completed.
        /// </summary>
        /// <remarks>
        /// Sets IsCompleted = true and updates UpdatedAt.
        /// </remarks>
        /// <response code="204">Todo marked as completed.</response>
        /// <response code="404">Todo not found.</response>
        [HttpPatch("{id:int}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete(int id) {
            var completed = await _todoService.CompleteAsync(id);
            if (!completed) return NotFound();

            return NoContent();
        }
    }
}
