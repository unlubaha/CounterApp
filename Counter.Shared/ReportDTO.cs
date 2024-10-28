using System;
using Counter.Shared.Enums;

namespace Counter.Shared.DTOs
{
    public class ReportDTO
    {
        public Guid UUID { get; set; } = Guid.NewGuid();
        public DateTime TalepTarihi { get; set; }
        public RaporDurumu Durum { get; set; }
        public OlcumTipi Icerik { get; set; }
    }
}



