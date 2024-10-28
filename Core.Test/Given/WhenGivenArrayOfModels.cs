using XspecT.Assert;

namespace XspecT.Test.Given;

public abstract class WhenGetArrayOfModels : Spec<MyService, MyModel[]>
{
    protected WhenGetArrayOfModels() => When(_ => _.GetModels());

    public class GivenMentionOfAnyArray_FollowedBy_MentionOfArrayOfSpecificLength : WhenGetArrayOfModels
    {
        public GivenMentionOfAnyArray_FollowedBy_MentionOfArrayOfSpecificLength()
            => Given<IMyRepository>().That(_ => _.GetModels()).Returns(() => A<MyModel[]>())
                .And(Two<MyModel>)
                .And<MyModel>(_ => _.Name = A<string>());

        [Fact]
        public void ThenGetArrayOfSpecificLength()
        {
            Then().Result.Is(Two<MyModel>()).And(Result).First().Name.Is(The<string>());
            Specification.Is(
                """
            Given MyModel { Name = a string }
              and two MyModel
              and IMyRepository.GetModels() returns a MyModel[]
            When _.GetModels()
            Then Result is two MyModel
              and Result.First().Name is the string
            """);
        }
    }

    public abstract class GivenSpecifiedCountOfArray : WhenGetArrayOfModels
    {
        protected GivenSpecifiedCountOfArray() => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>);

        public class GivenZero : GivenSpecifiedCountOfArray
        {
            public GivenZero() => Given().Zero<MyModel>();
            [Fact] public void ThenReturnedArrayHasZeroElements() => Then().Result.Length.Is(0);
        }

        public class GivenOne : GivenSpecifiedCountOfArray
        {
            public GivenOne() => Given().One<MyModel>();
            [Fact] public void ThenReturnedArrayHasOneElement() => Then().Result.Length.Is(1);
        }

        public class GivenTwo : GivenSpecifiedCountOfArray
        {
            public GivenTwo() => Given().Two<MyModel>();
            [Fact] public void ThenReturnedArrayHasTwoElements() => Then().Result.Length.Is(2);
        }

        public class GivenThree: GivenSpecifiedCountOfArray
        {
            public GivenThree() => Given().Three<MyModel>();
            [Fact] public void ThenReturnedArrayHasThreeElements() => Then().Result.Length.Is(3);
        }

        public class GivenFour : GivenSpecifiedCountOfArray
        {
            public GivenFour() => Given().Four<MyModel>();
            [Fact] public void ThenReturnedArrayHasFourElements() => Then().Result.Length.Is(4);
        }

        public class GivenFive : GivenSpecifiedCountOfArray
        {
            public GivenFive() => Given().Five<MyModel>();
            [Fact] public void ThenReturnedArrayHasFiveElements() => Then().Result.Length.Is(5);
        }
    }

    public class GivenOneWithSetup : WhenGetArrayOfModels
    {
        public GivenOneWithSetup()
            => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>)
            .And().One<MyModel>(_ => _.Id = An<int>());

        [Fact] public void ThenApplySetupToReturnedArray() => Then().Result.Single().Id.Is(The<int>());
    }

    public class GivenTwoWithSetup : WhenGetArrayOfModels
    {
        public GivenTwoWithSetup()
            => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>)
                .And().Two<MyModel>(_ => _.Id = An<int>());

        [Fact]
        public void GivenTwoWithSetup_ThenApplySetupToReturnedArray()
            => Then().Result.Has().All(m => m.Id == The<int>());
    }

    public class GivenThreeWithIndexedSetup : WhenGetArrayOfModels
    {
        public GivenThreeWithIndexedSetup()
            => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>)
                .And().Three<MyModel>((_, i) => _.Id = i + 1);

        [Fact] public void ThenApplySetupToReturnedArray() => Then().Result.Has().All((m, i) => m.Id == i + 1);
    }

    public class GivenFourWithTransform : WhenGetArrayOfModels
    {
        public GivenFourWithTransform()
            => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>)
                .And().Four<MyModel>(_ => _ with { Id = An<int>() });

        [Fact] public void ThenApplyTransformToReturnedArray() => Then().Result.Has().All(m => m.Id == The<int>());
    }

    public class GivenFiveWithIndexedTransform : WhenGetArrayOfModels
    {
        public GivenFiveWithIndexedTransform()
            => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>)
                .And().Five<MyModel>((_, i) => _ with { Id = i + 1 });

        [Fact] public void ThenApplyTransformToReturnedArray() => Then().Result.Has().All((m, i) => m.Id == i + 1);
    }
}
public abstract class WhenGivenArrayOfModelsAsync : Spec<MyService, MyModel[]>
{
    protected WhenGivenArrayOfModelsAsync() => When(_ => _.GetModelsAsync());

    public class GivenDefaultEnumerableProvided : WhenGivenArrayOfModelsAsync
    {
        public GivenDefaultEnumerableProvided()
            => Given<IMyRepository>().Returns(An<IEnumerable<MyModel>>);

        [Fact]
        public void ThenCanGetTaskOfEnumerable()
        {
            Then().DoesNotThrow();
            Specification.Is(
                """
            Given IMyRepository returns an IEnumerable<MyModel>
            When _.GetModelsAsync()
            Then does not throw
            """);
        }
    }
}

public class GivenDefaultEnumerableNotProvidedWhenGetTaskOfIEnumerable : Spec<MyService, MyModel[]>
{
    [Fact]
    public void GivenDefaultEnumerableNotProvided_WhenGetTaskOfEnumerable_ThrowSetupFailed()
    {
        Xunit.Assert.Throws<SetupFailed>(() =>
        When(_ => _.GetModelsAsync()).Then().DoesNotThrow());
    }
}