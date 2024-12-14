namespace Dirt;

/// <summary>
/// Represents a collection of command-line arguments.
/// </summary>
public interface IArgs
{
    /// <summary>
    /// Determines whether the specified flag is present.
    /// </summary>
    /// <param name="flag">The flag to check</param>
    /// <returns><see langword="true"/> if the specified flag is present; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Flags are case-sensitive.
    /// </remarks>
    bool HasFlag(string flag);

    /// <summary>
    /// Gets the number of times the flag was specified.
    /// </summary>
    /// <param name="flag">The flag to check</param>
    /// <returns>The number of times the flag was specified.</returns>
    int GetFlagCount(string flag);

    /// <summary>
    /// Gets the value of the specified flag.
    /// </summary>
    /// <param name="flag">The flag to check</param>
    /// <returns>The value of the specified flag, or <see langword="null"/> if the flag was not specified.</returns>
    string? GetFlagValue(string flag);

    /// <summary>
    /// Gets the values of the specified flag.
    /// </summary>
    /// <param name="flag">The flag to check</param>
    /// <returns>
    /// A list of values of the specified flag, or an empty list if either the flag was not specified or it
    /// has no specified values.
    /// </returns>
    IReadOnlyList<string> GetMultiFlagValue(string flag);

    /// <summary>
    /// Gets the remaining arguments that were not parsed as flags.
    /// </summary>
    IReadOnlyList<string> Remaining { get; }
}
