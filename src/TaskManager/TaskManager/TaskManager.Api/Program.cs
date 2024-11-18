using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskManager.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(x => { x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AutoRegister();

var mongoDbConnectionString = builder.Configuration.GetConnectionString("MongoDb")
                              ?? throw new InvalidOperationException("MongoDB connection string not configured.");
builder.Services.AddDbContext<TaskContext>(options => options.UseMongoDB(mongoDbConnectionString, "TaskManagementDb"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();