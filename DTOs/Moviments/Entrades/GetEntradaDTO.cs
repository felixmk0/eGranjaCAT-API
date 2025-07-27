using nastrafarmapi.DTOs.Farms;
using nastrafarmapi.DTOs.Moviments.Lots;
using nastrafarmapi.DTOs.Users;
using nastrafarmapi.Entities.Moviments;
using System.Text.Json.Serialization;

namespace nastrafarmapi.DTOs.Moviments.Entrades
{
    public class GetEntradaDTO
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipusCategories Categoria { get; set; }

        public DateTime Data { get; set; }

        public int NombreAnimals { get; set; }

        public double PesTotal { get; set; }

        public double PesIndividual { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipusOrigen Origen { get; set; }

        public string? MarcaOficial { get; set; }

        public string? CodiREGA { get; set; }

        public string NumeroDocumentTrasllat { get; set; } = null!;

        public string? Observacions { get; set; }

        public GetLotNoRelationsDTO Lot { get; set; }
        public GetFarmDTO Farm { get; set; }
        public GetUserDTO User { get; set; }
    }
}
