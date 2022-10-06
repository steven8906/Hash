using HashPDF.Models.Response;
using HashPDF.Resources;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Security.Cryptography;

namespace HashPDF.Services
{
    /// <summary>
    /// Source File:   CiphierService.cs                                             
    /// Description:   Service Class
    /// Author(es):    Edward Steven Hernández Lambraño
    /// Date:          03/10/2022
    /// Version:       1.0.0
    /// Copyright(c), 2022 
    /// </summary>
    public class CiphierService
    {
        #region Method's

        /// <summary>
        /// Procesa el archivo pdf generando hash y asignando marca de agua al mismo.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns>UploadFileResponse</returns>
        public ResponseModel<UploadFileResponse> ProccessFile(IFormFile formFile)
        {
            ResponseModel<UploadFileResponse> responseModel = new();
            try
            {
                using (MemoryStream memoryStreamFile = new MemoryStream())
                {
                    formFile.CopyTo(memoryStreamFile);
                    byte[] beforeFile = memoryStreamFile.ToArray();
                    string hashBeforeFile = GetHashByByteArray(beforeFile);
                    byte[] hashedFile = AddHashToPdf(beforeFile, hashBeforeFile);
                    string hashAfterFile = GetHashByByteArray(hashedFile);

                    UploadFileResponse uploadFileResponse = new()
                    {
                        File = hashedFile,
                        FirstHash = hashBeforeFile,
                        LastHash = hashAfterFile
                    };
                    responseModel.Success(uploadFileResponse);
                }
            }
            catch (Exception ex)
            {
                responseModel.InternalServerError();
            }

            return responseModel;
        }

        /// <summary>
        /// Obtiene el Hash del archivo 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public ResponseModel<string> CheckHashFile(IFormFile formFile)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    formFile.CopyTo(memoryStream);
                    string hash = GetHashByByteArray(memoryStream.ToArray());
                    responseModel.Success(hash);
                }
            }
            catch (Exception ex)
            {
                responseModel.InternalServerError();
            }

            return responseModel;
        }

        #endregion

        #region Private Method's

        /// <summary>
        /// Obtiene hash del archivo pdf.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        private string GetHashByByteArray(byte[] dataBytes)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(dataBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// Agrega el hash al PDF
        /// </summary>
        /// <param name="file"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private byte[] AddHashToPdf(byte[] fileBytes, string hash)
        {
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfReader reader = new PdfReader(fileBytes))
                {
                    int pageCount = reader.NumberOfPages;
                    using (PdfStamper stamper = new PdfStamper(reader, ms))
                    {
                        BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                     
                        for (int i = 1; i <= pageCount; i++)
                            AddWaterMark(stamper.GetOverContent(i), $"{CommonResource.VerificationCode}: {hash}", baseFont, BaseColor.BLACK);
                    }
                }
                bytes = ms.ToArray();
                return bytes;
            }
        }

        /// <summary>
        /// Agrega una marca de agua con el código de seguridad
        /// </summary>
        /// <param name="pdfContentByte"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        private void AddWaterMark(PdfContentByte pdfContentByte, string text, BaseFont font, BaseColor color)
        {
            PdfGState gstate = new PdfGState { FillOpacity = 0.3f, StrokeOpacity = 0.3f };
            pdfContentByte.SaveState();
            pdfContentByte.SetGState(gstate);
            pdfContentByte.SetColorFill(color);
            pdfContentByte.BeginText();
            pdfContentByte.SetFontAndSize(font, 8);
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, text, 10, 10, 0);
            pdfContentByte.EndText();
            pdfContentByte.RestoreState();
        }

        /// <summary>
        /// Agrega texto con el código de seguridad - Es un método alterno para agregar el texto del hash
        /// </summary>
        /// <param name="pdfContentByte"></param>
        /// <param name="baseFont"></param>
        /// <param name="text"></param>
        private void AddHashText(PdfContentByte pdfContentByte, BaseFont baseFont, string text)
        {
            pdfContentByte.BeginText();
            pdfContentByte.SetFontAndSize(baseFont, 8);
            pdfContentByte.ShowTextAligned(Element.ALIGN_LEFT, $"{CommonResource.VerificationCode}: {text}", 10, 10, 0);
            pdfContentByte.EndText();
        }

        #endregion
    }
}
