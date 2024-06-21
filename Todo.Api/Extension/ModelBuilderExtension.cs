using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Models;

namespace Todo.Api.Extension
{
	public static class ModelBuilderExtension
	{   
		public static void InitialSeed(this ModelBuilder modelBuilder) 
		{
			modelBuilder.Entity<TodoList>().HasData(
				new TodoList 
				{
					Id = 1,
					Title = "Todo List 1",
					IsActive = true,
					CreatedAt = DateTime.Now
				},
				new TodoList 
				{
					Id = 2,
					Title = "Todo List 2",
					IsActive = true,
					CreatedAt = DateTime.Now
				},
				new TodoList 
				{
					Id = 3,
					Title = "Todo List 3",
                    IsActive = true,
                    CreatedAt = DateTime.Now
				}
			);
		}
	}
}