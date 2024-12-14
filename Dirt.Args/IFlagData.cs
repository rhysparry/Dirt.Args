namespace Dirt;

/// <summary>
/// Data for a flag.
/// </summary>
public interface IFlagData
{
    /// <summary>
    /// The name of the flag.
    /// </summary>
    public string Flag { get; }
    /// <summary>
    /// The number of times the flag was specified.
    /// </summary>
    public int Count { get; }
    /// <summary>
    /// A list of values associated with the flag. If no values are associated with
    /// the flag, this list will be empty.
    /// </summary>
    public IReadOnlyList<string> Values { get; }
}
