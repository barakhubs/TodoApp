using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;

var allowedOrigins = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy(allowedOrigins, builder =>
	{
		builder.WithOrigins("http://localhost:3000") // add your frontend domain here
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});
builder.Services.AddControllers();

builder.Services.AddDbContext<TodoDbContext>(options =>
	options.UseSqlite(connectionString));

var app = builder.Build();


app.UseHttpsRedirection();

app.UseCors(allowedOrigins);

app.MapControllers();


app.Run();

