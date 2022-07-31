namespace ValidatorBuilder.Core;

/// <summary>
/// A collection of rules to be applied to a given value, identified by a unique key.
/// </summary>
public sealed class RuleGroup : Mvvm.ObservableBase
{
    private string? _key;
    private string? _header;
    private ObservableCollection<IRule> _rules = new();

    /// <summary>
    /// Gets or sets a unique key that will be used to get or set the rules to apply to the given value.
    /// </summary>
    public string? Key { get => _key; set => SetProperty(ref _key, value); }

    /// <summary>
    /// Gets or sets the message that will be displayed in the UI, describing the group of rules to be applied.
    /// </summary>
    public string? Header { get => _header; set => SetProperty(ref _header, value); }

    /// <summary>
    /// Gets or sets the list of rules.
    /// </summary>
    public ObservableCollection<IRule> Rules { get => _rules; set => SetProperty(ref _rules, value); }

    /// <summary>
    /// Creates a new instance of the <see cref="RuleGroup"/> type that represents a collection of rules
    /// to be applied to a given value, identified by a unique key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="header"></param>
    public RuleGroup(string? key, string? header)
    {
        Key = key;
        Header = header;
    }

    /// <summary>
    /// Returns <c>True</c> if all rules were successfully evaluated, otherwise, returns <c>False</c>.
    /// </summary>
    /// <returns>A boolean value.</returns>
    /// <exception cref="RuleGroupNullException"></exception>
    public bool AreValid()
    {
        if (Rules is null)
            throw new RuleGroupNullException(
                key: Key,
                message: "Rules is null in AreValid() method.");

        return !Rules.Any(r => r.IsValid == false);
    }
}
