# Lab 7

## 1. De ce Logout este POST si nu link GET?
Logout schimba starea aplicatiei (utilizatorul este delogat), deci trebuie sa fie POST, nu GET.
GET ar trebui sa fie doar pentru citire.

Daca logout ar fi GET pe un link, ar putea fi declansat fara intentie:
- printr-un link pus intr-un site extern,
- prin prefetch al browserului,
- printr-un tag de tip imagine/script care loveste URL-ul de logout.

## 2. De ce login-ul are doi pasi?
Noi logam cu email in formular, dar Identity autentifica prin UserName.
De aceea facem:
- pasul 1: cautam user-ul dupa email,
- pasul 2: apelam PasswordSignInAsync cu UserName.

Identity separa cele doua campuri:
- UserName = identificator de login folosit intern,
- Email = date de contact (poate fi folosit ca login doar daca implementezi explicit asta).

## 3. De ce nu e suficient sa ascunzi butoanele in View?
Ascunderea in View este doar UX. Utilizatorul poate totusi accesa direct URL-ul /Edit sau /Delete.
De aceea verificarea reala trebuie in controller cu [Authorize] + IsOwnerOrAdmin().

Invers, daca pui doar [Authorize] in controller si nu ascunzi in View:
- securitatea ramane corecta,
- dar UX-ul e slab: utilizatorul vede butoane pe care nu le poate folosi si primeste 403 dupa click.

## 4. Ce este middleware pipeline-ul?
Pipeline-ul este lantul de middleware-uri prin care trece fiecare request.
Fiecare middleware poate citi/modifica request-ul, poate opri executia sau poate trimite mai departe.

UseAuthentication() trebuie inainte de UseAuthorization() fiindca authorization are nevoie de utilizatorul deja identificat.
Daca le inversezi, autorizarea ruleaza fara user autentificat si regulile nu functioneaza corect (apar deny/redirect-uri gresite).

## 5. Ce implementam manual fara ASP.NET Core Identity?
Ar fi trebuit sa scriem noi, de la zero:
- modelul de utilizator si roluri,
- inregistrare, login, logout,
- hash + salt pentru parole,
- validari de parola,
- cookie auth / token auth,
- gestionare sesiune,
- protectie la brute force/lockout,
- reset parola, confirmare email,
- claims/roles si verificari de autorizare,
- protectii de securitate (CSRF, validari, audit minim).

## 6. Dezavantaje ASP.NET Core Identity
- E strans legat de ecosistemul ASP.NET Core + EF Core + schema lui de tabele.
- Migrarea catre alt sistem de utilizatori poate fi mai grea.
- Pentru API-uri (mobile/Angular) ai nevoie de configurari suplimentare (de obicei JWT/OAuth), deoarece flow-ul default e orientat pe cookie + pagini MVC.
- Pentru proiecte mici poate parea mai complex decat o solutie custom minimala.

## Link Youtube : https://youtu.be/APAmaTVZxOg

# Lab 6
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

