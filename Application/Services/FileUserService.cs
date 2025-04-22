using Application.Interfaces;

namespace Application.Services
{
    public class FileUserService : IFileUserService
    {
        public async Task DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                
                // Opcional: Remover diretório se estiver vazio
                var directory = Path.GetDirectoryName(filePath);
                if (Directory.Exists(directory) && !Directory.EnumerateFileSystemEntries(directory).Any())
                {
                    Directory.Delete(directory);
                }
            }
            await Task.CompletedTask;
        }

        public string GetUserPhotoPath(int matricula)
        {
            if (matricula <= 0)
                throw new ArgumentException("Matrícula inválida");

            string userFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Usuarios", $"{matricula}");
            
            if (!Directory.Exists(userFolder))
                return null ?? "";

            // Busca o primeiro arquivo no diretório do usuário (considerando que só deve haver um)
            var files = Directory.GetFiles(userFolder);
            return files.FirstOrDefault() ?? "";
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName, int matricula)
        {
            if (matricula <= 0)
                throw new ArgumentException("Matrícula inválida");

            // Caminho onde o arquivo será salvo 
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Usuarios", $"{matricula}");

            // Verificar se o caminho (pasta) existe, se não existir será criado
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            else
            {
                // Limpa arquivos existentes (garante apenas um arquivo por usuário)
                var existingFiles = Directory.GetFiles(folder);
                foreach (var existingFile in existingFiles)
                {
                    File.Delete(existingFile);
                }
            }

            // Extrai apenas o nome do arquivo sem o caminho (segurança)
            var safeFileName = Path.GetFileName(fileName);
            
            // Gera nome único mantendo a extensão original
            var fileExtension = Path.GetExtension(safeFileName);
            var uniqueFileName = $"{matricula}_profile{fileExtension}";

            // Cria caminho absoluto do arquivo
            string filePath = Path.Combine(folder, uniqueFileName);

            // Salvar arquivo
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retornar o caminho do arquivo
            return filePath;
        }

        public void ValidateFile(Stream file, string fileName)
        {
            // Verificar se o arquivo foi recebido
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Nenhuma imagem foi enviada.");
            }

            // Validar o tamanho máximo do arquivo (2MB)
            if (file.Length > 2 * 1024 * 1024)
            {
                throw new ArgumentException("A imagem deve ter no máximo 2MB");
            }

            // Lista de extensões permitidas
            var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png" };

            // Capturar extensão do arquivo
            var extensao = Path.GetExtension(fileName)?.ToLower();
            
            // Validar extensão permitida
            if (string.IsNullOrEmpty(extensao) || !extensoesPermitidas.Contains(extensao))
            {
                throw new ArgumentException("A imagem deve ser no formato JPG, JPEG, PNG");
            }
        }
    }
}