namespace Dirt;

/// <summary>
/// Describes a source of arguments.
/// </summary>
public interface IArgsSource
{
    /// <summary>
    /// Gets the raw arguments from the source.
    /// </summary>
    IEnumerable<string> GetArgs();
}