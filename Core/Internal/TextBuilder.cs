using System.Text;

namespace XspecT.Internal;

/// <summary>
/// Only made public for unit testing
/// </summary>
/// <param name="maxLineLength"></param>
/// <param name="indentationSize"></param>
public class TextBuilder(int maxLineLength = 80, int indentationSize = 2)
{
    private const int _maxIndentation = 3;
    private readonly StringBuilder _sb = new();
    private int _currentLineLength;

    internal void Init(string prologue) 
    {
        if (prologue is null)
            return;
        _sb.Append(prologue);
    }

    /// <summary>
    /// Only made public for unit testing
    /// </summary>
    public StringBuilder AddCapitalizedLine(string line) => AddIndentedLine(line.Capitalize(), 0);

    /// <summary>
    /// Only made public for unit testing
    /// </summary>
    public StringBuilder AddIndentedLine(string line, int indentation)
    {
        _sb.Append(Environment.NewLine);
        _sb.Append(new string(' ', _currentLineLength = indentation * indentationSize));
        AddText(line);
        return _sb;
    }

    /// <summary>
    /// Only made public for unit testing
    /// </summary>
    /// <param name="text"></param>
    public StringBuilder AddText(string text)
    {
        if (text is null)
            return _sb;
        var (first, rest) = IsExceedingMaxLineLength(text) ? BreakLine(text) : (text, null);
        _sb.Append(first);
        _currentLineLength += first.Length;
        return rest is null ? _sb : AddIndentedLine(rest, _maxIndentation);
    }

    private bool IsExceedingMaxLineLength(string text)
        => text.Length + _currentLineLength > maxLineLength;

    private (string first, string rest) BreakLine(string text)
    {
        var fitInLine = text[..(maxLineLength - _currentLineLength)];
        var nextChar = ' ';
        var first = new string(fitInLine
            .Reverse()
            .SkipWhile(c =>
            {
                var possibleLineBreak = IsLineBreakPossibleAfter(c, nextChar);
                nextChar = c;
                return !possibleLineBreak;
            })
            .Reverse()
            .ToArray())
            .TrimEnd();
        if (string.IsNullOrEmpty(first)) 
            first = fitInLine.Length < maxLineLength / 2 ? string.Empty : fitInLine;
        var rest = text[first.Length..].Trim();
        return (first, rest);
    }

    /// <summary>
    /// Only made public for unit testing
    /// </summary>
    /// <returns></returns>
    public override string ToString() => _sb.ToString().Trim().Capitalize();

    private static readonly char[] _lineBreakCues = ['.', '(', '[', '{'];

    private static bool IsLineBreakPossibleAfter(char c, char next)
        => char.IsWhiteSpace(c) 
        || (_lineBreakCues.Contains(c) && !IsFalseLineBreak(c, next));

    private static bool IsFalseLineBreak(char c, char next)
        => c == '.' && (next == '.' || char.IsDigit(next));
}