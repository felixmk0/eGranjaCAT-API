﻿using nastrafarmapi.Entities.Moviments;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace nastrafarmapi.DTOs.Moviments.Entrades
{
    public class CreateEntradaDTO : IValidatableObject
    {
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipusCategories Categoria { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required]
        public int NombreAnimals { get; set; }

        [Required]
        public double PesTotal { get; set; }

        [Required]
        public double PesIndividual { get; set; }

        [Required]
        public int LotId { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipusOrigen Origen { get; set; }

        public string? MarcaOficial { get; set; }

        public string? CodiREGA { get; set; }

        [Required]
        public string NumeroDocumentTrasllat { get; set; } = null!;

        public string? Observacions { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (Origen)
            {
                case TipusOrigen.MarcaOficial:
                    if (string.IsNullOrWhiteSpace(MarcaOficial))
                    {
                        yield return new ValidationResult(
                            "Marca Oficial és obligatori quan Origen és Marca Oficial.",
                            new[] { nameof(MarcaOficial) });
                    }
                    if (!string.IsNullOrWhiteSpace(CodiREGA))
                    {
                        yield return new ValidationResult(
                            "Codi REGA no ha de tenir valor quan Origen és Marca Oficial.",
                            new[] { nameof(CodiREGA) });
                    }
                    break;

                case TipusOrigen.CodiREGA:
                    if (string.IsNullOrWhiteSpace(CodiREGA))
                    {
                        yield return new ValidationResult(
                            "Codi REGA és obligatori quan Origen és Codi REGA.",
                            new[] { nameof(CodiREGA) });
                    }
                    if (!string.IsNullOrWhiteSpace(MarcaOficial))
                    {
                        yield return new ValidationResult(
                            "Marca Oficial no ha de tenir valor quan Origen és Codi REGA.",
                            new[] { nameof(MarcaOficial) });
                    }
                    break;
            }
        }
    }
}
