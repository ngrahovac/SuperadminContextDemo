using Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementContextAPI;

public class GroupEvent
{
    [NotMapped]
    public List<GroupEventCreated> DomainEvents { get; } = new();

    public long Id { get; set; }
    public string Name { get; set; }
    public bool RemovedBySuperadmin { get; set; }    // will be configured as a shadow prop


    public GroupEvent(string name)
    {
        Name = name;
        DomainEvents.Add(new() { Id = Id, Name = Name });
    }
}
