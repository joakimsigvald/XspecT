using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using XspecT.Internal.TestData;

namespace XspecT.Internal;

internal class SpecificationBuilder
{
    private string _description;
    private readonly List<Action> _applications = [];
    private int _givenCount;
    private int _recordingSuppressionCount;
    private int _thenCount;
    private string _currentMockSetup;
    private readonly TextBuilder _textBuilder = new();

    internal string Specification => _description ??= Build();

    internal void Add(Action apply)
    {
        if (_recordingSuppressionCount > 0)
            return;
        _applications.Add(apply);
    }

    internal string Build()
    {
        foreach (var apply in _applications) apply();
        return _textBuilder.ToString();
    }

    internal void AddMockSetup<TService>(string callExpr)
        => AddPhraseOrSentence($"{Given} {GetMockName<TService>('.')}{callExpr.ParseCall(true)}");

    internal void AddMockReturnsDefault<TService>(string returnsExpr)
        => AddPhraseOrSentence($"{Given} {GetMockName<TService>(' ')}returns {returnsExpr.ParseValue()}");

    internal void AddMockReturns(string returnsExpr)
        => AddWord($"returns {returnsExpr.ParseValue()}");

    internal void AddMockThrowsDefault<TService, TException>()
        => AddWord($"{Given} {GetMockName<TService>(' ')}throws {NameOf<TException>()}");

    internal void AddMockThrowsDefault<TService>(string expectedExpr)
        => AddWord($"{Given} {GetMockName<TService>(' ')}throws {expectedExpr.ParseValue()}");

    internal void AddMockThrows<TException>()
        => AddWord($"throws {NameOf<TException>()}");

    internal void AddMockThrows(string expectedExpr)
        => AddWord($"throws {expectedExpr.ParseValue()}");

    internal void AddWhen(string actExpr)
        => AddSentence($"when {actExpr.ParseCall()}");

    internal void AddAfter(string setUpExpr)
        => AddSentence($"after {setUpExpr.ParseCall()}");

    internal void AddBefore(string tearDownExpr)
        => AddSentence($"before {tearDownExpr.ParseCall()}");

    internal void AddAssert(string actual, string verb, string expected)
    {
        AddWord(actual.ParseActual());
        AddWord(verb.AsWords());
        AddWord(expected.ParseValue());
    }

    internal void AddThen() => AddPhraseOrSentence(Then);

    internal void AddGiven(string valueExpr, ApplyTo applyTo)
    {
        _currentMockSetup = null;
        AddPhraseOrSentence(string.Join(' ', GetWords()));

        IEnumerable<string> GetWords()
        {
            yield return Given;
            if (applyTo == ApplyTo.Using)
                yield return "using";
            yield return valueExpr.ParseValue();
            if (applyTo == ApplyTo.Default)
                yield return "as default";
        }
    }

    internal void AddGiven<TModel>(string setupExpr, string article)
    {
        _currentMockSetup = null;
        var articleStr = string.IsNullOrEmpty(article) ? string.Empty : $"{article.AsWords()} ";
        AddPhraseOrSentence($"{Given} {articleStr}{NameOf<TModel>()} {{ {setupExpr.ParseValue()} }}");
    }

    internal void AddVerify<TService>(string expressionExpr)
        => AddWord($"{NameOf<TService>()}.{expressionExpr.ParseCall(true)}");

    internal void AddAssertThrows<TError>(string binder)
        => AddWord($"throws {NameOf<TError>()} {binder}".Trim());

    internal void AddAssertThrows(string expectedExpr)
        => AddWord($"throws {expectedExpr.ParseValue()}");

    internal void AddTap(string expr) => AddWord($"tap({expr})");

    internal void AddAssert(string assertName) => AddWord(assertName.AsWords());

    internal void AddAssertConjunction(string conjunction) => AddPhrase(conjunction, 2);

    private string GetMockName<TService>(char binder)
    {
        var nextMockSetup = NameOf<TService>();
        var mockName = nextMockSetup == _currentMockSetup
            ? ""
            : $"{nextMockSetup}{binder}";
        _currentMockSetup = nextMockSetup;
        return mockName;
    }

    private void AddPhraseOrSentence(string phrase)
    {
        if (char.IsUpper(phrase[0]))
            AddSentence(phrase);
        else AddPhrase(phrase);
    }

    private static string NameOf<T>() => typeof(T).Alias();

    private void AddPhrase(string phrase, int indentation = 1)
        => _textBuilder.AddIndentedLine(phrase, indentation);

    private void AddSentence(string phrase)
        => _textBuilder.AddCapitalizedLine(phrase);

    private void AddWord(string word, string binder = " ")
    {
        if (string.IsNullOrEmpty(word))
            return;
        _textBuilder.AddText($"{binder}{word}");
    }

    internal void SuppressRecording() => _recordingSuppressionCount++;

    internal void InciteRecording() => _recordingSuppressionCount--;

    private string Given => 0 == _givenCount++ ? "Given" : "and";

    private string Then => 0 == _thenCount++ ? "Then" : "and";
}

internal class TextBuilder(int maxLineLength = 80, int indentationSize = 2)
{
    private readonly StringBuilder _sb = new();
    private int _currentLineLength;

    internal void AddCapitalizedLine(string line) => AddIndentedLine(line.Capitalize(), 0);

    internal void AddIndentedLine(string line, int indentation)
    {
        _sb.Append(Environment.NewLine);
        _sb.Append(new string(' ', _currentLineLength = indentation * indentationSize));
        AddText(line);
    }

    internal void AddText(string text)
    {
        var (first, rest) = IsExceedingMaxLineLength(text) ? BreakLine(text) : (text, null);
        _sb.Append(first);
        _currentLineLength += first.Length;
        if (rest is not null)
            AddIndentedLine(rest, 3);
    }

    private bool IsExceedingMaxLineLength(string text)
        => text.Length + _currentLineLength > maxLineLength;

    private (string first, string rest) BreakLine(string text)
    {
        var fitInLine = text[..(maxLineLength - _currentLineLength)];
        var first = new string(fitInLine
            .Reverse()
            .SkipWhile(c => c != '.' && !char.IsWhiteSpace(c))
            .Reverse()
            .ToArray())
            .TrimEnd();
        if (string.IsNullOrEmpty(first))
            first = fitInLine;
        var rest = text[first.Length..].Trim();
        return (first, rest);
    }

    public override string ToString() => _sb.ToString().Trim().Capitalize();
}