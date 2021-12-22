using Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperadminContextAPI;

public class Content
{
    [NotMapped]
    public List<ContentRemoved> DomainEvents { get; } = new();

    [Key]
    public long Id { get; set; }
    public long ActualEntityId { get; set; }
    public string Type { get; set; }
    public string[] TextContent { get; set; }
    public bool Removed { get; set; }
    public string Note { get; set; }


    public Content(long actualEntityId, string type, string[] textContent)
    {
        ActualEntityId = actualEntityId;
        Type = type;
        TextContent = textContent;
        Removed = false;
        Note = string.Empty;
    }

    public void MarkRemoved()
    {
        Removed = true;
        DomainEvents.Add(new() { ContentId = ActualEntityId, Type = Type });
    }
}
