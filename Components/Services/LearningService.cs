namespace LernCockpit.Components.Services;

using LernCockpit.Components.Models;
using LernCockpit.Data;
using Microsoft.EntityFrameworkCore;

public class LearningService : ILearningService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public LearningService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Folder>> GetUserFoldersAsync(int userId)
    {
        using var db = _dbFactory.CreateDbContext();
        return await db.Folders
            .Where(f => f.UserId == userId && f.IsHauptthema)
            .Include(f => f.Unterthema)
                .ThenInclude(u => u.Files)
            .Include(f => f.Files)
            .ToListAsync();
    }

    public async Task<Folder?> GetFolderByIdAsync(int folderId, int userId)
    {
        using var db = _dbFactory.CreateDbContext();
        return await db.Folders
            .Where(f => f.Id == folderId && f.UserId == userId)
            .Include(f => f.Files)
            .FirstOrDefaultAsync();
    }

    public async Task<Folder> CreateFolderAsync(Folder folder)
    {
        using var db = _dbFactory.CreateDbContext();
        db.Folders.Add(folder);
        await db.SaveChangesAsync();
        return folder;
    }

    public async Task UpdateFolderAsync(Folder folder)
    {
        using var db = _dbFactory.CreateDbContext();
        var existing = await db.Folders.FirstOrDefaultAsync(f => f.Id == folder.Id);
        if (existing != null)
        {
            existing.Name = folder.Name;
            existing.Status = folder.Status;
            existing.IsAufgeklappt = folder.IsAufgeklappt;
            
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteFolderAsync(int folderId, int userId)
    {
        using var db = _dbFactory.CreateDbContext();
        var folder = await db.Folders
            .Where(f => f.Id == folderId && f.UserId == userId)
            .Include(f => f.Unterthema)
            .Include(f => f.Files)
            .FirstOrDefaultAsync();

        if (folder != null)
        {
            db.Folders.Remove(folder);
            await db.SaveChangesAsync();
        }
    }

    public async Task<LearningFile> CreateFileAsync(LearningFile file)
    {
        using var db = _dbFactory.CreateDbContext();
        db.LearningFiles.Add(file);
        await db.SaveChangesAsync();
        return file;
    }

    public async Task UpdateFileAsync(LearningFile file)
    {
        using var db = _dbFactory.CreateDbContext();
        var existing = await db.LearningFiles
            .FirstOrDefaultAsync(f => f.Id == file.Id && f.UserId == file.UserId);

        if (existing != null)
        {
            existing.Name = file.Name;
            existing.Inhalt = file.Inhalt;
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteFileAsync(int fileId, int userId)
    {
        using var db = _dbFactory.CreateDbContext();
        var file = await db.LearningFiles
            .FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == userId);

        if (file != null)
        {
            db.LearningFiles.Remove(file);
            await db.SaveChangesAsync();
        }
    }
    
    // === AUSBILDER METHODEN ===

    public async Task<List<User>> GetAllUsersAsync()
    {
        using var db = _dbFactory.CreateDbContext();
    
        // Alle User außer Ausbilder
        return await db.Users
            .Where(u => !u.IsAusbilder)
            .ToListAsync();
    }

    public async Task<List<Folder>> GetFoldersByUserIdAsync(int userId)
    {
        using var db = _dbFactory.CreateDbContext();
    
        return await db.Folders
            .Where(f => f.UserId == userId && f.IsHauptthema)
            .Include(f => f.Unterthema)
            .ThenInclude(u => u.Files)
            .Include(f => f.Files)
            .ToListAsync();
    }
}