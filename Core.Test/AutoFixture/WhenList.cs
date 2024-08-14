namespace XspecT.Test.AutoFixture;

public class WhenList : Spec<MyRetriever, MyModel[]>
{
    public WhenList() => When(_ => _.List());

    public class GivenOneSpecificElement : WhenList
    {
        private readonly MyModel _theModel = new();
        public GivenOneSpecificElement() => Given<IMyRepository>().That(_ => _.List()).Returns(() => One(_theModel));

        [Fact]
        public void ThenElementCanBeRetrieved()
        {
            Then().Result.Single().Is(_theModel);
            Description.Is(
@"Given IMyRepository.List() returns one _theModel
When _.List()
Then Result.Single() is _theModel");
        }
    }

    public class GivenOneElement : WhenList
    {
        public GivenOneElement() => Given<IMyRepository>().That(_ => _.List()).Returns(() => One<MyModel>());

        [Fact]
        public void ThenCanRetrieveThatArray()
        {
            Then().Result.Is(The<MyModel[]>());
            Description.Is(
@"Given IMyRepository.List() returns one MyModel
When _.List()
Then Result is the MyModel[]");
        }

        [Fact]
        public void ThenArrayHasSingleElement()
        {
            Then().Result.Has().Count(1);
            Description.Is(
@"Given IMyRepository.List() returns one MyModel
When _.List()
Then Result has count 1");
        }

        [Fact]
        public void ThenElementCanBeRetrievedSeparately()
        {
            Then().Result.Single().Is(The<MyModel>());
            Description.Is(
@"Given IMyRepository.List() returns one MyModel
When _.List()
Then Result.Single() is the MyModel");
        }
    }

    public class GivenOneElementWithSetup : WhenList
    {
        public GivenOneElementWithSetup()
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => One<MyModel>(_ => _.Name = A<string>()));

        [Fact]
        public void ThenArrayHasSingleElementWithSetup()
        {
            Then().Result.Single().Name.Is(The<string>());
            Description.Is(
@"Given IMyRepository.List() returns one MyModel { Name = a string }
When _.List()
Then Result.Single().Name is the string");
        }
    }

    public class GivenTwoElements : WhenList
    {
        public GivenTwoElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Two<MyModel>());

        [Fact]
        public void ThenArrayHasTwoElements()
        {
            Then().Result.Has().Count(2);
            Description.Is(
@"Given IMyRepository.List() returns two MyModel
When _.List()
Then Result has count 2");
        }

        [Fact]
        public void ThenTheElementsAreDifferent()
        {
            Then().Result.First().Is().Not(Result.Last());
            Description.Is(
@"Given IMyRepository.List() returns two MyModel
When _.List()
Then Result.First() is not Result.Last()");
        }

        [Fact]
        public void ThenSecondElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheSecond<MyModel>());
            Description.Is(
@"Given IMyRepository.List() returns two MyModel
When _.List()
Then Result.Last() is the second MyModel");
        }
    }

    public class GivenTwoElementsWithSetup : WhenList
    {
        public GivenTwoElementsWithSetup()
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => Two<MyModel>(_ => _.Name = A<string>()));

        [Fact]
        public void ThenArrayHasTwoElements()
        {
            Then().Result.Has().Count(2);
            Description.Is(
@"Given IMyRepository.List() returns two MyModel { Name = a string }
When _.List()
Then Result has count 2");
        }

        [Fact]
        public void ThenFirstElementHaveSetup()
        {
            Then().Result.First().Name.Is(The<string>());
            Description.Is(
@"Given IMyRepository.List() returns two MyModel { Name = a string }
When _.List()
Then Result.First().Name is the string");
        }

        [Fact]
        public void ThenSecondElementHaveSetup()
        {
            Then().Result.Last().Name.Is(The<string>());
            Description.Is(
@"Given IMyRepository.List() returns two MyModel { Name = a string }
When _.List()
Then Result.Last().Name is the string");
        }
    }

    public class GivenTwoElementsWithIndexedSetup : WhenList
    {
        public GivenTwoElementsWithIndexedSetup()
            => Given<IMyRepository>().That(_ => _.List()).Returns(() => Two<MyModel>((_, i) => _.Name = $"X{i + 1}"));

        [Fact]
        public void ThenArrayHasTwoElements()
        {
            Then().Result.Has().Count(2);
            Description.Is(
                """
                Given IMyRepository.List() returns two MyModel { Name = "X{i + 1}" }
                When _.List()
                Then Result has count 2
                """);
        }

        [Fact]
        public void ThenFirstElementHaveSetup()
        {
            Then().Result.First().Name.Is("X1");
            Description.Is(
"""
Given IMyRepository.List() returns two MyModel { Name = "X{i + 1}" }
When _.List()
Then Result.First().Name is "X1"
""");
        }

        [Fact]
        public void ThenSecondElementHaveSetup()
        {
            Then().Result.Last().Name.Is("X2");
            Description.Is(
"""
Given IMyRepository.List() returns two MyModel { Name = "X{i + 1}" }
When _.List()
Then Result.Last().Name is "X2"
""");
        }
    }

    public class GivenThreeElements : WhenList
    {
        public GivenThreeElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Three<MyModel>());

        [Fact]
        public void ThenArrayHasThreeElements()
        {
            Then().Result.Has().Count(3);
            Description.Is(
@"Given IMyRepository.List() returns three MyModel
When _.List()
Then Result has count 3");
        }

        [Fact]
        public void ThenTheElementsAreDifferent()
        {
            Then().Result.First().Is().Not(Result.Last());
            Description.Is(
@"Given IMyRepository.List() returns three MyModel
When _.List()
Then Result.First() is not Result.Last()");
        }

        [Fact]
        public void ThenThirdElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheThird<MyModel>());
            Description.Is(
@"Given IMyRepository.List() returns three MyModel
When _.List()
Then Result.Last() is the third MyModel");
        }
    }

    public class GivenFourElements : WhenList
    {
        public GivenFourElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Four<MyModel>());

        [Fact]
        public void ThenArrayHasFourElements()
        {
            Then().Result.Has().Count(4);
            Description.Is(
@"Given IMyRepository.List() returns four MyModel
When _.List()
Then Result has count 4");
        }

        [Fact]
        public void ThenFourthElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheFourth<MyModel>());
            Description.Is(
@"Given IMyRepository.List() returns four MyModel
When _.List()
Then Result.Last() is the fourth MyModel");
        }
    }

    public class GivenFiveElements : WhenList
    {
        public GivenFiveElements() => Given<IMyRepository>().That(_ => _.List()).Returns(() => Five<MyModel>());

        [Fact]
        public void ThenArrayHasFiveElements()
        {
            Then().Result.Has().Count(5);
            Description.Is(
@"Given IMyRepository.List() returns five MyModel
When _.List()
Then Result has count 5");
        }

        [Fact]
        public void ThenFifthElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheFifth<MyModel>());
            Description.Is(
@"Given IMyRepository.List() returns five MyModel
When _.List()
Then Result.Last() is the fifth MyModel");
        }
    }
}