using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public TodoItemsController(ITodoRepository todoRepository)
        {
            Contract.Requires(todoRepository != null);
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
                log.Error(ex.Message);
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
                log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                log.Error("Wrong Item to update");
                return BadRequest();
            }            

            try
            {
               var updatedTodoItem =  await todoRepository.UpdateAsync(todoItem);
               return Ok(updatedTodoItem);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                log.Error(ex.Message);
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
                log.Error(ex.Message);
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
                log.Error(ex.Message);
                return BadRequest(ex);
            }
        }       
    }
}
