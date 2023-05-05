using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly DataContext _context;

        public TasksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems([FromQuery] string day)
        {
            var taskItems = _context.TaskItems.AsQueryable();

            if (day != null) taskItems = taskItems.Where(task => task.Day == day.ToLower());

            return Ok(await taskItems.ToListAsync());
        }

        [HttpDelete("{taskItemId}")]
        public async Task<ActionResult> DeleteTaskItem(string taskItemId)
        {
            var taskItem = await _context.TaskItems.FirstOrDefaultAsync(task => task.Id.ToString() == taskItemId);
            if (taskItem == null) return NotFound();
            _context.TaskItems.Remove(taskItem);
            if (await _context.SaveChangesAsync() > 0) return NoContent();
            else return BadRequest();
        }
    }
}
