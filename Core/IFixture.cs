using XspecT.Internal.Pipelines;

namespace XspecT;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSUT"></typeparam>
public interface IFixture<TSUT> 
{
    /// <summary>
    /// 
    /// </summary>
    string Specification { get; }
    internal Fixture<TSUT> Fixture { get; }
}