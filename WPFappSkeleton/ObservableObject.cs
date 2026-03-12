using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfMvvmSkeleton.Core
{
    // Base class for all ViewModels.
    // Implements INotifyPropertyChanged so the UI updates automatically
    // whenever a property value changes.
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
