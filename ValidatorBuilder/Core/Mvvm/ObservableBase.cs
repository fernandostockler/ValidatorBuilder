namespace ValidatorBuilder.Core.Mvvm;

/// <summary>
/// A base class for objects of which the properties must be observable.
/// </summary>
public abstract class ObservableBase : INotifyPropertyChanged
{
    /// <inheritdoc  cref="INotifyPropertyChanged.PropertyChanged"/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="e"></param>
    protected virtual void NotifyPropertyChanged(PropertyChangedEventArgs e)
        => PropertyChanged?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
        => NotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Compares the current and new values for a given property. If the value has changed,
    /// updates the property with the new value, then raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <typeparam name="T">The type of the property that changed.</typeparam>
    /// <param name="storage">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change occurred.</param>
    /// <param name="propertyName">(optional) The name of the property that changed.</param>
    /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
    protected virtual bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string? propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, newValue))
            return false;

        storage = newValue;
        NotifyPropertyChanged(propertyName);

        return true;
    }
}