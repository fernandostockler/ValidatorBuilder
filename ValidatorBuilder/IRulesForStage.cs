namespace ValidatorBuilder;

/// <summary>
/// Participates in building a validator using fluent design.
/// </summary>
public interface IRulesForStage
{
    /// <summary>
    /// Creates a new validation rule group associated with a key.
    /// </summary>
    /// <param name="key">A string that identifies a collection of validation rules.</param>
    /// <param name="header">A string that describes what is being validated. Example: 'Password requirements:'.</param>
    /// <returns><see cref="IRuleStage"/></returns>
    IRuleStage RulesFor(string? key, string? header);
}