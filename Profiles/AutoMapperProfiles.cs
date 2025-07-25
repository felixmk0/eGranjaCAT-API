using AutoMapper;
using nastrafarmapi.DTOs.Farms;
using nastrafarmapi.DTOs.Moviments.Entrades;
using nastrafarmapi.DTOs.Moviments.Lots;
using nastrafarmapi.DTOs.Users;
using nastrafarmapi.Entities;
using nastrafarmapi.Entities.Moviments;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CreateFarmDTO, Farm>();
        CreateMap<Farm, GetFarmDTO>();


        CreateMap<CreateUserDTO, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        CreateMap<User, TokenUserDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<User, GetUserDTO>();

        CreateMap<CreateLotDTO, Lot>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        CreateMap<UpdateLotDTO, Lot>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FarmId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedByUser, opt => opt.Ignore())
            .ForMember(dest => dest.Farm, opt => opt.Ignore());

        CreateMap<CreateEntradaDTO, Entrada>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
