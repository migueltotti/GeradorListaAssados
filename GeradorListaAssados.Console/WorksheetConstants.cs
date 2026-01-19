using ClosedXML.Excel;

namespace GeradorListaAssados.Console;

public static class WorksheetConstants
{
    public static XLColor CellNumber => XLColor.FromHtml("#4472C4");
    public const string SheetName = "Assados";
    public const string FontName = "Calibri";
    public const int MaxProductColumnHeight = 36;
}