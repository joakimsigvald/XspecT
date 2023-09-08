using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenValueSetup : SubjectSpec<MyService, MyModel>
{
    [Fact]
    public void AsFirstSentence_ThenUseSetupInPipeline()
        => GivenThe<MyModel>(_ => _.Name = A<string>())
        .When(_ => _.Echo(A<MyModel>()))
        .Then().Result.Name.Is(The<string>());

    [Fact]
    public void AsSecondSentence_ThenUseSetupInPipeline()
        => GivenThat<IMyRepository>(_ => _.Setup(_ => _.GetModel()).Returns(A<MyModel>()))
        .AndThe<MyModel>(_ => _.Name = A<string>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(The<string>());
}

public class MyService
{
    private readonly IMyRepository _repo;
    public MyService(IMyRepository repo) => _repo = repo;
    public MyModel GetModel() => _repo.GetModel();
    public MyModel Echo(MyModel model) => model;
}

public interface IMyRepository
{
    MyModel GetModel();
}

public class MyModel
{
    public string Name { get; set; }
}