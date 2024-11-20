using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(x => ConfigureJson(x.JsonSerializerOptions));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AutoRegister();
builder.Services.AddCors();

var mongoDbConnectionString = builder.Configuration.GetConnectionString("MongoDb")
                              ?? throw new InvalidOperationException("MongoDB connection string not configured.");

builder.Services.AddDbContext<TaskContext>(options => options.UseMongoDB(mongoDbConnectionString, "TaskManagementDb"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();
app.Run();

public partial class Program
{
    private Program() { }
    public static void ConfigureJson(JsonSerializerOptions options)
    {
        options.Converters.Add(new JsonStringEnumConverter()); 
    }
}