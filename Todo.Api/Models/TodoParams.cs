using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Api.Models
{
	public class TodoParams
	{
		public string? Sort { get; set; }
		public string? Filter { get; set; }
	}
}