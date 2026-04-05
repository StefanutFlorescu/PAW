### 1. De ce folosim Repository Pattern?
- Separa accesul la date de restul aplicatiei.
- Daca schimbam baza de date, controller-ele si service-urile raman neschimbate.

### 2. Ce s-ar intampla daca apelam _context direct din controller?
- Controller-ul devine legat de DB (tight coupling).
- Logica de date ajunge in controller, greu de testat si intretinut.

### 3. De ce avem un Service Layer separat?
- Pune logica aplicatiei (validari, reguli de business) intre controller si repository.
- Controller-ul doar primeste cereri si trimite raspunsuri.

### 4. Ce logica ar ajunge in controller fara el?
- Validari complexe, calcule, reguli de business.
- Controller-ul devine greu de citit si testat.

### 5. De ce folosim interfete (IArticleRepository, IArticleService)?
- Decuplare intre controller si implementari.
- Permite mocking si testare.
- Flexibilitate la schimbarea implementarilor.

### 6. Exemplu simplu (C#)
```csharp
public interface IArticleRepository { IEnumerable<Article> GetAll(); void Add(Article a); }
public class ArticleRepository : IArticleRepository { private AppDbContext _context; /* ... */ }

public interface IArticleService { IEnumerable<Article> GetAllArticles(); }
public class ArticleService : IArticleService { private IArticleRepository _repo; /* ... */ }

[ApiController]
public class ArticleController : ControllerBase
{
    private IArticleService _service;
    public ArticleController(IArticleService s){_service=s;}
    [HttpGet] public IActionResult Get() => Ok(_service.GetAllArticles());
}
```
### 7. Cum ajuta structura pentru:
- API REST: controller-ele si service-urile pot fi refolosite fara modificari.
- Aplicatie mobila: aceeasi logica de business, testare si mentenanta mai usoara.

### Link YouTube: https://youtu.be/_i-4_DuwMGs
