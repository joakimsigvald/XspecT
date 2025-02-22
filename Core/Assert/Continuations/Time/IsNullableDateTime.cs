﻿using System.Runtime.CompilerServices;
using XspecT.Assert.Continuations;

namespace XspecT.Assert.Continuations.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable DateTime
/// </summary>
public record IsNullableDateTime : IsNullableComparableStruct<DateTime, IsNullableDateTime, IsDateTime>
{
    /// <summary>
    /// Asserts that the nullable dateTime is before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Before(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x < 0, expectedExpr, "occur");

    /// <summary>
    /// Asserts that the nullable dateTime is after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> After(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x > 0, expectedExpr, "occur");

    /// <summary>
    /// Asserts that the nullable dateTime is not before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotBefore(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x >= 0, expectedExpr, "occur");

    /// <summary>
    /// Asserts that the nullable dateTime is not after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotAfter(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x <= 0, expectedExpr, "occur");
}