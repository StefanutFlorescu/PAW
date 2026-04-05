using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab06.Models;

namespace Lab06.Services;

public interface IArticleService
{
    Task<List<Article>> GetAllAsync(int? categoryId = null, CancellationToken cancellationToken = default);
    Task<int> CountAsync(int? categoryId = null, CancellationToken cancellationToken = default);
    Task<List<Article>> GetPagedAsync(int page, int pageSize, int? categoryId = null, CancellationToken cancellationToken = default);
    Task<Article?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Article article, CancellationToken cancellationToken = default);
    Task UpdateAsync(Article article, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
