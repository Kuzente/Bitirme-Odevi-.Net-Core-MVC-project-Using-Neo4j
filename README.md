# Bitirme-Odevi-.Net-Core-MVC-project-Using-Neo4j
Yapmış olduğum bitirme ödevimde .Net Core MVC mimarisi kullanarak bir web sitesi geliştirdim. Kullanıcıya sorulan 4 soru neticesinde gerekli ona enn uygun ürünler listelenerek sunulmaktadır.

Ürün Ekleme işlemi Admin panelinden yapılıyor olup ilgili kodlar ile Python da oluşturduğum local deki API ye urun eklenmek istenen kategoriyi post ediyor ve ilgili kategoriye ait ürünler BeautifulSoup kutuphanesi kullanılarak bilinen bir E-ticaret sisteminden ürünler çekiliyor daha sonrasında çekilen bu ürünler bir json dosyasına yazılıyor ve .net tarafında GET ediliyor. Böylece istenildiği zaman güncel ürünler çekilip kullanıcıya sunulabiliyor.

Veritabanı olarak GraphDB tabanlı Neo4j teknolojisini kullanmayı tercih ettim. Böylelikle ilk defa bir projede Graf veritabanıyla çalışma fırsatı bulmuş oldum.

Projede birçok eksik var bunun sebebi bitirme ödevi olduğu için belirli bir tarihe yetişmesi gerekiyordu.Fırsat buldukça proje üzerinde hataların giderilmesi ve geliştirilmesi konusunda çalışmalarım devam edecektir.
