using bitirmee.Models;

namespace bitirmee.DTO
{
    public class AdminDto
    {
        public IEnumerable<AltKategori> UrunsuzKategoris { get; set; }
        public IEnumerable<AltKategori> UrunluKategoris { get; set; }
        public IEnumerable<Urun>  Uruns { get; set; }
    }
}
