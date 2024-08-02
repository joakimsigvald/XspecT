﻿using static XspecT.Test.Helper;

namespace XspecT.Test.AutoFixture;

public class WhenSome : Spec<MyRetriever, MyModel[]>
{
    public WhenSome() => Given(Some<MyModel>).When(_ => _.List());

    public class GivenNoOtherReference : WhenSome
    {
        [Fact]
        public void ThenArrayHasTwoElements()
        {
            Result.Has().Count(2);
            VerifyDescription(
@"Given some MyModel,
 when List(),
 then Result has count 2");
        }
    }

    public class GivenOneIsMentionedAfter : WhenSome
    {
        public GivenOneIsMentionedAfter() => Given(One<MyModel>);

        [Fact]
        public void ThenCountIsOne()
        {
            Result.Has().Count(1);
            VerifyDescription(
@"Given some MyModel,
 given one MyModel,
 when List(),
 then Result has count 1");
        }
    }

    public class GivenThreeIsMentionedAfter : WhenSome
    {
        public GivenThreeIsMentionedAfter() => Given(Three<MyModel>);

        [Fact]
        public void ThenCountIsThree()
        {
            Result.Has().Count(3);
            VerifyDescription(
@"Given some MyModel,
 given three MyModel,
 when List(),
 then Result has count 3");
        }
    }

    public class GivenEmptyIsMentionedAfter : WhenSome
    {
        public GivenEmptyIsMentionedAfter() => Given(Array.Empty<MyModel>);

        [Fact]
        public void ThenCountIsZero()
        {
            Result.Has().Count(0);
            VerifyDescription(
@"Given some MyModel,
 given Array.Empty<MyModel>,
 when List(),
 then Result has count 0");
        }
    }

    public class GivenManyIsMentionedAfter : WhenSome
    {
        public GivenManyIsMentionedAfter() => Given(Many<MyModel>);

        [Fact]
        public void ThenCountIsFour()
        {
            Result.Has().Count(3);
            VerifyDescription(
@"Given some MyModel,
 given many MyModel,
 when List(),
 then Result has count 3");
        }
    }

    public class GivenOneIsMentionedBefore : WhenSome
    {
        public GivenOneIsMentionedBefore() => Given(One<MyModel>).And(Some<MyModel>);

        [Fact]
        public void ThenCountIsOne()
        {
            Result.Has().Count(1);
            VerifyDescription(
@"Given some MyModel,
 given one MyModel,
 given some MyModel,
 when List(),
 then Result has count 1");
        }
    }

    public class GivenTwoIsMentionedBefore : WhenSome
    {
        public GivenTwoIsMentionedBefore() => Given(Two<MyModel>).And(Some<MyModel>);

        [Fact]
        public void ThenCountIsTwo()
        {
            Result.Has().Count(2);
            VerifyDescription(
@"Given some MyModel,
 given two MyModel,
 given some MyModel,
 when List(),
 then Result has count 2");
        }
    }

    public class GivenEmptyIsMentionedBefore : WhenSome
    {
        public GivenEmptyIsMentionedBefore() => Given(Array.Empty<MyModel>).And(Some<MyModel>);

        [Fact]
        public void ThenCountIsTwo()
        {
            Result.Has().Count(2);
            VerifyDescription(
@"Given some MyModel,
 given Array.Empty<MyModel>,
 given some MyModel,
 when List(),
 then Result has count 2");
        }
    }

    public class GivenManyIsMentionedBefore : WhenSome
    {
        public GivenManyIsMentionedBefore() => Given(Many<MyModel>).And(Some<MyModel>);

        [Fact]
        public void ThenCountIsTwo()
        {
            Result.Has().Count(2);
            VerifyDescription(
@"Given some MyModel,
 given many MyModel,
 given some MyModel,
 when List(),
 then Result has count 2");
        }
    }
}