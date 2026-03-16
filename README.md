User click -> Command -> ViewModel -> Model

Usage Example:

public ICommand LoadDataCommand { get; }
public void LoadData()
{
    Users = repository.GetUsers();
}

Bound in XAML:

<Button Command="{Binding LoadDataCommand}" />


# Main components of the MVVM :

* Bindings

* Commands

* INotifyPropertyChanged

(NOTE: Without writing UI code in the code-behind.)

TODO 1:
WPF MVVM application that retrieves time-series data from InfluxDB and visualizes system metrics.

TODO2:
TODO the same for MAUI
