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
    public void GivenTwoNumberAssignment_ThenShowAssignments()
    {
        var ex = Xunit.Assert.Throws<XunitException>(()
            => When(_ => new MyModel { Id = Two<int>()[1] }).Then().Result.Is().Null());
        ex.Message.Is(
            """
            When new MyModel { Id = Two<int>()[1] }
            Then Result is null
            """);
        var ints = Two<int>();
        ex.HasInnerMessage($"Expected Result to be null but found MyModel {{ Id = {ints[1]}, Name = , Values =  }}");
        ex.HasAssignments(
            $"""
                int:1 = {ints[0]}
                int:2 = {ints[1]}
                int[]:1 = [{ints[0]}, {ints[1]}]
                """);
    }

    [Fact]
    public void GivenTaggedAssignment_ThenShowAssignments()
    {
        Tag<int> id = new(nameof(id));
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