namespace Dirt;

public class Args(IArgsData data) : IArgs, IArgsData
{
    public Args() : this(new DefaultArgsSource())
    {
    }

    public Args(IArgsSource args) : this(new ArgsData(args))
    {
    }

    public Args(IEnumerable<string> args) : this(new ArgsData(args))
    {
    }

    IReadOnlyDictionary<string, IFlagData> IArgsData.Flags => data.Flags;
    IReadOnlyDictionary<string, string> IArgsData.ValueFlags => data.ValueFlags;
    IReadOnlyDictionary<string, IReadOnlyList<string>> IArgsData.MultiValueFlags => data.MultiValueFlags;
    public IReadOnlyList<string> Remaining => data.Remaining;

    public bool HasFlag(string flag) => data.Flags.ContainsKey(flag);
    public int GetFlagCount(string flag) => data.Flags.GetValueOrDefault(flag)?.Count ?? 0;

    public string? GetFlagValue(string flag)
    {
        var values = GetMultiFlagValue(flag);
        return values.Count > 0 ? values[0] : null;
    }

    public IReadOnlyList<string> GetMultiFlagValue(string flag) =>
        data.Flags.GetValueOrDefault(flag)?.Values ?? new List<string>();
}