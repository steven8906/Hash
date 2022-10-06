using HashPDF.Resources;
using System.ComponentModel.DataAnnotations;

namespace HashPDF.Atributtes
{
    /// <summary>
    /// Source File:   AllowedExtensionsAttribute.cs                                             
    /// Description:   Attribute Class
    /// Author(es):    Edward Steven Hernández Lambraño
    /// Date:          03/10/2022
    /// Version:       1.0.0
    /// Copyright(c), 2022 
    /// </summary>

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        #region Internals
        private string[] extensions;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="extensions"></param>
        public AllowedExtensionsAttribute(string[] extensions) => this.extensions = extensions;
        #endregion

        #region Overrides
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value;

            if (value is null)  return null;
            if (extensions.Any(x => x.Equals(file.ContentType))) return ValidationResult.Success;

            return new ValidationResult(CommonResource.AllowFiles);
        }
        #endregion
    }
}
