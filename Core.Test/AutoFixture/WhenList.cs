using XspecT.Assert;

namespace XspecT.Test.AutoFixture;

public class WhenList : SubjectSpec<MyRetreiver, MyModel[]>
{
    public WhenList() => When(_ => _.List());

    public class GivenOneElement : WhenList
    {
        public GivenOneElement() => Given<IMyRepository>().That(_ => _.List()).Returns(One<MyModel>);
        [Fact] public void ThenCanRetreiveThatArray() => Then().Result.Is(The<MyModel[]>());
        [Fact] public void ThenArrayHasSingleElement() => Then().Result.Has().Count(1);
        [Fact] public void ThenElementCanBeRetreivedSeparately() => Then().Result.Single().Is(The<MyModel>());
    }

    public class GivenOneElementWithSetup : WhenList
    {
        public GivenOneElementWithSetup()
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => One<MyModel>(_ => _.Name = A<string>()));

        [Fact] public void ThenArrayHasSingleElementWithSetup() => Then().Result.Single().Name.Is(The<string>());
    }

    public class GivenTwoElements : WhenList
    {
        public GivenTwoElements() => Given<IMyRepository>().That(_ => _.List()).Returns(Two<MyModel>);
        [Fact] public void ThenArrayHasTwoElements() => Then().Result.Has().Count(2);
        [Fact] public void ThenTheElementsAreDifferent() => Then().Result.First().Is().Not(Result.Last());
        [Fact] public void ThenSecondElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheSecond<MyModel>());
    }

    public class GivenTwoElementsWithSetup : WhenList
    {
        public GivenTwoElementsWithSetup() 
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => Two<MyModel>(_ => _.Name = A<string>()));
        [Fact] public void ThenArrayHasTwoElements() => Then().Result.Has().Count(2);
        [Fact] public void ThenFirstElementHaveSetup() => Then().Result.First().Name.Is(The<string>());
        [Fact] public void ThenSecondElementHaveSetup() => Then().Result.Last().Name.Is(The<string>());
    }

    public class GivenTwoElementsWithIndexedSetup : WhenList
    {
        public GivenTwoElementsWithIndexedSetup() 
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => Two<MyModel>((_, i) => _.Name = $"X{i + 1}"));
        [Fact] public void ThenArrayHasTwoElements() => Then().Result.Has().Count(2);
        [Fact] public void ThenFirstElementHaveSetup() => Then().Result.First().Name.Is("X1");
        [Fact] public void ThenSecondElementHaveSetup() => Then().Result.Last().Name.Is("X2");
    }

    public class GivenThreeElements : WhenList
    {
        public GivenThreeElements() => Given<IMyRepository>().That(_ => _.List()).Returns(Three<MyModel>);
        [Fact] public void ThenArrayHasThreeElements() => Then().Result.Has().Count(3);
        [Fact] public void ThenTheElementsAreDifferent() => Then().Result.First().Is().Not(Result.Last());
        [Fact] public void ThenThirdElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheThird<MyModel>());
    }

    public class GivenFourElements : WhenList
    {
        public GivenFourElements() => Given<IMyRepository>().That(_ => _.List()).Returns(Four<MyModel>);
        [Fact] public void ThenArrayHasFourElements() => Then().Result.Has().Count(4);
        [Fact] public void ThenFourthElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheFourth<MyModel>());
    }

    public class GivenFiveElements : WhenList
    {
        public GivenFiveElements() => Given<IMyRepository>().That(_ => _.List()).Returns(Five<MyModel>);
        [Fact] public void ThenArrayHasFiveElements() => Then().Result.Has().Count(5);
        [Fact] public void ThenFifthElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheFifth<MyModel>());
    }
}