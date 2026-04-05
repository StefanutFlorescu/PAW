using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab06.Models;

namespace Lab06.Services;

public interface IUserService
{
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
}
