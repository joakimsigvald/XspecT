# XspecT: A fluent unit testing framework

Framework for writing and running automated tests in .Net in a fluent style, 
based on the popular "Given-When-Then" pattern, built upon XUnit, Moq, AutoMock, AutoFixture and FluentAssertions.

Whether you are beginner or expert in unit-testing, this framework will help you to write more descriptive, concise and maintainable tests.

## Usage

It is assumed that you are already familiar with Xunit and Moq, or similar test- and mocking frameworks.
There is an accompanying independent assertion framework called `XspecT.Assert`, which is built upon FluentAssertions, 
but with a less worthy syntax, based on the verbs `Is`, `Has` and `Does` instead of `Should`.

Is-assertions are recommended over Should-assertions for making the tests read more like specifications, listing requirements rather than asserting expected results.

This is an example of a complete test class (*specification*) with one test method (*requirement*):
```
using XspecT.Verification;
using XspecT.Fixture;

using static App.Calculator;

namespace App.Test;

public class CalculatorSpec : Spec<int>
{
    [Fact] public void WhenAdd_1_and_2_ThenSumIs_3() => When(_ => Add(1, 2)).Then().Result.Is(3);
}
```

When() is setting up the method to be called and Then() runs the test pipeline and provides the result.

### Test a static method with [Theory]

If you are used to writing one test class per production class and use Theory for test input, you can use a similar style with *XspecT*.
First you create your test-class overriding `Spec<[ReturnType]>` with the expected return type as generic argument.
Then create a test-method, attributed with `Theory` and `InlineData`, called `When[Something]`. 
This method call `When` to setup the test pipeline with test data and the method to test.
Finally verify the result by calling `Then().Result` (or only `Result`) on the returned pipeline and check the result with `Is`.

Example:
```
using XspecT.Verification;
using XspecT.Fixture;

using static App.Calculator;

namespace App.Test;

public class CalculatorSpec : Spec<int>
{
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(3, 4, 7)]
    public void GivenTwoNumbers_WhenAdd_ReturnSum(int term1, int term2, int sum)
        => When(_ => Add(term1, term2)).Then().Result.Is(sum);

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(3, 4, 12)]
    public void WhenMultiplyThenReturnProduct(int factor1, int factor2, int product)
        => When(_ => Multiply(factor1, factor2)).Then().Result.Is(product);
}
```

### Recommended conventions

For more complex and realistic scenarios, it is recommended to create tests in a separate project from the production code, named `[MyProject].Spec`. 
The test-project should mimmic the production project's folder structure, but in addition have one folder for each class to test, named as the class. 
Within that folder, create one test-class per method to test.

### Test a static void method
* When testing a static void method, there is no return value to verify in result and by convention the generic TResult parameter should be set to object.
* However you can use `Throws` or `NotThrows` to verify exceptions thrown.
 
Example:
```
namespace MyProject.Test.Validator;

public abstract class WhenVerifyAreEqual : Spec<object>
{
    protected WhenVerifyAreEqual() 
        => When(_ => MyProject.Validator.VerifyAreEqual(An<int>(), ASecond<int>()));

    public class Given_1_And_2 : WhenVerifyAreEqual
    {
        [Fact] public void ThenThrows_NotEqual() => Given(1, 2).Then().Throws<NotEqual>();
    }

    public class Given_2_And_2 : WhenVerifyAreEqual
    {
        [Fact] public void ThenDoNotThrow() => Given(2, 2).Then().DoesNotThrow();
    }
}
```

### Test a class with dependencies
* To test an instance method `[MyClass].[MyMethod]`, create an abstract class named `When[MyMethod]` inheriting `XspecT.Spec<[MyClass], [TheResult]>`.
* The subject under test will be created automatically with mocks and default values by AutoMock.
* Subject-under-test is available as the single input parameter to the lambda that is provided to the method `When`
You can supply or modify you own constructor arguments by calling `Given` or `Given().Using`.

* To mock behaviour of any dependency call `Given<[TheService]>().That(_ => _.[TheMethod](...)).Returns/Throws(...)`. 
* To verify a call to a dependency, write `Then<[TheService]>([SomeLambdaExpression])`. 
* Both mocking and verification of behaviour is based on Moq framework.
 
Example:
```
namespace MyProject.Spec.ShoppingService;

public abstract class WhenPlaceOrder : Spec<MyProject.ShoppingService, object>
{
    protected WhenPlaceOrder() 
        => When(_ => _.PlaceOrder(An<int>()))
        .Given<ICartRepository>().That(_ => _.GetCart(The<int>()))
        .Returns(() => A<Cart>(_ => _.Id = The<int>()));

    [Fact] public void ThenOrderIsCreated() => Then<IOrderService>(_ => _.CreateOrder(The<Cart>()));

    [Fact] public void ThenLogsOrderCreated()
        => Then<ILogger>(_ => _.Information($"OrderCreated from Cart {The<int>()}"));
}
```

All the examples above also works for async methods.

More examples and features can be found as Unit tests in the source code.