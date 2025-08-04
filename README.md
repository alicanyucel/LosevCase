# Losev Projesi

Losev, katmanl� mimariye sahip .NET 9 Web API uygulamas�d�r. Application, Domain, Infrastructure, WebAPI ve Test projelerinden olu�ur. Proje; Entity Framework Core, MediatR, FluentValidation, AutoMapper ve JWT kimlik do�rulama kullan�r.

## �zellikler
1. C# 13 dil �zellikleri
2. .NET Core 9.0 Web API
3. Katmanl� ve mod�ler mimari
4. SOLID prensipleri ve OOP
5. Clean Architecture ve Clean Code
6. AOP (Aspect Oriented Programming)
7. MediatR ile CQRS ve handler yap�s�
8. Generic Repository pattern
9. **Unit of Work pattern**
10. Scrutor ile otomatik servis kayd�
11. Result Pattern ile standart sonu� y�netimi
12. Roller ve rol tabanl� yetkilendirme
13. **Authentication ve Authorization mekanizmalar�**
14. **Identity API ile kimlik y�netimi**
15. **Ardalis.SmartEnum ile geli�mi� enum y�netimi**
16. **Serilog ile geli�mi� loglama**
17. **FluentValidation ile g��l� do�rulama**
18. **ExtensionsMiddleware ile ba�lang��ta otomatik admin kullan�c� olu�turma**
19. **Extensions metodlar� ile kodun geni�letilebilirli�i**
20. **JSON Converter ile veri d�n���m�**
21. **Password Hashing ile g�venli �ifre saklama**
22. **AutoMapper ile nesne e�leme**
23. **DTO (Data Transfer Object) kullan�m�**
24. **SQL Backup dosyas� ile veri yedekleme**
25. **Code First yakla��m� ile veritaban� y�netimi**
26. **Ayr�nt�l� README markdown dosyas� ile proje dok�mantasyonu**
27. **EF Core ile geli�mi� veri eri�imi**
28. JWT ile kimlik do�rulama
29. Swagger UI ile API dok�mantasyonu
30. Entity Framework Core ve SQL Server
31. CORS deste�i
32. Docker ile container olarak �al��t�rma
33. Health Check ile veritaban� ve servis sa�l��� izleme
34. Rate Limiting ile API istek s�n�rland�rma
35. MemoryCache ile bellek i�i h�zl� veri saklama
36. **XUnit ve Moq ile birim testler**
37. **527 repoya sahip yaz�l�m geli�tirici: [alicanyucel.com.tr](https://alicanyucel.com.tr/)**

## Kurulum

### Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (veya ba�lant� dizesini kendi veritaban�n�za g�re g�ncelleyin)
- [Docker](https://www.docker.com/) (opsiyonel)

### Ad�mlar
1. Depoyu klonlay�n:git clone <repo-url>2. NuGet paketlerini y�kleyin:dotnet restore3. `Losev.WebAPI` i�indeki `appsettings.json` dosyas�ndan ba�lant� dizesini g�ncelleyin.
4. Veritaban� migrasyonlar�n� uygulay�n:dotnet ef database update --project Losev/Losev.Infrastructure5. API'yi �al��t�r�n:dotnet run --project Losev/Losev.WebAPI
### Docker ile �al��t�rma
1. K�k dizinde veya `Losev.WebAPI` klas�r�nde a�a��daki komutla imaj� olu�turun:docker build -t losev-api .2. Container'� ba�lat�n:docker run -p 8080:80 losev-api3. Swagger aray�z�ne `http://localhost:8080/swagger` adresinden eri�ebilirsiniz.

### Kullan�m
- �lk �al��t�rmada otomatik olarak admin kullan�c�s� olu�turulur:
  - Kullan�c� ad�: `admin`
  - E-posta: `admin@admin.com`
  - �ifre: `1`

## Proje Yap�s�
- **Losev.Application**: �� mant���, CQRS handler'lar�
- **Losev.Domain**: Varl�klar, enumlar
- **Losev.Infrastructure**: Veri eri�imi, EF Core, repository'ler
- **Losev.WebAPI**: API controller'lar�, middleware, ba�lang��
- **Losev.Test**: Test katman�, birim testler (xUnit, Moq)

## Test Katman�
`Losev.Test` projesi, uygulaman�n birim testlerini i�erir. �zellikle Login i�lemleri i�in LoginCommandHandlerTests gibi testler eklenmi�tir. Testler Moq ve xUnit ile yaz�lm��t�r.

Testleri �al��t�rmak i�in:dotnet test
## Teknolojiler
- .NET 9
- Entity Framework Core
- MediatR
- FluentValidation
- AutoMapper
- Swashbuckle (Swagger)
- JWT Authentication
- Docker
- xUnit, Moq (Test)
- **Health Check**
- **Rate Limiting**
- **MemoryCache**

## Katk�
Pull request g�nderebilirsiniz. B�y�k de�i�iklikler i�in �nce bir issue a�arak tart��ma ba�latman�z �nerilir.

## Lisans
Lisans bilgisi buraya eklenmelidir.
