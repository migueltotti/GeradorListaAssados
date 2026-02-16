using GeradorListaAssados.Desktop.Navegation;
using GeradorListaAssados.Desktop.ViewModels;
using GeradorListaAssados.Desktop.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Navigation;

namespace GeradorListaAssados.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = _viewModel;

        SetDateLabel();
    }

    private void SetDateLabel()
    {
        DateTime now = DateTime.Now;
        lblDate.Content = $"{now:dd/MM/yyyy}";
    }

    private void BtnOpenPathPicker_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new CommonOpenFileDialog
        {
            Title = "Selecione um diretório",
            IsFolderPicker = true,
            AllowNonFileSystemItems = false,
            EnsurePathExists = true
        };

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            _viewModel.ExcelDownloadPath = dialog.FileName;

            lblSelectedPath.Content = _viewModel.ExcelDownloadPath;
        }
    }

    private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NavegationService.OpenWindow<AddProductWindow>();
    }

    private void btnGenerateFile_Click(object sender, RoutedEventArgs e)
    {
         _viewModel.GenerateExcelFile()
            .GetAwaiter()
            .GetResult();
    }
}