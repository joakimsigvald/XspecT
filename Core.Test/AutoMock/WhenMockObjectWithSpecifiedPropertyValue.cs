using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenMockObjectWithSpecifiedPropertyValue : SubjectSpec<StaticObjectService, MyObject>
{
    public WhenMockObjectWithSpecifiedPropertyValue() => When(_ => _.GetValue());

    public class GivenItWasProvided : WhenMockObjectWithSpecifiedPropertyValue
    {
        [Fact]
        public void Then_It_Has_ProvidedValue()
            => Given(A<MyObject>(_ => _.Age = 3)).Then().Result.Age.Is(3);

        [Fact]
        public void Then_Can_Retreive_The_Object()
            => Given(A<MyObject>(_ => _.Age = 3)).Then(The<MyObject>()).Age.Is(3);
    }
}

public class StaticObjectService
{
    private readonly MyObject _value;
    public StaticObjectService(MyObject value) => _value = value;
    public MyObject GetValue() => _value;
}


public class MyObject
{
    public string Name { get; set; }
    public int Age { get; set; }
}