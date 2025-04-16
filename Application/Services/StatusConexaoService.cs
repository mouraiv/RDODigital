// RDODigital.Application/Services/StatusConexaoService.cs
using Application.Interfaces;
using Application.DTOs;
using Domain.Interfaces;
using Domain.Entities;
using AutoMapper;

namespace RDODigital.Application.Services;

public class StatusConexaoService : IStatusConexaoService
{
    private readonly IStatusConexaoRepository _repository;
    private readonly IMapper _mapper;
    
    public StatusConexaoService(IStatusConexaoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task AtualizarStatusAsync(AtualizarStatusDTO statusDto)
    {
        var status = _mapper.Map<StatusConexao>(statusDto);
        status.UltimaVerificacao = DateTime.UtcNow;
        await _repository.AtualizarStatusAsync(status);
    }

    public async Task<StatusConexaoDTO> GetStatusUsuarioAsync(int idUsuario)
    {
        var status = await _repository.GetStatusPorUsuarioAsync(idUsuario);
        return _mapper.Map<StatusConexaoDTO>(status);
    }
}