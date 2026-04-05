using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab06.Models;

namespace Lab06.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);
}
