using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Extension;
using Todo.Api.Models;

namespace Todo.Api.Data
{
	public class TodoDbContext : DbContext
	{
		public TodoDbContext(DbContextOptions<TodoDbContext> options ) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.InitialSeed();
		}
		
		public DbSet<TodoList> Todos { get; set; }
		
	}
}