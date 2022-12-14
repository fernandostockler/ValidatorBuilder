namespace ValidatorBuilder.Core;

/// <summary>
/// The Rule class represents a rule that will be applied to a given value.
/// </summary>
public class Rule : Mvvm.ObservableBase, IRule
{
    bool? _isValid;
    string? _description;
    Predicate<string?>? _filter;

    /// <summary>
    /// Creates a new instance of type <see cref="Rule"/>.
    /// </summary>
    /// <param name="description">A message that will be displayed in the UI with the description of the rule.</param>
    /// <param name="filter"><inheritdoc cref="Predicate{T}"></inheritdoc>/></param>
    public Rule(string? description, Predicate<string?>? filter)
    {
        Description = description;
        Filter = filter;
    }

    /// <summary>
    /// <inheritdoc cref="IRule.Description"/>
    /// </summary>
    public string? Description { get => _description; set => SetProperty(ref _description, value); }

    /// <summary>
    /// <inheritdoc cref="IRule.Filter"/>
    /// </summary>
    public Predicate<string?>? Filter { get => _filter; set => SetProperty(ref _filter, value); }

    /// <summary>
    /// <inheritdoc cref="IRule.IsValid"/>
    /// </summary>
    public bool? IsValid { get => _isValid; set => SetProperty(ref _isValid, value); }

    /// <summary>
    /// <inheritdoc cref="IRule.Validate(string?)"/>
    /// </summary>
    /// <param name="s"></param>
    /// <returns><see langword="true"/> if it's a valid value, otherwise, returns <see langword="false"/></returns>
    public bool? Validate(string? s)
    {
        IsValid = Filter?.Invoke(s);
        return IsValid;
    }
}
