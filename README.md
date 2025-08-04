# Losev Projesi

Losev, katmanlý mimariye sahip .NET 9 Web API uygulamasýdýr. Application, Domain, Infrastructure, WebAPI ve Test projelerinden oluþur. Proje; Entity Framework Core, MediatR, FluentValidation, AutoMapper ve JWT kimlik doðrulama kullanýr.

## Özellikler
1. C# 13 dil özellikleri
2. .NET Core 9.0 Web API
3. Katmanlý ve modüler mimari
4. SOLID prensipleri ve OOP
5. Clean Architecture ve Clean Code
6. AOP (Aspect Oriented Programming)
7. MediatR ile CQRS ve handler yapýsý
8. Generic Repository pattern
9. **Unit of Work pattern**
10. Scrutor ile otomatik servis kaydý
11. Result Pattern ile standart sonuç yönetimi
12. Roller ve rol tabanlý yetkilendirme
13. **Authentication ve Authorization mekanizmalarý**
14. **Identity API ile kimlik yönetimi**
15. **Ardalis.SmartEnum ile geliþmiþ enum yönetimi**
16. **Serilog ile geliþmiþ loglama**
17. **FluentValidation ile güçlü doðrulama**
18. **ExtensionsMiddleware ile baþlangýçta otomatik admin kullanýcý oluþturma**
19. **Extensions metodlarý ile kodun geniþletilebilirliði**
20. **JSON Converter ile veri dönüþümü**
21. **Password Hashing ile güvenli þifre saklama**
22. **AutoMapper ile nesne eþleme**
23. **DTO (Data Transfer Object) kullanýmý**
24. **SQL Backup dosyasý ile veri yedekleme**
25. **Code First yaklaþýmý ile veritabaný yönetimi**
26. **Ayrýntýlý README markdown dosyasý ile proje dokümantasyonu**
27. **EF Core ile geliþmiþ veri eriþimi**
28. JWT ile kimlik doðrulama
29. Swagger UI ile API dokümantasyonu
30. Entity Framework Core ve SQL Server
31. CORS desteði
32. Docker ile container olarak çalýþtýrma
33. Health Check ile veritabaný ve servis saðlýðý izleme
34. Rate Limiting ile API istek sýnýrlandýrma
35. MemoryCache ile bellek içi hýzlý veri saklama
36. **XUnit ve Moq ile birim testler**
37. **527 repoya sahip yazýlým geliþtirici: [alicanyucel.com.tr](https://alicanyucel.com.tr/)**

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
