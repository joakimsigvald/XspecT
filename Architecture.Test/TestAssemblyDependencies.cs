using Xunit;

namespace XspecT.Architecture.Test;

public class TestAssemblyDependencies : ArchSpec
{
    public TestAssemblyDependencies() : base("XspecT") { }

    [Fact]
    public void TestAssemblyDependency()
        => Assembly("Architecture.Test").DependOn("Architecture");

    [Fact]
    public void TestIndirectAssemblyDependency()
        => Assembly("Architecture.Test").DependOn("Assert");

    [Fact]
    public void TestUnreferencedAssemblyDependency()
        => Assembly("Assert").DependOn("FluentAssertions");

    [Fact]
    public void TestNotDependOn()
        => Assembly("Architecture").DoNotDependOn("Architecture.Test");
}