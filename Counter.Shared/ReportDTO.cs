using System;
using System.Text.Json.Serialization;
using Counter.Shared.Enums;

namespace Counter.Shared.DTOs
{
    public class ReportRequestDTO
    {
        [JsonIgnore]
        public Guid UUID { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public DateTime TalepTarihi { get; set; }= DateTime.UtcNow;
        public RaporDurumu Durum { get; set; }
        public Icerik Icerik { get; set; }
    }
    public class ReportResponseDTO
    {
        public Guid UUID { get; set; } 
        public DateTime TalepTarihi { get; set; }
        public RaporDurumu Durum { get; set; }
        public Icerik Icerik { get; set; }
    }
    public class Icerik
    {
        public DateTime OlcumZamani { get; set; }
        public decimal SonEndeks { get; set; }
        public decimal Voltaj { get; set; }
        public decimal Akim { get; set; }
    }
}
