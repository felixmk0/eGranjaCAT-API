using System.Text.Json.Serialization;

namespace nastrafarmapi.DTOs.GTR
{
    public class LoadDSTGuidesResponseDTO
    {
        [JsonPropertyName("GUIAS")]
        public List<DSTGuideDTO> Guias { get; set; } = [];
    }
}
