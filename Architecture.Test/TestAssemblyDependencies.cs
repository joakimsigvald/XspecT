using XspecT.Architecture.Assertion;
using Xunit;

namespace XspecT.Architecture.Test;

public class TestAssemblyDependencies : ArchSpec
{
    [Fact]
    public void TestAssemblyDependencyByName()
        => AssemblyNamed("XspecT.Architecture.Test").Does().DependOn(AssemblyNamed("XspecT.Architecture"));

    [Fact]
    public void TestIndirectAssemblyDependencyByName()
        => AssemblyNamed("XspecT.Architecture.Test").Does().DependOn(AssemblyNamed("XspecT.Assert"));

    [Fact]
    public void TestUnreferencedAssemblyDependencyByName()
        => AssemblyNamed("XspecT.Assert").Does().DependOn(AssemblyNamed("FluentAssertions"));

    [Fact]
    public void TestAssemblyDependencyByType()
        => AssemblyOf<TestAssemblyDependencies>().Does().DependOn(AssemblyOf<ArchSpec>());

    [Fact]
    public void TestAssemblyNotDependOn()
        => AssemblyOf<ArchSpec>().Does().NotDependOn(AssemblyOf<TestAssemblyDependencies>());
}