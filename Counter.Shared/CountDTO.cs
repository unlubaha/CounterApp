using System;
using System.Text.Json.Serialization;

namespace Counter.Shared.DTOs
{
    public class CountRequestDTO
    {
        [JsonIgnore]
        public Guid UUID { get; set; } = Guid.NewGuid();
        public string SeriNumarasi { get; set; }

        [JsonIgnore]
        public DateTime OlcumZamani { get; set; } = DateTime.UtcNow;
        public decimal SonEndeks { get; set; }
        public decimal Voltaj { get; set; }
        public decimal Akim { get; set; }
    }
    public class CountResponseDTO
    {
        public Guid UUID { get; set; }
        public string SeriNumarasi { get; set; }
        public DateTime OlcumZamani { get; set; }
        public decimal SonEndeks { get; set; }
        public decimal Voltaj { get; set; }
        public decimal Akim { get; set; }
    }

}

