using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab06.Models;
using Lab06.Repositories;

namespace Lab06.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _categoryRepository = unitOfWork.CategoryRepository;
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _categoryRepository.GetAllAsync(cancellationToken);
    }
}
