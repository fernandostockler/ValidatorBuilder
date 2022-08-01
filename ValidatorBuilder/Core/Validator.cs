namespace ValidatorBuilder.Core;

/// <summary>
/// The Validator class aims to apply all rules associated with a given key.
/// </summary>
public sealed class Validator : IValidator
{
    /// <summary>
    /// Occurs when the rule group changes.
    /// </summary>
    public event EventHandler<RuleGroupChangedEventArgs>? RuleGroupChangedEvent;

    private void RaiseRuleGroupChangedEvent(RuleGroupChangedEventArgs rulesChangedEventArgs)
        => RuleGroupChangedEvent?.Invoke(this, rulesChangedEventArgs);

    private readonly ObservableCollection<RuleGroup> _ruleGroupCollection;

    /// <summary>
    /// Initializes a new instance of the <see cref="Validator"/> class.
    /// Used only by <see cref="ValidatorBuilder"/>.
    /// </summary>
    /// <param name="ruleGroupCollection"></param>
    internal Validator(ObservableCollection<RuleGroup> ruleGroupCollection) => _ruleGroupCollection = ruleGroupCollection;

    /// <summary>
    /// Represents a collection of validation rules related to a given key.
    /// </summary>
    /// <param name="key">A string that identifies a collection of validation rules.</param>
    /// <returns>A <see cref="RuleGroup"/> containing all validation rules associated with a given key.</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public RuleGroup GetRulesFor(string? key)
    {
        RuleGroup? ruleGroup = _ruleGroupCollection.FirstOrDefault(x => x.Key == key);

        if (ruleGroup is null)
            throw new KeyNotFoundException($"The informed key {key} is missing.");

        return ruleGroup;
    }

    /// <summary>
    /// Validates a string against all rules associated with a key.
    /// </summary>
    /// <param name="key">A string that identifies a collection of validation rules.</param>
    /// <param name="valueToValidade">A string to be validated.</param>
    /// <exception cref="RuleGroupNullException">If Rules in RuleGroup is <c>null</c>.</exception>
    public void ValidateFor(string? key, string? valueToValidade)
    {
        RuleGroup ruleGroup = GetRulesFor(key);

        if (ruleGroup.Rules is null)
            throw new RuleGroupNullException(key, $"The validator rules for '{key}' is null.");

        RaiseRuleGroupChangedEvent(new RuleGroupChangedEventArgs(key, ruleGroup));

        foreach (IRule rule in ruleGroup.Rules)
            _ = rule.Validate(valueToValidade);
    }
}