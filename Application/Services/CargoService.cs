using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class CargoService : ICargoService
{
    private readonly ICargoRepository _repository;
    private readonly IMapper _mapper;

    public CargoService(ICargoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CargoDTO?> GetByIdAsync(int id)
    {
        try
        {
            var cargo = await _repository.GetByIdAsync(id);
            if (cargo == null)
            {
                throw new NotFoundException($"Cargo com ID {id} não encontrado.");
            }
            return _mapper.Map<CargoDTO>(cargo);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao obter o cargo: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<CargoDTO>> GetAllAsync()
    {
        try
        {
            var cargos = await _repository.GetAllAsync();
            if (cargos == null || !cargos.Any())
            {
                throw new NotFoundException("Nenhum cargo encontrado.");
            }
            return _mapper.Map<IEnumerable<CargoDTO>>(cargos);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao obter os cargos: {ex.Message}", ex);
        }
    }

    public async Task<CargoDTO> CreateAsync(CreateCargoDTO dto)
    {
        try
        {
            var existingCargo = await _repository.GetByNameAsync(dto.Nome);
            if (existingCargo != null)
            {
                throw new NotFoundException($"Cargo com nome {dto.Nome} já existe.");
            }

            var cargo = _mapper.Map<Cargo>(dto);
            var id = await _repository.CreateAsync(cargo);
            var createdCargo = await _repository.GetByIdAsync(id);
            return _mapper.Map<CargoDTO>(createdCargo!);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao criar o cargo: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateAsync(UpdateCargoDTO dto)
    {
        try
        {
            var existingCargo = await _repository.GetByIdAsync(dto.Id);
            if (existingCargo == null)
            {
                throw new NotFoundException($"Cargo com ID {dto.Id} não encontrado.");
            }

            var cargo = _mapper.Map<Cargo>(dto);
            return await _repository.UpdateAsync(cargo);
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao atualizar o cargo: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var cargo = await _repository.DeleteAsync(id);
            if (!cargo)
            {
                throw new NotFoundException($"Cargo com ID {id} não encontrado.");
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new AppException($"Erro ao deletar o cargo: {ex.Message}", ex);
        }
    }
}