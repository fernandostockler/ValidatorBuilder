namespace ValidatorBuilder;

/// <summary>
/// 
/// </summary>
public class ValidatorBuilder : IRuleStage, IRulesForStage
{
    private string? currentKey;
    private readonly ObservableCollection<RuleGroup> RuleGroupCollection = new();

    private ValidatorBuilder() { }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IRulesForStage Create() => new ValidatorBuilder();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Validator Build() => new(RuleGroupCollection);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="description"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IRuleStage Rule(string? description, Predicate<string?>? filter)
    {
        RuleGroup? ruleGroup = RuleGroupCollection.FirstOrDefault(x => x.Key == currentKey);
        ruleGroup?.Rules?.Add(new Rule(description, filter));

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="header"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IRuleStage RulesFor(string? key, string? header)
    {
        RuleGroup? ruleGroup = RuleGroupCollection.FirstOrDefault(r => r.Key == key);
        RuleGroupCollection.Add(new(key, header));
        currentKey = key;

        return this;
    }
}