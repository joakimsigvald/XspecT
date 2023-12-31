﻿using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class SpecActor<TResult>
{
    private Action _setUp;
    private Action _command;
    private Func<TResult> _function;
    private Action _tearDown;
    private Exception _error;
    private TResult _result;

    internal void When(Action command)
    {
        if (_command is not null || _function is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _command = command;
    }

    internal void When(Func<TResult> function)
    {
        if (_command is not null || _function is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _function = function;
    }

    internal void After(Action setUp)
    {
        if (_setUp is not null)
            throw new SetupFailed("Cannot call After twice in the same pipeline");
        _setUp = setUp;
    }

    internal void Before(Action tearDown)
    {
        if (_tearDown is not null)
            throw new SetupFailed("Cannot call Before twice in the same pipeline");
        _tearDown = tearDown;
    }

    internal TestResult<TResult> Execute(Context context)
    {
        if (_setUp is not null)
            _setUp();
        try
        {
            CatchError(_command ?? GetResult);
            return new(_result, _error, context, _command is null);
        }
        finally
        {
            if (_tearDown is not null)
                _tearDown();
        }
    }

    private void GetResult()
    {
        if (_function is null)
            throw new SetupFailed("When must be called before Then");
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            _result = _function();
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }
    }

    private void CatchError(Action act)
    {
        try
        {
            act();
        }
        catch (SetupFailed)
        {
            throw;
        }
        catch (Exception ex)
        {
            _error = ex;
        }
    }
}