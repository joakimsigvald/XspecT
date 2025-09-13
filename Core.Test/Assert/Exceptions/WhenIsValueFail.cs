using XspecT.Assert;
using XspecT.Test.TestData;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Exceptions;

public class WhenIsValueFail : Spec<MyModel>
{
    [Fact]
    public void GivenNumberedAssignment_ThenShowAssignments()
    {
        var ex = Xunit.Assert.Throws<XunitException>(()
            => When(_ => new MyModel { Id = An<int>() }).Then().Result.Is().Null());
        ex.Message.Is(
            """
            When new MyModel { Id = an int }
            Then Result is null
            """);
        var theInt = The<int>();
        ex.HasInnerMessage($"Expected Result to be null but found MyModel {{ Id = {theInt}, Name = , Values =  }}");
        ex.HasAssignments($"int:1 = {theInt}");
    }

    [Fact]
    public void GivenTaggedAssignment_ThenShowAssignments()
    {
        Tag<int> id = new();
        //When(_ => new MyModel { Id = The(id) }).Then().Result.Is().Null();
        var ex = Xunit.Assert.Throws<XunitException>(()
            => When(_ => new MyModel { Id = The(id) }).Then().Result.Is().Null());
        ex.Message.Is(
            """
            When new MyModel { Id = the id }
            Then Result is null
            """);
        var theInt = The(id);
        ex.HasInnerMessage($"Expected Result to be null but found MyModel {{ Id = {theInt}, Name = , Values =  }}");
        ex.HasAssignments($"int:id = {theInt}");
    }
}
