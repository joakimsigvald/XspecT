﻿using System.Text;
using System.Text.RegularExpressions;
using XspecT.Internal;

namespace XspecT;

/// <summary>
/// 
/// </summary>
public static partial class ExpressionParser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public static string ParseValue(this string expr)
    {
        expr = expr.ToSingleLine();
        if (string.IsNullOrEmpty(expr))
            return expr;
        if (TryParseMentionType(expr, out string description))
            return description;
        if (TryParseMentionValue(expr, out description))
            return description;
        if (TryParseAssignmentLambda(expr, out description))
            return description;
        if (TryParseIndexedAssignment(expr, out description))
            return description;
        if (TryParseZeroArgLambda(expr, out description))
            return description;
        if (TryParseString(expr, out description))
            return description;
        if (TryParseMethodCall(expr, out description))
            return description;
        if (TryParseWith(expr, out description))
            return description;
        if (TryParseAssignment(expr, out description))
            return description;
        if (TryParseOneArgLambdaValueExpression(expr, out description))
            return description;
        if (TryParseTupleExpression(expr, out description))
            return description;
        if (TryParseArithmeticExpression(expr, out description))
            return description;
        return expr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr"></param>
    /// <param name="skipSubjectRef"></param>
    /// <returns></returns>
    public static string ParseCall(this string expr, bool skipSubjectRef = false)
    {
        expr = expr.ToSingleLine();
        if (string.IsNullOrEmpty(expr))
            return expr;
        if (TryParseOneArgLambdaInstanceMethodExpression(expr, skipSubjectRef, out string description))
            return description;
        if (TryParseMethodCall(expr, out description))
            return description;
        if (TryParseOneArgLambdaValueExpression(expr, out description))
            return description;
        return expr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public static string ParseActual(this string expr)
    {
        expr = expr.ToSingleLine();
        if (string.IsNullOrEmpty(expr))
            return expr;
        string prefix = null;
        var propNames = expr.Split('.').Reverse().ToList();
        var thenValueRegex = ThenValueRegex();
        var thenValueSegment = propNames.SkipWhile(prop => !thenValueRegex.IsMatch(prop)).FirstOrDefault();
        if (thenValueSegment is not null) 
        {
            propNames = propNames.TakeWhile(prop => prop != thenValueSegment).ToList();
            var valueMatch = thenValueRegex.Match(thenValueSegment);
            if (valueMatch.Groups.Count == 2 && !string.IsNullOrEmpty(valueMatch.Groups[1].Value))
                prefix = valueMatch.Groups[1].Value.ParseValue();
        }
        propNames.Reverse();
        if (prefix is null)
            return propNames.Count == 1 
                ? propNames.Single().ParseValue() 
                : string.Join('.', propNames);
        if (propNames.Count == 0)
            return prefix;
        return IsOneWord(prefix) 
            ? $"{prefix}.{string.Join('.', propNames)}" 
            : $"{prefix}'s {string.Join('.', propNames)}"; ;
    }

    private static bool TryParseMentionType(string expr, out string description)
    {
        description = null;
        var match = MentionTypeRegex().Match(expr);
        if (!match.Success)
            return false;

        var verb = match.Groups[1].Value;
        var type = match.Groups[2].Value;
        var continuation = match.Groups[3].Value;

        description = $"{verb.AsWords()} {type}";
        if (HasConstraint())
            description += $" {{ {ParseValue(continuation[1..^1])} }}";
        else if (HasDrilldown())
            description = $"{description}'s {continuation[1..]}";
        else if (!string.IsNullOrEmpty(continuation))
            return false;
        return true;

        bool HasConstraint() => continuation.StartsWith('(') && continuation.EndsWith(')');

        bool HasDrilldown() => continuation.StartsWith('.');
    }

    private static bool TryParseMentionValue(string expr, out string description)
    {
        description = null;
        var match = MentionValueRegex().Match(expr);
        if (!match.Success)
            return false;

        var verb = match.Groups[1].Value;
        var values = match.Groups[2].Value.Split(',').Select(v => v.Trim()).ToArray();
        description = $"{verb.AsWords()} {string.Join(", ", values.Select(v => v.ParseValue()))}";
        return true;
    }

    private static bool TryParseTupleExpression(string expr, out string description)
    {
        description = null;
        var match = TupleExpressionRegex().Match(expr);
        if (!match.Success)
            return false;

        var values = match.Groups[1].Value.Split(',').Select(v => v.Trim()).ToArray();
        description = $"({string.Join(", ", values.Select(v => v.ParseValue()))})";
        return true;
    }

    private static bool TryParseAssignmentLambda(string expr, out string description)
    {
        description = null;
        var match = AssignmentLambdaRegex().Match(expr);
        if (!match.Success)
            return false;
        var property = match.Groups[3].Value;
        var value = match.Groups[4].Value;

        description = $"{property} = {value.ParseValue()}";
        return true;
    }

    private static bool TryParseIndexedAssignment(string expr, out string description)
    {
        description = null;
        var match = IndexedAssignmentRegex().Match(expr);
        if (!match.Success)
            return false;

        var objArg = match.Groups[1].Value;
        var objRef = match.Groups[2].Value;
        if (objArg != objRef)
            return false;

        var propRef = match.Groups[3].Value;
        var valueExpr = match.Groups[4].Value;
        description = $"{propRef} = {ParseValue(valueExpr)}";
        return true;
    }

    private static bool TryParseZeroArgLambda(string expr, out string description)
    {
        description = null;
        var match = ZeroArgLambdaRegex().Match(expr);
        if (!match.Success)
            return false;

        description = ParseValue(match.Groups[2].Value);
        return true;
    }

    private static bool TryParseOneArgLambdaInstanceMethodExpression(
        string expr, bool skipSubjectRef, out string description)
    {
        description = null;
        var match = OneArgLambdaInstanceMethodRegex().Match(expr);
        if (!match.Success || match.Groups.Count != 4)
            return false;

        var objArg = match.Groups[1].Value;
        var objRef = match.Groups[2].Value;
        var call = match.Groups[3].Value;
        if (objArg == objRef || objArg == "_")
            description = skipSubjectRef
                ? ParseCall(call)
                : $"{objRef}.{ParseCall(call)}";
        else return false;
        return true;
    }

    private static bool TryParseOneArgLambdaValueExpression(string expr, out string description)
    {
        description = null;
        var match = OneArgLambdaValueRegex().Match(expr);
        if (!match.Success)
            return false;

        description = ParseValue(match.Groups[2].Value);
        return true;
    }

    private static bool TryParseMethodCall(string expr, out string description)
    {
        description = null;
        var match = MethodCallRegex().Match(expr);
        if (!match.Success)
            return false;

        var methodName = match.Groups[1].Value;
        var methodArgs = match.Groups[2].Value;
        var args = methodArgs.Split(',').Select(arg => ParseValue(arg.Trim()));
        description = $"{methodName}({string.Join(", ", args)})";
        return true;
    }

    private static bool TryParseWith(string expr, out string description)
    {
        description = null;
        var match = WithLambdaRegex().Match(expr);
        if (!match.Success)
            return false;

        var objArg = match.Groups[1].Value;
        var objRef = match.Groups[2].Value;
        if (objArg != objRef)
            return false;
        var transform = match.Groups[3].Value.Trim();
        var assignments = transform.Split(',').Select(s => s.Trim().ParseValue());
        description = string.Join(", ", assignments);
        return true;
    }

    private static bool TryParseAssignment(string expr, out string description)
    {
        description = null;
        var match = AssignmentRegex().Match(expr);
        if (!match.Success)
            return false;
        var property = match.Groups[1].Value;
        var value = match.Groups[2].Value;
        description = $"{property} = {value.ParseValue()}";
        return true;
    }

    private static bool TryParseString(string expr, out string description)
    {
        description = null;
        var match = StringRegex().Match(expr);
        if (!match.Success)
            return false;

        description = $"\"{match.Groups[1].Value}\"";
        return true;
    }

    private static bool TryParseArithmeticExpression(string expr, out string description)
    {
        description = null;
        var match = ArithmeticRegex().Match(expr);
        if (!match.Success)
            return false;

        var values = OperatorRegex().Split(expr);
        var opMatch = OperatorRegex().Matches(expr);
        var operators = opMatch
            .Select(c => c.Value.Trim())
            .ToArray();
        if (values.Length != operators.Length + 1)
            return false;
        description = values[0].ParseValue();
        for (int i = 0; i < operators.Length; i++)
            description += $" {operators[i]} {values[i+1].ParseValue()}";
        return true;
    }

    private static string ToSingleLine(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        var lines = LineBreakRegex()
            .Split(str)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim());
        StringBuilder sb = new();
        bool addSpace = false;
        foreach (var line in lines)
        {
            if (addSpace && !line.StartsWith('.'))
                sb.Append(' ');
            sb.Append(line);
            addSpace = !line.EndsWith('.');
        }
        return sb.ToString();
    }

    private static bool IsOneWord(string str) => ValueRegex().Match(str).Success;

    [GeneratedRegex(@"^(\w+)<([\w\[\]\(\),?\s<>]+)>(?:\(\))?(.*)$")]
    private static partial Regex MentionTypeRegex();

    [GeneratedRegex(@"^(\w+)\((.+)\)$")]
    private static partial Regex MentionValueRegex();

    [GeneratedRegex(@"^(\w+)\s*=>\s*(\w+)\.(\w+)\s*=\s*([\S\s]+)$")]
    private static partial Regex AssignmentLambdaRegex();

    [GeneratedRegex(@"^(\w+)\s*=+\s+(.+)$")]
    private static partial Regex AssignmentRegex();

    [GeneratedRegex(@"^\((\w+),\s*\w+\)\s*=>\s*(\w+)\.(\w+)\s*=+\s*(.+)$")]
    private static partial Regex IndexedAssignmentRegex();

    [GeneratedRegex(@"^(\(\)\s*=>\s*)(.+)$")]
    private static partial Regex ZeroArgLambdaRegex();

    [GeneratedRegex(@"^(\w+)\s*=>\s*(\w+)\.(.+)$")]
    private static partial Regex OneArgLambdaInstanceMethodRegex();

    [GeneratedRegex(@"^(\w+)\s*=>\s*(.+)$")]
    private static partial Regex OneArgLambdaValueRegex();

    [GeneratedRegex(@"^((?:new\s+)?\w+)\((.+)\)$")]
    private static partial Regex MethodCallRegex();

    [GeneratedRegex(@"^(\w+)\s*=>\s*(\w+)\s*with\s*\{(.+)\}$")]
    private static partial Regex WithLambdaRegex();

    [GeneratedRegex("^[$@]*\"(.+)\"$")]
    private static partial Regex StringRegex();

    [GeneratedRegex(@"^(?:Then|And)\((.*)\)$")]
    private static partial Regex ThenValueRegex();

    [GeneratedRegex(@"^\((.+)\)$")]
    private static partial Regex TupleExpressionRegex();

    [GeneratedRegex(@"(\r|\n)+")]
    private static partial Regex LineBreakRegex();

    [GeneratedRegex($@"^{_valueStr}$")]
    private static partial Regex ValueRegex();

    [GeneratedRegex($@"^({_valueStr})(?:\s+({_opStr})\s+({_valueStr}))+$")]
    private static partial Regex ArithmeticRegex();

    [GeneratedRegex($@"\s+{_opStr}\s+")]
    private static partial Regex OperatorRegex();

    private const string _opStr = @"[+\-*/&|]";
    private const string _valueStr = @"[\w()?!.<>]+";
}