using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace SIAO.SRV.TO
{
    public class FilesTO
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public int UserId { get; set; }
        public String cnpj { get; set; }
        public String tipo { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public String NomeFantasia { get; set; }
    }

    public class iTPageEventHandler : iTextSharp.text.pdf.PdfPageEventHelper
    {
        public Image ImageHeader { get; set; }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            ImageHeader.SetAbsolutePosition(30, 755);
            ImageHeader.ScalePercent(50);
            document.Add(ImageHeader);

            //float cellHeight = document.TopMargin;
            //Rectangle page = document.PageSize;

            //PdfPTable head = new PdfPTable(2);
            //head.TotalWidth = page.Width;
            
            //PdfPCell c = new PdfPCell(ImageHeader, true);
            //c.HorizontalAlignment = Element.ALIGN_RIGHT;
            //c.FixedHeight = cellHeight;
            //c.Border = PdfPCell.NO_BORDER;
            //head.AddCell(c);

            //c = new PdfPCell(new Phrase("Analise de vendas"));
            //c.Border = PdfPCell.NO_BORDER;
            //c.VerticalAlignment = Element.ALIGN_MIDDLE;
            //c.FixedHeight = cellHeight;
            //head.AddCell(c);

            //head.WriteSelectedRows(0, -1, 0, page.Height - cellHeight + head.TotalHeight, writer.DirectContent);
        }
    }
}
