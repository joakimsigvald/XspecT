using XspecT.Fixture;

using static XspecT.Test.Subjects.Calculator;
using XspecT.Verification;

namespace XspecT.Test.Tests.Calculator;

public abstract class WhenAddNullableFloat : StaticSpec<float?>
{
    public WhenAddNullableFloat() => When<float?, float?>(Add);

    public class Given_1_1 : WhenAddNullableFloat
    {
        public Given_1_1() => Given<float?, float?>(1, 1);
        [Fact] public void Then_Return_2() => Result.Is(2);
        [Fact] public void Then_Return_Between_1_And_3() => Result.Is().GreaterThan(1).And.LessThan(3);
    }

    public class Given_2_3 : WhenAddNullableFloat
    {
        public Given_2_3() => Given<float?, float?>(2, 3);
        [Fact] public void Then_Return_5() => Result.Is(5);
    }
}