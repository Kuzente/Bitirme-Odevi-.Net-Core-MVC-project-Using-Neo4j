# Bitirme-Odevi-.Net-Core-MVC-project-Using-Neo4j
Bitirme ödevimde .NET Core MVC mimarisini kullanarak bir web sitesi geliştirdim. Kullanıcılara 4 soru sorularak, onlara en uygun ürünleri listelemeyi hedefledim.

Ürün ekleme işlemi, admin paneli üzerinden gerçekleştiriliyor. Python tarafında oluşturduğum yerel API'ye, ürünün ekleneceği kategori POST isteğiyle gönderiliyor. Ardından, BeautifulSoup kütüphanesini kullanarak bilinen bir e-ticaret sitesinden ürünler çekiliyor ve bu ürünler bir JSON dosyasına yazılıyor. .NET tarafında ise bu dosya GET isteğiyle çekilerek kullanıcıya sunuluyor. Bu sayede istenildiğinde güncel ürünler çekilip kullanıcılara sunulabiliyor.

Veritabanı olarak Neo4j tabanlı GraphDB teknolojisini tercih ettim. Bu sayede graf veritabanıyla çalışma fırsatı yakaladım ve projede farklı bir veritabanı deneyimi yaşadım.

Proje, bir bitirme ödevi olduğu için zaman kısıtlamaları nedeniyle eksiklikler içerebilir. Ancak, fırsat buldukça projede hataları düzeltmek ve geliştirmeler yapmak için çalışmalarıma devam edeceğim.
