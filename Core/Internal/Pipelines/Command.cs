namespace XspecT.Internal.Pipelines;

internal record Command(Delegate Invocation, string Expression);