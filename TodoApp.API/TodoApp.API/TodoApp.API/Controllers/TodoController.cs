using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using TodoApp.API.Data;
using TodoApp.API.Models;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoDbContext;
        public TodoController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoDbContext.Todos.ToListAsync();
            return Ok(todos);
        }

        [HttpPost]
        public async  Task<IActionResult> AddTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();
            _todoDbContext.Todos.Add(todo);
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);
        }
    }
}
