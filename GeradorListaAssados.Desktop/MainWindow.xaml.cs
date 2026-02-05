using GeradorListaAssados.Desktop.ViewModels;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

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

    //private async Task AddProduct(Product product)
    //{
    //    var result = await _productService.AddAsync(product, CancellationToken.None);

    //    if (result.IsFailure)
    //    {
    //        MessageBox.Show(
    //            result.Error.Description, 
    //            "Erro ao adicionar produto.", 
    //            MessageBoxButton.OK);
    //    }

    //    await LoadProducts();
    //}


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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Abrir janela de adicionar produto
    }

    private void btnGenerateFile_Click(object sender, RoutedEventArgs e)
    {
         _viewModel.GenerateExcelFile()
            .GetAwaiter()
            .GetResult();
    }
}