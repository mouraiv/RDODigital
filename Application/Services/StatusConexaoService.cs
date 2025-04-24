// Application/Services/StatusConexaoService.cs
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class StatusConexaoService : IStatusConexaoService
{
    private readonly IStatusConexaoRepository _repository;
    private readonly IMapper _mapper;

    public StatusConexaoService(IStatusConexaoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StatusConexaoDTO?> GetByIdAsync(int id)
    {
        var status = await _repository.GetByIdAsync(id);
        return _mapper.Map<StatusConexaoDTO>(status);
    }

    public async Task<IEnumerable<StatusConexaoDTO>> GetByUsuarioIdAsync(int usuarioId)
    {
        var statusList = await _repository.GetByUsuarioIdAsync(usuarioId);
        return _mapper.Map<IEnumerable<StatusConexaoDTO>>(statusList);
    }

    public async Task<StatusConexaoDTO> CreateAsync(CreateStatusConexaoDTO dto)
    {
        var status = _mapper.Map<StatusConexao>(dto);
        await _repository.CreateAsync(status);
        return _mapper.Map<StatusConexaoDTO>(status);
    }

    public async Task<bool> UpdateAsync(UpdateStatusConexaoDTO dto)
    {
        var status = _mapper.Map<StatusConexao>(dto);
        return await _repository.UpdateAsync(status);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}