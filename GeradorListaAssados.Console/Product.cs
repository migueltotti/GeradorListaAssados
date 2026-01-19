using System.Drawing;

namespace GeradorListaAssados.Console;

public class Product
{
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public int Index { get; private set; }
    public string HexCodeColor { get; private set; }

    public Product(string name, int quantity, int index, string hexCodeColor)
    {
        Name = name;
        Quantity = quantity;
        Index = index;
        HexCodeColor = hexCodeColor;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }
    
    public void SetIndex(int index)
    {
        Index = index;
    }
    
    public void SetColor(string hexCodeColor)
    {
        HexCodeColor = hexCodeColor;
    }
}