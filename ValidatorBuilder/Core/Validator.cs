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
    /// Rules for a given key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns> 
    /// <exception cref="KeyNotFoundException"></exception>
    public RuleGroup RulesFor(string? key)
    {
        RuleGroup? ruleGroup = _ruleGroupCollection.FirstOrDefault(x => x.Key == key);

        if (ruleGroup is null)
            throw new KeyNotFoundException($"The informed key {key} is missing.");

        return ruleGroup;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="valueToValidade"></param>
    /// <exception cref="RuleGroupNullException"></exception>
    public void ValidateFor(string? key, string? valueToValidade)
    {
        RuleGroup ruleGroup = RulesFor(key);

        if (ruleGroup.Rules is null)
            throw new RuleGroupNullException(key, $"The validator rules for '{key}' is null.");

        RaiseRuleGroupChangedEvent(new RuleGroupChangedEventArgs(key, ruleGroup));

        foreach (IRule rule in ruleGroup.Rules)
            _ = rule.Validate(valueToValidade);
    }
}