namespace LernCockpit.Components.Services;

using LernCockpit.Components.Models;

public interface ILearningService
{
    Task<List<Folder>> GetUserFoldersAsync(int userId);
    Task<Folder?> GetFolderByIdAsync(int folderId, int userId);
    Task<Folder> CreateFolderAsync(Folder folder);
    Task UpdateFolderAsync(Folder folder);
    Task DeleteFolderAsync(int folderId, int userId);
    Task<LearningFile> CreateFileAsync(LearningFile file);
    Task UpdateFileAsync(LearningFile file);
    Task DeleteFileAsync(int fileId, int userId);
}