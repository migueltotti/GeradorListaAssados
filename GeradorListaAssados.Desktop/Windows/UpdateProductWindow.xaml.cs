using GeradorListaAssados.Desktop.ViewModels;
using GeradorListaAssados.Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeradorListaAssados.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for UpdateProductWindow.xaml
    /// </summary>
    public partial class UpdateProductWindow : Window
    {
        private readonly UpdateProductViewModel _viewModel;
        private readonly MainViewModel _mainViewModel;

        public UpdateProductWindow(UpdateProductViewModel viewModel, MainViewModel mainViewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _mainViewModel = mainViewModel;

            DataContext = _viewModel;

            SetFields(_viewModel.Product);
        }

        public void SetFields(Product product)
        {
            tbName.Text = product.Name;
            tbPrice.Text = product.Price.ToString();
            tbQuantity.Text = product.Quantity.ToString();
            tbIndex.Text = product.Index.ToString();
            _viewModel.HexColor = product.HexCodeColor;
        }

        private Product FieldsToProduct()
        {
            return Product.Builder.Create()
                .SetId(_viewModel.Product.Id)
                .SetName(tbName.Text.Replace("\r", "").Replace("\n", ""))
                .SetPrice(decimal.TryParse(tbPrice.Text, out var price) ? price : 0)
                .SetQuantity(int.TryParse(tbQuantity.Text, out var quantity) ? quantity : 0)
                .SetIndex(int.TryParse(tbIndex.Text, out var index) ? index : 0)
                .SetColor(_viewModel.HexColor)
                .Build();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var product = FieldsToProduct();

            var result = _viewModel.UpdateProduct(product)
                .GetAwaiter()
                .GetResult();

            if (result)
            {
                _mainViewModel.LoadProducts()
                    .GetAwaiter()
                    .GetResult();

                this.Hide();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var color = System.Drawing.Color.FromArgb(
                    dialog.Color.A,
                    dialog.Color.R,
                    dialog.Color.G,
                    dialog.Color.B
                );

                _viewModel.HexColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
        }
    }
}
