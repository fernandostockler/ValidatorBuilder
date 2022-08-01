namespace ValidatorBuilder;

/// <summary>
/// Allows build an instance of <see cref="Validator"/> through fluent design.
/// </summary>
public class ValidatorBuilder : IRuleStage, IRulesForStage
{
    private string? currentKey;
    private readonly ObservableCollection<RuleGroup> RuleGroupCollection = new();

    private ValidatorBuilder() { }

    /// <summary>
    /// Creates a new instance of <see cref="ValidatorBuilder"/>.
    /// </summary>
    /// <returns><see cref="IRulesForStage"/></returns>
    public static IRulesForStage Create() => new ValidatorBuilder();

    /// <inheritdoc/>
    public Validator Build() => new(RuleGroupCollection);

    /// <inheritdoc/>
    public IRuleStage Rule(string? description, Predicate<string?>? filter)
    {
        RuleGroup? ruleGroup = RuleGroupCollection.FirstOrDefault(x => x.Key == currentKey);
        ruleGroup?.Rules?.Add(new Rule(description, filter));

        return this;
    }

    /// <inheritdoc/>
    public IRuleStage RulesFor(string? key, string? header)
    {
        RuleGroup? ruleGroup = RuleGroupCollection.FirstOrDefault(x => x.Key == key);
        RuleGroupCollection.Add(new(key, header));
        currentKey = key;

        return this;
    }
}