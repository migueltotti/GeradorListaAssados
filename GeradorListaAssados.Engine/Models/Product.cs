namespace GeradorListaAssados.Engine.Models;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public int Index { get; private set; }
    public string HexCodeColor { get; private set; }

    private Product(Guid id, string name, decimal price, int quantity, int index, string hexCodeColor)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Index = index;
        HexCodeColor = hexCodeColor;
    }

    public Builder ToBuilder() => new()
    {
        Id = Id,
        Name = Name,
        Price = Price,
        Quantity = Quantity,
        Index = Index,
        HexCodeColor = HexCodeColor
    };

    public sealed class Builder
    {
        internal Guid Id = Guid.NewGuid();
        internal string Name = string.Empty;
        internal decimal Price = 0.00m;
        internal int Quantity = 0;
        internal int Index = 0;
        internal string HexCodeColor = "#FFFFFF";

        public static Builder Create() => new();

        public Builder SetName(string name) { Name = name; return this; }
        public Builder SetPrice(decimal price) { Price = price; return this; }
        public Builder SetQuantity(int quantity) { Quantity = quantity; return this; }
        public Builder SetIndex(int index) { Index = index; return this; }
        public Builder SetColor(string hexCodeColor) { HexCodeColor = hexCodeColor; return this; }
        
        public Product Build() => new(
            Id,
            Name,
            Price,
            Quantity,
            Index,
            HexCodeColor);
    }
}