using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Models;
using System.ComponentModel;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace GeradorListaAssados.Desktop.ViewModels
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService; 
        private string _hexColor;
        public event PropertyChangedEventHandler? PropertyChanged;
        public string HexColor
        {
            get => _hexColor;
            set
            {
                _hexColor = value;
                OnPropertyChanged(nameof(HexColor));
            }
        }

        public AddProductViewModel(IProductService productService)
        {
            _productService = productService;
        }

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public async Task<bool> AddProduct(Product product)
        {
            var result = await _productService.AddAsync(product, CancellationToken.None);

            if (result.IsFailure)
            {
                MessageBox.Show(
                    result.Error.Description,
                    "Erro ao adicionar produto.",
                    MessageBoxButton.OK);

                return false;
            }
            else
            {
                MessageBox.Show(
                    "Produto adicionado com sucesso",
                    "Adicionar produto.",
                    MessageBoxButton.OK);

                return true;
            }
        }
    }
}
