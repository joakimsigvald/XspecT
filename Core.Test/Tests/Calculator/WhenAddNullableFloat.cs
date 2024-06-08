using XspecT.Assert;

using static XspecT.Test.Subjects.Calculator;

namespace XspecT.Test.Tests.Calculator;

public class AddF_1_1 : Spec<object, float?>
{
    public AddF_1_1() => When(_ => Add(1, 1));
    [Fact] public void Then_Return_2() => Result.Is(2);
    [Fact] public void Then_Return_Between_1_And_3() => Result.Is().GreaterThan(1).And.LessThan(3);
}

public class AddF_2_3 : Spec<object, float?>
{
    public AddF_2_3() => When(_ => Add(2, 3));
    [Fact] public void Then_Return_5() => Result.Is(5);
}