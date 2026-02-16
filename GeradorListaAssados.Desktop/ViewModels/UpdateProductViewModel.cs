using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace GeradorListaAssados.Desktop.ViewModels
{
    public class UpdateProductViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private string _hexColor;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Product Product;

        public string HexColor
        {
            get => _hexColor;
            set
            {
                _hexColor = value;
                OnPropertyChanged(nameof(HexColor));
            }
        }

        public UpdateProductViewModel(IProductService productService)
        {
            _productService = productService;
        }

        public void SetProduct(Product product)
        {
            Product = product;
            HexColor = product.HexCodeColor;
        }

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _productService.UpdateAsync(product, CancellationToken.None);

            if (result.IsFailure)
            {
                MessageBox.Show(
                    result.Error.Description,
                    "Erro ao atualizar produto.",
                    MessageBoxButton.OK);

                return false;
            }
            else
            {
                MessageBox.Show(
                    "Produto atualizado com sucesso",
                    "Atualizar produto.",
                    MessageBoxButton.OK);

                return true;
            }
        }
    }
}
