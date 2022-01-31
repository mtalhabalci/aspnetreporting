# Rehber Uygulaması
### Açıklama

Rehber uygulaması kişilerin isim, soyisim ve şirketlerinin ve iletişim bilgilerinin tutulup excel formatı halinde rapor çıktısı veren bir uygulamadır.

Projede kullanılan teknolojiler; VueJS, ASP.Net Core, Postgresql, Rabbitmq

Arayüzden kişi ve iletişim bilgilerinin silme, güncelleme, listeleme işlemlerini yapıp aynı zamanda rapor isteyebildiği proje monolith olarak geliştirilmiştir.


Api projesine istek atıldığı zaman veri tabanına kayıt yapılıp kuyruğa alınma işlemi yapılıyor.

Kuyruğu dinleyen bir yapı ilgili event tetiklendiğinde rapor üretim veri tabanındaki rapor satırında gerekli güncellemeleri yapıyor.



*   [Açıklama](#definition)
*   [Clone](#clone)
*   [Projeyi Ayaklandırma](#projeyi-ayaklandırma)

    *   [Web Siteleri](#web-siteleri)
    
        *   [Public](#rise-public-uygulaması)
    
    *   [API Servisleri](#api-servisleri)
    
        *   [Public](#rise-public-api)


*   [Veri Tabanı](#veri-tabanı)
*   [Ortam Bilgileri](#environment)
    * [Development Ortamı](#development-ortamı)
    


# Clone
     git clone https://github.com/mtalhabalci/aspnetreporting.git

Projeyi yukarıdaki kodu çalıştırarak makinenize çekebilirsiniz.

# Projeyi Ayaklandırma
## Web Siteleri

### Rise Public Uygulaması

    cd clients\rise-client
    npm install
    npm start
Projeye `http://localhost:1071` portundan erişebilirsiniz.

## API Servisleri
### Rise Public API
    cd services\src\RiseWebApi
    dotnet run
API swagger adresleri: `https://localhost:30011/swagger/index.html` ve `http://localhost:30021/swagger/index.html`.


# Veri Tabanı
     cd services\src\Rise.Migrator
     dotnet run
Çalışan konsol uygulamasındaki yönlendirmeleri takip ederek veri tabanı kurulumunu tamamlayabilirsiniz.


# Environment
## Development Ortamı
Projeyi lokalinizde ayaklandırmanız için bilgisayarınızdaki ortam değişkenlerine adı "ASPNETCORE_ENVIRONMENT", değeri "Development" olan bir değişken eklemeniz gerekiyor. 

Client projeleri ile alakalı ortam ayarlarını proje dizinindeki `.env.development` dosyasından, API projeleri ile alakalı ortam ayarlarını proje dizinindeki `appsettings.Development.json` dosyalarından görebilirsiniz.
