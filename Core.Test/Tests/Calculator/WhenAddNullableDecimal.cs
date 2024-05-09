using XspecT.Assert;

using static XspecT.Test.Subjects.Calculator;

namespace XspecT.Test.Tests.Calculator;

public class Add_1_1 : StaticSpec<decimal?>
{
    public Add_1_1() => When<decimal?, decimal?>(Add, 1M, 1M);
    [Fact] public void Then_Return_2() => Result.Is(2);
    [Fact] public void Then_Return_Between_1_And_3() => Result.Is().GreaterThan(1).And.LessThan(3);
}

public class Add_2_3 : StaticSpec<decimal?>
{
    public Add_2_3() => When<decimal?, decimal?>(Add, 2, 3);
    [Fact] public void Then_Return_5() => Result.Is(5);
    [Fact] public void Then_Not_Return_Null() => Result.Is().NotNull();
}

public class Add_1_Null : StaticSpec<decimal?>
{
    public Add_1_Null() => When<decimal?, decimal?>(Add, 1, null);
    [Fact] public void Then_Return_Null() => Result.Is().Null();
}