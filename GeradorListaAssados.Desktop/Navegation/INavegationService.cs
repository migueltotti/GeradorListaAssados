using System.Windows;

namespace GeradorListaAssados.Desktop.Navegation
{
    public interface INavegationService
    {
        void OpenWindow<TWindow>() where TWindow : Window;
    }
}
