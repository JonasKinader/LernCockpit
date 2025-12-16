namespace LernCockpit.Components.Models;

public class Folder
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsHauptthema { get; set; } = false;
    public bool IsAufgeklappt { get; set; } = false;
    public LearningStatus Status { get; set; } = LearningStatus.NichtBegonnen;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int? HauptthemaId { get; set; }
    public Folder? Hauptthema { get; set; }
    public ICollection<Folder> Unterthema { get; set; } = new List<Folder>();
    public ICollection<LearningFile> Files { get; set; } = new List<LearningFile>();
}