# Losev Projesi

Losev, katmanlý mimariye sahip .NET 9 Web API uygulamasýdýr. Application, Domain, Infrastructure ve WebAPI projelerinden oluþur. Proje; Entity Framework Core, MediatR, FluentValidation, AutoMapper ve JWT kimlik doðrulama kullanýr.

## Özellikler
- Katmanlý ve modüler mimari
- JWT ile kimlik doðrulama
- Swagger UI ile API dokümantasyonu
- Entity Framework Core ve SQL Server
- Baþlangýçta otomatik admin kullanýcý oluþturma
- CORS desteði
- Docker ile container olarak çalýþtýrma

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

## Teknolojiler
- .NET 9
- Entity Framework Core
- MediatR
- FluentValidation
- AutoMapper
- Swashbuckle (Swagger)
- JWT Authentication
- Docker

## Katký
Pull request gönderebilirsiniz. Büyük deðiþiklikler için önce bir issue açarak tartýþma baþlatmanýz önerilir.

## Lisans
Lisans bilgisi buraya eklenmelidir.
