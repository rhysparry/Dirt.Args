namespace Dirt;

/// <summary>
/// Represents a view of the underlying argument data.
/// </summary>
public interface IArgsData
{
    /// <summary>
    /// A dictionary of flags and their associated data.
    /// </summary>
    IReadOnlyDictionary<string, IFlagData> Flags { get; }
    /// <summary>
    /// A dictionary of flags and their associated values.
    /// </summary>
    IReadOnlyDictionary<string, string> ValueFlags { get; }
    /// <summary>
    /// A dictionary of flags and their associated values.
    /// </summary>
    IReadOnlyDictionary<string, IReadOnlyList<string>> MultiValueFlags { get; }
    /// <summary>
    /// A list of remaining arguments that were not parsed as flags.
    /// </summary>
    IReadOnlyList<string> Remaining { get; }
}
