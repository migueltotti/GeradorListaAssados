using System;
using System.Collections.Generic;
using System.Text;

namespace GeradorListaAssados.Engine.Result;

public static class PathErrors
{
    public static Error InvalidPath => new(
        "Path.InvalidPath",
        "Caminho inválido ou não existente.");
}
