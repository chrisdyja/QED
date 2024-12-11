using Avalonia.Controls;

namespace QedFrontend.Views;

public partial class MainWindow : Window
{
    public MainWindow() // for avalonia
    {
        InitializeComponent();
    }
    public MainWindow(MainView view)
    {
        InitializeComponent();
        Content = view;
    }
}
