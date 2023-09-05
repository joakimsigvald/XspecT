using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoFixture;

public class WhenMany : SubjectSpec<Retreiver, Model[]>
{
    public WhenMany() => When(_ => _.List());

    public class GivenReferingManyTwice : WhenMany
    {
        public GivenReferingManyTwice() => Using(Many<Model>());
        [Fact] public void ThenCanRetreiveThatArray() => Result.Is(Many<Model>());
        [Fact] public void ThenArrayHasThreeElements() => Result.Has().Count(3);
        [Fact] public void ThenDifferentReferencesToMany_AreTheSameArray() 
            => Then(this).Many<Model>().Is(Many<Model>());
    }

    public class GivenReferingManyOfHigherCountSecondTime : WhenMany
    {
        public GivenReferingManyOfHigherCountSecondTime() => Using(Two<Model>());
        [Fact] public void ThenItIsDiffeentFromFirst() => Result.Is().Not(Three<Model>());
        [Fact] public void ThenArrayHasOriginalCount() => Result.Has().Count(2);
        [Fact] public void ThenLastElementIsCreated() => Then(this).TheThird<Model>().Is(Three<Model>().Last());
        [Fact] public void ThenDifferentReferencesToManyOfSameCount_HaveSameElements() 
            => Then(this).Three<Model>().Is().EqualTo(Three<Model>());
    }

    public class GivenReferingManyOfLowerCountSecondTime : WhenMany
    {
        public GivenReferingManyOfLowerCountSecondTime() => Using(Four<Model>());
        [Fact] public void ThenItIsDiffeentFromFirst() => Result.Is().Not(Three<Model>());
        [Fact] public void ThenArrayHasOriginalCount() => Result.Has().Count(4);
        [Fact] public void ThenDifferentReferencesToManyOfSameCount_HaveSameElements() 
            => Then(this).Three<Model>().Is().EqualTo(Three<Model>());
    }
}