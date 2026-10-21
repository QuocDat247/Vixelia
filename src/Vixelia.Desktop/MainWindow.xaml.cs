using System.Windows;
using Vixelia.Desktop.ViewModels;

namespace Vixelia.Desktop;

public partial class MainWindow : Window
{
    public MainWindow(ShellViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}