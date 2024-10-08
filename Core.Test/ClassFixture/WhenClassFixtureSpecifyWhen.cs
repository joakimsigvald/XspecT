﻿using XspecT.Assert;
using XspecT.Test.AutoMock;

namespace XspecT.Test.ClassFixture;

public static class WhenClassFixtureSpecifyWhen
{
    public class WhenGetValues : Spec<MyWrapper<int>, (int, int)>
    {
        public WhenGetValues() => When(_ => _.GetValues(An<int>()));
    }

    public class GivenOneAndTwo : Spec<MyWrapper<int>, (int, int)>, IClassFixture<WhenGetValues>
    {
        public GivenOneAndTwo(WhenGetValues _) : base(_)
            => Given().Using(1).And().Default(2);

        [Fact] public void ThenReturnOneAndTwo() => Result.Is((1, 2));
    }

    public class GivenThreeAndFour : Spec<MyWrapper<int>, (int, int)>, IClassFixture<WhenGetValues>
    {
        public GivenThreeAndFour(WhenGetValues _) : base(_) 
            => Given().Using(3).And().Default(4);

        [Fact] public void ThenReturnThreeAndFour() => Result.Is((3, 4));
    }
}