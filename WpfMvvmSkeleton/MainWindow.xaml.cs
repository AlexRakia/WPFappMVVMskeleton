using System.Windows;
using WpfMvvmSkeleton.ViewModels;
using WpfMvvmSkeleton.Services;

namespace WpfMvvmSkeleton.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // The ONE job of the code-behind:
            // create the ViewModel, inject its dependencies, assign DataContext.
            // Everything else happens through bindings.
            DataContext = new MainViewModel(new SampleService());
        }

        // ── Code-behind should stay THIS empty. ──────────────────────────
        // The only acceptable additions here are things that are
        // genuinely visual-only (e.g. drag-and-drop gestures, focus tricks,
        // animation triggers).  Any data or state belongs in the ViewModel.
    }
}
