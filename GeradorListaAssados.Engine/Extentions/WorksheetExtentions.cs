using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GeradorListaAssados.Engine.Constants;
using GeradorListaAssados.Engine.Models;
using System.Globalization;

namespace GeradorListaAssados.Engine.Extentions;

public static class WorksheetExtentions
{
    public static IXLWorksheet CreateWorksheet(this XLWorkbook workbook)
    {
        return workbook.Worksheets.Add(WorksheetConstants.SheetName);
    }

    public static IXLWorksheet ConfigureRowsAndColumn(this IXLWorksheet worksheet)
    {
        // Prepara a altura da primeira linda 
        worksheet.Row(1).Height = 14.4;

        // Prepara largura de todas as colunas
        for (var i = 1; i <= 7; i++)
        {
            worksheet.Column(i).Width = i % 2 == 1 ?
                2.32 : // colunas impares (A, C, E, G)
                48.67; // colunas pares (B, D, F)
        }

        return worksheet;
    }

    public static IXLWorksheet AddHeader(this IXLWorksheet worksheet)
    {
        // adiciona a data
        worksheet.Cell("F1").Value = DateTime.Now.ToString("dd/MM/yyyy");
        worksheet.Cell("F1").Style.Font.FontName = WorksheetConstants.FontName;
        worksheet.Cell("F1").Style.Font.FontSize = 11;
        worksheet.Cell("F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

        // Adiciona titulo juntando todas as colunas da segunda linha
        worksheet.Range("B2:F2").Merge();
        worksheet.Cell("B2").Style.Font.Bold = true;
        worksheet.Cell("B2").Value = WorksheetConstants.HeaderTitleText;
        worksheet.Cell("B2").Style.Font.FontName = WorksheetConstants.FontName;
        worksheet.Cell("B2").Style.Font.FontSize = 24;
        worksheet.Cell("B2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("B2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        return worksheet;
    }

    public static IXLWorksheet AddProducts(this IXLWorksheet worksheet, IEnumerable<Product> products)
    {
        var row = 3;
        var column = 2;

        foreach (var prod in products)
        {
            if (row + prod.Quantity >= WorksheetConstants.MaxProductColumnHeight)
            {
                column += 2;
                row = 3;
            }

            worksheet.Row(row).Height = 35.1;

            // Prepara titulo do assado
            var formatedPrice = prod.Price.ToString("N2", new CultureInfo("pt-BR"));
            worksheet.Cell(row, column).Value = $"{prod.Name} R$ {formatedPrice}";
            worksheet.Cell(row, column).Style.Font.FontSize = 14;
            worksheet.Cell(row, column).Style.Font.Bold = true;
            worksheet.Cell(row, column).Style.Font.FontName = WorksheetConstants.FontName;
            worksheet.Cell(row, column).Style.Fill.BackgroundColor = XLColor.FromHtml(prod.HexCodeColor);
            worksheet.Cell(row, column).Style.Alignment.WrapText = true;
            worksheet.Cell(row, column).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(row, column).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Cell(row, column).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            row++;

            // Prepara quantidade de caixas correspondente à quantidade desse assado
            for (var i = 1; i <= prod.Quantity; i++)
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

        return worksheet;
    }
}
