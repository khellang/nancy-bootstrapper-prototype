namespace Nancy.Core.Http
{
    using System.Collections.Generic;

    public interface IHeaderDictionary : IDictionary<string, string[]>
    {
    }
}
