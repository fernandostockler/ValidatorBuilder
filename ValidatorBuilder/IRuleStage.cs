namespace ValidatorBuilder;

/// <summary>
/// Participates in building a validator using fluent design.
/// </summary>
public interface IRuleStage
{
    /// <summary>
    /// Adds a validation rule <see cref="Rule"/> to the rule collection <see cref="RuleGroup"/>
    /// under the current key determined by the <see cref="ValidatorBuilder"/>.
    /// </summary>
    /// <param name="description">A message that will be displayed in the UI with the description of the rule.</param>
    /// <param name="filter"><inheritdoc cref="Predicate{T}"></inheritdoc>/></param>
    /// <returns><see cref="IRuleStage"/></returns>
    IRuleStage Rule(string? description, Predicate<string?>? filter);

    /// <summary>
    /// Creates a collection of validation rules <see cref="RuleGroup"/> related to a given key.
    /// </summary>
    /// <param name="key">A string that identifies a collection of validation rules.</param>
    /// <param name="header">A string that describes what is being validated. Example: 'Password requirements:'.</param>
    /// <returns><see cref="IRuleStage"/></returns>
    IRuleStage RulesFor(string? key, string? header);

    /// <summary>
    /// Constructs an instance of type <see cref="Validator"/>.
    /// </summary>
    /// <returns><see cref="Validator"/></returns>
    Validator Build();
}