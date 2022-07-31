namespace ValidatorBuilder;


/// <summary>
///
/// </summary>
public interface IRuleStage
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="message"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IRuleStage Rule(string? message, Predicate<string?>? predicate);

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    IRuleStage RulesFor(string? key, string? message);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Validator Build();
}
