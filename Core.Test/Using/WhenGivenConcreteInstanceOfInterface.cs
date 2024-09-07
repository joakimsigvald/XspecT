using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.Using;

public class WhenUsingConcreteInstanceOfInterface : Spec<MyService, int>
{
    public WhenUsingConcreteInstanceOfInterface() => When(_ => _.GetNextId());

    [Fact]
    public void WithoutCast_ThenUseIt()
    {
        Given().Using(new FakeRepository(An<int>()))
            .Then().Result.Is(The<int>());
    }

    [Fact]
    public void WithCast_ThenUseIt()
    {
        Given().Using<IMyRepository>(new FakeRepository(An<int>()))
            .Then().Result.Is(The<int>());
    }
}

public class FakeRepository(int fakeId) : IMyRepository
{
    public int[] GetIds() => [];
    public MyModel GetModel() => new();
    public MyModel[] GetModels() => [];
    public Task<IEnumerable<MyModel>> GetModelsAsync() => Task.FromResult(Enumerable.Empty<MyModel>());
    public int GetNextId() => fakeId;
    public MyModel SetModel(MyModel model) => model;
}