using System;
using Counter.Shared.Enums;

namespace Counter.Shared.DTOs
{
    public class ReportDTO
    {
        public Guid UUID { get; set; } = Guid.NewGuid();
        public DateTime TalepTarihi { get; set; }
        public RaporDurumu Durum { get; set; }
        public Icerik Icerik { get; set; }
    }
    public class Icerik
    {
        public DateTime OlcumZamani { get; set; }
        public int SonEndeks { get; set; }
        public double Voltaj { get; set; }
        public double Akim { get; set; }
    }
}



