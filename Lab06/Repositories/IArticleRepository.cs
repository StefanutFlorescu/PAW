using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab06.Models;

namespace Lab06.Repositories;

public interface IArticleRepository : IRepository<Article>
{
    Task<List<Article>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<Article?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Article>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<int> CountAsync(int? categoryId = null, CancellationToken cancellationToken = default);
    Task<List<Article>> GetPagedAsync(int page, int pageSize, int? categoryId = null, CancellationToken cancellationToken = default);
}
