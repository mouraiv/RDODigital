using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
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
        var cargo = await _repository.GetByIdAsync(id);
        return _mapper.Map<CargoDTO>(cargo);
    }

    public async Task<IEnumerable<CargoDTO>> GetAllAsync()
    {
        var cargos = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CargoDTO>>(cargos);
    }

    public async Task<CargoDTO> CreateAsync(CreateCargoDTO dto)
    {
        var existingCargo = await _repository.GetByNameAsync(dto.Nome);
        if (existingCargo != null)
            throw new DomainException("Já existe um cargo com este nome");

        var cargo = _mapper.Map<Cargo>(dto);
        var id = await _repository.CreateAsync(cargo);
        var createdCargo = await _repository.GetByIdAsync(id);
        return _mapper.Map<CargoDTO>(createdCargo!);
    }

    public async Task<bool> UpdateAsync(UpdateCargoDTO dto)
    {
        var existingCargo = await _repository.GetByIdAsync(dto.Id);
        if (existingCargo == null)
            return false;

        var cargo = _mapper.Map<Cargo>(dto);
        return await _repository.UpdateAsync(cargo);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}