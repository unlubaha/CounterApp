using System;

namespace Counter.Shared.DTOs
{
    public class CountDTO
    {
        public Guid UUID { get; set; } = Guid.NewGuid();
        public string SeriNumarasi { get; set; }
        public DateTime OlcumZamani { get; set; }
        public double SonEndeks { get; set; }
        public double Voltaj { get; set; }
        public double Akim { get; set; }
    }
}

