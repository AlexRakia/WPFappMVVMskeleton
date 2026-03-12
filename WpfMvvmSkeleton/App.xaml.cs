using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfMvvmSkeleton
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // App.xaml.cs is the right place for:
        //   • Global exception handling
        //   • Dependency-injection container setup
        //   • Startup / shutdown logic
        //
        // For a larger app replace the simple "new SampleService()" in
        // MainWindow.xaml.cs with a DI container configured here:
        //
        //   protected override void OnStartup(StartupEventArgs e)
        //   {
        //       var services = new ServiceCollection();
        //       services.AddSingleton<ISampleService, SampleService>();
        //       services.AddTransient<MainViewModel>();
        //       var provider = services.BuildServiceProvider();
        //
        //       var window = new MainWindow();
        //       window.DataContext = provider.GetRequiredService<MainViewModel>();
        //       window.Show();
        //   }
    }
}
