namespace Commercify.Core.Models;

public interface IEntity
{

}
public abstract class BaseEntity : IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

}
