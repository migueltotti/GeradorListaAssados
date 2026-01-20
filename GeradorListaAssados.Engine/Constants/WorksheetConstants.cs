using ClosedXML.Excel;

namespace GeradorListaAssados.Engine.Constants;

public static class WorksheetConstants
{
    public static XLColor CellNumber => XLColor.FromHtml("#4472C4");
    public const string SheetName = "Assados";
    public const string HeaderTitleText = "ASSADOS DE DOMINGO";
    public const string FontName = "Calibri";
    public const int MaxProductColumnHeight = 36;
}