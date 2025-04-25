using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class RelatorioDiarioService : IRelatorioDiarioService
{
    private readonly IRelatorioDiarioRepository _repository;
    private readonly IMapper _mapper;

    public RelatorioDiarioService(IRelatorioDiarioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RelatorioDiarioDTO?> GetByIdAsync(int id)
    {
        var RelatorioDiario = await _repository.GetByIdAsync(id);
        if (RelatorioDiario == null)
        {
            throw new NotFoundException($"Relatorio Diario com ID {id} não encontrado.");
        }
        return _mapper.Map<RelatorioDiarioDTO>(RelatorioDiario);
    }

    public async Task<IEnumerable<RelatorioDiarioDTO>> GetAllAsync()
    {
        var RelatorioDiarios = await _repository.GetAllAsync();
        if (RelatorioDiarios == null || !RelatorioDiarios.Any())
        {
            throw new NotFoundException("Nenhum Relatorio Diario encontrado.");
        }
        return _mapper.Map<IEnumerable<RelatorioDiarioDTO>>(RelatorioDiarios);
    }

    public async Task<RelatorioDiarioDTO> CreateAsync(CreateRelatorioDiarioDTO dto)
    {
        try
        {
            var RelatorioDiario = _mapper.Map<RelatorioDiario>(dto);
            var id = await _repository.CreateAsync(RelatorioDiario);
            var createdRelatorioDiario = await _repository.GetByIdAsync(id);
            return _mapper.Map<RelatorioDiarioDTO>(createdRelatorioDiario!);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao criar o Relatorio Diario: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(UpdateRelatorioDiarioDTO dto)
    {
        var existingRelatorioDiario = await _repository.GetByIdAsync(dto.Id);
        if (existingRelatorioDiario == null)
        {
            throw new NotFoundException($"Relatorio Diario com ID {dto.Id} não encontrado.");
        }

        try
        {
            var RelatorioDiario = _mapper.Map<RelatorioDiario>(dto);
            return await _repository.UpdateAsync(RelatorioDiario);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao atualizar o Relatorio Diario: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var RelatorioDiario = await _repository.DeleteAsync(id);
        if (!RelatorioDiario)
        {
            throw new NotFoundException($"Relatorio Diario com ID {id} não encontrado.");
        }
        return true;
    }
}