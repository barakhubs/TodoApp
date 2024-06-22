using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Api.Models
{
	public class TodoList
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public bool IsActive { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		
	}
}