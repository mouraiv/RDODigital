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

        // Mapeamentos para StatusConexao
        CreateMap<CreateStatusConexaoDTO, StatusConexao>()
            .ForMember(dest => dest.Id_Usuario, opt => opt.MapFrom(src => src.IdUsuario))
            .ForMember(dest => dest.Forca_Sinal, opt => opt.MapFrom(src => src.ForcaSinal))
            .ForMember(dest => dest.Tipo_Conexao, opt => opt.MapFrom(src => src.TipoConexao))
            .ForMember(dest => dest.Ultima_Verificacao, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateStatusConexaoDTO, StatusConexao>()
            .ForMember(dest => dest.Id_Status, opt => opt.MapFrom(src => src.IdStatus))
            .ForMember(dest => dest.Forca_Sinal, opt => opt.MapFrom(src => src.ForcaSinal))
            .ForMember(dest => dest.Tipo_Conexao, opt => opt.MapFrom(src => src.TipoConexao));

        CreateMap<StatusConexao, StatusConexaoDTO>()
            .ForMember(dest => dest.IdStatus, opt => opt.MapFrom(src => src.Id_Status))
            .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.Id_Usuario))
            .ForMember(dest => dest.UltimaVerificacao, opt => opt.MapFrom(src => src.Ultima_Verificacao))
            .ForMember(dest => dest.ForcaSinal, opt => opt.MapFrom(src => src.Forca_Sinal))
            .ForMember(dest => dest.TipoConexao, opt => opt.MapFrom(src => src.Tipo_Conexao));

        CreateMap<CreateCargoDTO, Cargo>()
            .ForMember(dest => dest.Nome_cargo, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Data_criacao, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateCargoDTO, Cargo>()
            .ForMember(dest => dest.Id_cargo, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nome_cargo, opt => opt.MapFrom(src => src.Nome));

        CreateMap<Cargo, CargoDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id_cargo))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome_cargo))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.Data_criacao));
    }
}
