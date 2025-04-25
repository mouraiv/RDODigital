using Application.DTOs;
using Domain.Entities;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Application.Exceptions;


namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _ClienteRepository;
        private readonly IFileUserService _fileUserService;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository ClienteRepository, IFileUserService fileUserService,IMapper mapper)
        {
            _ClienteRepository = ClienteRepository;
            _fileUserService = fileUserService;
            _mapper = mapper;

        }

        public async Task<ClienteDTO> GetByIdAsync(int id)
        {
            var Cliente = await _ClienteRepository.GetByIdAsync(id);
            if (Cliente == null)
            {
                throw new NotFoundException($"Cliente com ID {id} não encontrado.");
            }
            return _mapper.Map<ClienteDTO>(Cliente);
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            var Clientes = await _ClienteRepository.GetAllAsync();
            if (Clientes == null || !Clientes.Any())
            {
                throw new NotFoundException("Nenhum Cliente encontrado.");
            }
            return _mapper.Map<IEnumerable<ClienteDTO>>(Clientes);
        }

        public async Task<ClienteDTO> AddAsync(CreateClienteDTO ClienteDto, Stream fileStream, string fileName)
        {
            
            if (fileStream == null || fileStream.Length == 0)
            {
                throw new NotFoundException("Nenhuma imagem foi enviada.");
            }

            var Cliente = _mapper.Map<Cliente>(ClienteDto);

            if (Cliente.Nome_cliente == null)
            {
                throw new NotFoundException("Nome do Cliente não foi informado.");  
            }

            var ClienteExistente = await _ClienteRepository.GetByIdClienteAsync(Cliente.Nome_cliente);
            if (ClienteExistente != null)
                throw new ConflictException("O Cliente já existe, Insira outro cliente.");

            Cliente.Data_cadastro = DateTime.UtcNow;
            Cliente.Ativo = true;
            Cliente.Foto_perfil = await ProcessarImagemAsync(fileStream, fileName, Cliente.Nome_cliente);

            try
            {
                await _ClienteRepository.AddAsync(Cliente);

                string novoCaminho = await _fileUserService.SaveFileAsync(
                    fileStream,
                    fileName,
                    "Clientes",
                    0,
                    Cliente.Nome_cliente);

                return _mapper.Map<ClienteDTO>(Cliente);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao adicionar Cliente: {ex.Message}", ex);
            }   
        }

        public async Task UpdatePhotoPathAsync(int ClienteId, string pathFile)
        {
            var Cliente = await _ClienteRepository.GetByIdAsync(ClienteId);

            if (Cliente == null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }

            try
            {
                Cliente.Foto_perfil = pathFile;
                await _ClienteRepository.UpdateAsync(ClienteId, Cliente);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao atualizar caminho da imagem: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(int id, UpdateClienteDTO ClienteDto, Stream fileStream, string fileName)
        {
            var Cliente = await _ClienteRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Cliente não encontrado");

            if (ClienteDto.NomeCliente == null)
            {
                throw new NotFoundException("Nome do Cliente não foi informado.");  
            }

            var ClienteExistente = await _ClienteRepository.GetByIdClienteAsync(ClienteDto.NomeCliente);
            if (ClienteExistente != null && ClienteExistente.Id_cliente != Cliente.Id_cliente)
                throw new ConflictException("O Cliente já existe, insira outro nome de cliente.");


            try
            {
                _mapper.Map(ClienteDto, Cliente);

                if (fileStream != null && fileStream.Length > 0)
                {
                    _fileUserService.ValidateFile(fileStream, fileName);
                    string novoCaminho = await _fileUserService.SaveFileAsync(fileStream, fileName, "Clientes", 0, Cliente.Nome_cliente);
                    Cliente.Foto_perfil = novoCaminho;
                }

                await _ClienteRepository.UpdateAsync(id, Cliente);
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao atualizar Cliente: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            
            var Cliente = await _ClienteRepository.GetByIdAsync(id);
            if (Cliente == null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }

            try
            {
                await _ClienteRepository.DeleteAsync(id);

                // Remove a imagem associada se existir
                if (!string.IsNullOrEmpty(Cliente.Foto_perfil))
                {
                    await _fileUserService.DeleteFileAsync(Cliente.Foto_perfil);
                }
            }
            catch (Exception ex)
            {
                throw new AppException($"Erro ao deletar Cliente: {ex.Message}", ex);
            }

        }
        private async Task<string> ProcessarImagemAsync(Stream fileStream, string fileName, string nome)
        {
            _fileUserService.ValidateFile(fileStream, fileName);
            return await _fileUserService.SaveFileAsync(fileStream, fileName, "Clientes", 0, nome);
        }
    }
}