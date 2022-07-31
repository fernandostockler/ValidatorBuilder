namespace ValidatorBuilder;

/// <summary>
/// 
/// </summary>
public class ValidatorBuilder : IRuleStage, IRulesForStage
{
    private string? currentKey;
    private readonly ObservableCollection<RuleGroup> _ruleGroup = new();

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
    public Validator Build() => new Validator(_ruleGroup);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="description"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IRuleStage Rule(string? description, Predicate<string?>? filter)
    {
        RuleGroup? ruleGroup = _ruleGroup.FirstOrDefault(x => x.Key == currentKey);
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
        RuleGroup? ruleGroup = _ruleGroup.FirstOrDefault(r => r.Key == key);
        _ruleGroup.Add(new(key, header));
        currentKey = key;

        return this;
    }
}