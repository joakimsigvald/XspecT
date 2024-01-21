using XspecT.Architecture.Assertion;
using XspecT.Architecture.Exceptions;
using XspecT.Assert;
using Xunit;

namespace XspecT.Architecture.Test;

public class TestType : ArchSpec
{
    public TestType() : base("XspecT") { }

    [Fact]
    public void TestAssemblyClassesNotSealed()
        => Assembly("Architecture").Classes
        .That().ArePublic().And().AreNotStatic()
        .Are().NotSealed();

    [Fact]
    public void TestAssemblyClassesSealed()
        => Xunit.Assert.Throws<ArchitectureViolation>(
            () => Assembly("Architecture").Classes
            .That().ArePublic().And().AreNotStatic()
            .Are().Sealed())
        .Message.Does().Contain(nameof(ArchSpec));

    [Fact]
    public void TestNegativeInterfaceImplementation()
        => Xunit.Assert.Throws<ArchitectureViolation>(
            () => Assembly("Architecture.Test").Classes
            .Does().NotImplement(Assembly("Architecture").Interfaces))
        .Message.Does().Contain(nameof(InvalidImplementation)).And.Contain(nameof(IAssemblyReference));
}