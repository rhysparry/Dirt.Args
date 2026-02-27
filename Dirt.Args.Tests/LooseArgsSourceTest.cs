using System.Diagnostics.CodeAnalysis;

namespace Dirt.Tests;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class LooseArgsSourceTest
{
    [Fact]
    public void BasicLooseArgument()
    {
        var source = CreateLooseSource("--flag", "value");
        Assert.Equal(new[] { "--flag=value" }, source.GetArgs());
    }

    [Fact]
    public void MultipleLooseValues()
    {
        var source = CreateLooseSource("--flag", "value1", "value2");
        Assert.Equal(new[] { "--flag=value1", "--flag=value2" }, source.GetArgs());
    }

    [Fact]
    public void MultipleFlagsWithLooseValues()
    {
        var source = CreateLooseSource("--flag", "value1", "--other", "value2");
        Assert.Equal(new[] { "--flag=value1", "--other=value2" }, source.GetArgs());
    }

    [Fact]
    public void ExplicitValueDoesNotStartCollection()
    {
        var source = CreateLooseSource("--flag=explicit", "value");
        Assert.Equal(new[] { "--flag=explicit", "value" }, source.GetArgs());
    }

    [Fact]
    public void DoubleDashStopsCollection()
    {
        var source = CreateLooseSource("--flag", "--", "remaining");
        Assert.Equal(new[] { "--flag", "--", "remaining" }, source.GetArgs());
    }

    [Fact]
    public void ShortFlagsDoNotCollect()
    {
        var source = CreateLooseSource("-f", "value", "--flag", "value2");
        Assert.Equal(new[] { "-f", "value", "--flag=value2" }, source.GetArgs());
    }

    [Fact]
    public void NonFlagArgsBeforeAnyFlagPassThrough()
    {
        var source = CreateLooseSource("loose", "--flag", "value");
        Assert.Equal(new[] { "loose", "--flag=value" }, source.GetArgs());
    }

    [Fact]
    public void ExplicitThenLooseForSameFlag()
    {
        var source = CreateLooseSource("--flag=v1", "--flag", "v2");
        Assert.Equal(new[] { "--flag=v1", "--flag=v2" }, source.GetArgs());
    }

    [Fact]
    public void EmptyArgs()
    {
        var source = CreateLooseSource();
        Assert.Empty(source.GetArgs());
    }

    [Fact]
    public void OnlyFlags()
    {
        var source = CreateLooseSource("--flag1", "--flag2");
        Assert.Equal(new[] { "--flag1", "--flag2" }, source.GetArgs());
    }

    [Fact]
    public void OnlyValues()
    {
        var source = CreateLooseSource("value1", "value2");
        Assert.Equal(new[] { "value1", "value2" }, source.GetArgs());
    }

    [Fact]
    public void LooseValuesAfterDoubleDashAreNotTransformed()
    {
        var source = CreateLooseSource("--flag", "v1", "--", "--other", "v2");
        Assert.Equal(new[] { "--flag=v1", "--", "--other", "v2" }, source.GetArgs());
    }

    [Fact]
    public void ShortFlagGroupDoesNotCollect()
    {
        // ReSharper disable once StringLiteralTypo
        var source = CreateLooseSource("-abc", "value", "--flag", "value2");
        Assert.Equal(new[] { "-abc", "value", "--flag=value2" }, source.GetArgs());
    }

    [Fact]
    public void IntegrationWithArgs()
    {
        var source = CreateLooseSource("--flag", "value1", "value2", "--other=explicit", "loose");
        var args = new Args(source);

        Assert.True(args.HasFlag("flag"));
        Assert.Equal(new[] { "value1", "value2" }, args.GetMultiFlagValue("flag"));
        Assert.True(args.HasFlag("other"));
        Assert.Equal("explicit", args.GetFlagValue("other"));
        Assert.Equal(new[] { "loose" }, args.Remaining);
    }

    [Fact]
    public void IntegrationWithDoubleDash()
    {
        var source = CreateLooseSource("--flag", "value", "--", "--not-a-flag", "remaining");
        var args = new Args(source);

        Assert.True(args.HasFlag("flag"));
        Assert.Equal("value", args.GetFlagValue("flag"));
        Assert.False(args.HasFlag("not-a-flag"));
        Assert.Equal(new[] { "--not-a-flag", "remaining" }, args.Remaining);
    }

    private static LooseArgsSource CreateLooseSource(params string[] args) =>
        new(new TestArgsSource(args));

    private class TestArgsSource(string[] args) : IArgsSource
    {
        public IEnumerable<string> GetArgs() => args;
    }
}
