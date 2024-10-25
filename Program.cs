using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookDbContext>(x=>x.UseNpgsql(builder.Configuration["ConnectionString"]));
builder.Services.AddControllers();
builder.Services.AddTransient<IBookRepository, BookRepository>();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();