using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Library.Service.Dto.Library.Dto;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class ReportService : IReportService
    {
        public byte[] GenerateReportContent(ReservationDetailsDto reservation, ReturnBookDto returnBookInfo)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(ms))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        document.Add(new Paragraph("Reservation Report")
                             .SetTextAlignment(TextAlignment.CENTER)
                             .SetFontSize(24)
                             .SetBold());


                        document.Add(new Paragraph($"ID: {reservation.ReservationId}")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(16)
                            .SetPaddingTop(5)); ;

                        document.Add(new Paragraph($"Customer: {reservation.CustomerFullName}"));
                        document.Add(new Paragraph($"Employee: {reservation.EmployeeFullName}"));
                        document.Add(new Paragraph($"Checkout Time: {reservation.CheckoutTime}"));
                        document.Add(new Paragraph($"Supposed Return Date: {reservation.SupposedReturnDate}"));
                        document.Add(new Paragraph($"Actual Return Date: {reservation.ActualReturnDate ?? DateTime.Now}"));
                        document.Add(new Paragraph($"Is Late: {(reservation.IsLate ? "Yes" : "No")}"));

                        Table reservationTable = new Table(6).UseAllAvailableWidth();
                        reservationTable.AddHeaderCell("Book Title");
                        reservationTable.AddHeaderCell("Edition");
                        reservationTable.AddHeaderCell("Publisher");
                        reservationTable.AddHeaderCell("Quantity");
                        reservationTable.AddHeaderCell("Return Date");
                        reservationTable.AddHeaderCell("Returned By");

                        foreach (var item in reservation.ReservationItems)
                        {
                            reservationTable.AddCell(item.BookTitle);
                            reservationTable.AddCell(item.Edition);
                            reservationTable.AddCell(item.PublisherName);
                            reservationTable.AddCell(item.Quantity.ToString());
                            reservationTable.AddCell(item.ActualReturnDate?.ToString("d") ?? "Not returned");
                            reservationTable.AddCell(item.ReturnCustomerId ?? "N/A");
                        }

                        document.Add(reservationTable);


                        if (returnBookInfo != null && returnBookInfo.returnItems != null && returnBookInfo.returnItems.Any())
                        {
                            document.Add(new Paragraph("Return Book Information")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(16)
                                .SetBold()
                                .SetPaddingTop(20));

                            Table returnTable = new Table(2).UseAllAvailableWidth();
                            returnTable.AddHeaderCell("Return Status");
                            returnTable.AddHeaderCell("Quantity");

                            foreach (var item in returnBookInfo.returnItems)
                            {
                                returnTable.AddCell(item.ReturnStatus ?? "N/A");
                                returnTable.AddCell(item.Quantity.ToString());
                            }

                            document.Add(returnTable);
                        }
                    }
                }

                return ms.ToArray();
            }
        }
    }
}
