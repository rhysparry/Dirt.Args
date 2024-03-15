namespace Dirt;

public interface IArgsSource
{
    IEnumerable<string> GetArgs();
}