#nullable enable

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PL.Utilities
{
    public sealed class Prop<T>: INotifyPropertyChanged
    {
        private T _value = default!;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}