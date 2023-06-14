using bitirmee.DTO;
using bitirmee.Models;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Neo4jClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace bitirmee.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGraphClient _client;
        private readonly AdminDto _adminDto;

        public AdminController(IGraphClient client, AdminDto adminDto)
        {
            _client = client;
            _adminDto = adminDto;
        }

        public async Task<IActionResult> Index()
        {

            //Tüm Ürünleri Getir
            var urunler = await _client.Cypher.WithDatabase("yenidbdeneme")
                .Match("(u:Product)")
                .ReturnDistinct(u => u.As<Urun>())
                .ResultsAsync;
            _adminDto.Uruns = urunler;
            //Sadece ürünlere bağlı olan kategorileri getir diyoruz çünkü ürünü olmayan kategorilerin select listesinde görünmesini istemiyoruz
            var kategoriler = await _client.Cypher.WithDatabase("yenidbdeneme")
                .Match("(k:AltKategori)-[]->(u:Product)")
                .ReturnDistinct(k => k.As<AltKategori>())
                .OrderBy("k.adi")
                .ResultsAsync;
                _adminDto.UrunluKategoris = kategoriler;
 
            return View(_adminDto);
        }
        public async Task<IActionResult> Urunler()
        {
            //Burada ayrı ayrı filtreleme yapmamızın sebebi viewde ayrı ayrı ihtiyaç duymamızdan dolayıdır.

            //Herhangi bir ürüne bağlı olmayan kategorileri getir
            var urunsuzKategoriler = await _client.Cypher.WithDatabase("yenidbdeneme")
                .Match("(k:AltKategori)")
                .Where("NOT (k)-[]->(:Product)")
                .ReturnDistinct(k => k.As<AltKategori>())
                .OrderBy("k.adi")
                .ResultsAsync;
            //Sadece ürünlere bağlı olan kategorileri getir
            var urunluKategoriler2 = await _client.Cypher.WithDatabase("yenidbdeneme")
                .Match("(k:AltKategori)-[]->(u:Product)")
                .ReturnDistinct(k => k.As<AltKategori>())
                .OrderBy("k.adi")
                .ResultsAsync;
            _adminDto.UrunsuzKategoris = urunsuzKategoriler;
            _adminDto.UrunluKategoris = urunluKategoriler2;
            return View(_adminDto);
        }
        [HttpPost]
        public async Task<IActionResult> UrunEkle(string category)
        {
            //gönderilen categorynin url adresi
            var postUrl = "http://localhost:8000/api/send-string";
            using (var httpClient = new HttpClient())
            {
                //categoryi json olarak cliente ekliyoruz
                var content = new StringContent("{\"category\": \"" + category + "\"}", System.Text.Encoding.UTF8, "application/json");
                //burada categoryi 8000 portuna post ediyoruz
                var categoryResponse = await httpClient.PostAsync(postUrl, content);
                //8000 portundan ilgili categorynin ürünlerini get ediyoruz
                var response = await httpClient.GetAsync($"http://localhost:8000/api/{category}/products");
                //gelen responsu json olarak okuyoruz
                var json = await response.Content.ReadAsStringAsync();
                //okunan jsonu jarray e dönüştürüyoruz
                var products = JArray.Parse(json);
                //burada donguye girip urunlerin tek tek jsondan okunup cypher sorgusu ile create işlemini yaptırıyoruz
                foreach (var product in products)
                {
                    var link = product.Value<string>("UrunLink");
                    var name = product.Value<string>("UrunAd");
                    var price = product.Value<int>("UrunFiyat");
                    var brand = product.Value<string>("UrunMarka");
                    var image_url = product.Value<string>("UrunResim");
                    var query = _client.Cypher.WithDatabase("yenidbdeneme")
                .Match("(c:AltKategori {adi :$category})")
                .Create("(c)-[:Sahip]->(p:Product {link: $link, name: $name, price: toInteger($price), brand: $brand, image_url: $image_url})")
                .WithParam("link", link)
                .WithParam("name", name)
                .WithParam("price", price)
                .WithParam("brand", brand)
                .WithParam("category", category)
                .WithParam("image_url", image_url);
                    await query.ExecuteWithoutResultsAsync();
                }
            }
            return RedirectToAction("Urunler");
        }
        public async Task<IActionResult> UrunSil(string category)
        {
            //İlgili cypher sorgusu ile veritabanından silme işlemi
            await _client.Cypher.WithDatabase("yenidbdeneme")
           .Match("(a:AltKategori{adi : $category})-[s:Sahip]->(u:Product)")
           .WithParam("category", category)
           .Delete("u, s")
           .ExecuteWithoutResultsAsync();
            return RedirectToAction("Urunler");
        }
        [HttpGet]
        public async Task<IActionResult> GetByCategory(string category)
        {
            //istenilen kategoriye göre filtreleme işlemi
            var getProductByCategory = await _client.Cypher.WithDatabase("yenidbdeneme")
                .Match("(a:AltKategori{adi : $category})-[]->(u:Product)")
                .WithParam("category", category)
                .ReturnDistinct(u => u.As<Urun>())
                .ResultsAsync;
            _adminDto.Uruns = getProductByCategory;

            return View("Index", _adminDto);
        }
    }
}
