namespace Steep.Services.Web
{
    public interface IIdentifierProvider
    {
        string DecodeId(string urlId);

        string EncodeId(string id);
    }
}
