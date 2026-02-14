using System.Windows;

namespace GeradorListaAssados.Desktop.Navegation
{
    public class NavegationService(IServiceProvider serviceProvider) : INavegationService
    {
        public void OpenWindow<TWindow>() where TWindow : Window
        {
            var window = serviceProvider.GetService(typeof(TWindow)) as TWindow;
            if (window is System.Windows.Window w)
            {
                w.Show();
            }
        }
    }
}
