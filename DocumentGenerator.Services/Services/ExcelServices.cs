using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Services.Contracts.Models.DocumentProduct;
using System.Globalization;

namespace DocumentGenerator.Services.Services
{
    /// <inheritdoc cref="IExcelServices" />
    public class ExcelServices : IExcelServices
    {
        private readonly IVatRateProvider vatRateProvider;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ExcelServices(IVatRateProvider vatRateProvider)
        {
            this.vatRateProvider = vatRateProvider;
        }

        MemoryStream IExcelServices.Export(DocumentModel documentModel, CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream();
            using (SpreadsheetDocument excelDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, false))
            {
                WorkbookPart workbookPart = excelDocument.AddWorkbookPart();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                var sheets = new Sheets();
                var sheetData = new SheetData();
                var worksheet = new Worksheet();
                var columns = CreateColumns();

                worksheet.Append(columns);
                worksheet.Append(sheetData);
                workbookPart.Workbook = new Workbook(sheets);
                worksheetPart.Worksheet = worksheet;

                var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = CreateStylesheet();

                Sheet sheet = new()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Акт приёма передачи товара"
                };
                sheets.Append(sheet);

                var vatRate = vatRateProvider.GetVatRate();
                var totalCost = documentModel.Products.Sum(x => x.Cost * x.Quantity * (1.0m + vatRate));

                AddRow(sheetData, 1, textCells: [$"Приложение №{documentModel.DocumentNumber}"]);
                AddRow(sheetData, 1, textCells: ["к договору"]);
                AddRow(sheetData, 1, textCells: [$"от {documentModel.Date.ToString("«d» MMMM yyyy", new CultureInfo("ru-RU"))}г."]);
                AddRow(sheetData, 1, textCells: [$"№{documentModel.ContractNumber}"]);
                AddRow(sheetData, 2, textCells: ["АКТ ПРИЕМА ПЕРЕДАЧИ ТОВАРА"]);

                AddRow(sheetData, 3, "№", "Наименование", "Кол-во", "Цена с НДС", "Сумма с НДС");
                AddProductRows(sheetData, documentModel.Products);
                sheetData.Append(new Row(
                    CreateCell("", 3),
                    CreateCell("", 3),
                    CreateCell("", 3),
                    CreateCell("Итого на сумму", 3),
                    CreateNumericCell(totalCost, 5)));

                AddRow(sheetData, 7, $"Стоимость Товара поставленного в соответствии с условия Договора составляет {totalCost:f2} рублей, с учетом НДС.");
                AddRow(sheetData, 0, "");
                AddRow(sheetData, 2, "Покупатель:", "", "", "Продавец:");
                AddRow(sheetData, 2, $"{documentModel.Buyer.Job}", "", "", $"{documentModel.Seller.Job}");
                sheetData.Append(new Row(
                    CreateCell("__________", 2),
                    CreateCell($"{documentModel.Buyer.Name}", 0),
                    CreateCell("", 0),
                    CreateCell("__________", 2),
                    CreateCell($"{documentModel.Seller.Name}", 0)));
                AddRow(sheetData, 2, "М.П.", "", "", "М.П.");

                var mergeCells = CreateMergeCells(documentModel.Products.Count);
                worksheet.Append(mergeCells);

                excelDocument.Save();
            }
            return stream;
        }

        private Cell CreateCell(string text, uint styleIndex)
            => new()
            {
                DataType = CellValues.String,
                CellValue = new(text),
                StyleIndex = styleIndex,
            };

        private Cell CreateNumericCell(decimal num, uint styleIndex)
            => new()
            {
                DataType = CellValues.Number,
                CellValue = new(num.ToString(CultureInfo.InvariantCulture)),
                StyleIndex = styleIndex,
            };

        private MergeCell CreateMergeCell(string reference)
            => new()
            {
                Reference = new StringValue(reference),
            };

        private void AddRow(SheetData sheetData, uint styleIndex, params string[] textCells)
        {
            var row = new Row();
            foreach (var text in textCells)
            {
                row.Append(CreateCell(text, styleIndex));
            }
            sheetData.Append(row);
        }

        private void AddProductRows(SheetData sheetData, ICollection<DocumentProductModel> products)
        {
            var vatRate = vatRateProvider.GetVatRate();
            uint i = 1;
            foreach (var product in products)
            {
                sheetData.Append(new Row(
                    CreateNumericCell(i, 6),
                    CreateCell(product.Product.Name, 4),
                    CreateNumericCell(product.Quantity, 5),
                    CreateNumericCell(product.Cost * (1.0m + vatRate), 5),
                    CreateNumericCell(product.Cost * product.Quantity * (1.0m + vatRate), 5)));
                ++i;
            }
        }

        private MergeCells CreateMergeCells(int productsCount)
        {
            const int titleLines = 5,
                tableExtraLines = 2,
                underTableLine = 2,
                partiesJobTwoColumns = 2,
                partiesNameTwoColumns = 2;

            int productsBlockEnd = titleLines + tableExtraLines + productsCount;
            int underTableLineEnd = productsBlockEnd + underTableLine;
            int partiesHeaderBlockEnd = underTableLineEnd + partiesJobTwoColumns;
            int totalLines = partiesHeaderBlockEnd + partiesNameTwoColumns;

            var mergeCells = new MergeCells();
            for (int i = 1; i <= totalLines; ++i)
            {
                if (i <= titleLines)
                {
                    mergeCells.Append(CreateMergeCell($"A{i}:E{i}"));
                }
                else if (i > underTableLineEnd && i <= partiesHeaderBlockEnd)
                {
                    mergeCells.Append(CreateMergeCell($"A{i}:C{i}"));
                    mergeCells.Append(CreateMergeCell($"D{i}:F{i}"));
                }
                else if (i > underTableLineEnd && i <= totalLines)
                {
                    mergeCells.Append(CreateMergeCell($"B{i}:C{i}"));
                    mergeCells.Append(CreateMergeCell($"E{i}:F{i}"));
                }
            }
            mergeCells.Append(CreateMergeCell($"A{productsBlockEnd+1}:E{underTableLineEnd}"));

            return mergeCells;
        }

        private static Columns CreateColumns()
            => new Columns(
                new Column { Min = 1, Max = 1, Width = 15, CustomWidth = true },
                new Column { Min = 2, Max = 2, Width = 25, CustomWidth = true },
                new Column { Min = 3, Max = 3, Width = 10, CustomWidth = true },
                new Column { Min = 4, Max = 4, Width = 15, CustomWidth = true },
                new Column { Min = 5, Max = 5, Width = 20, CustomWidth = true } 
            );

        private static Stylesheet CreateStylesheet()
            => new(
                new NumberingFormats(
                    new NumberingFormat { NumberFormatId = 164, FormatCode = "#,##0.00" },
                    new NumberingFormat { NumberFormatId = 165, FormatCode = "#,##0" }
                ),
                new Fonts(
                    new Font(
                        new FontSize() { Val = 14 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "TimesNewRoman" }
                    ),
                    new Font(
                        new Bold(),
                        new FontSize() { Val = 14 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "TimesNewRoman" }
                    )
                ),
                new Fills(new Fill()),
                new Borders(
                    new Border(
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()
                    ),
                    new Border(
                        new LeftBorder() { Style = BorderStyleValues.Thin },
                        new RightBorder() { Style = BorderStyleValues.Thin },
                        new TopBorder() { Style = BorderStyleValues.Thin },
                        new BottomBorder() { Style = BorderStyleValues.Thin }
                    )
                ),
                new CellFormats(
                    new CellFormat(),
                    new CellFormat() // Normal Right Align = 1
                    {
                        Alignment = new() { Horizontal = HorizontalAlignmentValues.Right },
                        ApplyAlignment = true,
                    },
                    new CellFormat() // Normal Center Align = 2
                    {
                        Alignment = new() { Horizontal = HorizontalAlignmentValues.Center },
                        ApplyAlignment = true,
                    },
                    new CellFormat() // Table Header Center Align = 3
                    {
                        BorderId = 1,
                        Alignment = new() { Horizontal = HorizontalAlignmentValues.Center },
                        ApplyAlignment = true,
                        ApplyBorder = true,
                    },
                    new CellFormat() // Table Left Align = 4
                    {
                        BorderId = 1,
                        ApplyBorder = true,
                    },
                    new CellFormat() // Float Table Center Align = 5
                    {
                        BorderId = 1,
                        NumberFormatId = 164,
                        Alignment = new() { Horizontal = HorizontalAlignmentValues.Center },
                        ApplyAlignment = true,
                        ApplyBorder = true,
                        ApplyNumberFormat = true,
                    },
                    new CellFormat() // Int Table Center Align = 6
                    {
                        BorderId = 1,
                        NumberFormatId = 165,
                        Alignment = new() { Horizontal = HorizontalAlignmentValues.Center },
                        ApplyAlignment = true,
                        ApplyBorder = true,
                        ApplyNumberFormat = true,
                    },
                    new CellFormat() // Under Table Wrap Line = 7
                    {
                        Alignment = new() { WrapText = true },
                        ApplyAlignment = true,
                    }
                )
            );
    }
}
