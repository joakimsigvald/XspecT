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
        if (string.IsNullOrEmpty(expr))
            return expr;
        if (TryParseMentionTypeExpression(expr, out string description))
            return description;
        if (TryParseMentionValueExpression(expr, out description))
            return description;
        if (TryParseAssignmentExpression(expr, out description))
            return description;
        if (TryParseIndexedAssignmentExpression(expr, out description))
            return description;
        if (TryParseZeroArgLambdaExpression(expr, out description))
            return description;
        if (TryParseStringExpression(expr, out description))
            return description;
        if (TryParseMethodCallExpression(expr, out description))
            return description;
        return expr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public static string ParseCall(this string expr)
    {
        if (string.IsNullOrEmpty(expr))
            return expr;
        if (TryParseOneArgLambdaExpression(expr, out string description))
            return description;
        if (TryParseMethodCallExpression(expr, out description))
            return description;
        return expr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static string ParseActual(this string actualExpr)
    {
        if (string.IsNullOrEmpty(actualExpr))
            return actualExpr;
        var propNames = actualExpr.Split('.').Reverse().TakeWhile(prop => !IsThenExpr(prop)).ToArray();
        var andSegment = propNames.SkipWhile(prop => !prop.StartsWith("And(")).FirstOrDefault();
        if (andSegment is not null)
            propNames = propNames.TakeWhile(prop => prop != andSegment).Append(andSegment[4..^1]).ToArray();
        return string.Join('.', propNames.Reverse());
    }

    private static bool IsThenExpr(string expr)
        => ThenRegex().Match(expr).Success;

    private static bool TryParseMentionTypeExpression(string expr, out string description)
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
        if (HasDrilldown())
            description = $"{continuation[1..]} of {description}";
        return true;

        bool HasConstraint() => continuation.StartsWith('(') && continuation.EndsWith(')');

        bool HasDrilldown() => continuation.StartsWith('.');
    }

    private static bool TryParseMentionValueExpression(string expr, out string description)
    {
        description = null;
        var match = MentionValueRegex().Match(expr);
        if (!match.Success)
            return false;

        var verb = match.Groups[1].Value;
        var value = match.Groups[2].Value;
        description = $"{verb.AsWords()} {value}";
        return true;
    }

    private static bool TryParseAssignmentExpression(string expr, out string description)
    {
        description = null;
        var match = AssignmentRegex().Match(expr);
        if (!match.Success)
            return false;

        description = $"{match.Groups[3].Value} = {ParseValue(match.Groups[4].Value)}";
        return true;
    }

    private static bool TryParseIndexedAssignmentExpression(string expr, out string description)
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

    private static bool TryParseZeroArgLambdaExpression(string expr, out string description)
    {
        description = null;
        var match = ZeroArgLambdaRegex().Match(expr);
        if (!match.Success)
            return false;

        description = ParseValue(match.Groups[2].Value);
        return true;
    }

    private static bool TryParseOneArgLambdaExpression(string expr, out string description)
    {
        description = null;
        var match = OneArgLambdaRegex().Match(expr);
        if (!match.Success)
            return false;

        var objArg = match.Groups[1].Value;
        var objRef = match.Groups[2].Value;
        if (objArg != objRef)
            return false;

        description = ParseCall(match.Groups[3].Value);
        return true;
    }

    private static bool TryParseMethodCallExpression(string expr, out string description)
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

    private static bool TryParseStringExpression(string expr, out string description)
    {
        description = null;
        var match = StringRegex().Match(expr);
        if (!match.Success)
            return false;

        description = match.Groups[1].Value;
        return true;
    }

    [GeneratedRegex(@"^(\w+)<([\w\[\]\(\),\s<>]+)>(?:\(\))?(.*)$")]
    private static partial Regex MentionTypeRegex();

    [GeneratedRegex(@"^(\w+)\((.+)\)$")]
    private static partial Regex MentionValueRegex();

    [GeneratedRegex(@"^(\w+)\s*=>\s*(\w+)\.(\w+)\s*=\s*(.+)$")]
    private static partial Regex AssignmentRegex();

    [GeneratedRegex(@"^\((\w+),\s*\w+\)\s*=>\s*(\w+)\.(\w+)\s*=\s*(.+)$")]
    private static partial Regex IndexedAssignmentRegex();

    [GeneratedRegex(@"^(\(\)\s*=>\s*)(.+)$")]
    private static partial Regex ZeroArgLambdaRegex();

    [GeneratedRegex(@"^(\w+)\s*=>\s*(\w+)\.(.+)$")]
    private static partial Regex OneArgLambdaRegex();

    [GeneratedRegex(@"^((?:new\s+)?\w+)\((.+)\)$")]
    private static partial Regex MethodCallRegex();

    [GeneratedRegex("^[$@]*\"(.+)\"")]
    private static partial Regex StringRegex();

    [GeneratedRegex(@"^Then\(.*\)$")]
    private static partial Regex ThenRegex();
}