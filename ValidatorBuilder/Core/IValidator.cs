namespace ValidatorBuilder.Core;

/// <summary>
/// Represents a validator.
/// </summary>
public interface IValidator
{
    /// <summary>
    /// Occurs when the rule group changes.
    /// </summary>
    event EventHandler<RuleGroupChangedEventArgs>? RuleGroupChangedEvent;

    /// <summary>
    /// Returns a collection of rules registered to a given key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>A <see cref="RuleGroup"/> object.</returns>
    RuleGroup RulesFor(string? key);

    /// <summary>
    /// Validates a string value through the predicate associated with the key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="valueToValidade">The value to be evaluated by the predicate.</param>
    void ValidateFor(string? key, string? valueToValidade);
}