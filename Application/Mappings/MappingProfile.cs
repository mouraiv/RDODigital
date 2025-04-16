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
            .ForMember(dest => dest.SenhaHash, opt => opt.Ignore()) // Será tratado manualmente
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.FotoPerfil, opt => opt.Ignore()); // Opcional se não for enviado no DTO

        // Mapeamento de UpdateUsuarioDTO para Usuario
        CreateMap<UpdateUsuarioDTO, Usuario>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => 
                srcMember != null)); // Só atualiza se o valor não for nulo

        // Mapeamento de Usuario para UsuarioDTO
        CreateMap<Usuario, UsuarioDTO>();
    }
}