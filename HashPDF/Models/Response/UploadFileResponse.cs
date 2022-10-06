namespace HashPDF.Models.Response
{
    /// <summary>
    /// Source File:   UploadFileResponse.cs                                             
    /// Description:   Response Class
    /// Author(es):    Edward Steven Hernández Lambraño
    /// Date:          03/10/2022
    /// Version:       1.0.0
    /// Copyright(c), 2022 
    /// </summary>
    public class UploadFileResponse
    {
        #region Properties

        /// <summary>
        /// Código Hash del documento original
        /// </summary>
        /// <example>8decc8571946d4cd70a024949e033a2a2a54377fe9f1c1b944c20f9ee11a9e51</example>
        public string FirstHash { get; set; }

        /// <summary>
        /// Código Hash del documento luego de la marca de agua
        /// </summary>
        /// <example>8decc8571946d4cd70a024949e033a2a2a54377fe9f1c1b944c20f9ee11a9e51</example>
        public string LastHash { get; set; }

        /// <summary>
        /// Archivo en base64
        /// </summary>
        /// <example>JVBERi0xLjcKJeLjz9MKMiAwIG9iago8PC9UeXBlL0ZvbnQvU3VidHlwZS9UeXBlMS9CYXNlRm9udC9Db3VyaWVyLU</example>
        public byte[] File { get; set; }
        #endregion
    }
}
