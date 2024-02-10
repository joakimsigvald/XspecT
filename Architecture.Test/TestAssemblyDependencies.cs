using Xunit;

namespace XspecT.Architecture.Test;

public class TestAssemblyDependencies : ArchSpec
{
    public TestAssemblyDependencies() : base("XspecT") { }

    [Fact]
    public void TestAssemblyDependency()
        => Project("Architecture.Test").DependOn("Architecture");

    [Fact]
    public void TestIndirectAssemblyDependency()
        => Project("Architecture.Test").DependOn("Assert");

    [Fact]
    public void TestUse()
        => Project("Assert").Use("FluentAssertions");

    [Fact]
    public void TestNotDependOn()
        => Project("Architecture").DoNotDependOn("Architecture.Test");

    [Fact]
    public void TestDoNotUse()
        => Project("Architecture").DoNotUse("FluentAssertions");
}