# XspecT — Fluent, specification-style unit testing for .NET

XspecT is a fluent, specification-oriented testing framework for .NET that sits on top of xUnit.
It follows the Given–When–Then pattern and integrates seamlessly with Moq, AutoMock, and AutoFixture.
Tests run on the standard xUnit runner and can live side-by-side with existing xUnit tests.

Whether you are new to unit testing or an experienced practitioner, XspecT helps you express test intent clearly by removing boilerplate, enforcing structure, and generating readable failure descriptions.

Example: testing the `PlaceOrder` method on `ShoppingService`:
```
public class WhenPlaceOrder : Spec<ShoppingService>
{
    static Tag<Guid> cartId = new(); //auto-generated Guid

    public WhenPlaceOrder()
        => When(_ => _.PlaceOrder(The(cartId)))
           .Given<ICartRepository>()
           .That(_ => _.GetCart(The(cartId)))
           .Returns(A<Cart>());

    [Fact] public void ThenCreatesOrder()
        => Then<IOrderService>(_ => _.CreateOrder(The<Cart>()));
}
```

The example above highlights how XspecT reduces boilerplate by handling test data, dependency mocking, and interaction verification declaratively.
In real-world usage, this typically leads to substantially smaller tests compared to xUnit + Moq, while maintaining coverage and improving readability.

## Introduction

This documentation assumes familiarity with xUnit and Moq, or similar testing and mocking frameworks.

The following is a complete XspecT test class (a *specification*) containing a single test method (a *requirement*):

```
using XspecT;
using XspecT.Assert;
using static App.Calculator;

namespace App.Test;

public class CalculatorSpec : Spec<int>
{
    [Fact] public void WhenAdd_1_and_2_ThenSumIs_3() => When(_ => Add(1, 2)).Then().Result.Is(3);
}
```

To write a test with XspecT, such as the one above, you start by subclassing `Spec`.
Each test is expressed as a specification and executed as a pipeline with three phases:
*arrange*, *act*, and *assert*.

We will begin with the first stage: arranging the test pipeline.

### Arrange

The *arrange* stage defines the setup of the test pipeline.
In XspecT, this is done by calling methods on `Spec`, either directly or fluently chained together.

The following methods are used to arrange a test:

* `Given`  — defines test setup and input data
* `After`  — setup that runs *before* the action
* `Before` — teardown or verification that runs *after* the action

The names `After` and `Before` reflect when they are executed relative to the `When` stage:
`When` is executed **after** `After` and **before** `Before`.

This means that setup and teardown behavior follows a fixed semantic order,
independent of how the corresponding methods are arranged in the test code.

XspecT also provides mechanisms for referring to test data in a stable way,
so the same values can be consistently reused across arrangement,
execution, and assertion.

### Act

The *act* stage specifies the behavior under test by calling `When` with a lambda expression. 
The lambda takes the subject under test as argument and should call the method under test.

The subject under test is automatically created based on the arrangement,
unless it is static or explicitly provided.

As with arrangement, the order in which `Given`, `After`, `Before`, and `When`
are written does not matter; they may be chained in any order.

Each specification defines exactly one action under test and therefore contains a single `When` stage.

### Assert

XspecT includes a fluent assertion library, `XspecT.Assert`, conceptually similar to FluentAssertions,
but with a more compact syntax based on the verbs `Is`, `Has`, and `Does`.

The *assert* stage is specified by calling `Then` or `Result`, followed by one or more assertions.
It is only when one of these methods is called that the test pipeline is executed and the result evaluated.

Because execution is deferred until the assert stage, XspecT can execute the test pipeline
in its proper order, regardless of how the individual steps were written. In most cases, this means that
you do not have to worry about the order in which arrangement and action steps are specified.

If a test fails, this is either due to an invalid test setup or because an assertion was not satisfied.
In the latter case, XspecT provides detailed assertion failures together with an automatically
generated description of the specification, making it easier to understand the intended behavior.

Example:
```
Test:
=> When(_ => _.List())
   .Given<IMyRepository>()
   .That(_ => _.List()).Returns(A<MyModel[]>)
   .Given().Three<MyModel>()
   .Then().Result.Has().Count(4)

---

Output:
Expected Result to have count 4 but found 3...
---- 
Given three MyModel
  and IMyRepository.List() returns a MyModel[]
When _.List()
Then Result has count 4
```

In addition to verifying return values, exceptions can also be asserted using `Then().Throws`.

## Test a static method with [Theory]

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
}
```

## Test a class instance with dependencies

To test an instance method `[MyClass].[MyMethod]`, create an abstract class named `When[MyMethod]` inheriting `XspecT.Spec<[MyClass], [ReturnType]>`.
The subject under test will be created automatically with mocks and default values by AutoMock.
Subject-under-test is available as the single input parameter to the lambda that is provided to the method `When`.
You can supply or modify you own constructor arguments by calling `Given` or `Given().Using`.
You can even provide the instance to test by using any of those two methods.

To mock behavior of any dependency call `Given<[TheService]>().That(_ => _.[TheMethod](...)).Returns/Throws(...)`. 

To verify a call to a mocked dependency, call `Then<[TheService]>([SomeLambdaExpression])`. 

Both mocking and verification of behavior is based on `Moq` framework.
 
Example:
```
namespace MyProject.Spec.ShoppingService;

public class WhenPlaceOrder : Spec<MyProject.ShoppingService>
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

## Recommended conventions

For more complex and realistic scenarios, it is recommended to create tests in a separate project from the production code, named `[MyProject].Spec` or `[MyProject].Test`. 
The test-project should mimic the production project's folder structure, but in addition have one folder for each class to test, named as the class. 
Within that folder, create one test-class per method to test, named `When[Something]`. 
Within the when-class, which should be abstract, create a nested public subclass for each condition, called `Given[Something]`, in which one test method is defined for each logical assert. 

Example:
```
namespace MyProject.Test.Validator;

public abstract class WhenVerifyAreEqual : Spec
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

Note that when no return value is asserted, we can use the non-generic base class `Spec`.

`Throws` and `DoesNotThrow` can be used to verify exceptions.

## Sync vs. Async

Weather your method-under-test or mocked methods are sync or async, the tests are specified in the exact same way. 
The XspecT framework will call async methods synchronously, so that the test does not have to await any calls, but can always be treated as if they are testing synchronous methods.
However in some cases you have to use async and await keywords in the lambdas you provide to the test-pipeline to deal with async scenarios.

## Test data helpers

XspecT provides a set of helpers for referring to test data that can either be supplied explicitly
or automatically generated (optionally with constraints).

Up to five distinct values of a given type can be referenced within a test,
as well as collections of up to five elements.

For one single value, the helpers include:

`A`, `An`, `The`, `AFirst`, `TheFirst`

For additional single values, the helpers include:

`ASecond`, `TheSecond`, `AThird`, `TheThird`, `AFourth`, `TheFourth`,`AFifth`, `TheFifth`,

For collections, the helpers include:

`Zero`, `One`, `Two`, `Three`, `Four`, `Five`, `Some` (at least one), `Many` (at least two), `AnyNumberOf`

For auto-generating values that cannot be referenced again, the helpers include:

`Any` and `Another`

To ensure that different references of a specific type (such as `AFirst` and `ASecond` really are distinct), the helper `Unique` may be used. 

## Class fixtures

XspecT now support the Xunit feature of sharing setup between tests in a common class fixture.
A class fixture is created in the same way as a test class, by inheriting `Spec` and providing setup.
The only difference between a class fixture implemented with XspecT and a test class implemented with XspecT
is that class fixtures don't have test methods or assertions.

When using a class fixture and having more that one test method, no setup should be put in the constructor, 
since the constructor is run once for each test method and provide the setup to the shared class fixture 
(i.e would add the same setup multiple times and second time after the test pipeline was executed, which is not allowed).

## Tags
Tags can be used as a complement to AFirst, ASecond, etc. to refer to values in a test setup in a more expressive way.
They are particularly useful when having several values of the same type.
A tag is a simple object of the generic type Tag<TValue>. Each tag instance represents a unique value.
Start by creating one tag for each 'tagged' value you want to use in the test setup.

Example:
```
protected static Tag<string> name = new();
protected static Tag<int> age = new(), shoeSize= new();
```

### Set and reference tagged values
Then you can use the tags to set or reference values in a test-pipeline.

Example:
```
Given(surname).Is("Anton").And(lastname).Is("Jonson")
.When(_ => _.CreateUser(The(surname), The(lastname)))
.Then().Result.FullName.Is("Anton Jonson");
```

### Use tagged values as default or for auto-generation
You can also specify that the value associated with a tag should be the default value of that type, or used when auto-generating subject-under-test.

Example:
```
Given().Default(name).and.Using(age);
```

## Assertions
A deep dive in the fluent assertion framework.

### Fluent assertions
Assertions are made directly on the value to be verified. 
Several assertions can be chained together using property-binders between assertions (lowercase).
Every assertion returns a continuation (unless it fails and throws a XunitException).
The continuation is context-aware and allows different assertions depending on what was asserted previously.

#### And, Either, Or
Examples:
`3.Is().GreaterThan(2).and.LessThan(4);` //both must pass
`3.Is().either.GreaterThan(4).or.LessThan(4);` //at least one must pass

#### Not
Any assertion can be negated by placing not before (note lowercase not)
```
3.Is().not.GreaterThan(4);
```

### Values
Values of any type can be verified with any of the two extension methods `Is` and `Has`

#### Is
- Equal:  
  `Result.Is(3)`  
  `Result.Is().EqualTo(3)`  
- Equivalent: (for objects)  
  `Result.Is().Like(new MyObject {Id = 3})`  
  `Result.Is().EquivalentTo(new MyObject {Id = 3})`  
- Not equal:  
  `Result.Is().Not(3)`  
- Null:  
  `Result.Is().Null()`  
- Greater than:  
  `3.Is().GreaterThan(2)`  
- Less than:  
  `3.Is().LessThan(2)`  
- Aproximally equal with tolerance:  
  `Result.Is().Around(3, 0.1)`  
- Even: (true if number is divisable by 2)  
  `Result.Is().Even()`  
- OneOf:  
  `Result.Is().OneOf(Three<int>())`  
- True: (for booleans)  
  `Result.Is().True()`  
- False: (for booleans)  
  `Result.Is().False()`  

#### Has
- Verify that the result has a given condition:  
  `Result.Has(_ => _.Id == 3)`  
- Verify that the result has the given type:  
  `Result.Has().Type<MyModel>()`  

### Strings
#### Is
- Like  
  `" ABC ".Is().Like("abc")`  
- EquivalentTo  
  `" ABC ".Is().EquivalentTo("abc")`  
- Empty  
  `"".Is().Empty()`  
- NullOrEmpty  
  `((string)null).Is().NullOrEmpty()`  
- NullOrWhitespace  
  `" ".Is().NullOrWhitespace()`  

#### Does
- Contain  
  `"ABC".Does().Contain("AB")`  
- StartWith  
  `"ABC".Does().StartWith("AB")`  
- EndWith  
  `"ABC".Does().EndWith("BC")`  

### Time
- Before  
  `DateTime.Now.Is().Before(DateTime.Now.AddDays(1))`  
- After  
  `DateTime.Now.Is().After(DateTime.Now.AddDays(-1))`  
- CloseTo  
  `DateTime.Now.Is().CloseTo(DateTime.Now.AddDays(1), TimeSpan.FromDays(2))`  
  `TimeSpan.FromDays(4).Is().CloseTo(TimeSpan.FromDays(3), TimeSpan.FromDays(2))`  
- Positive  
  `TimeSpan.FromDays(1).Is().Positive()`  
- Negative  
  `TimeSpan.FromDays(1).Is().Negative()`  

### Collections
#### Is
- EqualTo  
  all elements are equal and in the same order  
  `list.Is().EqualTo(otherList)`  
- Like  
  all elements are equal but order may differ  
  `list.Is().Like(otherList)`  
- SameAs  
  reference equal  
  `list.Is().SameAs(otherList)`  
- EquivalentTo  
  all elements are equal but order may differ  
  `list.Is().EquivalentTo(otherList)`  
- Empty  
  `list.Is().Empty()`  
- Distinct  
  `list.Is().Distinct()` // all elements in the collection are different  
  `list.Is().Distinct(it => it.Id)` // all elements have different values of the given property  

#### Does
- Contain  
  `list.Does().Contain(3)`  

#### Has
- Count  
  `list.Has().Count(3)`  
  `list.Has().Count(it => it > 3).At(2)` // with condition  
- Count at least  
  `list.Has().Count().AtLeast(2)`  
  `list.Has().Count(it => it > 3).AtLeast(2)` // with condition  
- Count at most  
  `list.Has().Count().AtMost(2)`  
  `list.Has().Count(it => it > 3).AtMost(2)` // with condition  
- Count in range  
  `list.Has().Count().InRange(2, 4)`  
  `list.Has().Count(it => it > 3).InRange(2, 4)` // with condition  
- Order ascending  
  `list.Has().Order().Ascending()`  
  `list.Has().Order(it => it.Age).Ascending()` // with condition  
- Order descending  
  `list.Has().Order().Descending()`  
  `list.Has().Order(it => it.Age).Descending()` // with condition  
- [One/Two/Three/Four/Five]Items  
  verify that the collection has the given number of items and return them as a n-tuple  
  `number.Has().OneItem().that.Is(3)` // numbers have one item, and that item is 3  
  `patients.Has().OneItem().that.Age.Is(3)` // patients have one item, and its age is 3  
  `patients.Has().OneItem(it => it.Age == 3).that.Gender.Is('F')` // patients have one item aged 3, and its gender is female  
- All  
  `list.Has().All(it => it.Age > 3)` // all items in the collection match the criteria  
  `list.Has().All((it, i) => it.Age > i)` // with index of item  
  `list.Has().All(it => it.Age.Is().GreaterThan(3))` // apply assertion to all items  
- Some  
  `list.Has().Some(it => it.Age > 3)` - at least one item in the collection matches the criteria  
- None  
  `list.Has().None(it => it.Age > 3)` - no item in the collection matches the criteria  