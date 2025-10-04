﻿using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public record HasEnumerable<TItem> : EnumerableConstraint<TItem, HasEnumerableContinuation<TItem>>
{
    /// <summary>
    /// Assert that the enumerable contains exactly one item
    /// </summary>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the one item</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, TItem> OneItem()
    {
        TItem? theItem = default;
        return Assert("",
            NotNullAnd(actual => theItem = Xunit.Assert.Single(actual)), auxVerb: "have")
            .AndThat(theItem!);
    }

    /// <summary>
    /// Assert that the enumerable contains exactly two items
    /// </summary>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the two items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second)> TwoItems()
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        return Assert("",
            NotNullAnd(actual =>
            {
                var items = actual.ToArray();
                Xunit.Assert.Equal(2, items.Length);
                firstItem = items[0];
                secondItem = items[1];
            }), auxVerb: "have")
            .AndThat((firstItem!, secondItem!));
    }

    /// <summary>
    /// Assert that the enumerable contains exactly three items
    /// </summary>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the three items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second, TItem third)> ThreeItems()
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        TItem? thirdItem = default;
        return Assert("",
            NotNullAnd(actual =>
            {
                var items = actual.ToArray();
                Xunit.Assert.Equal(3, items.Length);
                firstItem = items[0];
                secondItem = items[1];
                thirdItem = items[2];
            }), auxVerb: "have")
            .AndThat((firstItem!, secondItem!, thirdItem!));
    }

    /// <summary>
    /// Assert that the enumerable contains exactly four items
    /// </summary>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the four items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second, TItem third, TItem fourth)> FourItems()
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        TItem? thirdItem = default;
        TItem? fourthItem = default;
        return Assert("",
            NotNullAnd(actual =>
            {
                var items = actual.ToArray();
                Xunit.Assert.Equal(4, items.Length);
                firstItem = items[0];
                secondItem = items[1];
                thirdItem = items[2];
                fourthItem = items[3];
            }), auxVerb: "have")
            .AndThat((firstItem!, secondItem!, thirdItem!, fourthItem!));
    }

    /// <summary>
    /// Assert that the enumerable contains exactly five items
    /// </summary>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the five items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second, TItem third, TItem fourth, TItem fifth)> FiveItems()
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        TItem? thirdItem = default;
        TItem? fourthItem = default;
        TItem? fifthItem = default;
        return Assert("",
            NotNullAnd(actual =>
            {
                var items = actual.ToArray();
                Xunit.Assert.Equal(5, items.Length);
                firstItem = items[0];
                secondItem = items[1];
                thirdItem = items[2];
                fourthItem = items[3];
                fifthItem = items[4];
            }), auxVerb: "have")
            .AndThat((firstItem!, secondItem!, thirdItem!, fourthItem!, fifthItem!));
    }

    /// <summary>
    /// Assert that the enumerable contains a single item satisfying the given condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the single item</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, TItem> OneItem(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
    {
        TItem? theItem = default;
        return Assert(
                "satisfying the condition",
                NotNullAnd(actual => theItem = Xunit.Assert.Single(actual, new Predicate<TItem>(condition))),
                expectedExpr!,
                "have")
            .AndThat(theItem!);
    }

    /// <summary>
    /// Assert that the enumerable contains exactly two items satisfying the given condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the two items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second)> TwoItems(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        return Assert(
                "satisfying the condition",
                NotNullAnd(actual =>
                {
                    var matchingItems = actual.Where(condition).ToArray();
                    Xunit.Assert.Equal(2, matchingItems.Length);
                    firstItem = matchingItems[0];
                    secondItem = matchingItems[1];
                }),
                expectedExpr!,
                "have")
            .AndThat((firstItem!, secondItem!));
    }

    /// <summary>
    /// Assert that the enumerable contains exactly three items satisfying the given condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the three items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second, TItem third)> ThreeItems(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        TItem? thirdItem = default;
        return Assert(
                "satisfying the condition",
                NotNullAnd(actual =>
                {
                    var matchingItems = actual.Where(condition).ToArray();
                    Xunit.Assert.Equal(3, matchingItems.Length);
                    firstItem = matchingItems[0];
                    secondItem = matchingItems[1];
                    thirdItem = matchingItems[2];
                }),
                expectedExpr!,
                "have")
            .AndThat((firstItem!, secondItem!, thirdItem!));
    }

    /// <summary>
    /// Assert that the enumerable contains exactly four items satisfying the given condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the four items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second, TItem third, TItem fourth)> FourItems(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        TItem? thirdItem = default;
        TItem? fourthItem = default;
        return Assert(
                "satisfying the condition",
                NotNullAnd(actual =>
                {
                    var matchingItems = actual.Where(condition).ToArray();
                    Xunit.Assert.Equal(4, matchingItems.Length);
                    firstItem = matchingItems[0];
                    secondItem = matchingItems[1];
                    thirdItem = matchingItems[2];
                    fourthItem = matchingItems[3];
                }),
                expectedExpr!,
                "have")
            .AndThat((firstItem!, secondItem!, thirdItem!, fourthItem!));
    }

    /// <summary>
    /// Assert that the enumerable contains exactly five items satisfying the given condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the five items</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, (TItem first, TItem second, TItem third, TItem fourth, TItem fifth)> FiveItems(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
    {
        TItem? firstItem = default;
        TItem? secondItem = default;
        TItem? thirdItem = default;
        TItem? fourthItem = default;
        TItem? fifthItem = default;
        return Assert(
                "satisfying the condition",
                NotNullAnd(actual =>
                {
                    var matchingItems = actual.Where(condition).ToArray();
                    Xunit.Assert.Equal(5, matchingItems.Length);
                    firstItem = matchingItems[0];
                    secondItem = matchingItems[1];
                    thirdItem = matchingItems[2];
                    fourthItem = matchingItems[3];
                    fifthItem = matchingItems[4];
                }),
                expectedExpr!,
                "have")
            .AndThat((firstItem!, secondItem!, thirdItem!, fourthItem!, fifthItem!));
    }

    /// <summary>
    /// Assert that the enumerable has the given count
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(expected, NotNullAnd(actual => Xunit.Assert.Equal(expected, actual.Count())), expectedExpr!, "have").And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given indexed condition.
    /// Pass if empty.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
        => Assert("elements satisfying the condition",
            NotNullAnd(actual => Xunit.Assert.DoesNotContain(actual.Select((it, i) => (it, i)), t => !condition(t.it, t.i))),
            expectedExpr!,
            "have")
        .And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given condition.
    /// Pass if empty.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
        => Assert(
            "elements satisfying the condition",
            NotNullAnd(actual => Xunit.Assert.DoesNotContain(actual, it => !condition(it))),
            expectedExpr!,
            "have").And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given index assertion.
    /// Pass if empty.
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem, int> assert, [CallerArgumentExpression(nameof(assert))] string? assertExpr = null)
        => Assert(
            "elements satisfying the assertion",
            NotNullAnd(actual => actual.Select((it, i) => (it, i)).ToList().ForEach(t => assert(t.it, t.i))),
            assertExpr!,
            "have").And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given assertion.
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem> assert, [CallerArgumentExpression(nameof(assert))] string? assertExpr = null)
        => Assert(
            "elements satisfying the assertion",
            NotNullAnd(actual => actual.ToList().ForEach(assert)), assertExpr!,
            "have").And();

    internal override HasEnumerableContinuation<TItem> Continue() => Create(Actual, ActualExpr);
}