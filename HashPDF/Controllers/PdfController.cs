using HashPDF.Models.Request;
using HashPDF.Models.Response;
using HashPDF.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HashPDF.Controllers
{
    /// <summary>
    /// Source File:   PdfController.cs                                             
    /// Description:   Class Controller
    /// Author(es):    Edward Steven Hernández Lambraño
    /// Date:          03/10/2022
    /// Version:       1.0.0
    /// Copyright(c), 2022 
    /// </summary>

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        #region Internal's
        private CiphierService ciphierService = new();
        #endregion

        #region Endpoint's

        /// <summary>
        /// Carga archivo PDF
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadFile([FromForm] UploadFileRequest model)
        {
            ResponseModel<UploadFileResponse> responseModel = ciphierService.ProccessFile(model.File);
            return ExcecuteResponse(responseModel);
        }

        /// <summary>
        /// Convierte byte array a PDF
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ConvertToPDF(ConvertToPDFRequest model) => File(model.FileByteArray, "application/pdf", model.FileName);

        /// <summary>
        /// Comprueba Hash del archivo cargado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CheckHashFile([FromForm] UploadFileRequest model)
        {
            ResponseModel<string> responseModel = ciphierService.CheckHashFile(model.File);
            return ExcecuteResponse(responseModel);
        }

        #endregion

        #region Private Method's

        /// <summary>
        /// Respuesta generica para los controladores
        /// </summary>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        private ActionResult ExcecuteResponse<T>(ResponseModel<T> responseModel)
        {
            if (responseModel.Code is HttpStatusCode.OK)
                return Ok(responseModel);
            else
                return StatusCode(500, responseModel.Message);
        }

        #endregion
    }
}
