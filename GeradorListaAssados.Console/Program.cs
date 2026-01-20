using ClosedXML.Excel;
using GeradorListaAssados.Console;
using GeradorListaAssados.Engine.Extentions;

var filePath = Path.Combine(Path.GetTempPath(), $"lista-{Guid.NewGuid()}.xlsx");

var workbook = new XLWorkbook();

var assadosList = ProductsSample.AssadosList;

assadosList.Sort((p1, p2) => p1.Index < p2.Index ? -1 : 1);

workbook
    .CreateWorksheet()
    .ConfigureRowsAndColumn()
    .AddHeader()
    .AddProducts(assadosList);

workbook.SaveAs(filePath);

Console.WriteLine(filePath);

