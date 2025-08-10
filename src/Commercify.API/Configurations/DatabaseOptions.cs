using System.ComponentModel.DataAnnotations;

namespace Commercify.API.Configurations;

public class DatabaseOptions
{
    [Required]
    public string ConnectionString { get; set; } = string.Empty;
    [Range(0, 10)]
    public int MaxRetryCount { get; set; }
    [Range(0, 1000)]
    public int CommandTimeout { get; set; }
}
