using Task4TaskApi.Models;

namespace Task4TaskApi.Services;

public interface ITaskService
{
    Task<PagedResult<TaskItem>> GetTasksAsync(bool? isCompleted, int page, int pageSize, CancellationToken cancellationToken);
    Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TaskItem> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken);
    Task<TaskItem?> UpdateAsync(int id, UpdateTaskRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
