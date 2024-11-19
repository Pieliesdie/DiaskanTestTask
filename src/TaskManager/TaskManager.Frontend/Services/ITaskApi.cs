using Refit;
using TaskManager.Common.Models.Tasks;

namespace TaskManager.Frontend.Services;

public interface ITasksApi
{
    [Get("/api/Tasks/GetAll")]
    Task<List<TaskClientDto>> GetAllTasksAsync(CancellationToken ct = default);

    [Get("/api/Tasks/Get/{taskId}")]
    Task<TaskClientDto> GetTaskByIdAsync(Guid taskId, CancellationToken ct = default);

    [Put("/api/Tasks/Create")]
    Task CreateTaskAsync([Body] TaskClientDto taskClientDto, CancellationToken ct = default);

    [Patch("/api/Tasks/Update")]
    Task UpdateTaskAsync([Body] TaskClientDto taskClientDto, CancellationToken ct = default);

    [Delete("/api/Tasks/Remove/{taskId}")]
    Task DeleteTaskAsync(Guid taskId, CancellationToken ct = default);
}