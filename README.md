# Losev Projesi

Losev, katmanl� mimariye sahip .NET 9 Web API uygulamas�d�r. Application, Domain, Infrastructure, WebAPI ve Test projelerinden olu�ur. Proje; Entity Framework Core, MediatR, FluentValidation, AutoMapper ve JWT kimlik do�rulama kullan�r.

## �zellikler
- Katmanl� ve mod�ler mimari
- SOLID prensipleri ve OOP
- Clean Architecture ve Clean Code
- AOP (Aspect Oriented Programming)
- MediatR ile CQRS ve handler yap�s�
- Generic Repository pattern
- **Unit of Work pattern**
- Scrutor ile otomatik servis kayd�
- Result Pattern ile standart sonu� y�netimi
- Roller ve rol tabanl� yetkilendirme
- **Ardalis.SmartEnum ile geli�mi� enum y�netimi**
- **Serilog ile geli�mi� loglama**
- **FluentValidation ile g��l� do�rulama**
- **ExtensionsMiddleware ile ba�lang��ta otomatik admin kullan�c� olu�turma**
- JWT ile kimlik do�rulama
- Swagger UI ile API dok�mantasyonu
- Entity Framework Core ve SQL Server
- CORS deste�i
- Docker ile container olarak �al��t�rma
- Health Check ile veritaban� ve servis sa�l��� izleme
- Rate Limiting ile API istek s�n�rland�rma
- MemoryCache ile bellek i�i h�zl� veri saklama
- **XUnit ve Moq ile birim testler**

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
