namespace Dirt;

/// <summary>
/// An <see cref="IArgsSource"/> that supports loose arguments.
/// </summary>
/// <remarks>
/// <para>
/// Loose arguments allow values to be associated with a flag without using the <c>=</c> separator.
/// For example, <c>--flag value</c> is equivalent to <c>--flag=value</c>.
/// </para>
/// <para>
/// Values are collected until the next flag or the <c>--</c> separator is encountered.
/// Multiple values are associated with the same flag, so <c>--flag value1 value2</c> is
/// equivalent to <c>--flag=value1 --flag=value2</c>.
/// </para>
/// <para>
/// Short flags (e.g. <c>-f</c>) do not participate in loose argument collection and remain
/// boolean-only. Explicit values (e.g. <c>--flag=value</c>) do not start a collection context.
/// </para>
/// </remarks>
/// <param name="source">The underlying source of arguments.</param>
public class LooseArgsSource(IArgsSource source) : IArgsSource
{
    /// <summary>
    /// Creates a new instance of <see cref="LooseArgsSource"/> using the default argument source.
    /// </summary>
    public LooseArgsSource()
        : this(new DefaultArgsSource()) { }

    /// <inheritdoc />
    public IEnumerable<string> GetArgs()
    {
        string? currentFlag = null;
        string? pendingFlag = null;
        var passThrough = false;

        foreach (var arg in source.GetArgs())
        {
            if (passThrough)
            {
                yield return arg;
                continue;
            }

            if (arg == "--")
            {
                if (pendingFlag is not null)
                {
                    yield return pendingFlag;
                    pendingFlag = null;
                }
                currentFlag = null;
                passThrough = true;
                yield return arg;
                continue;
            }

            if (arg.StartsWith("--"))
            {
                if (pendingFlag is not null)
                {
                    yield return pendingFlag;
                    pendingFlag = null;
                }

                if (arg.Contains('='))
                {
                    currentFlag = null;
                    yield return arg;
                }
                else
                {
#if NETSTANDARD2_0
                    currentFlag = arg.Substring(2);
#else
                    currentFlag = arg[2..];
#endif
                    pendingFlag = arg;
                }
            }
            else if (arg.StartsWith("-"))
            {
                if (pendingFlag is not null)
                {
                    yield return pendingFlag;
                    pendingFlag = null;
                }
                currentFlag = null;
                yield return arg;
            }
            else if (currentFlag is not null)
            {
                pendingFlag = null;
                yield return $"--{currentFlag}={arg}";
            }
            else
            {
                yield return arg;
            }
        }

        if (pendingFlag is not null)
        {
            yield return pendingFlag;
        }
    }
}
