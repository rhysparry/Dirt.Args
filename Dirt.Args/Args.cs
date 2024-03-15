namespace Dirt;

public class Args : IArgs, IArgsData
{
    private readonly IArgsData _argsData;

    public Args() : this(new DefaultArgsSource())
    {
    }

    public Args(IArgsSource args) : this(new ArgsData(args))
    {
    }

    public Args(IEnumerable<string> args) : this(new ArgsData(args))
    {
    }

    public Args(IArgsData data)
    {
        _argsData = data;
    }

    IReadOnlyDictionary<string, IFlagData> IArgsData.Flags => _argsData.Flags;
    IReadOnlyDictionary<string, string> IArgsData.ValueFlags => _argsData.ValueFlags;
    IReadOnlyDictionary<string, IReadOnlyList<string>> IArgsData.MultiValueFlags => _argsData.MultiValueFlags;
    public IReadOnlyList<string> Remaining => _argsData.Remaining;

    public bool HasFlag(string flag) => _argsData.Flags.ContainsKey(flag);
    public int GetFlagCount(string flag) => _argsData.Flags.GetValueOrDefault(flag)?.Count ?? 0;

    public string? GetFlagValue(string flag)
    {
        var values = GetMultiFlagValue(flag);
        return values.Count > 0 ? values[0] : null;
    }

    public IReadOnlyList<string> GetMultiFlagValue(string flag) =>
        _argsData.Flags.GetValueOrDefault(flag)?.Values ?? new List<string>();
}