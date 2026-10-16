using Microsoft.EntityFrameworkCore;
using Task4TaskApi.Data;
using Task4TaskApi.Models;

namespace Task4TaskApi.Services;

public sealed class TaskService(AppDbContext dbContext) : ITaskService
{
    public async Task<PagedResult<TaskItem>> GetTasksAsync(bool? isCompleted, int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Tasks.AsNoTracking().AsQueryable();
        if (isCompleted.HasValue)
            query = query.Where(task => task.IsCompleted == isCompleted.Value);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(task => task.CreatedAtUtc)
            .ThenBy(task => task.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TaskItem> { Items = items, Page = page, PageSize = pageSize, TotalCount = totalCount };
    }

    public Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(task => task.Id == id, cancellationToken);

    public async Task<TaskItem> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = new TaskItem { Title = request.Title.Trim(), Description = request.Description?.Trim(), IsCompleted = false, CreatedAtUtc = DateTime.UtcNow };
        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync(cancellationToken);
        return task;
    }

    public async Task<TaskItem?> UpdateAsync(int id, UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (task is null) return null;
        task.Title = request.Title.Trim();
        task.Description = request.Description?.Trim();
        task.IsCompleted = request.IsCompleted;
        await dbContext.SaveChangesAsync(cancellationToken);
        return task;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (task is null) return false;
        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
