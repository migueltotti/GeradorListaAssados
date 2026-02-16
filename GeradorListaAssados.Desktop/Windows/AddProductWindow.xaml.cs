using GeradorListaAssados.Desktop.ViewModels;
using GeradorListaAssados.Engine.Models;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DataFormats = System.Windows.DataFormats;

namespace GeradorListaAssados.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private readonly AddProductViewModel _viewModel;
        private readonly MainViewModel _mainViewModel;

        public AddProductWindow(AddProductViewModel viewModel, MainViewModel mainViewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _mainViewModel = mainViewModel;

            DataContext = _viewModel;
        }

        private Product FieldsToProduct()
        {
            return Product.Builder.Create()
                .SetName(tbName.Text.Replace("\r", "").Replace("\n", ""))
                .SetPrice(decimal.TryParse(tbPrice.Text, out var price) ? price : 0)
                .SetQuantity(int.TryParse(tbQuantity.Text, out var quantity) ? quantity : 0)
                .SetIndex(int.TryParse(tbIndex.Text, out var index) ? index : 0)
                .SetColor(_viewModel.HexColor)
                .Build();
        }

        private void ClearAllFields()
        {
            tbName.Text = string.Empty;
            tbPrice.Text = string.Empty;
            tbQuantity.Text = string.Empty;
            tbIndex.Text = string.Empty;
            _viewModel.HexColor = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var product = FieldsToProduct();

            var result = _viewModel.AddProduct(product)
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
            ClearAllFields();
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

        private void OnlyNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void OnlyNumbers_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = (string)e.DataObject.GetData(DataFormats.Text)!;

                if (!text.All(char.IsDigit))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
