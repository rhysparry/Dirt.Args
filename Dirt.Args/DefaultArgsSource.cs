namespace Dirt;

internal class DefaultArgsSource : IArgsSource
{
    private readonly List<string> _args = Environment.GetCommandLineArgs().Skip(1).ToList();

    public IEnumerable<string> GetArgs() => _args;
}