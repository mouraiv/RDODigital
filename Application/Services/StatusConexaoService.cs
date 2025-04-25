// Application/Services/StatusConexaoService.cs
using Application.DTOs;
using Application.Exceptions;
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
        if (status == null)
        {
            throw new NotFoundException($"Status de conexão com ID {id} não encontrado.");
        }
        return _mapper.Map<StatusConexaoDTO>(status);
    }

    public async Task<IEnumerable<StatusConexaoDTO>> GetByUsuarioIdAsync(int usuarioId)
    {
        var statusList = await _repository.GetByUsuarioIdAsync(usuarioId);
        if (statusList == null || !statusList.Any())
        {
            throw new NotFoundException($"Nenhum status encontrado para o usuário ID {usuarioId}.");
        }
        return _mapper.Map<IEnumerable<StatusConexaoDTO>>(statusList);
    }

    public async Task<StatusConexaoDTO> CreateAsync(CreateStatusConexaoDTO dto)
    {
        var statusEntity = _mapper.Map<StatusConexao>(dto);

        var newId = await _repository.CreateAsync(statusEntity);
        if (newId == 0)
        {
            throw new ConflictException("Erro ao criar o status de conexão.");
        }

        var statusCriado = await _repository.GetByIdAsync(newId);
        return _mapper.Map<StatusConexaoDTO>(statusCriado);
    }


    public async Task<bool> UpdateAsync(UpdateStatusConexaoDTO dto)
    {
        var statusDto = _mapper.Map<StatusConexao>(dto);
        var status = await _repository.UpdateAsync(statusDto);
        if (!status)
        {
            throw new ConflictException("Erro ao atualizar o status de conexão.");
        }
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var status = await _repository.DeleteAsync(id);
        if (!status)
        {
            throw new NotFoundException($"Status de conexão com ID {id} não encontrado para exclusão.");
        }
        return true;
    }
}