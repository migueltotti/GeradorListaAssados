using GeradorListaAssados.Desktop.Commands;
using GeradorListaAssados.Desktop.Navegation;
using GeradorListaAssados.Desktop.Windows;
using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace GeradorListaAssados.Desktop.ViewModels
{
    public class MainViewModel
    {
        private readonly IProductService _productService;
        private readonly UpdateProductViewModel _updateProductViewModel;
        public readonly INavegationService NavegationService;

        public ObservableCollection<Product> Products { get; set; }
        public string ExcelDownloadPath = "";

        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public MainViewModel(IProductService productService, UpdateProductViewModel updateProductViewModel, INavegationService navegationService)
        {
            _productService = productService;
            _updateProductViewModel = updateProductViewModel;
            NavegationService = navegationService;

            LoadProducts()
                .GetAwaiter()
                .GetResult();

            EditProductCommand = new RelayCommand<Product>(EditProduct);
            DeleteProductCommand = new RelayCommand<Product>(DeleteProduct);
        }

        private void EditProduct(Product product)
        {
            _updateProductViewModel.SetProduct(product);
            NavegationService.OpenWindow<UpdateProductWindow>();
        }

        private void DeleteProduct(Product product)
        {
            if(MessageBox.Show(
                "Deseja remover esse produto? Operação irreversivel!",
                "Remover Produto",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = _productService.DeleteAsync(product.Id, CancellationToken.None)
                    .GetAwaiter()
                    .GetResult();

                if (result.IsFailure)
                {
                    MessageBox.Show(
                        result.Error.Description,
                        "Erro ao remover produto.",
                        MessageBoxButton.OK);
                }
                else
                {
                    LoadProducts()
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }

        #region Methods

        public async Task LoadProducts()
        {
            var products = await _productService.GetAllAsync(CancellationToken.None);

            if (Products is not null)
            {
                Products.Clear();
            }
            else
            {
                Products = [];
            }

            products.ToList().ForEach(product => Products.Add(product));
        }

        public async Task GenerateExcelFile()
        {
            if (string.IsNullOrEmpty(ExcelDownloadPath))
            {
                MessageBox.Show(
                    "Selecione um diretório para salvar o arquivo Excel.",
                    "Caminho Inválido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                var result = _productService.GenerateProductsListExcelFileAsync(ExcelDownloadPath, CancellationToken.None)
                    .GetAwaiter()
                    .GetResult();

                if (result.IsSuccess)
                {
                    MessageBox.Show(
                        $"Arquivo gerado com sucesso!\nVocê pode encontra-lo no caminho: {result.Data}",
                        "Arquivo Gerado",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"Arquivo gerado com falha!\nCódigo: {result.Error.Code}\nDescrição: {result.Error.Description}",
                        "Erro na geração do arquivo",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }

        #endregion
    }
}
