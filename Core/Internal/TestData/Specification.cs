﻿using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace XspecT.Internal.TestData;

/// <summary>
/// 
/// </summary>
public static partial class Specification
{
    [ThreadStatic]
    private static StringBuilder _specificationBuilder;
    [ThreadStatic]
    private static Stack<string> _words;

    internal static void Clear()
    {
        _words = null;
        _specificationBuilder = null;
    }

    /// <summary>
    /// 
    /// </summary>
    public static string Description => Builder.ToString().TrimStart().Capitalize();

    internal static void AddMockSetup<TService, TActualReturns>(Expression<Func<TService, TActualReturns>> expression)
    {
        var sb = new StringBuilder();
        sb.Append($"given {typeof(TService).Name}.{expression.GetMethodName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddPhrase(sb.ToString());
    }

    internal static void AddMockReturns(string returnsExpr)
    {
        var sb = new StringBuilder();
        sb.Append("returns ");
        sb.Append(returnsExpr.ParseReturnsExpression());
        AddWord(sb.ToString());
    }

    internal static void AddWhen<TSUT, TResult>(Expression<Func<TSUT, TResult>> expression)
    {
        var sb = new StringBuilder();
        sb.Append($"when {expression.GetMethodName()}");
        AddMethodArguments(sb, expression.Body as MethodCallExpression);
        AddPhrase(sb.ToString());
    }

    internal static void AddAssert(Action assert, string actual = null, [CallerMemberName] string verb = "")
    {
        AddActual(actual);
        AddWord(verb.AsWords());
        PopWords();
        try
        {
            assert();
        }
        catch (XunitException ex)
        {
            throw new XunitException(Description, ex);
        }
    }

    internal static void AddMethodArguments(StringBuilder sb, MethodCallExpression body)
    {
        if (body is null)
            return;
        sb.Append('(');
        foreach (var argument in body.Arguments)
            sb.Append(DescribeArgument(argument));
        sb.Append(')');
    }

    internal static void AddPhrase(string phrase)
    {
        if (HasText)
            Builder.Append($",{Environment.NewLine}");
        AddWord(phrase);
    }

    internal static void AddWord(string word)
    {
        Builder.Append(' ');
        Builder.Append(word);
    }

    internal static void PushStop() => PushWord(null);

    internal static void PushWord(string fragment)
        => (_words ??= new()).Push(fragment);

    internal static void PopWords()
    {
        while (_words?.Count > 0)
        {
            var word = _words.Pop();
            if (word is null)
                return;
            AddWord(word);
        }
    }

    private static void AddActual(string callerExpr)
    {
        if (callerExpr is null)
            return;
        var propNames = callerExpr.Split('.').Reverse().TakeWhile(prop => prop != "Then()");
        var actual = string.Join('.', propNames.Reverse());
        AddWord(actual);
    }

    private static string DescribeArgument(Expression expr)
        => expr switch
        {
            MethodCallExpression mce => $"{mce.Method.Name.AsWords()} {mce.Method.ReturnType.Alias()}",
            UnaryExpression ue => DescribeArgument(ue.Operand),
            MemberExpression => "TODO",
            ParameterExpression => "TODO",
            ConstantExpression => "TODO",
            LambdaExpression le => DescribeLambdaExpression(le),
            _ => throw new SetupFailed($"Unknown argument expression: {expr.NodeType}")
        };

    //private static string DescribeArgument(string returnsExpr)
    //{
    //    if (returnsExpr.StartsWith("() => "))
    //        return DescribeArgument(returnsExpr[6..]);
    //    if (returnsExpr.IndexOf('<') > 0 && returnsExpr.IndexOf('<') < returnsExpr.IndexOf('>'))
    //    {
    //        var parts = returnsExpr.Split('<', 2, StringSplitOptions.RemoveEmptyEntries);
    //        var verb = parts[0];
    //        parts = parts[1].Split('>', 2, StringSplitOptions.RemoveEmptyEntries);
    //        var type = parts[0];
    //        if (parts.Length == 2)
    //        {
    //            var constraint = parts[1];
    //            if (constraint.Length > 2)
    //                return $"{verb.AsWords()} {type} {{ {DescribeConstraint(constraint)} }}";
    //        }
    //        return $"{verb.AsWords()} {type}";
    //    }
    //    return returnsExpr;
    //}

    //private static string DescribeConstraint(string constraint)
    //{
    //    Regex arrow = ArrowRegex();
    //    var parts = ArrowRegex().Split(constraint, 2);
    //    return constraint;
    //}

    private static string DescribeLambdaExpression(LambdaExpression expr)
    {
        var body = expr.Body as MethodCallExpression;
        var methodName = DescribeArgument(body);
        if (body.Arguments.Count() == 0)
            return methodName;
        return $"{methodName} {{ {DescribeCriteria(body.Arguments[0] as UnaryExpression)} }}";
    }

    private static string DescribeCriteria(UnaryExpression criteria)
    {
        var operand = criteria.Operand as LambdaExpression;
        var body = operand.Body as BinaryExpression;
        var left = body.Left as MemberExpression;
        var prop = left.Member as PropertyInfo;
        return $"{prop.Name} = {DescribeArgument(body.Right)}";
    }

    private static bool HasText => _specificationBuilder is not null;
    private static StringBuilder Builder => _specificationBuilder ??= new();

    [GeneratedRegex("=>")]
    private static partial Regex ArrowRegex();
}