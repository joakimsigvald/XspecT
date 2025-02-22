﻿namespace XspecT.Assert.Continuations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TContinuation"></typeparam>
/// <typeparam name="TThat"></typeparam>
/// <param name="continuation"></param>
/// <param name="that"></param>
public class ContinueWithThat<TContinuation, TThat>(TContinuation continuation, TThat that)
    : ContinueWith<TContinuation>(continuation) where TContinuation : Constraint
{
    /// <summary>
    /// 
    /// </summary>
    public TThat That => that;
}