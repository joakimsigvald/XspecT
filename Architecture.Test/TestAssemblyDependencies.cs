using XspecT.Architecture.Assertion;
using Xunit;

namespace XspecT.Architecture.Test;

public class TestAssemblyDependencies : ArchSpec
{
    [Fact]
    public void TestAssemblyDependencyByName()
        => Assembly.Named("XspecT.Architecture.Test").Does().DependOn(Assembly.Named("XspecT.Architecture"));

    [Fact]
    public void TestAssemblyDependencyByType()
        => Assembly.Of<TestAssemblyDependencies>().Does().DependOn(Assembly.Of<ArchSpec>());

    [Fact]
    public void TestAssemblyNotDependOn()
        => Assembly.Of<ArchSpec>().Does().NotDependOn(Assembly.Of<TestAssemblyDependencies>());
}