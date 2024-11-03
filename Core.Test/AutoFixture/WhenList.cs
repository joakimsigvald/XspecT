using XspecT.Assert;

namespace XspecT.Test.AutoFixture;

public class WhenList : Spec<MyRetriever, MyModel[]>
{
    public WhenList() 
        => When(_ => _.List())
        .Given<IMyRepository>().That(_ => _.List()).Returns(A<MyModel[]>);

    public class GivenOneSpecificElement : WhenList
    {
        private readonly MyModel _theModel = new();
        public GivenOneSpecificElement() => Given().One(_theModel);

        [Fact]
        public void ThenElementCanBeRetrieved()
        {
            Then().Result.Single().Is(_theModel);
            Specification.Is(
@"Given one MyModel { _theModel }
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.Single() is _theModel");
        }
    }

    public class GivenOneElement : WhenList
    {
        public GivenOneElement() => Given().One<MyModel>();

        [Fact]
        public void ThenCanRetrieveThatArray()
        {
            Then().Result.Is(The<MyModel[]>());
            Specification.Is(
@"Given one MyModel
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result is the MyModel[]");
        }

        [Fact]
        public void ThenArrayHasSingleElement()
        {
            Then().Result.Has().Count(1);
            Specification.Is(
@"Given one MyModel
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result has count 1");
        }

        [Fact]
        public void ThenElementCanBeRetrievedSeparately()
        {
            Then().Result.Single().Is(The<MyModel>());
            Specification.Is(
@"Given one MyModel
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.Single() is the MyModel");
        }
    }

    public class GivenOneElementWithSetup : WhenList
    {
        public GivenOneElementWithSetup()
            => Given().One<MyModel>(_ => _.Name = A<string>());

        [Fact]
        public void ThenArrayHasSingleElementWithSetup()
        {
            Then().Result.Single().Name.Is(The<string>());
            Specification.Is(
@"Given one MyModel { Name = a string }
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.Single().Name is the string");
        }
    }

    public class GivenOneElementWithTransform : WhenList
    {
        public GivenOneElementWithTransform()
            => Given().One<MyModel>(_ => _ with { Name = A<string>() });

        [Fact]
        public void ThenArrayHasSingleElementWithSetup()
        {
            Then().Result.Single().Name.Is(The<string>());
            Specification.Is(
@"Given one MyModel { Name = a string }
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.Single().Name is the string");
        }
    }

    public class GivenTwoElements : WhenList
    {
        public GivenTwoElements() => Given().Two<MyModel>();

        [Fact]
        public void ThenArrayHasTwoElements()
        {
            Then().Result.Has().Count(2);
            Specification.Is(
@"Given two MyModel
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result has count 2");
        }

        [Fact]
        public void ThenTheElementsAreDifferent()
        {
            Then().Result.First().Is().Not(Result.Last());
            Specification.Is(
@"Given two MyModel
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.First() is not Result.Last()");
        }

        [Fact]
        public void ThenSecondElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheSecond<MyModel>());
            Specification.Is(
@"Given two MyModel
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.Last() is the second MyModel");
        }
    }

    public class GivenTwoElementsWithSetup : WhenList
    {
        public GivenTwoElementsWithSetup()
            => Given().Two<MyModel>(_ => _.Name = A<string>());

        [Fact]
        public void ThenArrayHasTwoElements()
        {
            Then().Result.Has().Count(2);
            Specification.Is(
@"Given two MyModel { Name = a string }
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result has count 2");
        }

        [Fact]
        public void ThenFirstElementHaveSetup()
        {
            Then().Result.First().Name.Is(The<string>());
            Specification.Is(
@"Given two MyModel { Name = a string }
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.First().Name is the string");
        }

        [Fact]
        public void ThenSecondElementHaveSetup()
        {
            Then().Result.Last().Name.Is(The<string>());
            Specification.Is(
@"Given two MyModel { Name = a string }
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.Last().Name is the string");
        }
    }

    public class GivenTwoElementsWithTransform : WhenList
    {
        public GivenTwoElementsWithTransform()
            => Given().Two<MyModel>(_ => _ with { Name = A<string>() });

        [Fact]
        public void ThenSecondElementIsTransformed()
        {
            Then().Result.Last().Name.Is(The<string>());
            Specification.Is(
@"Given two MyModel { Name = a string }
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result.Last().Name is the string");
        }
    }

    public class GivenTwoElementsWithIndexedSetup : WhenList
    {
        public GivenTwoElementsWithIndexedSetup()
            => Given().Two<MyModel>((_, i) => _.Name = $"X{i + 1}");

        [Fact]
        public void ThenArrayHasTwoElements()
        {
            Then().Result.Has().Count(2);
            Specification.Is(
                """
                Given two MyModel { Name = "X{i + 1}" }
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result has count 2
                """);
        }

        [Fact]
        public void ThenFirstElementHaveSetup()
        {
            Then().Result.First().Name.Is("X1");
            Specification.Is(
                """
                Given two MyModel { Name = "X{i + 1}" }
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result.First().Name is "X1"
                """);
        }

        [Fact]
        public void ThenSecondElementHaveSetup()
        {
            Then().Result.Last().Name.Is("X2");
            Specification.Is(
                """
                Given two MyModel { Name = "X{i + 1}" }
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result.Last().Name is "X2"
                """);
        }
    }

    public class GivenTwoElementsWithIndexedTransform : WhenList
    {
        public GivenTwoElementsWithIndexedTransform()
            => Given().Two<MyModel>((_, i) => _ with { Name = $"X{i + 1}" });

        [Fact] public void ThenSecondElementIsTransformed() => Then().Result.Last().Name.Is("X2");
    }

    public class GivenThreeElements : WhenList
    {
        public GivenThreeElements() => Given().Three<MyModel>();

        [Fact]
        public void ThenArrayHasThreeElements()
        {
            Then().Result.Has().Count(3);
            Specification.Is(
                """
                Given three MyModel
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result has count 3
                """);
        }

        [Fact]
        public void ThenTheElementsAreDifferent()
        {
            Then().Result.First().Is().Not(Result.Last());
            Specification.Is(
                """
                Given three MyModel
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result.First() is not Result.Last()
                """);
        }

        [Fact]
        public void ThenThirdElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheThird<MyModel>());
            Specification.Is(
                """
                Given three MyModel
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result.Last() is the third MyModel
                """);
        }
    }

    public class GivenFourElements : WhenList
    {
        public GivenFourElements() => Given().Four<MyModel>();

        [Fact]
        public void ThenArrayHasFourElements()
        {
            Then().Result.Has().Count(4);
            Specification.Is(
                """
                Given four MyModel
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result has count 4
                """);
        }

        [Fact]
        public void ThenFourthElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheFourth<MyModel>());
            Specification.Is(
                """
                Given four MyModel
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result.Last() is the fourth MyModel
                """);
        }
    }

    public class GivenFiveElements : WhenList
    {
        public GivenFiveElements() => Given().Five<MyModel>();

        [Fact]
        public void ThenArrayHasFiveElements()
        {
            Then().Result.Has().Count(5);
            Specification.Is(
                """
                Given five MyModel
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result has count 5
                """);
        }

        [Fact]
        public void ThenFifthElementCanBeRetrievedSeparately()
        {
            Then().Result.Last().Is(TheFifth<MyModel>());
            Specification.Is(
                """
                Given five MyModel
                  and IMyRepository.List() returns a MyModel[]
                When _.List()
                Then Result.Last() is the fifth MyModel
                """);
        }
    }

    public class GivenListIsNull : WhenList 
    {
        public GivenListIsNull() => Given((MyModel[])null);
        [Fact] public void ThenReturnNull() => Result.Is().Null();
    }
}