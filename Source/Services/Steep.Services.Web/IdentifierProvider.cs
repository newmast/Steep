namespace Steep.Services.Web
{
    using System;
    using System.Text;

    public class IdentifierProvider : IIdentifierProvider
    {
        public int DecodeId(string urlId)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(urlId);
            return int.Parse(Encoding.UTF8.GetString(base64EncodedBytes));
        }

        public string EncodeId(string id)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(id);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
