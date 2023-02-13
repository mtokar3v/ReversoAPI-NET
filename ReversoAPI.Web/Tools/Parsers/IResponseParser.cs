namespace ReversoAPI.Web.Tools.Parsers
{
    public interface IResponseParser<TResult>
    {
        public TResult Invoke();
    }
}
