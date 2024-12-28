Proje Adı: Kuaför/Güzellik Salonu Yönetim Uygulaması
Amaç:
Web Programlama dersi kapsamında, ASP.NET Core MVC kullanarak bir Kuaför/Güzellik Salonu (bayanlar) ve Kuaför/Berber (erkekler) salonu yönetim sistemi geliştirilmesi amaçlanmaktadır. Proje, salon yönetimini daha verimli hale getirecek bir dizi işlevi içerir. Kullanıcılar, kuaför ya da berber salonlarına ait işlemleri, çalışanları, randevuları ve yapay zeka entegrasyonu ile saç önerilerini kolaylıkla yönetebileceklerdir.

Proje Özeti:
Proje, salonların sunduğu işlemleri, bu işlemlerin süre ve ücret bilgilerini, çalışanların uzmanlık alanlarını ve müsaitlik durumlarını tanımlayan bir sistem geliştirmeyi hedefler. Kullanıcılar, uygun çalışanlardan randevu alabilir, çalışanlar ise günlük kazançları ve verimliliklerini izleyebilir. Ayrıca, REST API ve yapay zeka entegrasyonu sayesinde kullanıcılar saç modeli ve renk önerileri alabileceklerdir.

Kullanılacak Teknolojiler:
ASP.NET Core MVC (6 ve üzeri sürümler)
C#
Veritabanı: SQL Server, PostgreSQL veya benzeri
Entity Framework Core ORM
Bootstrap: Responsive ve modern kullanıcı arayüzü için
HTML5, CSS3, JavaScript, jQuery: Frontend geliştirme
Yapay Zeka Entegrasyonu: Saç modeli ve renk önerileri için AI aracı
Proje Konsepti ve Gereksinimler:
Kuaför/Berber Tanımlamaları:

Salon Yönetimi: Kuaför ve berber salonları farklı yetkilerle tanımlanabilir. Bir salonun çalışma saatleri, sunduğu işlemler, işlem süreleri ve ücretleri detaylı şekilde tanımlanabilir.
İşlem Tanımlamaları: Kuaför/berber salonlarının sunduğu işlemler, her işlem için süre ve ücret bilgileri eklenebilir.
Çalışan Yönetimi:

Çalışan Ekleme: Salonlarda çalışan personel sisteme tanımlanabilir.
Uzmanlık Alanı ve Beceri Tanımlamaları: Çalışanların uzmanlık alanları ve gerçekleştirebileceği işlemler belirlenebilir.
Uygunluk Saatleri: Çalışanların uygun saatleri sisteme tanımlanarak, kullanıcılar bu saatler üzerinden randevu alabilir.
Randevu Sistemi:

Randevu Alma: Kullanıcılar, sistem üzerinden çalışanların müsait olduğu saatlere ve yapılacak işlemlere göre randevu alabilirler.
Randevu Uyarı Mekanizması: Önceki randevular göz önünde bulundurularak, çakışan saatlerde kullanıcıya uyarı verilecektir.
Randevu Onay Mekanizması: Kullanıcılar randevu almak için bir işlem talebinde bulunur, ardından salon yetkilisi veya çalışan tarafından onaylanması gerekir.
Randevu Detayları: İşlem, ücret, süre ve çalışan bilgileri saklanır.
REST API Kullanımı:

Projenin en az bir kısmında REST API kullanılacaktır. Bu API, veritabanı ile iletişim kurmak ve veri almak için kullanılabilir. API üzerinden veritabanı işlemleri yapılabilir ve sistemin diğer bölümleriyle entegre çalışabilir.
Yapay Zeka Entegrasyonu:

Projeye Yapay Zeka entegrasyonu eklenerek kullanıcılar, sisteme fotoğraf yükleyip yapay zeka aracılığıyla saç kesimi ve renk önerileri alabilirler. Bu, kullanıcı deneyimini artıracak ve salona gelen kişilere kişisel öneriler sunacaktır.
Modeller ve Veritabanı:
Admin: Admin kullanıcıları ile ilgili bilgileri tutar.
Calisan: Salon çalışanları bilgilerini, becerilerini ve uzmanlık alanlarını içerir.
Islem: Salonun sunduğu işlemler hakkında bilgileri içerir (ad, ücret, süre).
Randevu: Kullanıcıların ve çalışanların randevu bilgilerini içerir (işlem, ücret, çalışan bilgisi).
Salon: Salon türünü (Kuaför/Berber) ve çalışma saatlerini belirtir.
Proje Akış Süreci:
Veritabanı ve Modellerin Oluşturulması:

Çalışanlar, işlemler, salonlar ve randevular için gerekli modeller tanımlanacak ve veritabanı ile entegre edilerek migration yapılacaktır.
REST API Geliştirilmesi:

API üzerinden veritabanı işlemleri yapılacak ve sorgulama yapılabilecek bir endpoint oluşturulacak.
Yapay Zeka Entegrasyonu:

Saç modeli ve renk önerileri için bir yapay zeka API'si veya aracı entegre edilecek.
Admin Paneli ve Kullanıcı Arayüzü:

Admin paneli üzerinden kullanıcılar, çalışanlar ve randevular yönetilebilecek. Ayrıca, müşteri randevu alma ekranı da kolay bir şekilde tasarlanacak.
Test ve Yayınlama:

Tüm modüller ve işlevler test edilecek, eksik veya hatalı noktalar giderildikten sonra proje sonlandırılacaktır.
