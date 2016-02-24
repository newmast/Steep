namespace Steep.Services.Web
{
    using System;
    using System.Text;

    public class IdentifierProvider : IIdentifierProvider
    {
        public string DecodeId(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string EncodeId(string id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
