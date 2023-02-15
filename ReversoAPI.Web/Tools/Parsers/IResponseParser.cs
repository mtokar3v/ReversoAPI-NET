namespace ReversoAPI.Web.Tools.Parsers
{
    public interface IResponseParser<TResult>
    {
        TResult Invoke(string html);
    }
}
