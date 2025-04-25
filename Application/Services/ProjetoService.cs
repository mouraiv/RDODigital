using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _repository;
    private readonly IMapper _mapper;

    public ProjetoService(IProjetoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProjetoDTO?> GetByIdAsync(int id)
    {
        var Projeto = await _repository.GetByIdAsync(id);
        if (Projeto == null)
        {
            throw new NotFoundException($"Projeto com ID {id} não encontrado.");
        }
        return _mapper.Map<ProjetoDTO>(Projeto);
    }

    public async Task<IEnumerable<ProjetoDTO>> GetAllAsync()
    {
        var Projetos = await _repository.GetAllAsync();
        if (Projetos == null || !Projetos.Any())
        {
            throw new NotFoundException("Nenhum Projeto encontrado.");
        }
        return _mapper.Map<IEnumerable<ProjetoDTO>>(Projetos);
    }

    public async Task<ProjetoDTO> CreateAsync(CreateProjetoDTO dto)
    {
        try
        {
            var Projeto = _mapper.Map<Projeto>(dto);
            var id = await _repository.CreateAsync(Projeto);
            var createdProjeto = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProjetoDTO>(createdProjeto!);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao criar o Projeto: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(UpdateProjetoDTO dto)
    {
        var existingProjeto = await _repository.GetByIdAsync(dto.Id);
        if (existingProjeto == null)
        {
            throw new NotFoundException($"Projeto com ID {dto.Id} não encontrado.");
        }

        try
        {
            var Projeto = _mapper.Map<Projeto>(dto);
            return await _repository.UpdateAsync(Projeto);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao atualizar o Projeto: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var Projeto = await _repository.DeleteAsync(id);
        if (!Projeto)
        {
            throw new NotFoundException($"Projeto com ID {id} não encontrado.");
        }
        return true;
    }
}