using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models;

public class Platform
{
    [Key]
    public int Id { get; set; }
    public required int OriginalId { get; set; }
    public required string Name { get; set; }
    public ICollection<Command> Commands { get; set; } = [];
}