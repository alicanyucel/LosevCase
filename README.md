# Losev Projesi

Losev, katmanlý mimariye sahip .NET 9 Web API uygulamasýdýr. Application, Domain, Infrastructure, WebAPI ve Test projelerinden oluþur. Proje; Entity Framework Core, MediatR, FluentValidation, AutoMapper ve JWT kimlik doðrulama kullanýr.

## Özellikler
- Katmanlý ve modüler mimari
- SOLID prensipleri ve OOP
- Clean Architecture ve Clean Code
- AOP (Aspect Oriented Programming)
- MediatR ile CQRS ve handler yapýsý
- Generic Repository pattern
- **Unit of Work pattern**
- Scrutor ile otomatik servis kaydý
- Result Pattern ile standart sonuç yönetimi
- Roller ve rol tabanlý yetkilendirme
- **Ardalis.SmartEnum ile geliþmiþ enum yönetimi**
- **Serilog ile geliþmiþ loglama**
- **FluentValidation ile güçlü doðrulama**
- **ExtensionsMiddleware ile baþlangýçta otomatik admin kullanýcý oluþturma**
- JWT ile kimlik doðrulama
- Swagger UI ile API dokümantasyonu
- Entity Framework Core ve SQL Server
- CORS desteði
- Docker ile container olarak çalýþtýrma
- Health Check ile veritabaný ve servis saðlýðý izleme
- Rate Limiting ile API istek sýnýrlandýrma
- MemoryCache ile bellek içi hýzlý veri saklama
- **XUnit ve Moq ile birim testler**

## Kurulum

### Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (veya baðlantý dizesini kendi veritabanýnýza göre güncelleyin)
- [Docker](https://www.docker.com/) (opsiyonel)

### Adýmlar
1. Depoyu klonlayýn:git clone <repo-url>2. NuGet paketlerini yükleyin:dotnet restore3. `Losev.WebAPI` içindeki `appsettings.json` dosyasýndan baðlantý dizesini güncelleyin.
4. Veritabaný migrasyonlarýný uygulayýn:dotnet ef database update --project Losev/Losev.Infrastructure5. API'yi çalýþtýrýn:dotnet run --project Losev/Losev.WebAPI
### Docker ile Çalýþtýrma
1. Kök dizinde veya `Losev.WebAPI` klasöründe aþaðýdaki komutla imajý oluþturun:docker build -t losev-api .2. Container'ý baþlatýn:docker run -p 8080:80 losev-api3. Swagger arayüzüne `http://localhost:8080/swagger` adresinden eriþebilirsiniz.

### Kullaným
- Ýlk çalýþtýrmada otomatik olarak admin kullanýcýsý oluþturulur:
  - Kullanýcý adý: `admin`
  - E-posta: `admin@admin.com`
  - Þifre: `1`

## Proje Yapýsý
- **Losev.Application**: Ýþ mantýðý, CQRS handler'larý
- **Losev.Domain**: Varlýklar, enumlar
- **Losev.Infrastructure**: Veri eriþimi, EF Core, repository'ler
- **Losev.WebAPI**: API controller'larý, middleware, baþlangýç
- **Losev.Test**: Test katmaný, birim testler (xUnit, Moq)

## Test Katmaný
`Losev.Test` projesi, uygulamanýn birim testlerini içerir. Özellikle Login iþlemleri için LoginCommandHandlerTests gibi testler eklenmiþtir. Testler Moq ve xUnit ile yazýlmýþtýr.

Testleri çalýþtýrmak için:dotnet test
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

## Katký
Pull request gönderebilirsiniz. Büyük deðiþiklikler için önce bir issue açarak tartýþma baþlatmanýz önerilir.

## Lisans
Lisans bilgisi buraya eklenmelidir.
