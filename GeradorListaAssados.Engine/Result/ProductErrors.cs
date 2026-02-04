namespace GeradorListaAssados.Engine.Result;

public static class ProductErrors
{
    public static Error NotFound => new(
        "Product.NotFound",
        "Produto não encontrado.");

    public static Error NoProductsFound => new(
        "Product.NoProductsFound",
        "Nenhum produto encontrado para gerar arquivo lista.");

    public static Error AlreadyExists => new(
        "Product.AlreadyExists",
        "Produto com esse Id já existe.");

    public static Error DuplicatedIndex => new(
        "Product.DuplicatedIndex",
        "Produto com esse indice já existe.");

    public static Error ValidationError(string details) => new(
        "BarberError.ValidationError",
        details);
}
