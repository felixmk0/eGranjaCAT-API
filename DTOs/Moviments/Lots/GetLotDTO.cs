using nastrafarmapi.DTOs.Farms;
using nastrafarmapi.DTOs.Users;

namespace nastrafarmapi.DTOs.Moviments.Lots
{
    public class GetLotDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public GetUserDTO User { get; set; }
        public GetFarmDTO Farm { get; set; }
    }
}
