namespace ValidatorBuilder;

/// <summary>
/// 
/// </summary>
public interface IRulesForStage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    IRuleStage RulesFor(string? key, string? message);
}
