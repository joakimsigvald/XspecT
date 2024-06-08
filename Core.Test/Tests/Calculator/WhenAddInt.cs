using XspecT.Assert;

using static XspecT.Test.Subjects.Calculator;

namespace XspecT.Test.Tests.Calculator;

public abstract class WhenAddInt : Spec<object, int>
{
    public class Given_1_1 : WhenAddInt
    {
        public Given_1_1() => When(_ => Add(1, 1));
        [Fact] public void Then_Return_2() => Result.Is(2);
        [Fact] public void Then_Return_Between_1_And_3() => Result.Is().GreaterThan(1).And.LessThan(3);
    }

    public class Given_2_3 : WhenAddInt
    {
        public Given_2_3() => When(_ => Add(2, 3));
        [Fact] public void Then_Return_5() => Result.Is(5);
    }
}

public abstract class WhenAddIntAsync : Spec<object, int>
{
    public class Given_1_1 : WhenAddIntAsync
    {
        public Given_1_1() => When(_ => AddAsync(1, 1));
        [Fact] public void Then_Return_2() => Result.Is(2);
    }

    public class Given_2_3 : WhenAddIntAsync
    {
        [Fact] public void Then_Return_5() => When(_ => AddAsync(2, 3)).Then().Result.Is(5);
    }
}