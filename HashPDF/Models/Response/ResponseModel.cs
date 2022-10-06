using HashPDF.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HashPDF.Models.Response
{
    /// <summary>
    /// Source File:   Response.cs                                             
    /// Description:   Model Class - Response
    /// Author(es):    Edward Steven Hernández Lambraño
    /// Date:          03/10/2022
    /// Version:       1.0.0
    /// Copyright(c), 2022 
    /// </summary>

    public class ResponseModel<TResponseData>
    {
        #region Properties
        /// <summary>
        /// Code
        /// </summary>
        /// <example>200</example>
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
        /// <summary>
        /// Message
        /// </summary>
        /// <example>Solicitud realizada con éxito</example>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ResponseData
        /// </summary>
        /// <example>[{id:12345}]</example>
        public TResponseData Data { get; set; }
        #endregion

        #region Method's

        /// <summary>
        /// Asigna respuesta exitosa.
        /// </summary>
        /// <param name="responseData"></param>
        public void Success(TResponseData responseData)
        {
            Data = responseData;
            Message = CommonResource.SuccessRequest;
        }

        /// <summary>
        /// Asigna error interno.
        /// </summary>
        public void InternalServerError()
        {
            Data = default;
            Message = CommonResource.InternalServerError;
            Code = HttpStatusCode.InternalServerError;
        }

        #endregion
    }
}
