using XspecT.Assert;
using XspecT.Test.AutoMock;
using Xunit;

namespace XspecT.Test.ClassFixture;

public static class WhenClassFixtureSpecifyBeforeAndAfter
{
    public class WhenGetValues : Spec<MyWrapper<int>, (int, int)>
    {
        internal int _setupCount = 0;

        public WhenGetValues()
            => When(_ => _.GetValues(An<int>()))
            .After(_ => _setupCount++)
            .Before(_ =>
            {
                Xunit.Assert.Equal(1, _setupCount);
                An<int>(_ => 999);
            });
    }

    public class GivenOneAndTwo : Spec<MyWrapper<int>, (int, int)>, IClassFixture<WhenGetValues>
    {
        private readonly WhenGetValues _classFixture;

        public GivenOneAndTwo(WhenGetValues classFixture) : base(classFixture)
        {
            Given().Using(1).And().Default(2);
            _classFixture = classFixture;
        }

        [Fact] public void ThenReturnOneAndTwo() => Result.Is((1, 2));
        //[Fact] public void ThenIsSetup() => _classFixture._setupCount.Is(1);
    }

    public class GivenThreeAndFour : Spec<MyWrapper<int>, (int, int)>, IClassFixture<WhenGetValues>
    {
        private readonly WhenGetValues _classFixture;

        public GivenThreeAndFour(WhenGetValues _) : base(_)
            => Given().Using(3).And().Default(4);

        [Fact] public void ThenReturnThreeAndFour() => Result.Is((3, 4));
        //[Fact] public void ThenIsSetup() => _classFixture._setupCount.Is(1);
    }
}