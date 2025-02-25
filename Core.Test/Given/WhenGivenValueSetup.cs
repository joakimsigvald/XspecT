using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenValueSetup : Spec<MyService, MyModel>
{
    [Fact]
    public void AsFirstSentence_ThenUseSetupInPipeline()
    {
        Given<MyModel>(_ => _.Name = A<string>())
            .When(_ => MyService.Echo(A<MyModel>()))
            .Then().Result.Name.Is(The<string>());
        Specification.Is(
            """
            Given MyModel { Name = a string }
            When MyService.Echo(a MyModel)
            Then Result.Name is the string
            """);
    }

    [Fact]
    public void AsSecondSentence_ThenUseSetupInPipeline()
    {
        Given<IMyRepository>().That(_ => _.GetModel()).Returns(A<MyModel>)
            .And<MyModel>(_ => _.Name = A<string>())
            .When(_ => _.GetModel())
            .Then().Result.Name.Is(The<string>());
        Specification.Is(
            """
            Given MyModel { Name = a string }
              and IMyRepository.GetModel() returns a MyModel
            When _.GetModel()
            Then Result.Name is the string
            """);
    }

    [Fact]
    public void WithArithmeticExpression_ThenParseValues()
    {
        Given<MyModel>(_ => _.Name = A<string>() + ASecond<string>())
            .When(_ => MyService.Echo(A<MyModel>()))
            .Then().Result.Name.Does().StartWith(The<string>());
        Specification.Is(
            """
            Given MyModel { Name = a string + a second string }
            When MyService.Echo(a MyModel)
            Then Result.Name starts with the string
            """);
    }

    [Fact]
    public void WithLineBreaks_ThenMergeToOneLine()
    {
        Given("abc"
            .ToUpper()
            .ToLower())
            .When(_ => _.GetModel())
            .Then().Result.Name.Satisfies(_ => char.IsLower(_[0]));
        Specification.Is(
            """
            Given "abc".ToUpper().ToLower()
            When _.GetModel()
            Then Result.Name satisfies char.IsLower(_[0])
            """);
    }

    [Fact]
    public void WithLongLine_ThenInsertLineBreaks()
    {
        Given("abc".ToUpper().ToLower().ToUpper().ToLower()
            .ToUpper().ToLower().ToUpper().ToLower().ToUpper()
            .ToLower().ToUpper().ToLower().ToUpper().ToLower())
            .When(_ => _.GetModel())
            .Then().Result.Name.Satisfies(_ => char.IsLower(_[0]));
        Specification.Is(
            """
            Given "abc".ToUpper().ToLower().ToUpper().ToLower().ToUpper().ToLower().ToUpper(
                  ).ToLower().ToUpper().ToLower().ToUpper().ToLower().ToUpper().ToLower()
            When _.GetModel()
            Then Result.Name satisfies char.IsLower(_[0])
            """);
    }
}