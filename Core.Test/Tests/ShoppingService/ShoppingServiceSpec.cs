using XspecT.Fixture;

namespace XspecT.Test.Tests.ShoppingService;

public abstract class ShoppingServiceSpec<TResult> : SubjectSpec<Subjects.ShoppingService, TResult>
{
    protected ShoppingServiceSpec() => Using(("", ""), 1);
}