namespace Application.Interfaces{

    public interface IFileUserService{
        Task<string> SaveFileAsync(Stream file, string fileName, string profile, int? matricula, string? nome);
        void ValidateFile(Stream file, string fileName);
        Task DeleteFileAsync(string filePath); // Novo método para deletar arquivo
        string GetUserPhotoPath(int? matricula, string profile); // Novo método para obter o caminho atual
    }
}