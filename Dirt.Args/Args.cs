namespace Dirt;

/// <summary>
/// Represents a collection of command-line arguments.
/// </summary>
/// <param name="data">The underlying argument data.</param>
public class Args(IArgsData data) : IArgs, IArgsData
{
    /// <summary>
    /// Creates a new instance of <see cref="Args"/> using the current running
    /// program's command line arguments.
    /// </summary>
    public Args() : this(new DefaultArgsSource())
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="Args"/> using the specified arguments.
    /// </summary>
    /// <param name="args">The source of the arguments.</param>
    public Args(IArgsSource args) : this(new ArgsData(args))
    {
    }

    /// <summary>
    /// Create a new instance of <see cref="Args"/> using the specified arguments.
    /// </summary>
    /// <param name="args">The string arguments to use.</param>
    public Args(IEnumerable<string> args) : this(new ArgsData(args))
    {
    }

    IReadOnlyDictionary<string, IFlagData> IArgsData.Flags => data.Flags;
    IReadOnlyDictionary<string, string> IArgsData.ValueFlags => data.ValueFlags;
    IReadOnlyDictionary<string, IReadOnlyList<string>> IArgsData.MultiValueFlags => data.MultiValueFlags;
    /// <summary>
    /// Gets the remaining arguments that were not parsed as flags.
    /// </summary>
    public IReadOnlyList<string> Remaining => data.Remaining;

    /// <inheritdoc />
    public bool HasFlag(string flag) => data.Flags.ContainsKey(flag);

    /// <inheritdoc />
    public int GetFlagCount(string flag) => data.Flags.GetValueOrDefault(flag)?.Count ?? 0;

    /// <inheritdoc />
    public string? GetFlagValue(string flag)
    {
        var values = GetMultiFlagValue(flag);
        return values.Count > 0 ? values[0] : null;
    }

    /// <inheritdoc />
    public IReadOnlyList<string> GetMultiFlagValue(string flag) =>
        data.Flags.GetValueOrDefault(flag)?.Values ?? new List<string>();
}