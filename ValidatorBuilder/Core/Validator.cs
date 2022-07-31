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

    private readonly ObservableCollection<RuleGroup> rulesCollection;

    /// <summary>
    /// Initializes a new instance of the <see cref="Validator"/> class.
    /// Used only by <see cref="ValidatorBuilder"/>.
    /// </summary>
    /// <param name="rules"></param>
    internal Validator(ObservableCollection<RuleGroup> rules) => rulesCollection = rules;

    /// <summary>
    /// Rules for a given key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public RuleGroup RulesFor(string? key)
    {
        RuleGroup? rules = rulesCollection.FirstOrDefault(x => x.Key == key);

        if (rules is null)
            throw new KeyNotFoundException($"The informed key {key} is missing.");

        return rules;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="valueToValidade"></param>
    /// <exception cref="RuleGroupNullException"></exception>
    public void ValidateFor(string? key, string? valueToValidade)
    {
        RuleGroup rules = RulesFor(key);

        if (rules.Rules is null)
            throw new RuleGroupNullException(key: key, message: $"The validator rules for '{key}' is null.");

        RaiseRuleGroupChangedEvent(new RuleGroupChangedEventArgs(key, rules));

        foreach (IRule rule in rules.Rules)
            _ = rule.Validate(valueToValidade);
    }
}