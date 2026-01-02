# XspecT: A fluent unit testing framework

Framework for writing and running automated tests in .Net (10+) in a fluent style, 
based on the popular "Given-When-Then" pattern, built upon XUnit, Moq, AutoMock and AutoFixture.

Whether you are beginner or expert in unit-testing, this framework will help you to write more descriptive, concise and maintainable tests with less code.

## Introduction

It is assumed that you are already familiar with Xunit and Moq, or similar test- and mocking frameworks.
XspecT includes a fluent assertion framework called `XspecT.Assert`, which is similar to FluentAssertions, 
but with a less worthy syntax, based on the verbs `Is`, `Has` and `Does` instead of `Should`.

This is an example of a complete test class (*specification*) with one test method (*requirement*):
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

To write a test with the XspecT framework, such as the one above, you first need to subclass `Spec`.
A test execution contains three different phases: *arrange*, *act* and *assert*.

We will begin with the first stage:

### Arrange

There are a number of different methods in `Spec` that can be called to arrange the test pipeline.
These are:
* `Given` (for arrangement)
* `After` (for setup)
* `Before` (for teardown)

The motivation behind the naming of `After` and before `Before` is that `When` is executed before `Before` and after `After`.
These methods can be called directly on the base class, or chained on each other (most tests can be expressed as one-liners).

In addition there are a number of methods to refer to test-data that can either be provided explicitly or auto-generated (with or without constraints).
Up to 5 different values can be provided of any given type, as well as collections of up to five elements of any type.
The methods for referring to/creating test data are named `A`, `An`, `The`, `AFirst`, `TheFirst`, `ASecond`, `TheSecond` and so on for single values
and `Some`, `Many`, `Zero`, `One`, `Two`, `Three`, `Four` and `Five` for collections.
Calling any of these methods multiple times yield the same value every time within the same test.
`A`, `An`, `The`, `AFirst` and `TheFirst` are synonyms,yielding the first auto-generated value of the given type.
`Any` or `Another` give a new value that cannot be referenced again.

### Act

The *act* stage is specified by calling `When` with the lambda that will be executed. 
The lambda takes the subject-under-test as argument and should call the method-under-test.
The subject-under-test will be automatically generated based on the arrangement (unless static or explicitly provided).
It doesn't matter in which order `Given`, `Before`, `After` or `When` is called, and they may be chained in any order.

### Assert

Finally to specify the *assert* stage, call `Then` or `Result`, followed by any assertions you want to make. 
It is not until one of these two methods are called that the test-pipeline is executed and the test result provided.
This allows the XspecT framework to arrange the test-pipeline in the natural order, regardless of in what order those arrangements were supplied in the implementation of the test.
This means that in most cases you don't have to worry about the order in which the steps of the test is specified (as long as assert comes after arrange and act).
In more complex tests, different arrangements may depend on each other, which makes the order in which they are supplied significant, but it is recommended to keep unit tests as simple, targeted and readable as possible.

Should a test fail, this can be due to either invalid setup or that the test condition (assertion) is not satisfied.
In the first case a `SetupFailed` exception is thrown detailing the error in the setup (this could be for instance if `Given` is called after `Then`, or `When` is called multiple times)
In the second case, you are in the *red* zone of the red-green-refactor cycle and need to either fix the test or the implementation under test.
To help with this, the built in assertion framework supply not only the details of the error, but also a complete description of the test (the *specification*, which is auto-generated from the test implementation),
so that you can more easily se what behavior the test actually expects, than from reading the test implementation alone.

Apart from verifying the result (return value) of the method, you can also verify that it throws the expected exceotion with `Then().Throws`.

After this introduction, we should be ready to look at more examples.

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
Given().Default(name).And().Using(age);
```

## Assertions
A deep dive in the fluent assertion framework.

### Fluent assertions
Assertions are made directly on the value to be verified. 
Several assertions can be chained together: Every assertion returns a continuation (unless it fails and throws a XunitException).
The continuation is context-aware and allows different assertions depending on what was asserted previously.
```
3.Is().GreaterThan(2).And.LessThan(4);

3.Is().Either.GreaterThan(4).Or.LessThan(4);
```

#### Not
Any assertion can be negated by placing Not before
```
3.Is().Not().GreaterThan(4);
```

### Values
Values of any type can be verified with any of the two extension methods `Is` and `Has`

#### Is
* Equal: 
  `Result.Is(3)`
  `Result.Is().EqualTo(3)`
* Equivalent: (for objects)
  `Result.Is().Like(new MyObject {Id = 3})`
  `Result.Is().EquivalentTo(new MyObject {Id = 3})`
* Not equal: 
  `Result.Is().Not(3)`
* Null:
  `Result.Is().Null()`
* Greater than:
  `3.Is().GreaterThan(2)`
* Less than:
  `3.Is().LessThan(2)`
* Aproximally equal with tolerance: 
  `Result.Is().Around(3, 0.1)`
* Even: (true if number is divisable by 2)
  `Result.Is().Even()`
* OneOf:
  `Result.Is().OneOf(Three<int>())`
* True: (for booleans)
  `Result.Is().True()`
* False: (for booleans)
  `Result.Is().False()`

#### Has
* Verify that the result has a given condition: 
  `Result.Has(_ => _.Id == 3)`
* Verify that the result has the given type: 
  `Result.Has().Type<MyModel>()`

### Strings
#### Is
* Like
  `" ABC ".Is().Like("abc")`
* EquivalentTo
  `" ABC ".Is().EquivalentTo("abc")`
* Empty
  `"".Is().Empty()`
* NullOrEmpty
  `((string)null).Is().NullOrEmpty()`
* NullOrWhitespace
  `" ".Is().NullOrWhitespace()`

#### Does
* Contain
  `"ABC".Does().Contain("AB")`
* StartWith
  `"ABC".Does().StartWith("AB")`
* EndWith
  `"ABC".Does().EndWith("BC")`

### Time
* Before
  `DateTime.Now.Is().Before(DateTime.Now.AddDays(1))`
* After
  `DateTime.Now.Is().After(DateTime.Now.AddDays(-1))`
* CloseTo
  `DateTime.Now.Is().CloseTo(DateTime.Now.AddDays(1), TimeSpan.FromDays(2))`
  `TimeSpan.FromDays(4).Is().CloseTo(TimeSpan.FromDays(3), TimeSpan.FromDays(2))`
* Positive
  `TimeSpan.FromDays(1).Is().Positive()`
* Negative
  `TimeSpan.FromDays(1).Is().Negative()`

### Collections
#### Is
* EqualTo 
  - all elements are equal and in the same order
  `list.Is().EqualTo(otherList)`
* Like
  - all elements are equal but order may differ
  `list.Is().Like(otherList)`
* SameAs
  - reference equal
  `list.Is().SameAs(otherList)`
* EquivalentTo
  - all elements are equal but order may differ
  `list.Is().EquivalentTo(otherList)`
* Empty
  `list.Is().Empty()`
* Distinct
  `list.Is().Distinct()` - all elements in the collection are different
#### Does
* Contain
  `list.Does().Contain(3)`

#### Has
* Count
  `list.Has().Count(3)`
  `list.Has().Count(it => it > 3).At(2)` - with condition
* Count at least
  `list.Has().Count().AtLeast(2)`
  `list.Has().Count(it => it > 3).AtLeast(2)` - with condition
* Count at most
  `list.Has().Count().AtMost(2)`
  `list.Has().Count(it => it > 3).AtMost(2)` - with condition
* Count in range
  `list.Has().Count().InRange(2, 4)`
  `list.Has().Count(it => it > 3).InRange(2, 4)` - with condition
* Order ascending
  `list.Has().Order().Ascending()`
  `list.Has().Count(it => it.Age).Ascending()` - with condition
* Order descending
  `list.Has().Order().Descending()`
  `list.Has().Count(it => it.Age).Descending()` - with condition
* [One/Two/Three/Four/Five]Items
  - verify that the collection has the given number of items and return them as a n-tuple
  `list.Has().OneItem().That.Age.Is(3)`
  `list.Has().OneItem(it => it.Age == 3).That.Gender.Is('F')` - with filter
* All
  `list.Has().All(it => it.Age > 3)` - all items in the collection match the criteria
  `list.Has().All((it, i) => it.Age > i)` - with index of item
  `list.Has().All(it => it.Age.Is().GreaterThan(3))` - apply assertion to all items
* Some
  `list.Has().Some(it => it.Age > 3)` - at least one item in the collection matches the criteria