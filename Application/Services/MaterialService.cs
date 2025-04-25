using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _repository;
    private readonly IMapper _mapper;

    public MaterialService(IMaterialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MaterialDTO?> GetByIdAsync(int id)
    {
        var Material = await _repository.GetByIdAsync(id);
        if (Material == null)
        {
            throw new NotFoundException($"Material com ID {id} não encontrado.");
        }
        return _mapper.Map<MaterialDTO>(Material);
    }

    public async Task<IEnumerable<MaterialDTO>> GetAllAsync()
    {
        var Materials = await _repository.GetAllAsync();
        if (Materials == null || !Materials.Any())
        {
            throw new NotFoundException("Nenhum Material encontrado.");
        }
        return _mapper.Map<IEnumerable<MaterialDTO>>(Materials);
    }

    public async Task<MaterialDTO> CreateAsync(CreateMaterialDTO dto)
    {
        try
        {
            var Material = _mapper.Map<Material>(dto);
            var id = await _repository.CreateAsync(Material);
            var createdMaterial = await _repository.GetByIdAsync(id);
            return _mapper.Map<MaterialDTO>(createdMaterial!);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao criar o Material: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(UpdateMaterialDTO dto)
    {
        var existingMaterial = await _repository.GetByIdAsync(dto.Id);
        if (existingMaterial == null)
        {
            throw new NotFoundException($"Material com ID {dto.Id} não encontrado.");
        }

        try
        {
            var Material = _mapper.Map<Material>(dto);
            return await _repository.UpdateAsync(Material);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao atualizar o Material: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var Material = await _repository.DeleteAsync(id);
        if (!Material)
        {
            throw new NotFoundException($"Material com ID {id} não encontrado.");
        }
        return true;
    }
}