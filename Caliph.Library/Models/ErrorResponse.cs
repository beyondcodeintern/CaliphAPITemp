namespace Caliph.Library.Models
{
    /// <summary>
    /// Error Response
    /// </summary>
    public class ErrResponse
    {
        /// <summary>
        /// Custom status code
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// Custom error message
        /// </summary>
        public string StatusMsg { get; set; }
    }

    public class ResponseApiModel : ErrResponse
    {
        public object data { get; set; }
        public long ItemCount { get; set; }
    }
}
