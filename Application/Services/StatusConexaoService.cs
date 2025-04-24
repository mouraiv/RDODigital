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
        try
        {
            var status = await _repository.GetByIdAsync(id);
            if (status == null)
            {
                throw new NotFoundException($"Status de conexão com ID {id} não encontrado.");
            }
            return _mapper.Map<StatusConexaoDTO>(status);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao obter o status de conexão: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<StatusConexaoDTO>> GetByUsuarioIdAsync(int usuarioId)
    {
        try
        {
            var statusList = await _repository.GetByUsuarioIdAsync(usuarioId);
            if (statusList == null || !statusList.Any())
            {
                throw new NotFoundException($"Nenhum status encontrado para o usuário ID {usuarioId}.");
            }
            return _mapper.Map<IEnumerable<StatusConexaoDTO>>(statusList);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao obter o status de conexão: {ex.Message}", ex);
        }
    }

    public async Task<StatusConexaoDTO> CreateAsync(CreateStatusConexaoDTO dto)
    {
        try
        {
            var statusDto = _mapper.Map<StatusConexao>(dto);  
            var status = await _repository.CreateAsync(statusDto);
            if (status == 0)
            {
                throw new NotFoundException("Erro ao criar o status de conexão.");
            }
            return _mapper.Map<StatusConexaoDTO>(status);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao criar o status de conexão: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(UpdateStatusConexaoDTO dto)
    {
        try
        {
            var statusDto = _mapper.Map<StatusConexao>(dto);
            var status = await _repository.UpdateAsync(statusDto);
            if (!status)
            {
                throw new NotFoundException("Erro ao atualizar o status de conexão.");
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao atualizar o status de conexão: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var status = await _repository.DeleteAsync(id);
            if (!status)
            {
                throw new NotFoundException($"Status de conexão com ID {id} não encontrado para exclusão.");
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao deletar o status de conexão: {ex.Message}", ex);
        }
    }
}