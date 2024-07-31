using static XspecT.Test.Helper;

namespace XspecT.Test.AutoFixture;

public class WhenList : Spec<MyRetriever, MyModel[]>
{
    public WhenList() => When(_ => _.List());

    public class GivenOneSpecificElement : WhenList
    {
        private readonly MyModel _theModel = new();
        public GivenOneSpecificElement() => Given<IMyRepository>().That(_ => _.List()).Returns(() => One(_theModel));

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ThenElementCanBeRetrieved(bool fail)
        {
            Then().Result.Single().Is(_theModel);

            if (fail)
                VerifyDescription(
    @"Given IMyRepository.List() returns one _theModel,
 when List(),
 then Result.Single() is _theModel");
        }
    }

    public class GivenOneElement : WhenList
    {
        public GivenOneElement() => Given<IMyRepository>().That(_ => _.List()).Returns(() => One<MyModel>());

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ThenCanRetrieveThatArray(bool fail)
        {
            Then().Result.Is(The<MyModel[]>());

            if (fail)
                VerifyDescription(
    @"Given IMyRepository.List() returns one MyModel,
 when List(),
 then Result is the MyModel[]");
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ThenArrayHasSingleElement(bool fail)
        {
            Then().Result.Has().Count(1);

            if (fail)
                VerifyDescription(
    @"Given IMyRepository.List() returns one MyModel,
 when List(),
 then Result has count 1");
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ThenElementCanBeRetrievedSeparately(bool fail)
        {
            Then().Result.Single().Is(The<MyModel>());

            if (fail)
                VerifyDescription(
    @"Given IMyRepository.List() returns one MyModel,
 when List(),
 then Result.Single() is the MyModel");
        }
    }

    public class GivenOneElementWithSetup : WhenList
    {
        public GivenOneElementWithSetup()
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => One<MyModel>(_ => _.Name == A<string>()));

        [Fact] public void ThenArrayHasSingleElementWithSetup() => Then().Result.Single().Name.Is(The<string>());
    }

    public class GivenTwoElements : WhenList
    {
        public GivenTwoElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Two<MyModel>());
        [Fact] public void ThenArrayHasTwoElements() => Then().Result.Has().Count(2);
        [Fact] public void ThenTheElementsAreDifferent() => Then().Result.First().Is().Not(Result.Last());
        [Fact] public void ThenSecondElementCanBeRetrievedSeparately() => Then().Result.Last().Is(TheSecond<MyModel>());
    }

    public class GivenTwoElementsWithSetup : WhenList
    {
        public GivenTwoElementsWithSetup() 
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => Two<MyModel>(_ => _.Name == A<string>()));
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
        public GivenThreeElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Three<MyModel>());
        [Fact] public void ThenArrayHasThreeElements() => Then().Result.Has().Count(3);
        [Fact] public void ThenTheElementsAreDifferent() => Then().Result.First().Is().Not(Result.Last());
        [Fact] public void ThenThirdElementCanBeRetrievedSeparately() => Then().Result.Last().Is(TheThird<MyModel>());
    }

    public class GivenFourElements : WhenList
    {
        public GivenFourElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Four<MyModel>());
        [Fact] public void ThenArrayHasFourElements() => Then().Result.Has().Count(4);
        [Fact] public void ThenFourthElementCanBeRetrievedSeparately() => Then().Result.Last().Is(TheFourth<MyModel>());
    }

    public class GivenFiveElements : WhenList
    {
        public GivenFiveElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Five<MyModel>());
        [Fact] public void ThenArrayHasFiveElements() => Then().Result.Has().Count(5);
        [Fact] public void ThenFifthElementCanBeRetrievedSeparately() => Then().Result.Last().Is(TheFifth<MyModel>());
    }
}