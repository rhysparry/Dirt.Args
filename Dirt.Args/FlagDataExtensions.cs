namespace Dirt;

internal static class FlagDataExtensions
{
    public static void AddFlagValue(
        this IDictionary<string, FlagData> flags,
        string flag,
        string value
    )
    {
        if (flags.TryGetValue(flag, out var flagData))
        {
            flags[flag] = flagData.AddValue(value);
        }
        else
        {
            flags[flag] = new FlagData(flag, value);
        }
    }

    public static void IncrementFlag(this IDictionary<string, FlagData> flags, string flag)
    {
        if (flags.TryGetValue(flag, out var flagData))
        {
            flags[flag] = flagData.Increment();
        }
        else
        {
            flags[flag] = new FlagData(flag);
        }
    }

    public static IReadOnlyDictionary<string, IFlagData> AsReadOnly(
        this IDictionary<string, FlagData> flags
    ) => flags.ToDictionary(x => x.Key, x => (IFlagData)x.Value);
}
