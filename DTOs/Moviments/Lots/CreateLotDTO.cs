using System.ComponentModel.DataAnnotations;

namespace nastrafarmapi.DTOs.Moviments.Lots
{
    public class CreateLotDTO
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required int FarmId { get; set; }
    }
}