// Application/Mappings/MappingProfile.cs
using Application.DTOs;
using Domain.Entities;
using AutoMapper;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamento de CreateUsuarioDTO para Usuario
        CreateMap<CreateUsuarioDTO, Usuario>()
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Telefone_corporativo, opt => opt.MapFrom(src => src.TelefoneCorporativo))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(_ => true))
            .ForMember(dest=> dest.Data_admissao, opt => opt.MapFrom(src => src.DataAdmissao))
            .ForMember(dest => dest.Senha_hash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Senha)));

        // Mapeamento de UpdateUsuarioDTO para Usuario (CORRIGIDO)
        CreateMap<UpdateUsuarioDTO, Usuario>()
            .ForMember(dest => dest.Id_usuario, opt => opt.Ignore())
            .ForMember(dest => dest.Telefone_corporativo, opt => opt.MapFrom(src => src.TelefoneCorporativo))
            .ForMember(dest=> dest.Data_admissao, opt => opt.MapFrom(src => src.DataAdmissao))
            .ForMember(dest => dest.Senha_hash, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.Senha) ? null : BCrypt.Net.BCrypt.HashPassword(src.Senha)));

        // Mapeamento de Usuario para UsuarioDTO (CORRIGIDO)
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id_usuario))
            .ForMember(dest => dest.TelefoneCorporativo, opt => opt.MapFrom(src => src.Telefone_corporativo))
            .ForMember(dest=> dest.DataAdmissao, opt => opt.MapFrom(src => src.Data_admissao))
            // Garanta que nenhuma propriedade extra seja mapeada
            .ForAllMembers(opts => opts.ExplicitExpansion());
    }
}
