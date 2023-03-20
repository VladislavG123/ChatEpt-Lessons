namespace ChatEpt.Models;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid(); // uuid
    public DateTime CreationDate { get; set; } = DateTime.Now;
}