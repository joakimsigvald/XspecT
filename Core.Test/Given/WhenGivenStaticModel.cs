﻿using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenStaticModel : Spec<MyModel>
{
    [Theory]
    [InlineData("abc")]
    public void GivenDefaultSetup_ThenUseDefaultSetupOnSUT(string value)
    {
        Given().Default<MyModel>(_ => _.Name = value)
            .When(_ => _)
            .Then().Result.Name.Is(value);
        Specification.Is(
            """
            Given MyModel { Name = value }
            When _
            Then Result.Name is value
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenDefaultSetup_ThenUseDefaultSetupOnAValue(string value)
    {
        Given().Default<MyModel>(_ => _.Name = value)
            .When(_ => A<MyModel>())
            .Then().Result.Name.Is(value);
        Specification.Is(
            """
            Given MyModel { Name = value }
            When a MyModel
            Then Result.Name is value
            """);
    }

    [Theory]
    [InlineData("abc", 123)]
    public void GivenDefaultSetupAndModel_ThenUseDefaultSetupOnModel(string name, int id)
    {
        Given().Default<MyModel>(_ => _.Name = name).And(new MyModel { Id = id })
            .When(_ => _)
            .Then().Result.Name.Is(name).And(Result).Id.Is(id);
        Specification.Is(
            """
            Given MyModel { Name = name }
              and new MyModel { Id = id }
            When _
            Then Result.Name is name
              and Result.Id is id
            """);
    }

    [Theory]
    [InlineData("abc", 123)]
    public void GivenTwoDefaultSetup_ThenApplyBoth(string name, int id)
    {
        Given().Default<MyModel>(_ => _.Name = name).And<MyModel>(_ => _.Id = id)
            .When(_ => _)
            .Then().Result.Name.Is(name).And(Result).Id.Is(id);
        Specification.Is(
            """
            Given MyModel { Name = name }
              and MyModel { Id = id }
            When _
            Then Result.Name is name
              and Result.Id is id
            """);
    }

    [Theory]
    [InlineData("abc", 123)]
    public void GivenDefaultSetupAndAModel_ThenNotUseDefaultSetupOnTheModel(string name, int id)
    {
        Given().Default<MyModel>(_ => _.Name = name).And().A(new MyModel { Id = id })
            .When(_ => The<MyModel>())
            .Then().Result.Name.Is().Null().And(Result).Id.Is(id);
        Specification.Is(
            """
            Given MyModel { Name = name }
              and a MyModel { new MyModel { Id = id } }
            When the MyModel
            Then Result.Name is null
              and Result.Id is id
            """);
    }

    [Theory]
    [InlineData("abc", 123)]
    public void GivenDefaultSetupAndSpecificSetup_ThenUseBothSetupsOnTheModel(string name, int id)
    {
        Given().Default<MyModel>(_ => _.Name = name).And().A<MyModel>(_ => _.Id = id)
            .When(_ => The<MyModel>())
            .Then().Result.Name.Is(name).And(Result).Id.Is(id);
        Specification.Is(
            """
            Given MyModel { Name = name }
              and a MyModel { Id = id }
            When the MyModel
            Then Result.Name is name
              and Result.Id is id
            """);
    }

    [Theory]
    [InlineData("abc", "def")]
    public void GivenDefaultSetupOverriddenBySpecificSetup_ThenUseSpecificSetupOnTheModel(string defaultName, string name)
    {
        Given().Default<MyModel>(_ => _.Name = defaultName).And().A<MyModel>(_ => _.Name = name)
            .When(_ => The<MyModel>())
            .Then().Result.Name.Is(name);
        Specification.Is(
            """
            Given MyModel { Name = defaultName }
              and a MyModel { Name = name }
            When the MyModel
            Then Result.Name is name
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenSetupSecondModel_ThenApplySetupOn_TheSecondModel(string name)
    {
        Given().ASecond<MyModel>(_ => _.Name = name)
            .When(_ => TheSecond<MyModel>())
            .Then().Result.Name.Is(name);
        Specification.Is(
            """
            Given a second MyModel { Name = name }
            When the second MyModel
            Then Result.Name is name
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenSetupThirdModel_ThenApplySetupOn_TheThirdModel(string name)
    {
        Given().AThird<MyModel>(_ => _.Name = name)
            .When(_ => TheThird<MyModel>())
            .Then().Result.Name.Is(name);
        Specification.Is(
            """
            Given a third MyModel { Name = name }
            When the third MyModel
            Then Result.Name is name
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenSetupFourthModel_ThenApplySetupOn_TheFourthModel(string name)
    {
        Given().AFourth<MyModel>(_ => _.Name = name)
            .When(_ => TheFourth<MyModel>())
            .Then().Result.Name.Is(name);
        Specification.Is(
            """
            Given a fourth MyModel { Name = name }
            When the fourth MyModel
            Then Result.Name is name
            """);
    }

    [Theory]
    [InlineData("abc")]
    public void GivenSetupFifthModel_ThenApplySetupOn_TheFifthModel(string name)
    {
        Given().AFifth<MyModel>(_ => _.Name = name)
            .When(_ => TheFifth<MyModel>())
            .Then().Result.Name.Is(name);
        Specification.Is(
            """
            Given a fifth MyModel { Name = name }
            When the fifth MyModel
            Then Result.Name is name
            """);
    }
}