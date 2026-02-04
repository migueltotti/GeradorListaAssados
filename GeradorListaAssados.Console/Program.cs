using ClosedXML.Excel;
using GeradorListaAssados.Console;
using GeradorListaAssados.Engine.Extentions;

var fileName = $"lista-{Guid.NewGuid()}.xlsx";

string downloadsPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
    "Downloads",
    fileName
);

var workbook = new XLWorkbook();

var assadosList = ProductsSample.AssadosList;

assadosList.Sort((p1, p2) => p1.Index < p2.Index ? -1 : 1);

workbook
    .CreateWorksheet()
    .ConfigureRowsAndColumn()
    .AddHeader()
    .AddProducts(assadosList);

workbook.SaveAs(downloadsPath);

Console.WriteLine(downloadsPath);

