using System.Text.Json.Serialization;

namespace nastrafarmapi.DTOs.GTR.Guies
{
    public class LoadDSTGuidesResponseDTO
    {
        [JsonPropertyName("GUIAS")]
        public List<DSTGuideDTO> Guias { get; set; } = [];
    }
}
