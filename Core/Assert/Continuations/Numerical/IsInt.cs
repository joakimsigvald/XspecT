﻿namespace XspecT.Assert.Continuations.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided int
/// </summary>
public record IsInt : IsNumerical<int, IsInt> 
{
    /// <summary>
    /// Invert the following assertion
    /// </summary>
    public IsInt Not => Create(Actual, ActualExpr, true);
}