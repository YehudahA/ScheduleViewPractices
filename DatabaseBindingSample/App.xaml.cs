using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace DatabaseBindingSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new ViewModels.MainViewModel();
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            this.MainWindow = mainWindow;
            this.MainWindow.Show();
        }
    }
}
