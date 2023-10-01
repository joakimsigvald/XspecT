using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoFixture;

public class WhenMany : SubjectSpec<MyRetreiver, MyModel[]>
{
    public WhenMany() => When(_ => _.List());

    public class GivenReferingManyTwice : WhenMany
    {
        public GivenReferingManyTwice() => Given(Many<MyModel>());
        [Fact] public void ThenCanRetreiveThatArray() => Result.Is(Many<MyModel>());
        [Fact] public void ThenArrayHasThreeElements() => Result.Has().Count(3);
        [Fact] public void ThenDifferentReferencesToMany_AreTheSameArray() 
            => Then(this).Many<MyModel>().Is(Many<MyModel>());
    }

    public class GivenReferingManyOfHigherCountSecondTime : WhenMany
    {
        public GivenReferingManyOfHigherCountSecondTime() => Given(Two<MyModel>());
        [Fact] public void ThenItIsDiffeentFromFirst() => Result.Is().Not(Three<MyModel>());
        [Fact] public void ThenArrayHasOriginalCount() => Result.Has().Count(2);
        [Fact] public void ThenLastElementIsCreated() => Then(this).TheThird<MyModel>().Is(Three<MyModel>().Last());
        [Fact] public void ThenDifferentReferencesToManyOfSameCount_HaveSameElements() 
            => Then(this).Three<MyModel>().Is().EqualTo(Three<MyModel>());
    }

    public class GivenReferingManyOfLowerCountSecondTime : WhenMany
    {
        public GivenReferingManyOfLowerCountSecondTime() => Given(Four<MyModel>());
        [Fact] public void ThenItIsDiffeentFromFirst() => Result.Is().Not(Three<MyModel>());
        [Fact] public void ThenArrayHasOriginalCount() => Result.Has().Count(4);
        [Fact] public void ThenDifferentReferencesToManyOfSameCount_HaveSameElements() 
            => Then(this).Three<MyModel>().Is().EqualTo(Three<MyModel>());
    }
}