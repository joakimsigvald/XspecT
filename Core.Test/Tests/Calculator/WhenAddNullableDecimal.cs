using XspecT.Fixture;

using static XspecT.Test.Subjects.Calculator;
using XspecT.Verification;

namespace XspecT.Test.Tests.Calculator;

public abstract class WhenAddNullableDecimal : StaticSpec<decimal?>
{
    public WhenAddNullableDecimal() => When<decimal?, decimal?>(Add);

    public class Given_1_1 : WhenAddNullableDecimal
    {
        public Given_1_1() => Given<decimal?, decimal?>(1, 1);
        [Fact] public void Then_Return_2() => Result.Is(2);
        [Fact] public void Then_Return_Between_1_And_3() => Result.Is().GreaterThan(1).And.BeLessThan(3);
    }

    public class Given_2_3 : WhenAddNullableDecimal
    {
        public Given_2_3() => Given<decimal?, decimal?>(2, 3);
        [Fact] public void Then_Return_5() => Result.Is(5);
        [Fact] public void Then_Not_Return_Null() => Result.Is().NotNull();
    }

    public class Given_1_Null : WhenAddNullableDecimal
    {
        public Given_1_Null() => Given<decimal?, decimal?>(1, null);
        [Fact] public void Then_Return_Null() => Result.Is().Null();
    }
}