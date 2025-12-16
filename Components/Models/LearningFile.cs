namespace LernCockpit.Components.Models;

public class LearningFile
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsNotiz { get; set; } = true;
    public string? Inhalt { get; set; }
    public byte []? FileData { get; set; }
    public string? OriginalFileName { get; set; }
    public long? DateiGroesse { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int FolderId { get; set; }
    public Folder Folder { get; set; } = null!;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    
    private string GetGroesseText(long? bytes)
    {
        if (bytes == null) return "";
        if (bytes < 1024) return $"{bytes} B";
        if (bytes < 1024 * 1024) return $"{bytes / 1024} KB";
        return $"{bytes / (1024 * 1024)} MB";
    }
}

