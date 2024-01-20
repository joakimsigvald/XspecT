using XspecT.Architecture.Assertion;
using XspecT.Architecture.Exceptions;
using XspecT.Assert;
using Xunit;

namespace XspecT.Architecture.Test;

public class TestType : ArchSpec
{
    [Fact]
    public void TestAssemblyDependencyByName()
        => Classes.In(AssemblyNamed("XspecT.Architecture"))
        .That().ArePublic().And().AreNotStatic()
        .Are().NotSealed();

    [Fact]
    public void TestNegativeInterfaceImplementation()
        => Xunit.Assert.Throws<ArchitectureViolation>(() => 
        
        Classes.In(AssemblyNamed("XspecT.Architecture.Test"))
        .Does().NotImplement(Interfaces.In(AssemblyNamed("XspecT.Architecture"))))

        .Message.Does().Contain(nameof(InvalidImplementation)).And.Contain(nameof(IClassesContinuation));
}