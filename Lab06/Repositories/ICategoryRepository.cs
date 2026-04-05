using System.Threading;
using System.Threading.Tasks;
using Lab06.Models;
using System.Collections.Generic;

namespace Lab06.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
