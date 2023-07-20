using XspecT.Fixture;

namespace XspecT.Test.Tests.AsyncShoppingService;

public abstract class ShoppingServiceAsyncSpec<TResult>
    : SubjectSpecAsync<Subjects.ShoppingServiceAsync, TResult>
{
}