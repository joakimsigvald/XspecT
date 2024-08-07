﻿using XspecT.Assert;

namespace XspecT.Test.AutoFixture;

public class WhenMany : Spec<MyRetreiver, MyModel[]>
{
    public WhenMany() => When(_ => _.List());

    public class GivenReferingManyTwice : WhenMany
    {
        public GivenReferingManyTwice() => Given(Many<MyModel>());
        [Fact] public void ThenCanRetreiveThatArray() => Result.Is(Many<MyModel>());
        [Fact] public void ThenArrayHasThreeElements() => Result.Has().Count(3);
        [Fact]
        public void ThenDifferentReferencesToMany_AreTheSameArray()
            => Then(Many<MyModel>()).Is(Many<MyModel>());
    }

    public class GivenReferingManyOfHigherCountSecondTime : WhenMany
    {
        public GivenReferingManyOfHigherCountSecondTime() => Given(Two<MyModel>());
        [Fact] public void ThenItIsDiffeentFromFirst() => Result.Is().Not(Three<MyModel>());
        [Fact] public void ThenArrayHasOriginalCount() => Result.Has().Count(2);
        [Fact] public void ThenLastElementIsCreated() => Then(TheThird<MyModel>()).Is(Three<MyModel>().Last());
        [Fact]
        public void ThenDifferentReferencesToManyOfSameCount_HaveSameElements()
            => Then(Three<MyModel>()).Is().EqualTo(Three<MyModel>());
    }

    public class GivenReferingManyOfLowerCountSecondTime : WhenMany
    {
        public GivenReferingManyOfLowerCountSecondTime() => Given(Four<MyModel>());
        [Fact] public void ThenItIsDiffeentFromFirst() => Result.Is().Not(Three<MyModel>());
        [Fact] public void ThenArrayHasOriginalCount() => Result.Has().Count(4);
        [Fact]
        public void ThenDifferentReferencesToManyOfSameCount_HaveSameElements()
            => Then(Three<MyModel>()).Is().EqualTo(Three<MyModel>());
    }

    public class GivenMentionManyAfterTwo : WhenMany
    {
        [Fact]
        public void ThenReturnTwoAsMany()
            => Given<IMyRepository>().That(_ => _.List()).Returns(Many<MyModel>)
            .And(Two<MyModel>).Then().Result.Has().Count(2);
    }

    public class GivenMentionManyAfterFour : WhenMany
    {
        [Fact]
        public void ThenReturnFourAsMany()
            => Given<IMyRepository>().That(_ => _.List()).Returns(Many<MyModel>)
            .And(Four<MyModel>).Then().Result.Has().Count(4);
    }

    public class GivenMentionManyAfterOne : WhenMany
    {
        [Fact]
        public void ThenReturnThreeAsMany()
            => Given<IMyRepository>().That(_ => _.List()).Returns(Many<MyModel>)
            .And(One<MyModel>).Then().Result.Has().Count(3);
    }
}

public class WhenMockReturnsFewerElementsThanPreviouslyMentioned : Spec<MyRetreiver, MyModel[]>
{
    public WhenMockReturnsFewerElementsThanPreviouslyMentioned()
        => When(_ => _.Create(An<int>()));

    [Fact]
    public void ThenItIsDifferentFromFirst()
        => Given(3)
        .And<IMyRepository>().That(_ => _.Create(Three<MyModel>().Length))
        .Returns(Two<MyModel>)
        .Then().Result.Has().Count(2);
}