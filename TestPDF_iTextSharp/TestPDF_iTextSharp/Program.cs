using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 참고 사이트 : https://www.c-sharpcorner.com/blogs/create-table-in-pdf-using-c-sharp-and-itextsharp
/// </summary>
namespace TestPDF_iTextSharp
{
    class Program
    {
        static BaseFont _baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\malgun.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        static Font _fontTitle = new Font(_baseFont, 20, Font.BOLD | Font.UNDERLINE);
        static Font _fontNormal = new Font(_baseFont);

        static void Main(string[] args)
        {

            // 파일 IO 스트림을 취득한다.
            using (var stream = new FileStream(Environment.CurrentDirectory + "/simple.pdf", FileMode.Create, FileAccess.Write))
            {
                // Pdf형식의 document를 생성한다.
                Document document = new Document(PageSize.A4, 20, 20, 20, 20);
                // PdfWriter를 취득한다.
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                
                
                try
                {
                    //
                    // 어도비 리더앱의 속성창에서 보이는 내용
                    //
                    document.AddAuthor("박영기");
                    document.AddCreator("박영기");
                    document.AddKeywords("iTextSharp-LGPL 테스트");
                    document.AddSubject("제목");
                    document.AddTitle("제목");
                    
                    document.Open();

                    //
                    // 제목
                    //
                    AddTitle(document, "OO결과확인서");

                    //
                    // 본문
                    //
                    document.Add(new Paragraph("Hello World"));
                    document.Add(new Paragraph(50f, "hello World with leading 50f"));

                    document.Add(Korean("ALIGN_LEFT", Element.ALIGN_LEFT));
                    document.Add(Korean("ALIGN_CENTER", Element.ALIGN_CENTER));
                    document.Add(Korean("ALIGN_RIGHT", Element.ALIGN_RIGHT));

                    AddBR(document);

                    //
                    // 테이블1
                    //
                    {
                        PdfPTable talbe = new PdfPTable(3);
                        talbe.AddCell("Row1,Col1");
                        talbe.AddCell("Row1,Col2");
                        talbe.AddCell("Row1,Col3");
                        
                        talbe.AddCell(Korean("로우2,컬럼1"));
                        talbe.AddCell("Row2,Col2");
                        talbe.AddCell("Row2,Col3");

                        document.Add(talbe);
                    }

                    AddBR(document);
                    //
                    // 테이블2
                    //
                    {
                        PdfPTable talbe = new PdfPTable(3);
                        PdfPCell cell = new PdfPCell(Korean("병합셀"));
                        cell.Colspan = 3;

                        talbe.AddCell(cell);
                        talbe.AddCell("Row2,Col1");
                        talbe.AddCell("Row2,Col2");
                        talbe.AddCell("Row2,Col3");

                        document.Add(talbe);
                    }
                }
                finally
                {
                    // document Close
                    document.Close();
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="text"></param>
        /// <param name="alignment">Element.ALIGN_***</param>
        static void AddParagraph(Document d, string text, int alignment)
        {
            Paragraph p = new Paragraph(text, _fontNormal);
            p.Alignment = alignment;
            d.Add(p);
        }

        static void AddParagraph(Document d, string text)
        {
            AddParagraph(d, text, Element.ALIGN_LEFT);
        }

        static void AddBR(Document d)
        {
            AddParagraph(d, " ", Element.ALIGN_LEFT);
        }

        static void AddTitle(Document d, string text)
        {
            Paragraph p = new Paragraph(text, _fontTitle);
            p.Alignment = Element.ALIGN_CENTER;
            d.Add(p);
        }

        static void AddBR(Document d, string text)
        {
            Paragraph p = new Paragraph(text, _fontTitle);
            d.Add(p);
        }

        static Paragraph Korean(string text)
        {
            return new Paragraph(text, _fontNormal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alignment">Element.ALIGN_***</param>
        /// <returns></returns>
        static Paragraph Korean(string text, int alignment)
        {
            Paragraph p = new Paragraph(text, _fontNormal);
            p.Alignment = alignment;
            return p;
        }
    }
}
