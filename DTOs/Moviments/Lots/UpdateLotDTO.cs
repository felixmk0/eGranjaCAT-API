using System.ComponentModel.DataAnnotations;

namespace nastrafarmapi.DTOs.Moviments.Lots
{
    public class UpdateLotDTO
    {
        public string? Name { get; set; }
        public bool? Active { get; set; } = true;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
