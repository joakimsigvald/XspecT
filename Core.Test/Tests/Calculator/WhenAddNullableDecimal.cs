using XspecT.Assert;

using static XspecT.Test.Subjects.Calculator;

namespace XspecT.Test.Tests.Calculator;

public class Add_1_1 : Spec<decimal?>
{
    public Add_1_1() => When(_ => Add(1M, 1M));
    [Fact] public void Then_Return_2() => Result.Is(2);
    [Fact] public void Then_Return_Between_1_And_3() => Result.Is().GreaterThan(1).And.LessThan(3);
}

public class Add_2_3 : Spec<decimal?>
{
    public Add_2_3() => When(_ => Add(2, 3));
    [Fact] public void Then_Return_5() => Result.Is(5);
    [Fact] public void Then_Not_Return_Null() => Result.Is().NotNull();
}

public class Add_1_Null : Spec<decimal?>
{
    public Add_1_Null() => When(_ => Add(1M, null));
    [Fact] public void Then_Return_Null() => Result.Is().Null();
}