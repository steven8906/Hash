using System.Text.Json.Serialization;

namespace HashPDF.Models.Request
{
    /// <summary>
    /// Source File:   ConvertByteArrayRequest.cs                                             
    /// Description:   Response Class
    /// Author(es):    Edward Steven Hernández Lambraño
    /// Date:          05/10/2022
    /// Version:       1.0.0
    /// Copyright(c), 2022 
    /// </summary>

    public class ConvertToPDFRequest
    {
        /// <summary>
        /// PDF en string base64
        /// </summary>
        /// <example>JVBERi0xLjcKJeLjz9MKMiAwIG9iago8PC9UeXBlL0ZvbnQvU3VidHlwZS9UeXBlMS9CYXNlRm9udC9Db3VyaWVyLU</example>
        [JsonPropertyName("textFile")]
        public byte[] FileByteArray { get; set; }

        /// <summary>
        /// Nombre de salida del archivo
        /// </summary>
        /// <example>Archivo.pdf</example>
        public string FileName { get; set; }
    }
}
