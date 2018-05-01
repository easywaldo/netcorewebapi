using System;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;
using System.Collections.Generic;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem
                {
                    Name = "Item1"
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        [Route("getList")]
        public IEnumerable<TodoItem> GetAll()
        {            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetToDo")]
        [Route("getbyid/{id}")]
        public IActionResult GetById(long id)
        {            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}
