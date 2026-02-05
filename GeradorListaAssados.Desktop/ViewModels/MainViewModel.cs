using GeradorListaAssados.Desktop.Commands;
using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GeradorListaAssados.Desktop.ViewModels
{
    public class MainViewModel
    {
        private readonly IProductService _productService;

        public ObservableCollection<Product> Products { get; set; }
        public string ExcelDownloadPath = "";

        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        public MainViewModel(IProductService productService)
        {
            _productService = productService;

            //LoadProducts()
            //    .GetAwaiter()
            //    .GetResult();

            Products = new ObservableCollection<Product>
            {
                Product.Builder.Create()
                    .SetName("FRANGO")
                    .SetPrice(46.90m)
                    .SetQuantity(21)
                    .SetIndex(1)
                    .SetColor("#FFD966")
                    .Build(),
                Product.Builder.Create()
                    .SetName("FRANGO DESOSSADO RECHEADO TOMATE")
                    .SetPrice(74.90m)
                    .SetQuantity(1)
                    .SetIndex(2)
                    .SetColor("#FFD966")
                    .Build(),
                Product.Builder.Create()
                    .SetName("COPA LOMBO")
                    .SetPrice(0)
                    .SetQuantity(2)
                    .SetIndex(7)
                    .SetColor("#9BC2E6")
                    .Build(),
                Product.Builder.Create()
                    .SetName("MAMINHA")
                    .SetPrice(0)
                    .SetQuantity(3)
                    .SetIndex(13)
                    .SetColor("#EF6FC1")
                    .Build()
            };

            EditProductCommand = new RelayCommand<Product>(EditProduct);
            DeleteProductCommand = new RelayCommand<Product>(DeleteProduct);
        }

        private void EditProduct(Product product)
        {
            // abrir modal de edição

            //var result = _productService.UpdateAsync(product, CancellationToken.None)
            //    .GetAwaiter()
            //    .GetResult();

            //if (result.IsFailure)
            //{
            //    MessageBox.Show(
            //        result.Error.Description,
            //        "Erro ao editar produto.",
            //        MessageBoxButton.OK);
            //}

            //LoadProducts()
            //    .GetAwaiter()
            //    .GetResult();
        }

        private void DeleteProduct(Product product)
        {
            if(MessageBox.Show(
                "Deseja remover esse produto? Operação irreversivel!",
                "Remover Produto",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //var result = _productService.DeleteAsync(product.Id, CancellationToken.None)
                //    .GetAwaiter()
                //    .GetResult();

                //if (result.IsFailure)
                //{
                //    MessageBox.Show(
                //        result.Error.Description,
                //        "Erro ao remover produto.",
                //        MessageBoxButton.OK);
                //}
                //else
                //{
                //    LoadProducts()
                //        .GetAwaiter()
                //        .GetResult();
                //}
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
                        $"Arquivo gerado com sucesso! Você pode encontra-lo no caminho: {result.Data}",
                        "Arquivo Gerado",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"Arquivo gerado com falha! Código: {result.Error.Code}\nDescrição: {result.Error.Description}",
                        "Erro na geração do arquivo",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }

        #endregion
    }
}
