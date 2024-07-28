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
    public static string ParseReturnsExpression(this string expr)
    {
        if (string.IsNullOrEmpty(expr))
            return expr;
        if (TryParseMentionExpression(expr, out string description))
            return description;
        if (TryParseAssignmentExpression(expr, out description))
            return description;
        if (TryParseLambdaExpression(expr, out description))
            return description;
        return expr;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="callerExpr"></param>
    /// <returns></returns>
    public static string ParseActual(this string callerExpr)
    {
        if (string.IsNullOrEmpty(callerExpr))
            return callerExpr;
        var propNames = callerExpr.Split('.').Reverse().TakeWhile(prop => prop != "Then()").ToArray();
        var andSegment = propNames.SkipWhile(prop => !prop.StartsWith("And(")).FirstOrDefault();
        if (andSegment is not null)
            propNames = propNames.TakeWhile(prop => prop != andSegment).Append(andSegment[4..^1]).ToArray();
        return string.Join('.', propNames.Reverse());
    }

    private static bool TryParseMentionExpression(string expr, out string description)
    {
        description = null;
        var match = MentionRegex().Match(expr);
        if (!match.Success)
            return false;

        var verb = match.Groups[1].Value;
        var type = match.Groups[2].Value;
        description = $"{verb.AsWords()} {type}";
        var constraint = match.Groups[3].Value;
        if (constraint.Length > 2)
            description += $" {{ {ParseReturnsExpression(constraint[1..^1])} }}";
        return true;
    }

    private static bool TryParseAssignmentExpression(string expr, out string description)
    {
        description = null;
        var match = AssignmentRegex().Match(expr);
        if (!match.Success)
            return false;

        description = $"{match.Groups[3].Value} = {ParseReturnsExpression(match.Groups[4].Value)}";
        return true;
    }

    private static bool TryParseLambdaExpression(string expr, out string description)
    {
        description = null;
        var match = LambdaRegex().Match(expr);
        if (!match.Success)
            return false;

        description = ParseReturnsExpression(match.Groups[2].Value);
        return true;
    }

    [GeneratedRegex(@"^(\w+)<(\w+)>(.*)$")]
    private static partial Regex MentionRegex();

    [GeneratedRegex(@"^(\w+)\s*=>\s*(\w+)\.(\w+)\s*=\s*(.+)$")]
    private static partial Regex AssignmentRegex();

    [GeneratedRegex(@"^(\(\)\s*=>\s*)(.+)$")]
    private static partial Regex LambdaRegex();
}