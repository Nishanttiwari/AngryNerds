namespace APIHelper
{
    using System.Runtime.Serialization;

    public interface IJSONProvider
    {
        string GetJSON(string URL);
        string GetJSONFromThirdParty(string URL);
    }
}
