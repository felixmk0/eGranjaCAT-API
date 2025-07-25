namespace nastrafarmapi.DTOs
{
    public class AuthResponseDTO
    {
        public required string Token { get; set; }
        public DateTime Expiraton { get; set; }
    }
}
