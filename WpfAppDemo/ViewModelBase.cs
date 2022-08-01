namespace WpfAppDemo;

public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    //
    // Resumo:
    //     Raises the ViewModelBase.PropertyChanged event.
    //
    // Parâmetros:
    //   e:
    //     An instance of System.ComponentModel.PropertyChangedEventArgs.
    protected virtual void NotifyPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

    //
    // Resumo:
    //     Raises the ViewModelBase.PropertyChanged event.
    //
    // Parâmetros:
    //   propertyName:
    //     (optional) The name of the property that changed.
    protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
    {
        NotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    //
    // Resumo:
    //     Raises the ViewModelBase.PropertyChanging event.
    //
    // Parâmetros:
    //   e:
    //     An instance of System.ComponentModel.PropertyChangingEventArgs.
    protected virtual void NotifyPropertyChanging(PropertyChangingEventArgs e)
    {
        PropertyChanging?.Invoke(this, e);
    }

    //
    // Resumo:
    //     Raises the ViewModelBase.PropertyChanging event.
    //
    // Parâmetros:
    //   propertyName:
    //     (optional) The name of the property that is changing.
    protected virtual void NotifyPropertyChanging([CallerMemberName] string? propertyName = "")
    {
        NotifyPropertyChanging(new PropertyChangingEventArgs(propertyName));
    }

    //
    // Resumo:
    //     Compares the property's stored value with the new entered value. If they are
    //     equal, false is returned, otherwise the stored value is updated with the new
    //     value, PropertyChanging, PropertyChanged events are raised, and true is returned.
    //
    // Parâmetros:
    //   storage:
    //     The currently stored value.
    //
    //   newValue:
    //     The updated value for this property.
    //
    //   propertyName:
    //     The property name (optional).
    //
    // Parâmetros de Tipo:
    //   T:
    //     The type of property that is changing.
    //
    // Devoluções:
    //     true if the property was changed, false otherwise.
    protected virtual bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string? propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, newValue))
            return false;

        NotifyPropertyChanging(propertyName);
        storage = newValue;
        NotifyPropertyChanged(propertyName);

        return true;
    }
}