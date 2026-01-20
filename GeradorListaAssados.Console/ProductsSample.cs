using GeradorListaAssados.Engine.Models;

namespace GeradorListaAssados.Console;

public static class ProductsSample
{
    public static List<Product> AssadosList =>
    [
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
            .SetName("FRANGO DESOSSADO RECHEADO CALABRESA")
            .SetPrice(74.90m)
            .SetQuantity(1)
            .SetIndex(3)
            .SetColor("#FFD966")
            .Build(),
        Product.Builder.Create()
            .SetName("FRANGO DESOSSADO RECHEADO BATATA")
            .SetPrice(74.90m)
            .SetQuantity(1)
            .SetIndex(4)
            .SetColor("#FFD966")
            .Build(),
        Product.Builder.Create()
            .SetName("FRANGO DESOSSADO RECHEADO FAROFA")
            .SetPrice(74.90m)
            .SetQuantity(3)
            .SetIndex(5)
            .SetColor("#FFD966")
            .Build(),

        Product.Builder.Create()
            .SetName("COXA COM SOBRECOXA DESOSS. RECH   33 UND")
            .SetPrice(0)
            .SetQuantity(8)
            .SetIndex(6)
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
            .SetName("COSTELA SUINA")
            .SetPrice(0)
            .SetQuantity(4)
            .SetIndex(8)
            .SetColor("#9BC2E6")
            .Build(),
        Product.Builder.Create()
            .SetName("JOELHO")
            .SetPrice(0)
            .SetQuantity(2)
            .SetIndex(9)
            .SetColor("#9BC2E6")
            .Build(),
        Product.Builder.Create()
            .SetName("PANCETA")
            .SetPrice(0)
            .SetQuantity(6)
            .SetIndex(10)
            .SetColor("#9BC2E6")
            .Build(),
        Product.Builder.Create()
            .SetName("COSTELINHA CROCK")
            .SetPrice(0)
            .SetQuantity(2)
            .SetIndex(11)
            .SetColor("#9BC2E6")
            .Build(),

        Product.Builder.Create()
            .SetName("FRALDINHA")
            .SetPrice(0)
            .SetQuantity(5)
            .SetIndex(12)
            .SetColor("#EF6FC1")
            .Build(),
        Product.Builder.Create()
            .SetName("MAMINHA")
            .SetPrice(0)
            .SetQuantity(3)
            .SetIndex(13)
            .SetColor("#EF6FC1")
            .Build(),
        Product.Builder.Create()
            .SetName("MAMINHA RECHEADA")
            .SetPrice(0)
            .SetQuantity(3)
            .SetIndex(14)
            .SetColor("#EF6FC1")
            .Build(),
        Product.Builder.Create()
            .SetName("COSTELA BOVINA")
            .SetPrice(0)
            .SetQuantity(6)
            .SetIndex(15)
            .SetColor("#EF6FC1")
            .Build(),
        Product.Builder.Create()
            .SetName("CUPIM")
            .SetPrice(0)
            .SetQuantity(4)
            .SetIndex(16)
            .SetColor("#EF6FC1")
            .Build(),
        Product.Builder.Create()
            .SetName("PONTA DE PEITO")
            .SetPrice(0)
            .SetQuantity(1)
            .SetIndex(17)
            .SetColor("#EF6FC1")
            .Build()
    ];
}
