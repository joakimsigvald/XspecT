﻿using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenValue : Spec<MyService, MyModel>
{
    [Fact]
    public void AsFirstSentence_ThenValueInPipeline()
    {
        Given(() => new MyModel() { Name = A<string>() })
            .When(_ => MyService.Echo(The<MyModel>()))
            .Then().Result.Name.Is(The<string>());
        Specification.Is(
            """
            Given new MyModel() { Name = A<string>() }
            When MyService.Echo(the MyModel)
            Then Result.Name is the string
            """);
    }

    [Fact]
    public void AsSecondSentence_ThenUseValueInPipeline()
    {
        Given<IMyRepository>().That(_ => _.GetModel()).Returns(A<MyModel>)
            .And(() => new MyModel() { Name = A<string>() })
            .When(_ => _.GetModel())
            .Then().Result.Name.Is(The<string>());
        Specification.Is(
            """
            Given new MyModel() { Name = A<string>() }
              and IMyRepository.GetModel() returns a MyModel
            When _.GetModel()
            Then Result.Name is the string
            """);
    }

    [Fact]
    public void AsReturnsValue_ThenUseValueInPipeline()
    {
        Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => A(new MyModel() { Name = A<string>() }))
            .When(_ => _.GetModel())
            .Then().Result.Name.Is(The<string>());
        Specification.Is(
            """
            Given IMyRepository.GetModel() returns a new MyModel() { Name = A<string>() }
            When _.GetModel()
            Then Result.Name is the string
            """);
    }

    [Fact]
    public void GivenNull_ThenUseNullInPipeline()
    {
        Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => A<MyModel>())
            .And((MyModel)null)
            .When(_ => _.GetModel()).Then().Result.Is().Null();
        Specification.Is(
            """
            Given (MyModel)null
              and IMyRepository.GetModel() returns a MyModel
            When _.GetModel()
            Then Result is null
            """);
    }
}

public class WhenGivenTagSetup : Spec<MyService, MyModel> 
{
    [Fact]
    public void ApplyAction() 
    { 
        Tag<MyModel> model = new();
        Given(model).Has(_ => _.Name = A<string>())
            .And(model).Has(_ => _.Id = An<int>())
            .And().Default(model)
            .When(_ => _.GetModel())
            .Then().Result.Name.Is(The<string>()).And(Result).Id.Is(The<int>());
        Specification.Is(
            """
            Given model is default
              and model has Id = an int
              and model has Name = a string
            When _.GetModel()
            Then Result.Name is the string
              and Result.Id is the int
            """);
    }

    [Fact]
    public void ApplyTransform()
    {
        Tag<MyModel> model = new();
        Given(model).Has(_ => _ with { Name = A<string>(), Id = An<int>() })
            .And<IMyRepository>().Returns(model)
            .When(_ => _.GetModel())
            .Then().Result.Name.Is(The<string>()).And(Result).Id.Is(The<int>());
        Specification.Is(
            """
            Given model has Name = a string, Id = an int
              and IMyRepository returns model
            When _.GetModel()
            Then Result.Name is the string
              and Result.Id is the int
            """);
    }
}