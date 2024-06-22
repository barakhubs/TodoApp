using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Models;

namespace Todo.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodosController : ControllerBase
	{
		private readonly TodoDbContext _dbContext;
		
		public TodosController(TodoDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		// get all todos
		[HttpGet]
		public async Task<IActionResult> GetTodos([FromQuery] TodoParams todoParams) 
		{
			IQueryable<TodoList> todoLists = _dbContext.Todos;

			todoLists = todoLists.OrderByDescending(t => t.CreatedAt);
			
			if (!string.IsNullOrEmpty(todoParams.Sort))
			{
				switch (todoParams.Sort)
				{
					case "latest": 
						todoLists = todoLists.OrderByDescending(t => t.CreatedAt);
						break;
					case "oldest":
						todoLists = todoLists.OrderBy(t => t.CreatedAt);
						break;
					case "desc":
						todoLists = todoLists.OrderByDescending(t => t.Title);
						break;
					case "asc":
						todoLists = todoLists.OrderBy(t => t.Title);
						break;
					default:
						todoLists = todoLists.OrderBy(t => t.CreatedAt);
						break;
					
				}
				
			}

			todoLists = todoLists.Take(5);
			
			return Ok(await todoLists.ToArrayAsync());
		}
		
		// create todo list
		[HttpPost]
		public async Task<IActionResult> CreateTodo(TodoList todoList) 
		{
			if(!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			
			_dbContext.Todos.Add(todoList);
			await _dbContext.SaveChangesAsync();
			
			return CreatedAtAction(nameof(GetTodos), new { id = todoList.Id }, todoList);
		}
		
		// update todo list
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodo(int id, TodoList todoList) 
		{
			if(!ModelState.IsValid) 
			{
				return BadRequest(ModelState);
			}
			
			if(id != todoList.Id) 
			{
				return BadRequest();
			}
			
			// _dbContext.Entry(todoList).State = EntityState.Modified;
			_dbContext.Entry(todoList).Property(t => t.IsActive).IsModified = true;
			
			try
			{
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_dbContext.Todos.Any(t => t.Id == id))
				{
					return NotFound();
				}
				throw;
			}
			
			return NoContent();
		}
		
		// delete todo list
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodo(int id)  
		{
			var todoList = await _dbContext.Todos.FindAsync(id);
			
			if (todoList == null)
			{
				return NotFound();
			}
			
			_dbContext.Todos.Remove(todoList);
			await _dbContext.SaveChangesAsync();
			
			return NoContent();
		}
		
	}
}