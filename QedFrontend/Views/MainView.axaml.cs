using Avalonia.Controls;
using QedFrontend.ViewModels;

namespace QedFrontend.Views;

public partial class MainView : UserControl
{
    public MainView() // for avalonia
    {
        InitializeComponent();
    }
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
