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

    internal void Init(string prologue) => _textBuilder.Init(prologue);

    internal void Add(Action apply)
    {
        if (_recordingSuppressionCount > 0)
            return;
        _applications.Add(apply);
    }

    private string Build()
    {
        foreach (var apply in _applications) apply();
        return _textBuilder.ToString();
    }

    internal void AddMockSetup<TService>(string callExpr)
        => AddPhraseOrSentence($"{Given} {GetMockName<TService>('.')}{callExpr.ParseCall(true)}");

    internal void AddMockReturnsDefault<TService>(string returnsExpr)
        => AddPhraseOrSentence($"{Given} {GetMockName<TService>(' ')}returns {returnsExpr.ParseValue()}");

    internal void AddMockReturns(string returnsExpr)
        => AddWord($"returns {returnsExpr?.ParseValue()}".Trim());

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

    internal void AddGivenCount<TModel>(string count)
    {
        _currentMockSetup = null;
        var articleStr = string.IsNullOrEmpty(count) ? string.Empty : $"{count.AsWords()} ";
        AddPhraseOrSentence($"{Given} {articleStr}{NameOf<TModel>()}");
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
    {
        //if (_description is not null)
        //    return;
        _textBuilder.AddIndentedLine(phrase, indentation);
    }

    private void AddSentence(string phrase)
    {
        //if (_description is not null)
        //    return;
        _textBuilder.AddCapitalizedLine(phrase);
    }

    private void AddWord(string word, string binder = " ")
    {
        //if (_description is not null)
        //    return;
        if (string.IsNullOrEmpty(word))
            return;
        _textBuilder.AddText($"{binder}{word}");
    }

    internal void SuppressRecording() => _recordingSuppressionCount++;

    internal void InciteRecording() => _recordingSuppressionCount--;

    private string Given => 0 == _givenCount++ ? "Given" : "and";

    private string Then => 0 == _thenCount++ ? "Then" : "and";
}