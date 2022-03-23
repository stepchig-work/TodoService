using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Interface;
using Todo.Presentation.Entities;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository todoRepository;

        public TodoItemsController(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
			try
			{
                var todoItems = await todoRepository.GetAllEntitiesAsync();
                return Ok(todoItems);
			}
            catch(Exception ex)
			{
                return BadRequest(ex.Message);
			}
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
			try
            {
                var todoItem = await todoRepository.FindAsync(id);

                if (todoItem == null)
                {
                    return NotFound();
                }

                return todoItem;

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }            

            try
            {
               var updatedTodoItem =  await todoRepository.UpdateAsync(todoItem);
               return Ok(updatedTodoItem);
            }
            catch (DbUpdateConcurrencyException) 
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            try
            {
                var newTodoItem = await todoRepository.AddAsync(todoItem);
                return CreatedAtAction(newTodoItem.Name, newTodoItem.IsComplete, newTodoItem);
            }
			catch(Exception ex)
			{
                return BadRequest(ex);
			}
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
			try
            {
                await todoRepository.RemoveAsync(id);
                return NoContent();
            }
			catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }       
    }
}
