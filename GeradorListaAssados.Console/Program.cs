using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GeradorListaAssados.Console;
using Color = System.Drawing.Color;

var filePath = Path.Combine(Path.GetTempPath(), $"lista-{Guid.NewGuid()}.xlsx");

var workbook = new XLWorkbook();

var worksheet = workbook.Worksheets.Add(WorksheetConstants.SheetName);

// Prepara a altura da primeira linda 
worksheet.Row(1).Height = 14.4;

// Prepara largura de todas as colunas
for (var i = 1; i <= 7; i++)
{
    worksheet.Column(i).Width = i % 2 == 1 ?
        2.32 : // colunas impares (A, C, E, G)
        48.67; // colunas pares (B, D, F)
}

// adiciona a data
worksheet.Cell("F1").Value = DateTime.Now.ToString("dd/MM/yyyy");
worksheet.Cell("F1").Style.Font.FontName = WorksheetConstants.FontName;
worksheet.Cell("F1").Style.Font.FontSize = 11;
worksheet.Cell("F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

// Adiciona titulo juntando todas as colunas da segunda linha
worksheet.Range("B2:F2").Merge();
worksheet.Cell("B2").Style.Font.Bold = true;
worksheet.Cell("B2").Value = "ASSADOS DE DOMINGO";
worksheet.Cell("B2").Style.Font.FontName = WorksheetConstants.FontName;
worksheet.Cell("B2").Style.Font.FontSize = 24;
worksheet.Cell("B2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
worksheet.Cell("B2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

// Preparar sample de assados
var assadosList = new List<Product>
{
    new Product("FRANGO", 21, 1, "#FFD966"),
    new Product("FRANGO DESOSSADO RECHEADO TOMATE", 1, 2, "#FFD966"),
    new Product("FRANGO DESOSSADO RECHEADO CALABRESA", 1, 3, "#FFD966"),
    new Product("FRANGO DESOSSADO RECHEADO BATATA", 1, 4, "#FFD966"),
    new Product("FRANGO DESOSSADO RECHEADO FAROFA", 3, 5, "#FFD966"),
    
    new Product("COXA COM SOBRECOXA DESOSS. RECH   33 UND", 8, 6, "#FFD966"),
    new Product("COPA LOMBO", 2, 7, "#9BC2E6"),
    new Product("COSTELA SUINA", 4, 8, "#9BC2E6"),
    new Product("JOELHO", 2, 9, "#9BC2E6"),
    new Product("PANCETA", 6, 10, "#9BC2E6"),
    new Product("COSTELINHA CROCK", 2, 11, "#9BC2E6"),
    
    new Product("FRALDINHA", 5, 12, "#EF6FC1"),
    new Product("MAMINHA", 3, 13, "#EF6FC1"),
    new Product("MAMINHA RECHEADA", 3, 14, "#EF6FC1"),
    new Product("COSTELA BOVINA", 6, 15, "#EF6FC1"),
    new Product("CUPIM", 4, 16, "#EF6FC1"),
    new Product("PONTA DE PEITO", 1, 17, "#EF6FC1"),
};

assadosList.Sort((p1, p2) => p1.Index < p2.Index ? -1 : 1);

var row = 3;
var column = 2;

foreach (var assado in assadosList)
{
    if (row + assado.Quantity >= WorksheetConstants.MaxProductColumnHeight)
    {
        column+= 2;
        row = 3;
    }

    worksheet.Row(row).Height = 35.1;
    
    // Prepara titulo do assado
    worksheet.Cell(row, column).Value = assado.Name;
    worksheet.Cell(row, column).Style.Font.FontSize = 14;
    worksheet.Cell(row, column).Style.Font.Bold = true;
    worksheet.Cell(row, column).Style.Font.FontName = WorksheetConstants.FontName;
    worksheet.Cell(row, column).Style.Fill.BackgroundColor = XLColor.FromHtml(assado.HexCodeColor);
    worksheet.Cell(row, column).Style.Alignment.WrapText = true;
    worksheet.Cell(row, column).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    worksheet.Cell(row, column).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    worksheet.Cell(row, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

    row++;
    
    var initialRow = row;
    
    // Prepara quantidade de caixas correspondente à quantidade desse assado
    for (int i = 1; i <= assado.Quantity; i++)
    {
        worksheet.Row(row).Height = 35.1;
        
        worksheet.Cell(row, column).Value = i;
        worksheet.Cell(row, column).Style.Font.FontSize = 16;
        worksheet.Cell(row, column).Style.Font.FontName = WorksheetConstants.FontName;
        worksheet.Cell(row, column).Style.Font.FontColor = WorksheetConstants.CellNumber;
        worksheet.Cell(row, column).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        worksheet.Cell(row, column).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        worksheet.Cell(row, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        
        row++;
    }
}

workbook.SaveAs(filePath);

Console.WriteLine(filePath);

