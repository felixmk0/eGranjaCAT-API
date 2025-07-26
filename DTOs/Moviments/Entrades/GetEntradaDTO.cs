using nastrafarmapi.Entities.Moviments;
using System.Text.Json.Serialization;

namespace nastrafarmapi.DTOs.Moviments.Entrades
{
    public class GetEntradaDTO
    {
        public int Id { get; set; }

        public int FarmId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipusCategories Categoria { get; set; }

        public DateTime Data { get; set; }

        public int NombreAnimals { get; set; }

        public double PesTotal { get; set; }

        public double PesIndividual { get; set; }

        public int LotId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipusOrigen Origen { get; set; }

        public string? MarcaOficial { get; set; }

        public string? CodiREGA { get; set; }

        public string NumeroDocumentTrasllat { get; set; } = null!;

        public string? Observacions { get; set; }
    }
}
