using nastrafarmapi.Entities;

namespace nastrafarmapi.Entities.Moviments
{
    public class Entrada
    {
        public int Id { get; set; }

        public TipusCategories Categoria { get; set; }

        public DateTime Data { get; set; }
        public int NombreAnimals { get; set; }
        public double PesTotal { get; set; }
        public double PesIndividual { get; set; }
        public int LotId { get; set; }

        public TipusOrigen Origen { get; set; }

        public string? MarcaOficial { get; set; }
        public string? CodiREGA { get; set; }

        public string NumeroDocumentTrasllat { get; set; } = null!;
        public string? Observacions { get; set; }


        public string CreatedBy { get; set; } = null!;
        public User CreatedByUser { get; set; } = null!;

        public int FarmId { get; set; }
        public Farm Farm { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
