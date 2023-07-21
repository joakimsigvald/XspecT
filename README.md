# XspecT: A fluent unit testing framework

## About

Framework for writing and running automated tests in .Net in a flexible and fluent style, 
based on the popular "Given-When-Then" pattern, built upon XUnit, Moq, AutoMock and FluentAssertions.

Whether you are beginner or expert in unit-testing, this framework will help you to write more descriptive, concise and maintainable tests.

## How to use

It is assumed that you are already familiar with Xunit and Moq, or similar test and mocking frameworks.
This package includes FluentAssertions, but also comes with its own, more limited but less wordy assertion methods, based on the verb `Is` instead of `Should`.
Is-assertions have the same return-types as Should-assertions, so they can be combined in the same sentence.

Is-assertions are recommended to make the tests read more like specifications, listing requirements rather than asserting expected results.

This is an example of a complete test class (*specification*) with one test method (*requirement*):
```
using XspecT.Verification;
using XspecT.Fixture;

using static App.Calculator;

namespace App.Test;

public class CalculatorSpec : StaticSpec<int>
{
    [Fact] public void WhenAdd_1_and_2_ThenSumIs_3() => Given(1, 2).When(Add).Then.Result.Is(3);
}
```

### Test a static method with [Theory]

If you are used to writing one test class per production class and use Theory for test input, you can use a similar style with *XspecT*.
First you create your test-class overriding `StaticSpec<[ReturnType]>` with the expected return type as generic argument.
Then create a test-method, attributed with `Theory` and `InlineData`, called `When[Something]`. 
This method call `Given` and `When`, in any order, to setup the test pipeline with test data and the method to test.
Finally verify the result by calling `Then.Result` (or only `Result`) on the returned pipeline and check the result with `Is`.

Example:
```
public class CalculatorSpec : StaticSpec<int>
{
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(3, 4, 7)]
    public void GivenTwoNumbers_WhenAdd_ReturnSum(int x, int y, int sum)
        => Given(x, y).When(Calculator.Add).Result.Is(sum);

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(3, 4, 12)]
    public void WhenMultiplyThenReturnProduct(int x, int y, int product)
        => Given(x, y).When(Calculator.Multiply).Result.Is(product);
}
```

### Recommended conventions

For more complex and realistic scenarios, it is recommended to create tests in a separate project from the production code, named `[MyProject].Test`. 
The test-project should mimmic the production project's folder structure, but in addition have one folder for each class to test, named as the class. 
Within that folder, create one test-class per method to test.

### Test a static void method
* When testing a static void method, there is no return value to verify in result and by convention the generic TResult parameter is set to object.
* However you can use `Throws` or `NotThrows` to verify exceptions thrown.
 
Example:
```
namespace MyProject.Test.Validator;

public abstract class WhenVerifyAreEqual : StaticSpec<object>
{
    protected WhenVerifyAreEqual() => When<int, int>(MyProject.Validator.VerifyAreEqual);

    public class Given_1_And_2 : WhenVerifyAreEqual
    {
        [Fact] public void ThenThrows_NotEqual() => Given(1, 2).Then.Throws<NotEqual>();
    }

    public class Given_2_And_2 : WhenVerifyAreEqual
    {
        [Fact] public void ThenDoNotThrow() => Given(2, 2).Then.NotThrows();
    }
}
```

### Test a class with dependencies
* To test an instance method `[MyClass].[MyMethod]`, inherit `XspecT.SubjectSpec<[MyClass], TResult>`.
* It is recommended practice to create a common baseclass for all tests of `[MyClass]`, named `[MyClass]Spec`.
* The subject under test (sut) will be created automatically with mocks and default values by AutoMock. 
You can supply you own constructor arguments by calling `Using` (which will be applied in the same order when test pipeline is executed).
* For each method to test, create an abstract class named `When[MyMethod]` inheriting `[MyClass]Spec` in the same way as for static methods.

* To mock behaviour of any dependency, either override `Setup` or provide the mocking by calling `GivenThat`. 
Each call to `GivenThat` will provide additional arrangement that will be applied on test execution on the inversed order.
* The framework gives you direct access to one (lazily generated) mock each of any class type type. You can access a mock by `The<MyMockedInterface>()`.
* To verify a call to a dependency, write `Then.Does<MyMockedInterface>([SomeLambdaExpression])`. 
* Moq framework is used to express both mocking and verification of behaviour.
 
Example:
```
namespace MyProject.Test.ShoppingService;

public abstract class ShoppingServiceSpec<TResult> : SubjectSpec<MyProject.ShoppingService, TResult>
{
    protected ShoppingServiceSpec() => Using(new MyTestLogger());
}

public abstract class WhenPlaceOrder : ShoppingServiceSpec<object>
{
    protected ShoppingCart Cart;

    protected WhenPlaceOrder() 
        => When(() => SUT.PlaceOrder(Cart)).GivenThat(() => The<ICartRepository>().ReturnsDefault(Cart));

    public class GivenCart : WhenPlaceOrder
    {
        [Fact] public void ThenOrderIsCreated() 
            => GivenThat(() => Cart = new()).Then.Does<IOrderService>(_ => _.CreateOrder(Cart));
    }
}
```

### Test async methods

All the examples above also works for async methods, with small modifications.

More examples can be found as Unit tests in the source code.