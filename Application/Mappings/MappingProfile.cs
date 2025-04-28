// Application/Mappings/MappingProfile.cs
using Application.DTOs;
using Domain.Entities;
using AutoMapper;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamento para Usuario
        CreateMap<CreateUsuarioDTO, Usuario>()
            .ForMember(dest => dest.Data_criacao, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Telefone_corporativo, opt => opt.MapFrom(src => src.TelefoneCorporativo))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(_ => true))
            .ForMember(dest=> dest.Data_admissao, opt => opt.MapFrom(src => src.DataAdmissao))
            .ForMember(dest => dest.Senha_hash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Senha)));

        CreateMap<UpdateUsuarioDTO, Usuario>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Telefone_corporativo, opt => opt.MapFrom(src => src.TelefoneCorporativo))
            .ForMember(dest=> dest.Data_admissao, opt => opt.MapFrom(src => src.DataAdmissao))
            .ForMember(dest => dest.Senha_hash, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.Senha) ? null : BCrypt.Net.BCrypt.HashPassword(src.Senha)));

        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.TelefoneCorporativo, opt => opt.MapFrom(src => src.Telefone_corporativo))
            .ForMember(dest=> dest.DataAdmissao, opt => opt.MapFrom(src => src.Data_admissao))
            .ForAllMembers(opts => opts.ExplicitExpansion());

        // Mapeamentos para StatusConexao
        CreateMap<CreateStatusConexaoDTO, StatusConexao>()
            .ForMember(dest => dest.Forca_Sinal, opt => opt.MapFrom(src => src.ForcaSinal))
            .ForMember(dest => dest.Tipo_Conexao, opt => opt.MapFrom(src => src.TipoConexao))
            .ForMember(dest => dest.Ultima_Verificacao, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateStatusConexaoDTO, StatusConexao>()
            .ForMember(dest => dest.Forca_Sinal, opt => opt.MapFrom(src => src.ForcaSinal))
            .ForMember(dest => dest.Tipo_Conexao, opt => opt.MapFrom(src => src.TipoConexao));

        CreateMap<StatusConexao, StatusConexaoDTO>()
            .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.Id_Usuario))
            .ForMember(dest => dest.UltimaVerificacao, opt => opt.MapFrom(src => src.Ultima_Verificacao))
            .ForMember(dest => dest.ForcaSinal, opt => opt.MapFrom(src => src.Forca_Sinal))
            .ForMember(dest => dest.TipoConexao, opt => opt.MapFrom(src => src.Tipo_Conexao));

        // Mapeamentos para cargo
        CreateMap<CreateCargoDTO, Cargo>()
            .ForMember(dest => dest.Nome_cargo, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Data_criacao, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateCargoDTO, Cargo>()
            .ForMember(dest => dest.Nome_cargo, opt => opt.MapFrom(src => src.Nome));

        CreateMap<Cargo, CargoDTO>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome_cargo))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.Data_criacao));

        // Mapeamentos para cliente
        CreateMap<CreateClienteDTO, Cliente>()
            .ForMember(dest => dest.Nome_cliente, opt => opt.MapFrom(src => src.NomeCliente))
            .ForMember(dest => dest.Data_criacao, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(_ => true));
        
        CreateMap<UpdateClienteDTO, Cliente>()
            .ForMember(dest => dest.Nome_cliente, opt => opt.MapFrom(src => src.NomeCliente))
            .ForMember(dest => dest.Data_criacao, opt => opt.MapFrom(src => src.DataCriacao))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
            .ForMember(dest => dest.Foto_perfil, opt => opt.MapFrom(src => src.Foto_perfil));

        CreateMap<Cliente, ClienteDTO>()
            .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Nome_cliente))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.Data_criacao))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
            .ForMember(dest => dest.Foto_perfil, opt => opt.MapFrom(src => src.Foto_perfil));

        // Mapeamentos para Atividade
        CreateMap<CreateAtividadeDTO, Atividade>()
            .ForMember(dest => dest.Nome_atividade, opt => opt.MapFrom(src => src.NomeAtividade))
            .ForMember(dest => dest.Id_cliente, opt => opt.MapFrom(src => src.IdCliente))
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item))
            .ForMember(dest => dest.Classe, opt => opt.MapFrom(src => src.Classe))
            .ForMember(dest => dest.Unidade_medida, opt => opt.MapFrom(src => src.UnidadeMedida));

        CreateMap<UpdateAtividadeDTO, Atividade>()
            .ForMember(dest => dest.Nome_atividade, opt => opt.MapFrom(src => src.NomeAtividade))
            .ForMember(dest => dest.Id_cliente, opt => opt.MapFrom(src => src.IdCliente))
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item))
            .ForMember(dest => dest.Classe, opt => opt.MapFrom(src => src.Classe))
            .ForMember(dest => dest.Unidade_medida, opt => opt.MapFrom(src => src.UnidadeMedida));

        CreateMap<Atividade, AtividadeDTO>()
            .ForMember(dest => dest.NomeAtividade, opt => opt.MapFrom(src => src.Nome_atividade))
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Id_cliente))
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item))
            .ForMember(dest => dest.Classe, opt => opt.MapFrom(src => src.Classe))
            .ForMember(dest => dest.UnidadeMedida, opt => opt.MapFrom(src => src.Unidade_medida));

        // Mapeamentos para projeto
        CreateMap<CreateProjetoDTO, Projeto>()
            .ForMember(dest => dest.Titulo_infovia, opt => opt.MapFrom(src => src.TituloInfovia))
            .ForMember(dest => dest.Id_cliente, opt => opt.MapFrom(src => src.IdCliente))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
            .ForMember(dest => dest.Mes_referencia, opt => opt.MapFrom(src => src.MesReferencia))
            .ForMember(dest => dest.Id_fiscal, opt => opt.MapFrom(src => src.IdFiscal))
            .ForMember(dest => dest.Id_supervisor, opt => opt.MapFrom(src => src.IdSupervisor))
            .ForMember(dest => dest.Data_inicio, opt => opt.MapFrom(src => src.DataInicio))
            .ForMember(dest => dest.Data_fim, opt => opt.MapFrom(src => src.DataFim))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));

        CreateMap<UpdateProjetoDTO, Projeto>()
            .ForMember(dest => dest.Titulo_infovia, opt => opt.MapFrom(src => src.TituloInfovia))
            .ForMember(dest => dest.Id_cliente, opt => opt.MapFrom(src => src.IdCliente))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
            .ForMember(dest => dest.Mes_referencia, opt => opt.MapFrom(src => src.MesReferencia))
            .ForMember(dest => dest.Id_fiscal, opt => opt.MapFrom(src => src.IdFiscal))
            .ForMember(dest => dest.Id_supervisor, opt => opt.MapFrom(src => src.IdSupervisor))
            .ForMember(dest => dest.Data_inicio, opt => opt.MapFrom(src => src.DataInicio))
            .ForMember(dest => dest.Data_fim, opt => opt.MapFrom(src => src.DataFim))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Progresso_tempo, opt => opt.MapFrom(src => src.ProgressoTempo))
            .ForMember(dest => dest.Progresso_projeto, opt => opt.MapFrom(src => src.ProgressoProjeto));

        CreateMap<Projeto, ProjetoDTO>()
            .ForMember(dest => dest.TituloInfovia, opt => opt.MapFrom(src => src.Titulo_infovia))
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Id_cliente))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
            .ForMember(dest => dest.MesReferencia, opt => opt.MapFrom(src => src.Mes_referencia))
            .ForMember(dest => dest.IdFiscal, opt => opt.MapFrom(src => src.Id_fiscal))
            .ForMember(dest => dest.IdSupervisor, opt => opt.MapFrom(src => src.Id_supervisor))
            .ForMember(dest => dest.DataInicio, opt => opt.MapFrom(src => src.Data_inicio))
            .ForMember(dest => dest.DataFim, opt => opt.MapFrom(src => src.Data_fim))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ProgressoTempo, opt => opt.MapFrom(src => src.Progresso_tempo))
            .ForMember(dest => dest.ProgressoProjeto, opt => opt.MapFrom(src => src.Progresso_projeto))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.Data_criacao));

        // Mapeamentos para Material
        CreateMap<CreateMaterialDTO, Material>()
            .ForMember(dest => dest.Id_projeto, opt => opt.MapFrom(src => src.IdProjeto))
            .ForMember(dest => dest.Data_hora, opt => opt.MapFrom(src => src.DataHora))
            .ForMember(dest => dest.Id_atividade, opt => opt.MapFrom(src => src.IdAtividade))
            .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));

        CreateMap<UpdateMaterialDTO, Material>()
            .ForMember(dest => dest.Id_projeto, opt => opt.MapFrom(src => src.IdProjeto))
            .ForMember(dest => dest.Data_hora, opt => opt.MapFrom(src => src.DataHora))
            .ForMember(dest => dest.Id_atividade, opt => opt.MapFrom(src => src.IdAtividade))
            .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));

        CreateMap<Material, MaterialDTO>()
            .ForMember(dest => dest.IdProjeto, opt => opt.MapFrom(src => src.Id_projeto))
            .ForMember(dest => dest.DataHora, opt => opt.MapFrom(src => src.Data_hora))
            .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Id_atividade))
            .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));

        // Mapeamentos para RelatorioDiario
        CreateMap<CreateRelatorioDiarioDTO, RelatorioDiario>()
            .ForMember(dest => dest.Sincronizado, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.Ultima_sincronizacao, opt => opt.Ignore());

        CreateMap<UpdateRelatorioDiarioDTO, RelatorioDiario>();

        CreateMap<RelatorioDiario, RelatorioDiarioDTO>();

    }
}
