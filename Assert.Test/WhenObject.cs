using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenObject : Spec<object>
{
    internal record MyModel(string Value) { }
    internal record MyArrayModel(params int[] Values) { }

    [Fact] public void IsSame() => The<MyModel>().Is(The<MyModel>());
    [Fact] public void IsNot() => new MyModel("A").Is().Not(new MyModel("A"));
    [Fact] public void IsEqual() => new MyModel("AbC").Is().EqualTo(new MyModel("AbC"));
    [Fact] public void IsLike() 
        => new MyArrayModel(1, 2).Is().NotEqualTo(new MyArrayModel(1, 2))
        .But.Like(new MyArrayModel(1, 2)).And.Like(new MyArrayModel(2, 1));

    [Fact] public void IsEquivalentTo() => new MyArrayModel(1, 2).Is().EquivalentTo(new MyArrayModel(2, 1));
    [Fact] public void IsNotLike() => new MyArrayModel(1, 2).Is().NotLike(new MyArrayModel(1, 2, 1));
    [Fact] public void IsNotEquivalentTo() => new MyArrayModel(1, 2).Is().NotEquivalentTo(new MyArrayModel(1, 2, 1));

    [Fact] public void Match() => new MyArrayModel(1, 2).Match(_ => _.Values.Length == 2);
}