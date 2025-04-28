using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class AtividadeService : IAtividadeService
{
    private readonly IAtividadeRepository _repository;
    private readonly IMapper _mapper;

    public AtividadeService(IAtividadeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AtividadeDTO?> GetByIdAsync(int id)
    {
        var Atividade = await _repository.GetByIdAsync(id);
        if (Atividade == null)
        {
            throw new NotFoundException($"Atividade com ID {id} não encontrado.");
        }
        return _mapper.Map<AtividadeDTO>(Atividade);
    }

    public async Task<IEnumerable<AtividadeDTO>> GetAllAsync()
    {
        var Atividades = await _repository.GetAllAsync();
        if (Atividades == null || !Atividades.Any())
        {
            throw new NotFoundException("Nenhum Atividade encontrado.");
        }
        return _mapper.Map<IEnumerable<AtividadeDTO>>(Atividades);
    }

    public async Task<AtividadeDTO> CreateAsync(CreateAtividadeDTO dto)
    {
        var existingAtividade = await _repository.GetByNameAsync(dto.NomeAtividade);
        if (existingAtividade != null)
        {
            throw new ConflictException($"Atividade com nome {dto.NomeAtividade} já existe.");
        }

        try
        {
            var Atividade = _mapper.Map<Atividade>(dto);
            var id = await _repository.CreateAsync(Atividade);
            var createdAtividade = await _repository.GetByIdAsync(id);
            return _mapper.Map<AtividadeDTO>(createdAtividade!);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao criar o Atividade: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(UpdateAtividadeDTO dto)
    {
        var existingAtividade = await _repository.GetByIdAsync(dto.Id);
        if (existingAtividade == null)
        {
            throw new NotFoundException($"Atividade com ID {dto.Id} não encontrado.");
        }

        var AtividadeExistente = await _repository.GetByNameAsync(dto.NomeAtividade);
            if (AtividadeExistente != null && AtividadeExistente.Id != existingAtividade.Id)
                throw new ConflictException("A atividade já existe, insira outro nome de atividade.");

        try
        {
            var Atividade = _mapper.Map<Atividade>(dto);
            return await _repository.UpdateAsync(Atividade);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao atualizar o Atividade: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var Atividade = await _repository.DeleteAsync(id);
        if (!Atividade)
        {
            throw new NotFoundException($"Atividade com ID {id} não encontrado.");
        }
        return true;
    }
}