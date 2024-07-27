﻿using XspecT.Assert;

using static XspecT.Test.Helper;

namespace XspecT.Test.AutoFixture;

public class WhenGetWithSetup : Spec<MyMappingRetreiver, MyModel>
{
    public WhenGetWithSetup() 
        => When(_ => _.Get(An<int>()))
        .Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => A<MyModel>(_ => _.Name = A<string>()));

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Setup_CanBeProvided_ToPreviouslyMentionedModel(bool fail)
    {
        Given<IMyMapper>().That(_ => _.Map(A<MyModel>())).Returns(() => The<MyModel>())
            .Then().Result.Name.Is(The<string>());

        if (fail)
            VerifyDescription(
@"Given IMyMapper.Map(a MyModel) returns the MyModel,
 given IMyRepository.Get(the int) returns a MyModel { Name = a string },
 when Get(an int),
 then Result.Name is the string");
    }

    [Fact]
    public void Setup_CanBeProvided_MoreThanOnce_ToSameModel()
        => Given<IMyMapper>().That(_ => _.Map(The<MyModel>()))
        .Returns(() => A<MyModel>(_ => _.Id = An<int>()))
        .Then().Result.Name.Is(The<string>()).And(Result).Id.Is(The<int>());

    [Fact]
    public void Setup_CannotBeProvided_AfterThen()
        => Xunit.Assert.Throws<SetupFailed>(
            () => Given<IMyMapper>().That(_ => _.Map(A<MyModel>())).Returns(() => The<MyModel>())
            .Then().Result.Is(A<MyModel>(_ => _.Name = "abc")));
}