using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoFixture;

public class WhenList : SubjectSpec<Retreiver, Model[]>
{
    public WhenList() => When(_ => _.List());

    public class GivenOneElement : WhenList
    {
        public GivenOneElement() => Given<IRepository>().That(_ => _.List()).Returns(One<Model>);
        [Fact] public void ThenCanRetreiveThatArray() => Then().Result.Is(The<Model[]>());
        [Fact] public void ThenArrayHasSingleElement() => Then().Result.Has().Count(1);
        [Fact] public void ThenElementCanBeRetreivedSeparately() => Then().Result.Single().Is(The<Model>());
    }

    public class GivenOneElementWithSetup : WhenList
    {
        public GivenOneElementWithSetup()
            => Given<IRepository>().That(_ => _.List()).Returns(() => One<Model>(_ => _.Name = A<string>()));

        [Fact] public void ThenArrayHasSingleElementWithSetup() => Then().Result.Single().Name.Is(The<string>());
    }

    public class GivenTwoElements : WhenList
    {
        public GivenTwoElements() => Given<IRepository>().That(_ => _.List()).Returns(Two<Model>);
        [Fact] public void ThenArrayHasTwoElements() => Then().Result.Has().Count(2);
        [Fact] public void ThenTheElementsAreDifferent() => Then().Result.First().Is().Not(Result.Last());
        [Fact] public void ThenSecondElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheSecond<Model>());
    }

    public class GivenTwoElementsWithSetup : WhenList
    {
        public GivenTwoElementsWithSetup() 
            => Given<IRepository>().That(_ => _.List()).Returns(() => Two<Model>(_ => _.Name = A<string>()));
        [Fact] public void ThenArrayHasTwoElements() => Then().Result.Has().Count(2);
        [Fact] public void ThenFirstElementHaveSetup() => Then().Result.First().Name.Is(The<string>());
        [Fact] public void ThenSecondElementHaveSetup() => Then().Result.Last().Name.Is(The<string>());
    }

    public class GivenTwoElementsWithIndexedSetup : WhenList
    {
        public GivenTwoElementsWithIndexedSetup() 
            => Given<IRepository>().That(_ => _.List()).Returns(() => Two<Model>((_, i) => _.Name = $"X{i + 1}"));
        [Fact] public void ThenArrayHasTwoElements() => Then().Result.Has().Count(2);
        [Fact] public void ThenFirstElementHaveSetup() => Then().Result.First().Name.Is("X1");
        [Fact] public void ThenSecondElementHaveSetup() => Then().Result.Last().Name.Is("X2");
    }

    public class GivenThreeElements : WhenList
    {
        public GivenThreeElements() => Given<IRepository>().That(_ => _.List()).Returns(Three<Model>);
        [Fact] public void ThenArrayHasThreeElements() => Then().Result.Has().Count(3);
        [Fact] public void ThenTheElementsAreDifferent() => Then().Result.First().Is().Not(Result.Last());
        [Fact] public void ThenThirdElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheThird<Model>());
    }

    public class GivenFourElements : WhenList
    {
        public GivenFourElements() => Given<IRepository>().That(_ => _.List()).Returns(Four<Model>);
        [Fact] public void ThenArrayHasFourElements() => Then().Result.Has().Count(4);
        [Fact] public void ThenFourthElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheFourth<Model>());
    }

    public class GivenFiveElements : WhenList
    {
        public GivenFiveElements() => Given<IRepository>().That(_ => _.List()).Returns(Five<Model>);
        [Fact] public void ThenArrayHasFiveElements() => Then().Result.Has().Count(5);
        [Fact] public void ThenFifthElementCanBeRetreivedSeparately() => Then().Result.Last().Is(TheFifth<Model>());
    }
}