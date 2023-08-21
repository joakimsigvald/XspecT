using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenConstructorOf_SUTTakeArrayOfObject: SubjectSpec<ArrayService, SomeValue[]>
{
    [Fact] public void ThenArrayIsEmpty() => When(_ => _.GetValues())
        .Then().Result.Has().Count(1); //Unfortunately AutoMock always create array with one mocked object
}

public class ArrayService
{
    private readonly SomeValue[] _values;

    public ArrayService(SomeValue[] values) => _values = values;

    public SomeValue[] GetValues() => _values;
}

public class SomeValue
{
}