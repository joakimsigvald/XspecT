# XspecT: A fluent unit testing framework

Framework for writing and running automated tests in .Net in a flexible and fluent style, 
based on the popular "Given-When-Then" pattern, built upon XUnit, Moq, AutoMock and FluentAssertions.

Whether you are beginner or expert in unit-testing, this framework will help you to write more descriptive tests with less code.

## Usage

It is assumed that you are already familiar with Xunit and Moq, or similar test and mocking frameworks.
This package includes FluentAssertions, but also comes with its own, more limited but less wordy assertion methods, based on the verb `Is` instead of `Should`.
Is-assertions have the same return-types as Should-assertions, so they can be combined in the same sentence.

Is-assertions are recommended to make the tests read more like specifications, stating facts rather than expectations.

For instance `When(SUT.AddNumbers(X, Y)).Given((X, Y) = (1, 2).Then.Result.Is(3)`

### Test a static method with [Theory]

If you are used to writing one test class per production class and use Theory for test input, you can use a similar style to write minimalistic tests with a minimum of clutter.
First you create your test-class, or *Specification*, overriding `StaticSpec<[TReturn]>` with the expected return type as generic argument.
Then create a test pipeline by calling `When` with a lambda expression to test.
Finally you can verify the result by calling `Then` on the returned pipeline and check the result with `Is`

Example:
```
public class CalculatorSpec : StaticSpec<int>
{
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(3, 4, 7)]
    public void WhenAddThenReturnSum(int x, int y, int sum)
        => When(() => Calculator.Add(x, y)).Result.Is(sum);

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(3, 4, 12)]
    public void WhenMultiplyThenReturnProduct(int x, int y, int product)
        => When(() => Calculator.Multiply(x, y)).Result.Is(product);
}
```

### Recommended conventions

For more complex and realistic scenarious, it is recommended to create tests in a separate project from the production code, named `[MyProject].Test`. 
The test-project should mimmic the production project's folder structure, but in addition have one folder for each class to test, named as the class. 
Within that folder, create one test-class per method to test.

### Test a static method with [Fact]

* To test a static method `[MyClass].[MyMethod]` returning `[ReturnType]`, Create a new abstract class named `When[MyMethod]` inheriting `StaticSpec<[ReturnType]>`.
* Create a parameterless constructor that calls `When` with a lambda expression calling the method to test: `When(() => [MyClass].[MyMethod]([Args...]))`.
* Each argument should be defined as a protected field at the top of the class.

* For each set of preconditions you want to test, Create a nested class within the When-class with the name `Given[SomePrecondition]` inheriting the outer class.
* Override `Set` or call `Given` to assign values to the arguments of the method-to-test.
* Finally, create a test method, attributed with [Fact], for each logical assertion you want to make.
* The assertion should first call Then, which executes the test and returns the result.
* If there is only one test method in the Given-class, the call to `Given` can be placed in the chain before `Then`
* You can for instance test if a certain value was returned by writing `Result.Is([SomeValue])`.
 
Example:
```
namespace MyProject.Test.Calculator;

public abstract class WhenAddTwoNumbers : StaticSpec<int>
{
    protected int X, Y;
    public WhenAddTwoNumbers() => When(() => MyProject.Calculator.Add(X, Y));

    public class Given_1_And_1 : WhenAdd // Using Set to arrange
    {
        protected override void Set() => (X, Y) = (1, 1);
        [Fact] public void ThenReturn_3() => Result.Is(3);
    }

    public class Given_3_And_4 : WhenAdd // Using Given to arrange
    {
        [Fact] public void ThenReturn_7() => Given(() => (X, Y) = (3, 4)).Result.Is(7);
    }
}
```

### Test a static void method
* When testing a static void method, there is no return value to verify in result and by convention the generic TResult parameter is set to object.
* However you can us `Throws` or `NotThrows` to verify exceptions thrown.
 
Example:
```
namespace MyProject.Test.Validator;

public abstract class WhenVerifyAreEqual : StaticSpec<object>
{
    protected int X, Y;
    protected WhenVerifyAreEqual() => When(() => MyProject.Validator.VerifyAreEqual(X, Y));

    public class Given_1_And_2 : WhenVerifyAreEqual
    {
        [Fact] public void ThenThrows_NotEqual() => Given(() => (X, Y) = (1, 2)).Then.Throws<NotEqual>();
    }

    public class Given_2_And_2 : WhenVerifyAreEqual
    {
        [Fact] public void ThenDoNotThrow() => Given(() => (X, Y) = (2, 2)).Then.NotThrows();
    }
}
```

### Test a class with dependencies
* To test an instance method `[MyClass].[MyMethod]`, inherit `XspecT.SubjectSpec<[MyClass], TResult>`.
* It is recommended practice to create a common baseclass for all tests of `[MyClass]`, named `[MyClass]Spec`.
* The subject under test (sut) will be created automatically with mocks and default values by AutoMock. 
You can supply you own constructor arguments by calling `Using` (which will be applied in the same order when test pipeline is executed).
* For each method to test, create an abstract class named `When[MyMethod]` inheriting `[MyClass]Spec` in the same way as for static methods.

* To mock behaviour of any dependency, either override `Setup` or provide the mocking by calling `Given`. 
Each call to `Given` will provide additional arrangement that will be applied on test execution on the inversed order.
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
        => When(() => SUT.PlaceOrder(Cart)).Given(() => The<ICartRepository>().ReturnsDefault(Cart));

    public class GivenCart : WhenPlaceOrder
    {
        [Fact] public void ThenOrderIsCreated() 
            => Given(() => Cart = new()).Then.Does<IOrderService>(_ => _.CreateOrder(Cart));
    }
}
```

### Test async methods

All the examples above also works for async methods, with small modifications.

More examples can be found as Unit tests in the source code.