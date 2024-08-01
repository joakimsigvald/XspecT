using static XspecT.Test.Subjects.Calculator;

namespace XspecT.Test.Tests.Calculator;

public abstract class WhenAddInt : Spec<int>
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

    public class Given_TwoInts : WhenAddInt
    {
        public Given_TwoInts() => When(_ => Add(An<int>(), ASecond<int>()));

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 2, 3)]
        public void Then_Return_Sum(int t1, int t2, int sum) 
            => Given([t1, t2]).Then().Result.Is(sum);
    }
}

public abstract class WhenAddIntAsync : Spec<int>
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