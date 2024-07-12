using System.ComponentModel;

namespace Assimalign.OGraph;

public abstract partial class Entity<T> : INotifyPropertyChanging, INotifyPropertyChanged
{
    private event PropertyChangingEventHandler? propertyChanging;
    private event PropertyChangedEventHandler? propertyChanged;


    event PropertyChangingEventHandler? INotifyPropertyChanging.PropertyChanging
    {
        add => this.propertyChanging += value;
        remove => this.propertyChanging -= value;
    }
    event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
    {
        add => this.propertyChanged += value;
        remove => this.propertyChanged -= value;
    }
}
