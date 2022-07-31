namespace ValidatorBuilder.Core;

/// <summary>
/// Represents a change in the RuleGroup.
/// </summary>
public class RuleGroupChangedEventArgs : EventArgs
{
    /// <summary>
    /// A Key that identifies a RuleGroup.
    /// </summary>
    public string? Key { get; init; }

    /// <summary>
    /// <inheritdoc cref="RuleGroup"/>
    /// </summary>
    public RuleGroup? RuleGroup { get; init; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ruleGroup"></param>
    public RuleGroupChangedEventArgs(string? key, RuleGroup? ruleGroup)
    {
        Key = key;
        RuleGroup = ruleGroup;
    }
}