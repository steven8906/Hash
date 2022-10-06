using HashPDF.Atributtes;
using HashPDF.Resources;
using System.ComponentModel.DataAnnotations;

namespace HashPDF.Models.Request
{
    /// <summary>
    /// Source File:   UploadFileModel.cs                                             
    /// Description:   Request Model Class
    /// Author(es):    Edward Steven Hernández Lambraño
    /// Date:          03/10/2022
    /// Version:       1.0.0
    /// Copyright(c), 2022 
    /// </summary>

    public class UploadFileRequest
    {
        #region Properties
        /// <summary>
        /// File
        /// </summary>
        /// <example>Documento.pdf</example>
        [Required(ErrorMessageResourceName = nameof(CommonResource.Required), ErrorMessageResourceType = typeof(CommonResource))]
        [AllowedExtensions(new[] { "application/pdf", "pdf" })]
        public IFormFile File { get; set; }
        #endregion
    }
}
