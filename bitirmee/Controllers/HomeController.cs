using bitirmee.Models;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Neo4jClient;
using System.Diagnostics;

namespace bitirmee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGraphClient _client;

        public HomeController(IGraphClient client, ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = client;
        }
        
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> HediyeListem(string cinsiyet, DateTime dateofbirth, string hediyeturu, List<string> kategori)
        {
            // Kişinin yaşını hesaplamak için formdan gelen tarihten şuanki tarih çıkartılıyor ve sonrasında string olarak yas aralığı belirleniyor.
            DateTime now = DateTime.Now;
            int age = now.Year - dateofbirth.Year;

            string yasAraligi;

            if (age >= 0 && age <= 5)
            {
                yasAraligi = "Bebek";
            }
            else if (age > 5 && age <= 25)
            {
                yasAraligi = "Genç";
            }
            else if (age > 25 && age <= 55)
            {
                yasAraligi = "Yetişkin";
            }
            else
            {
                yasAraligi = "Yaşlı";
            }
            //Kullanıcıdan alınan verilere göre veritabanında filtreleme işlemi yapılıyor ve ürün türünde viewe ürünler dönülüyor.
            var query = await _client.Cypher.WithDatabase("yenidbdeneme")
    .Match("(c:Cinsiyet)-[:Iliski]->(a:AltKategori)", "(y:YasAraligi)-[:Iliski]->(a)", "(h:HediyeTuru)-[:Iliski]->(a)", "(k:Kategori)-[:Iliski]->(a) ,(a)-[:Sahip]->(p:Product)")
    .Where("y.`YaşAralığı` in [$yasAraligi] and c.türü in [$cinsiyet] and h.türü in [$hediyeturu] and k.türü in $kategori")
    .WithParam("yasAraligi", yasAraligi)
    .WithParam("cinsiyet", cinsiyet)
    .WithParam("hediyeturu", hediyeturu)
    .WithParam("kategori", kategori)
    .ReturnDistinct(p => p.As<Urun>())
    .ResultsAsync;
            //Burada gelen ürünleri rastgele bir şekilde dağıtıyoruz...
            var shuffledList = query.OrderBy(x => Guid.NewGuid()).AsEnumerable();
            return View(shuffledList);
        }
        public IActionResult HediyeOner()
        {

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}