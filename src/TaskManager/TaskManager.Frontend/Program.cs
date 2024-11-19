using MudBlazor.Services;
using Refit;
using TaskManager.Frontend.Components;
using TaskManager.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var backendConnectionString = builder.Configuration.GetConnectionString("Backend")
    ?? throw new InvalidOperationException("Backend connection string not configured.");
builder.Services
    .AddRefitClient<ITasksApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(backendConnectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();